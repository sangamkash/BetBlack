using System;
using System.Collections;
using System.Collections.Generic;
using BulletEcho.DataSystem;
using TMPro;
using UnityEngine;

namespace BulletEcho.UI
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

    }
}
