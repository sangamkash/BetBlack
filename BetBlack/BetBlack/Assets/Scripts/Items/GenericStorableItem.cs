using InventorySystem.DataSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Items
{
    public abstract class GenericStorableItem : GenericCarryableItem, IStorable, IPoolObj
    {
        protected DataManager dataManager=>DataManager.Instance;
        public Action onReset { get; set; }

        public abstract ItemType GetItemType();

        public abstract int GetSubType();

        public void Store()
        {
            onReset?.Invoke();
            ThrowAway();
            var itemType = GetItemType();
            var subItemType = GetSubType();
            if (dataManager.GetLevelData().bagPackItems == null)
            {
                dataManager.GetLevelData().bagPackItems = new Dictionary<ItemType, Dictionary<int, ItemData>> (); ;
            }

            var dic = dataManager.GetLevelData().bagPackItems;
            if (dic.ContainsKey(GetItemType()))
            {
                if(dic[GetItemType()].ContainsKey(subItemType))
                {
                    dic[GetItemType()][subItemType].count += 1;
                }
                else
                {
                    var itemData = new ItemData()
                    {
                        itemType = itemType,
                        subItemType = subItemType,
                        count = 1
                    };
                    dic[GetItemType()].Add(itemData.subItemType, itemData);
                }
            }
            else
            {
                var itemData = new ItemData()
                {
                    itemType = itemType,
                    subItemType = subItemType,
                    count = 1
                };
                var data = new Dictionary<int, ItemData>();
                data.Add(itemData.subItemType, itemData);
                dic.Add(GetItemType(), data);
            }
            dataManager.Save();
        }
    }
}