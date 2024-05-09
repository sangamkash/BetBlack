using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletEcho.UI
{
    public class HealthIconLinker : MonoBehaviour
    {
        [SerializeField] private Sprite iconSprite;
        [SerializeField] private bool clamp;
        private HealthIcon icon;

        private void Start()
        {
            icon = UIManager.Instance.GetHealthUI();
            icon.SetIcon(iconSprite);
        }

        public void healthUpdate(int health)
        {
            icon.UpdateHealth(health / 100f);
        }

        private void Update()
        {
            icon.UpdatedPosition(transform.position,clamp);
        }

        private void OnDestroy()
        {
            icon.onReset();
        }
    }
}
