using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletEcho
{
    public interface IPoolObj
    {
        public Action onReset { get; set; }
        public void ResetBackToPool()
        {
            onReset.Invoke();
        }
    }
    public class GenericMonoPool<T> where T : MonoBehaviour , IPoolObj
    {
        private List<T> activePool;
        private List<T> inActivePool;

        private GameObject prefab;

        public GenericMonoPool(T prefab, int capacity = 20)
        {
            this.prefab = prefab.gameObject;
            activePool = new List<T>(capacity);
            inActivePool = new List<T>(capacity);
        }


        public GenericMonoPool(GameObject prefab, int capacity = 20)
        {
            this.prefab = prefab;
            activePool = new List<T>(capacity);
            inActivePool = new List<T>(capacity);
        }

        public T GetObject(Transform container)
        {
            if (inActivePool.Count > 0)
            {
                var index = inActivePool.Count - 1;
                var t = inActivePool[index];
                activePool.Add(t);
                inActivePool.RemoveAt(index);
                if (t.transform.parent != container)
                    t.transform.SetParent(container);
                t.gameObject.SetActive(true);
                var poolObj=(IPoolObj)t;
                if (poolObj != null)
                    poolObj.onReset = () => Reset(t);
                return t;
            }
            else
            {
                var t = MonoBehaviour.Instantiate(prefab.gameObject, container).GetComponent<T>();
                activePool.Add(t);
                t.gameObject.SetActive(true);
                var poolObj = (IPoolObj)t;
                if (poolObj != null)
                    poolObj.onReset = () => Reset(t);
                return t;
            }
        }

        public void ResetAllPool()
        {
            foreach (var obj in activePool)
            {
                inActivePool.Add(obj);
                obj.gameObject.SetActive(false);
            }
            activePool.Clear();
        }

        public void Reset(T obj)
        {
            if(obj == null || obj.gameObject==null)
                return;
            obj.gameObject.SetActive(false);
            activePool.Remove(obj);
            inActivePool.Add(obj);
        }

        public void Reset(GameObject obj)
        {
            Reset(obj.GetComponent<T>());
        }

    }
    
    public class GenericGameObj
    {
        private List<GameObject> activePool;
        private List<GameObject> inActivePool;

        private GameObject prefab;

        public GenericGameObj(GameObject prefab, int capacity = 20)
        {
            this.prefab = prefab;
            activePool = new List<GameObject>(capacity);
            inActivePool = new List<GameObject>(capacity);
        }

        public GameObject GetObject(Transform container)
        {
            if (inActivePool.Count > 0)
            {
                var index = inActivePool.Count - 1;
                var t = inActivePool[index];
                activePool.Add(t);
                inActivePool.RemoveAt(index);
                if (t.transform.parent != container)
                    t.transform.SetParent(container);
                t.gameObject.SetActive(true);
                return t;
            }
            else
            {
                var t = MonoBehaviour.Instantiate(prefab, container);
                activePool.Add(t);
                t.gameObject.SetActive(true);
                return t;
            }
        }

        public void ResetAllPool()
        {
            foreach (var obj in activePool)
            {
                inActivePool.Add(obj);
                obj.gameObject.SetActive(false);
            }

            activePool.Clear();
        }

        public void Reset(GameObject obj)
        {
            if(obj == null || obj.gameObject==null)
                return;
            obj.gameObject.SetActive(false);
            activePool.Remove(obj);
            inActivePool.Add(obj);
        }

    }
}
