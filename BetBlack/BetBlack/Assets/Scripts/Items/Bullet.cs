using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Items.WeaponeSystem
{
    public class Bullet : MonoBehaviour, IPoolObj
    {
        public Action onReset { get; set; }

        public void Init(Vector3 dir,Vector3 startingPoint,float force,float life)
        {
            transform.position = startingPoint;
            var rb = GetComponent<Rigidbody>();
            rb.AddForce(dir * force);
            StartCoroutine(Die(life));
        }

        private IEnumerator Die(float life)
        {
            yield return new WaitForSeconds(life);
            onReset?.Invoke();
        }
    }
}
