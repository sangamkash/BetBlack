using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletEcho.Items.WeaponeSystem
{
    public class Bullet : MonoBehaviour, IPoolObj
    {
        public Action onReset { get; set; }

        public void Init(Vector3 dir,Vector3 startingPoint,float speed,float life)
        {
            transform.position = startingPoint;
            StartCoroutine(Die(life,dir,speed));
        }

        private IEnumerator Die(float life,Vector3 dir,float speed)
        {
            var startTime = Time.time;
            while (Time.time-startTime<life)
            {
                transform.position += dir * (Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onReset?.Invoke();
        }
    }
}
