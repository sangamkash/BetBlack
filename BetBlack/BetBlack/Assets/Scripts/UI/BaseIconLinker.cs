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
        private void Start()
        {
            icon = GetIcon();
            icon.SetIcon(iconSprite);
        }

        protected abstract T GetIcon();
        
        private void Update()
        {
            icon.UpdatedPosition(transform.position, clamp,fadeOnDistance);
        }

        private void OnDestroy()
        {
            icon.onReset();
        }
    }
}
