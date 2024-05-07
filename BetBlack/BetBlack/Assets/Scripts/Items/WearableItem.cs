using InventorySystem.DataSystem;
using InventorySystem.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearableItem : GenericStorableItem, IWearable
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private WearableType wearableType;

    public override ItemType GetItemType()
    {
        return itemType;
    }

    public override int GetSubType()
    {
        return (int)(wearableType);
    }

    public void Wear()
    {
        
    }

}
