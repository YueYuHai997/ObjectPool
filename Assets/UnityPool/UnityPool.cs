using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
namespace UnityPool
{
    /// <summary>
    /// Unity对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePool<T> where T : Component
    {
        [SerializeField] T Prefab;
        [SerializeField] int Defaultsize = 100;
        [SerializeField] int Maxsize = 500;
        [SerializeField] Transform Parent;

        public ObjectPool<T> pool;
        public int CountActive => pool.CountActive;
        public int CountInactive => pool.CountInactive;
        public int TotalCount => pool.CountAll;

        protected void Initialized(T prefab, Transform parent, bool collectionCheck = true)
        {
            Prefab = prefab;
            Parent = parent;
            pool = new ObjectPool<T>(OnCeratePoolItem, OnGetPoolItem, OnReleaseItem, OnDestroyItem, collectionCheck, Defaultsize, Maxsize);
        }

        protected virtual void OnDestroyItem(T obj) => Debug.Log(1);//Destroy(obj);

        protected virtual void OnReleaseItem(T obj) => obj.gameObject.SetActive(false);

        protected virtual void OnGetPoolItem(T obj) => obj.gameObject.SetActive(true);

        protected virtual T OnCeratePoolItem()
        {
            var t = GameObject.Instantiate(Prefab, Parent);
            t.name = t.name.Replace("(Clone)", "");
            return t;
        }

        /// <summary>
        /// 获取一个对象池对象
        /// </summary>
        /// <returns></returns>
        public T Get() => pool.Get();

        /// <summary>
        /// 释放一个对象到对象池
        /// </summary>
        /// <param name="obj"></param>
        public void Release(T obj) => pool.Release(obj);

        /// <summary>
        /// 对象池整体释放
        /// </summary>
        public void Clear() => pool.Clear();
    }
}