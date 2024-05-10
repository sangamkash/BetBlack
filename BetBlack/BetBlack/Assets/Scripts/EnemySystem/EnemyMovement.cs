using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace BulletEcho.EnemySystem
{
    public class EnemyMovement : MonoBehaviour
    {
        private GameManager gameManager => GameManager.Instance;
        [SerializeField] private EnemyGun gun;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField]private float waitTime = 2;
        private float reachedTime;
        private Vector3 destination = Vector3.zero;
        private int lastRandomIndex = 0;
        private bool opponentDetected = false;
        private bool newTargetAssigned = false;

        private void Start()
        {
            AssignRandomPath();
            gameManager.AddEnemy(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameConstant.TAG_PLAYER))
            {
                destination = other.transform.position;
                newTargetAssigned = true;
                opponentDetected = true;
                gun.StartShooting(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameConstant.TAG_PLAYER))
            {
                opponentDetected = false;
                gun.StopShooting();
            }
        }

        private void Update()
        {
            if (newTargetAssigned)
            {
                navMeshAgent.destination = destination;
                newTargetAssigned = false;
            }
            else if (opponentDetected == false)
            {
                var diff = transform.position - destination;
                var stopDis = navMeshAgent.stoppingDistance + 0.05f;
                var sqrStopDis = stopDis * stopDis;
                if (diff.sqrMagnitude <= sqrStopDis && Time.time - reachedTime > waitTime)
                {
                    reachedTime = Time.time;
                    AssignRandomPath();
                }
            }
        }

        private void AssignRandomPath()
        {
            newTargetAssigned = true;
            destination = RandomPoints.Instance.GetRandomPosition(ref lastRandomIndex);
        }

        public void OnDie()
        {
            gameManager.RemoveEnemy(this);
        }
    }
}
