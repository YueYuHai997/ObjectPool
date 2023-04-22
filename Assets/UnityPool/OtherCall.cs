using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityPool;

public class OtherCall : MonoBehaviour
{
    public Gm preab;
    public GemPool gemPool;


    private void Awake()
    {
        gemPool = new GemPool();
        gemPool.Init(preab,this.transform);

        //GameObject g = new GameObject("G");
        //var t =g.AddComponent<GemPool>();
        //t.prefab = preab;
        //GemPool = t;
    }

    private void Update()
    {
        gemPool.Get();
    }

}
