using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;
using System;

namespace Util.pool
{

    public class ObjectPool<T> : IObjectPool<T>, IDisposable where T : class
    {
        private GameObject m_container;
        private Queue<T> m_pool;

        public int CountInactive => m_pool.Count;
        public Queue<T> Pool{ get { return m_pool; } }

        public void Dispose()
        {

        }

        public T Get()
        {
            throw new NotImplementedException();
        }

        public PooledObject<T> Get(out T v)
        {
            throw new NotImplementedException();
        }

        public void Release(T element)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}