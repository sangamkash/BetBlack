using System;
using System.Collections;
using UnityEngine;

namespace BulletEcho.Items
{
    public class GenericCarryableItem : MonoBehaviour,ICarryable
    {
        protected Action onThrowDone;
        protected Collider[] colls;

        public virtual void pickUp(Transform container, Action onThrowDone = null)
        {
            this.onThrowDone = onThrowDone;
            colls = GetComponentsInChildren<Collider>();
            foreach (var col in colls)
            {
                col.enabled = false;
            }
            transform.SetParent(container.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public virtual void ThrowAway()
        {
            transform.SetParent(null);
            StartCoroutine(Delay(1f, () =>
            {
                foreach (var col in colls)
                {
                    col.enabled = true;
                }
            }));
            onThrowDone?.Invoke();  
        }

        private IEnumerator Delay(float delaytime,Action OnComplete)
        {
            yield return new WaitForSeconds(delaytime);
            OnComplete?.Invoke();
        }
        
    }
}