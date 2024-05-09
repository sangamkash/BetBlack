using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BulletEcho.EnemySystem
{
    public class EnemyMovement : MonoBehaviour
    {
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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameConstant.TAG_PLAYER))
            {
                destination = other.transform.position;
                newTargetAssigned = true;
                opponentDetected = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameConstant.TAG_PLAYER))
            {
                opponentDetected = false;
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
                var stopDis = navMeshAgent.stoppingDistance + 0.005f;
                var sqrStopDis = stopDis * stopDis;
                Debug.Log($"dis=={diff.sqrMagnitude} <= {sqrStopDis} ");
                if (diff.sqrMagnitude <= sqrStopDis && Time.time - reachedTime > waitTime)
                {
                    reachedTime = Time.time;
                    //had Reached;
                    AssignRandomPath();
                }
            }
        }

        private void AssignRandomPath()
        {
            Debug.Log("new path assigned");
            newTargetAssigned = true;
            destination = RandomPoints.Instance.GetRandomPosition(ref lastRandomIndex);
        }
    }
}
