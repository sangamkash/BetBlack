using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletEcho.UI
{
    public abstract class BaseIconLinker<T> : MonoBehaviour where T: BaseIcon
    {
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private bool clamp;
        [SerializeField] private bool fadeOnDistance;
        protected T icon;
        
        private void OnEnable()
        {
            if (icon == null)
            {
                icon = GetIcon();
                icon.SetIcon(iconSprite);
            }
        }
        private void OnDisable()
        {
            if (icon != null)
                icon.onReset?.Invoke();
            icon = null;
        }
        protected abstract T GetIcon();
        
        private void Update()
        {
            icon.UpdatedPosition(transform.position, clamp,fadeOnDistance);
        }
    }
}
