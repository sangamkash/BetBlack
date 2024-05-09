using System;
using System.Collections;
using System.Collections.Generic;
using BulletEcho.DataSystem;
using UnityEngine;

namespace BulletEcho.Items
{
    public class SpawnManager : MonoBehaviour
    {
        struct TypeKey
        {
            private ItemType type;
            private int subType;
            public TypeKey(ItemType type, int subType)
            {
                this.type = type;
                this.subType = subType;
            }
        }
        [SerializeField] private ItemTypes itemTypes;
        private Dictionary<TypeKey, GenericMonoPool<GenericStorableItem>> pool;

        private void Awake()
        {
            pool = new Dictionary<TypeKey, GenericMonoPool<GenericStorableItem>>();
        }
        public GameObject Spawn(ItemType type, int subType,Vector3 position,Transform parent)
        {;
            var key = new TypeKey(type, subType);
            if (!pool.ContainsKey(key))
            {
                var prefab = itemTypes.GetItem(type, subType);
                pool.Add(key,new GenericMonoPool<GenericStorableItem>(prefab));
            }
            var gameObj=pool[key].GetObject(parent).gameObject;
            gameObj.transform.position= position;
            return gameObj;
        }

        public void ResetAll(ItemType type, int subType)
        {
            var key = new TypeKey(type, subType);
            if (pool.ContainsKey(key))
            {
                pool[key].ResetAllPool();
            }
        }
        
        public void Reset(ItemType type, int subType,GameObject gameObj)
        {
            var key = new TypeKey(type, subType);
            if (pool.ContainsKey(key))
            {
                pool[key].Reset(gameObj);
            }
        }
    }
}
