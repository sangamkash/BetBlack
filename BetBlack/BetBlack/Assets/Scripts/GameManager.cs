using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem.Items;
using InventorySystem.DataSystem;
using System;
using Unity.VisualScripting.Dependencies.NCalc;
using InventorySystem.PlayerSystem;
using InventorySystem.UI;

namespace InventorySystem
{
    [System.Serializable]
    public class SpawnPoint
    {
        public ItemType itemType;
        public int subType;
        public Vector3 position;
        public SpawnPoint(ItemType itemType, int subType, Vector3 position)
        {
            this.itemType = itemType;
            this.subType = subType;
            this.position = position;
        }
    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private SpawnManager spawnManager;
        [SerializeField] private Vector3 spawnAreaSize;
        [SerializeField] private Vector3 gridSize;
        [SerializeField] private List<SpawnPoint> spawnPoints;
        [SerializeField] private PlayerBodyPartController playerBodyController;
        private DataManager dataManager=>DataManager.Instance;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            GenerateItems();
        }

        private void GenerateItems()
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                var spawnPoint = spawnPoints[i];
                if (spawnPoint.itemType == ItemType.Food || spawnPoint.itemType == ItemType.Weapon)//TODO @Sangam Remove this on adding stone and Wearable
                {
                    spawnManager.Spawn(spawnPoint.itemType, spawnPoint.subType, spawnPoint.position, null);
                }
            }
        }

        [ContextMenu("Generate Spawn point")]
        private void GenerateRandomPoints()
        {
            spawnPoints.Clear();
            var objs = new List<Tuple<ItemType, int>>();
            var itemTypeCount = Enum.GetNames(typeof(ItemType)).Length;
            for (int j = 0; j < itemTypeCount; j++)
            {
                var type = (ItemType)j;
                var subItemCount = GetSubEnumCount(type);
                for (int k = 0; k < subItemCount; k++)
                {
                    objs.Add(new Tuple<ItemType, int>(type, k));
                }
            }
            var i = 0;
            for (float x = -spawnAreaSize.x / 2; x < spawnAreaSize.x / 2; x += gridSize.x)
            {
                for (float z = -spawnAreaSize.z / 2; z < spawnAreaSize.z / 2; z += gridSize.z)
                {
                    if (i == objs.Count)
                        i = 0;
                    Vector3 spawnPosition = new Vector3(x, 10f, z) + transform.position;
                    var itemType = objs[i].Item1;
                    var itemSubType = objs[i].Item2;
                    i++;
                    spawnPoints.Add(new SpawnPoint(itemType, itemSubType, spawnPosition));
                }
            }
        }

        private int GetSubEnumCount(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Stone:
                    return Enum.GetNames(typeof(StoneType)).Length;
                case ItemType.Food:
                    return Enum.GetNames(typeof(FoodType)).Length;
                case ItemType.Weapon:
                    return Enum.GetNames(typeof(WearableType)).Length;
                case ItemType.Wearable:
                    return Enum.GetNames(typeof(WearableType)).Length;
            }
            return 0;
        }

        public void EquipItem(ItemType itemType,int subType)
        {
            var gameObj=spawnManager.Spawn(itemType, subType, Vector3.zero, null);
            playerController.EquipItem(gameObj);
            var data=dataManager.GetLevelData();
            if (data != null)
            {
                data.bagPackItems[itemType][subType].count -= 1;
            }
            dataManager.Save();
        }

    }
}
