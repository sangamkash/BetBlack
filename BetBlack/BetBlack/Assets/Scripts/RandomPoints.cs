using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BulletEcho.EnemySystem
{
    public class RandomPoints : MonoBehaviourSingleton<RandomPoints>
    {
        [SerializeField] private Transform container;
        [SerializeField] private List<Transform> points;

        [ContextMenu("Assign Ref")]
        private void OnValidate()
        {
            points.AddRange(container.GetComponentsInChildren<Transform>());
            points.RemoveAt(0);
        }

        private void OnDrawGizmos()
        {
            foreach (var point in points)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(point.position, 0.5f);
            }
        }

        public Vector3 GetRandomPosition(ref int lastIndex)
        {
            var randomIndex = Random.Range(0, points.Count);
            lastIndex = randomIndex;
            return points[randomIndex].position;
        }
    }
}