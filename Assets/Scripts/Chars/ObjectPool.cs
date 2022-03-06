using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chars.Tools
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        [Serializable]
        public class Pool
        {
            public string Tag;
            public GameObject Prefab;
            public int Size;
            public List<GameObject> Pooled;
            public List<GameObject> Instatiated;

            public Pool(string tag, GameObject prefab, int size)
            {
                Tag = tag;
                Prefab = prefab;
                Size = size;
                Pooled = new List<GameObject>(size);
                Instatiated = new List<GameObject>();
            }
        }

        [SerializeField] private List<Pool> _poolsList = new List<Pool>();
        private Dictionary<string, Pool> _TagDictionaryToPool;

        //Esto lo hago solamente por el entregable, realmente podria usar una cola y/o una Pila
        public enum RemoveLike { Queue, Stack };
        public RemoveLike likeWhat;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _TagDictionaryToPool = new Dictionary<string, Pool>();
            InitializePool();
        }

        private void InitializePool()
        {
            _TagDictionaryToPool = _poolsList.ToDictionary(x => x.Tag, x => x);

            foreach (var tag in _TagDictionaryToPool.Keys)
            {
                var currentPool = _TagDictionaryToPool[tag];

                for (var i = 0; i < currentPool.Size; i++)
                {
                    var instance = CreateGameObject(currentPool.Prefab);
                    currentPool.Pooled.Add(instance);
                }
            }
        }

        private GameObject CreateGameObject(GameObject prefab)
        {
            var instance = Instantiate(prefab);
            instance.SetActive(false);
            return instance;
        }

        public GameObject GetPooledObject(string tag)
        {
            if (!_TagDictionaryToPool.ContainsKey(tag))
            {
                Debug.LogWarning($"The {tag} dont exist", this);
                return null;
            }

            var instance = GetInstanceFromPool(tag);
            instance.SetActive(true);
            return instance;
        }

        private GameObject GetInstanceFromPool(string tag)
        {
            var pool = GetPoolFromTag(tag);

            if (pool == null)
            {
                Debug.LogWarning($"Dont found a Pool with that {tag} name", this);
                return null;
            }

            if (pool.Pooled.Count > 0)
            {
                if (likeWhat == RemoveLike.Queue)
                {
                    return RemoveLast(pool);
                }

                if (likeWhat == RemoveLike.Stack)
                {
                    return RemoveFirts(pool);
                }
            }

            var newPoolObject = CreateGameObject(pool.Prefab);
            pool.Instatiated.Add(newPoolObject);
            pool.Size++;
            return newPoolObject;
        }

        //Act Like Queue
        private static GameObject RemoveLast(Pool pool)
        {
            var index = pool.Pooled.Count - 1;
            var poolObject = pool.Pooled[index];
            pool.Pooled.RemoveAt(index);
            pool.Instatiated.Add(poolObject);
            return poolObject;
        }

        //Act like a Stack
        private static GameObject RemoveFirts(Pool pool)
        {
            var poolObject = pool.Pooled[0];
            pool.Pooled.RemoveAt(0);
            pool.Instatiated.Add(poolObject);
            return poolObject;
        }

        private GameObject[] GetAllPooledObjects(string tag)
        {
            return !_TagDictionaryToPool.ContainsKey(tag) ? null : _TagDictionaryToPool[tag].Pooled.ToArray();
        }

        public Pool GetPoolFromTag(string tag)
        {
            if (_TagDictionaryToPool.TryGetValue(tag, out Pool pool))
            {
                return pool;
            }
            return null;
        }

        public void ReturnToPool(GameObject objectToRecycle, string tag)
        {
            var pool = GetPoolFromTag(tag);
            objectToRecycle.SetActive(false);

            if (pool == null)
            {
                var newPool = new Pool(tag, objectToRecycle, 1);
                _poolsList.Add(newPool);
                _TagDictionaryToPool.Add(tag, newPool);
                return;
            }

            if (!pool.Pooled.Contains(objectToRecycle))
                pool.Pooled.Add(objectToRecycle);

            pool.Instatiated.RemoveAll(x => x == objectToRecycle);
        }

        public void CaptureFromPool(GameObject go, string tag)
        {
            var pool = GetPoolFromTag(tag);

            if (pool == null) return;
            
            pool.Pooled.RemoveAll(x => x == go);
                
            if (!pool.Instatiated.Contains(go))
                pool.Instatiated.Add(go);

            go.SetActive(true);
        }

    }
}
