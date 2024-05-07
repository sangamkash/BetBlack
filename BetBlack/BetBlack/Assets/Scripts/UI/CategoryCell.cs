using InventorySystem.DataSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InventorySystem.UI
{
    public class CategoryCell : MonoBehaviour, IPoolObj
    {
        [SerializeField] private TextMeshProUGUI heading;
        [SerializeField] private RectTransform container;
        [SerializeField] private ItemCell itemCellPrefab;
        private GenericMonoPool<ItemCell> cellPool;

        public Action onReset { get; set; }

        private void Awake()
        {
            cellPool = new GenericMonoPool<ItemCell>(itemCellPrefab);
        }

        public void Init(ItemType itemType, Dictionary<int, ItemData> datas, Action<ItemType, int> equip, Action hideUI)
        {
            cellPool.ResetAllPool();
            heading.text = itemType.ToString();
            var i = 0;
            foreach (var data in datas)
            {
                if (data.Value.count > 0)
                {
                    var cell = cellPool.GetObject(container);
                    cell.Init(data.Key.GetSubtypeName(itemType), data.Value.count.ToString(), () =>
                    {
                        hideUI?.Invoke();
                        equip.Invoke(itemType, data.Value.subItemType);
                    });
                    cell.GetComponent<RectTransform>().SetSiblingIndex(i);
                    i++;
                }
            }
        }

    }
}
