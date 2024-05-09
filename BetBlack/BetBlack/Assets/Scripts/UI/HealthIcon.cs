using System;
using System.Collections;
using System.Collections.Generic;
using BulletEcho.UI;
using TMPro;
using UnityEngine;

namespace BulletEcho.UI
{
    public class HealthIcon : BaseIcon,IPoolObj
    {
        [SerializeField] private TextMeshProUGUI heading;
        public Action onReset { get; set; }

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
