using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletEcho.UI
{
    public class ItemCell : MonoBehaviour , IPoolObj
    {
        public Action onReset { get; set; }
        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private TextMeshProUGUI countTxt;
        [SerializeField] private Button equpBtn;
        private Action equip;
        public void Init(string name, string count,Action equip)
        {
            this.equip = equip;
            nameTxt.text = name;
            countTxt.text = count;
            equpBtn.onClick.RemoveAllListeners();
            equpBtn.onClick.AddListener(OnEquip);
        }

        private void OnEquip()
        {
            equip?.Invoke();
        }
    }
}
