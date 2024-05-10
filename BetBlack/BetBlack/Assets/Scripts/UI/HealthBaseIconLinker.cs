using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletEcho.UI
{
    public class HealthBaseIconLinker : BaseIconLinker<HealthIcon>
    {
        protected override HealthIcon GetIcon()
        {
           return UIManager.Instance.GetHealthUI();;
        }

        public void healthUpdate(int health)
        {
            icon.UpdateHealth(health / 100f);
        }
    }
}
