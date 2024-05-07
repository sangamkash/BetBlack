using System;
using UnityEngine;

namespace InventorySystem.Items
{
    public class GenericCarryableItem : MonoBehaviour,ICarryable
    {
        protected Action onThrowDone;
        public virtual void pickUp(Transform container,Action onThrowDone)
        {
            this.onThrowDone=onThrowDone;
            var colls = GetComponentsInChildren<Collider>();
            foreach (var col in colls)
            {
                col.enabled = false;
            }
            var rb = GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;
            transform.SetParent(container.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public virtual void ThrowAway()
        {
            transform.SetParent(null);
            var colls = GetComponentsInChildren<Collider>();
            foreach (var col in colls)
            {
                col.enabled = true;
            }
            var rb = GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;
            rb.AddForce(transform.forward * 200);
            onThrowDone?.Invoke();  
        }
    }
}