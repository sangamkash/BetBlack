using System;
using UnityEngine;
using System.Collections;

namespace BulletEcho
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }

        private void OnDestroy()
        {
            _instance = null;
        }
    }
}