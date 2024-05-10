using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletEcho.UI
{
    public class GenericIconLinker : BaseIconLinker<GenericIcon>
    {
        private UIManager uiManager => UIManager.Instance;
        protected override GenericIcon GetIcon()
        {
            return uiManager.GetGenericIconUI();
        }

        public void SetIconColor(Color color)
        {
            icon.SetIconColor(color);
        }
    }
}