using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Generator : MonoBehaviour
{
    public GameObject[] games;
    public PoolBase[] gems;
    [SerializeField]
    private Pool<PoolBase>[] pools ;
    [SerializeField]
    private Dictionary<string, Pool<PoolBase>> dic = new Dictionary<string, Pool<PoolBase>>();
    public float Interval;
    public int Num;
    public UnityAction<GameObject> destroy;
    public UnityAction<string, PoolBase> pooldestroy;

    private float TimeSpawn;


    private void Awake()
    {
        destroy += (GameObject _) => { Destroy(_, 1); _.tag = "Ignore"; };

        //pools = new Pool<PoolBase>[gems.Length];
        //pooldestroy = (string _, PoolBase y) => { dic[_].DestObject(y, 1); y.tag = "Ignore"; };
        //for (int i = 0; i < gems.Length; i++)
        //{
        //    pools[i] = new Pool<PoolBase>(gems[i], this.transform, 50);
        //    dic.Add(gems[i].name, pools[i]);
        //}
    }

    private void Update()
    {
        TimeSpawn += Time.deltaTime;
        if (TimeSpawn > Interval)
        {
            CommonGenera();
            //PoolGenera();

            TimeSpawn = 0;
        }
    }


    public void CommonGenera()
    {
        for (int i = 0; i < Num; i++)
        {
            var value = Random.Range(0, games.Length);
            var gem = GameObject.Instantiate(games[value],this.transform);

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

}
