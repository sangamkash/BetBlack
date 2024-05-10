using System;
using System.Collections;
using System.Collections.Generic;
using BulletEcho.UI;
using TMPro;
using UnityEngine;

namespace BulletEcho.UI
{
    public class HealthIcon : BaseIcon
    {
        [SerializeField] private TextMeshProUGUI heading;

        public void Init(string heading)
        {
            this.heading.text = heading;
        }

        public void UpdateHealth(float fillAmount)
        {
            icon.fillAmount = fillAmount;
        }
    }
}
