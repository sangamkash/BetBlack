using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.DataSystem
{
    [System.Serializable]
    public class ItemPrefab
    {
        public ItemType itemType;
        public GameObject[] prefabs;
    }
    
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class ItemTypes : ScriptableObject
    {
        [SerializeField] private ItemPrefab[] itemPrefabs;
        private Dictionary<ItemType, Dictionary<int, GameObject>> dicOfItems=null;

        private void Init()
        {
            if(dicOfItems != null)
                return;
            dicOfItems = new Dictionary<ItemType, Dictionary<int, GameObject>>();
            foreach (var item in itemPrefabs)
            {
                for (var i = 0; i < item.prefabs.Length; i++)
                {
                    var prefab = item.prefabs[i];
                    if (!dicOfItems.ContainsKey(item.itemType))
                    {
                        var dic = new Dictionary<int, GameObject>();
                        dic.Add(i,prefab);
                        dicOfItems.Add(item.itemType,dic);
                    }
                    else
                    {
                        dicOfItems[item.itemType].Add(i, prefab);
                    }
                }
            }
        }

        public GameObject GetItem(ItemType type,int subType)
        {
            Init();
            if (dicOfItems.ContainsKey(type) && dicOfItems[type].ContainsKey(subType))
            {
                return dicOfItems[type][subType].gameObject;
            }
            Debug.LogError($"TAG::ItemTypes type :{type} and subType:{subType} not found");
            return null;
        }
    }
}
