using UnityEngine;
using System.Collections.Generic;

namespace Util.pool
{

    public class Pool<T> where T : Component
    {
        private GameObject m_container;
        

        private Queue<GameObject> itemPool;

        private void Init()
        {

        }
    }
}