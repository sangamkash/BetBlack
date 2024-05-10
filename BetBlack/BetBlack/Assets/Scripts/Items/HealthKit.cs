using System.Collections;
using System.Collections.Generic;
using BulletEcho.UI;
using UnityEngine;

namespace BulletEcho.Items
{
    public class HealthKit : GenericConsumable
    {
        [SerializeField] private int healValue = 20;
        [SerializeField] private GenericIconLinker iconLinker;
        protected override void ApplyItem(Transform consumer)
        {
            var healthBar=consumer.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                healthBar.RepairDamage(healValue);
            }
        }

        protected override void OnItemDisabled()
        {
            base.OnItemDisabled();
            iconLinker.SetIconColor(Color.gray);
        }

        protected override void OnItemReady()
        {
            base.OnItemReady();
            iconLinker.SetIconColor(Color.white);
        }
    }
}