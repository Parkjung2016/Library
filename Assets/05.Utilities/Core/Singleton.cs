using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Parkjung2016
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        protected void DonDestoroy()
        {
            T[] objs = FindObjectsByType<T>(FindObjectsSortMode.None);
            if (objs.Length < 2)
                DontDestroyOnLoad(gameObject);
            else
                Destroy(gameObject);
        }
    }
}