using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityPool;


public class Generator : MonoBehaviour
{
    public GameObject[] games;
    public PoolBase[] gems;
    public Gm[] gms;

    [SerializeField]
    private Pool<PoolBase>[] pools;


    private Dictionary<string, Pool<PoolBase>> dic = new Dictionary<string, Pool<PoolBase>>();

    private Dictionary<string, GemPool> Unitydic = new Dictionary<string, GemPool>();
    public float Interval;
    public int Num;
    public UnityAction<GameObject> destroy;
    public UnityAction<string, PoolBase> pooldestroy;
    public UnityAction<string, Gm> Unitypooldestroy;

    public GemPool[] gempools;

    private float TimeSpawn;


    private void Awake()
    {
        //destroy += (GameObject _) => { Destroy(_, 1); _.tag = "Ignore"; };

        //pools = new Pool<PoolBase>[gems.Length];
        //pooldestroy = (string _, PoolBase y) => { dic[_].DestObject(y, 1); y.tag = "Ignore"; };
        //for (int i = 0; i < gems.Length; i++)
        //{
        //    pools[i] = new Pool<PoolBase>(gems[i], this.transform, 50);
        //    dic.Add(gems[i].name, pools[i]);
        //}

        Unitypooldestroy = (string _, Gm y) => { Unitydic[_].Release(y); y.tag = "Ignore"; };
        gempools = new GemPool[gms.Length];
        for (int i = 0; i < gempools.Length; i++)
        {
            gempools[i] = new GemPool();
            gempools[i].Init(gms[i], this.transform);
            Unitydic.Add(gems[i].name, gempools[i]);
        }
    }

    private void Update()
    {
        TimeSpawn += Time.deltaTime;
        if (TimeSpawn > Interval)
        {
            //CommonGenera();
            //PoolGenera();

            UnityPoolGenera();

            TimeSpawn = 0;
        }
    }


    public void CommonGenera()
    {
        for (int i = 0; i < Num; i++)
        {
            var value = Random.Range(0, games.Length);
            var gem = GameObject.Instantiate(games[value], this.transform);

            gem.transform.localPosition = Random.insideUnitSphere * 2f;
        }
    }


    public void PoolGenera()
    {
        for (int i = 0; i < Num; i++)
        {
            var value = Random.Range(0, gems.Length);
            var gem = pools[value].GetObject();

            gem.transform.localPosition = Random.insideUnitSphere * 2f;
        }
    }


    public void UnityPoolGenera()
    {
        for (int i = 0; i < Num; i++)
        {
            var value = Random.Range(0, gems.Length);
            var gem = gempools[value].Get();

            gem.transform.localPosition = Random.insideUnitSphere * 2f;
        }
    }

}
