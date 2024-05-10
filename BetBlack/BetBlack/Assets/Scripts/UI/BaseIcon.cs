using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace BulletEcho.UI
{ 
    public abstract class BaseIcon : MonoBehaviour,IPoolObj
    {
        [SerializeField] protected Image icon;
        [SerializeField] private RectTransform rectTransform;
        private UIManager uiManager => UIManager.Instance;

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
        public void UpdatedPosition(Vector3 position,bool clampOnScreen=false, bool fadeOnDistance=false)
        {
            var viewportPoint=uiManager.GetUICamera.WorldToViewportPoint(position);
            if (fadeOnDistance)
            {
                var diff = new Vector2(viewportPoint.x, viewportPoint.y) - Vector2.one * 0.5f;
                var sqrDis = diff.sqrMagnitude;
                var fad = 1f-Mathf.InverseLerp(0f, 0.8f, Mathf.Clamp01(sqrDis));
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, Mathf.Clamp(fad, 0.2f, 1f));
            }
            if (clampOnScreen)
            {
                viewportPoint = new Vector3(Mathf.Clamp01(viewportPoint.x), Mathf.Clamp01(viewportPoint.y),
                    viewportPoint.z);
            }
            rectTransform.position = uiManager.GetUICamera.ViewportToScreenPoint(viewportPoint);
            
        }

        public Action onReset { get; set; }
    }
}
