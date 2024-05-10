using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletEcho.Items
{
    public abstract class GenericConsumable : MonoBehaviour,IConsumable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float reappairAfter = 5f;
        private Collider[] colliders;

        private void Start()
        {
            colliders=transform.GetComponents<Collider>();
        }

        public void Consume(Transform consumer)
        {
            StopAllCoroutines();
            ApplyItem(consumer);
            DisableItem();
            StartCoroutine(Delay(5, EnableItem));
        }

        private void DisableItem()
        {
            spriteRenderer.color=Color.gray;
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            OnItemDisabled();
        }

        protected virtual void OnItemDisabled()
        {
            
        }
        
        private void EnableItem()
        {
            spriteRenderer.color=Color.white;
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }

            OnItemReady();
        }

        protected virtual void OnItemReady()
        {
            
        }
        protected abstract void ApplyItem(Transform consumer);

        private IEnumerator Delay(int value,Action OnComplete)
        {
            yield return new WaitForSeconds(value);
            OnComplete?.Invoke();
        }
    }
}