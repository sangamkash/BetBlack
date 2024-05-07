using InventorySystem.DataSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Items
{
    public class FoodItem : GenericStorableItem, IConsumable
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private FoodType foodType;

        public override ItemType GetItemType()
        {
            return itemType;
        }

        public override int GetSubType()
        {
            return (int)(foodType);
        }
        public void Consume()
        {
            
        }

    }
}
