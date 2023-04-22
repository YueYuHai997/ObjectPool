using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPool
{
    public class GemPool : BasePool<Gm>
    {
        public float TimeSpawn { get; private set; }
        public float Interval = 0.5f;


        public void Init(Gm gm, Transform t)
        {
            Initialized(gm, t);
        }

        private void Update()
        {
            TimeSpawn += Time.deltaTime;
            if (TimeSpawn > Interval)
            {
                Get();
                TimeSpawn = 0;
            }
        }

        protected override void OnGetPoolItem(Gm obj)
        {
            base.OnGetPoolItem(obj);
            obj.Reset();
            //obj.transform.position = transform.position + new Vector3();
            obj.transform.localPosition = UnityEngine.Random.insideUnitSphere * 2f;
        }

    }
}
