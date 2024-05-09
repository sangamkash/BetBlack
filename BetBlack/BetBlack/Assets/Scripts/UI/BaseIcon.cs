using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletEcho.UI
{ 
    public abstract class BaseIcon : MonoBehaviour
    {
        [SerializeField] protected Image icon;
        [SerializeField] private RectTransform rectTransform;
        private UIManager uiManager => UIManager.Instance;

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
        public void UpdatedPosition(Vector3 position,bool clampOnScreen=false)
        {
            var viewportPoint=uiManager.GetUICamera.WorldToViewportPoint(position);
            if (clampOnScreen)
            {
                viewportPoint = new Vector3(Mathf.Clamp01(viewportPoint.x), Mathf.Clamp01(viewportPoint.y),
                    viewportPoint.z);
            }

            rectTransform.position = uiManager.GetUICamera.ViewportToScreenPoint(viewportPoint);
        }
    }
}
