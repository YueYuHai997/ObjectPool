using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;
using System.Threading.Tasks;

[Serializable]
public class Pool<T> : MonoBehaviour where T : PoolBase
{
    /// <summary>
    ///  构造器 初始化时产生多少物体
    /// </summary>
    /// <param name="Tempt"></param>
    /// <param name="trans"></param>
    /// <param name="Size"></param>
    public Pool(T Tempt, Transform parent, int Size)
    {
        t = Tempt;
        MaxSize = Size;
        Items = new T[MaxSize];
        Parent = parent;
        //首先初始化出MaxSize个
        for (int i = 0; i < MaxSize; i++)
        {
            Items[i] = Instantiate(t, parent.position, parent.rotation, parent);
            Items[i].gameObject.SetActive(false);
            Items[i].name = Tempt.name;
        }
    }


    /// <summary>   位置   </summary>
    private Transform Parent;
    /// <summary>   类型   </summary>
    private T t;
    /// <summary>   对象池最大容量   </summary>
    public int MaxSize;
    /// <summary>   对象池   </summary>
    private T[] Items;

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <returns></returns>
    public T GetObject() 
    {
        for (int i = 0; i < MaxSize; i++)
        {
            if (!(Items[i].IsUse))
            {
                Items[i].Reset();
                Items[i].num = i;
                Items[i].gameObject.SetActive(true);
                Items[i].IsUse = true;
                return Items[i];
            }
        }
        Debug.Log(132);
        //遍历数组后没有不再使用的
        return DynamicAddSize();
    }


    /// <summary>
    /// 获取对象
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public T GetObject(UnityAction<PoolBase> action)
    {
        for (int i = 0; i < MaxSize; i++)
        {
            if (!Items[i].IsUse)
            {
                action?.Invoke(Items[i]);
                Items[i].gameObject.SetActive(true);
                Items[i].IsUse = true;
                return Items[i];
            }
        }
        Debug.Log(132);
        //遍历数组后没有不再使用的
        return DynamicAddSize();
    }


    /// <summary>
    /// 销毁对象
    /// </summary>
    /// <param name="t"></param>
    public void DestObject(T t)
    {
        //for (int i = 0; i < MaxSize; i++)
        //{
        //    if (Items[i] == t)
        //    {
        //        Items[i].Reset();
        //        Items[i].IsUse = false;
        //        Items[i].gameObject.SetActive(false);
        //        return;
        //    }
        //}
        //Debug.LogError("销毁未找到物体");

        Items[t.num].Reset();
        Items[t.num].IsUse = false;
        Items[t.num].gameObject.SetActive(false);
    }

    async public void DestObject(T t, float Time)
    {
        await Task.Delay((int)(Time * 1000));
        DestObject(t);
    }



    /// <summary>
    /// 动态调整数组长度
    /// </summary>
    public T DynamicAddSize()
    {
        Debug.Log($"数组长度不足");
        int RecordNum;
        int n = 1;
        while (n <= MaxSize) n *= 2;
        T[] Temp = new T[n];
        Array.Copy(Items, 0, Temp, 0, Items.Length);
        Items = Temp;
        for (int i = MaxSize; i < n; i++)
        {
            Items[i] = Instantiate(t, Parent);
            Items[i].num = i;
            Items[i].gameObject.SetActive(false);
            Items[i].IsUse = false;
            Items[i].name = t.name;
        }
        RecordNum = MaxSize;
        MaxSize = n;
        Debug.Log($"数组长度不足，动态调整数组长度,调整后长度{ MaxSize }");

        Items[RecordNum].gameObject.SetActive(true);
        return Items[RecordNum];
    }


    //批量式初始化
    public void DynamicAddSize(int size)
    {
        Debug.Log("数组长度不足，动态调整数组长度");
        int n = 1;
        while (n <= size) n *= 2;
        T[] Temp = new T[n];
        Array.Copy(Items, 0, Temp, 0, Items.Length);
        Items = Temp;
        for (int i = MaxSize; i < n; i++)
        {
            Items[i] = Instantiate(t, Parent);
            Items[i].num = i;
            Items[i].gameObject.SetActive(false);
            Items[i].IsUse = false;
            Items[i].name = t.name;
        }
        MaxSize = n;
        Debug.Log($"调整后长度{ MaxSize }");
    }


    //TODO 暂时不知道怎么处理
    public void DynamciReduceSize()
    {
        Debug.Log("数组长度过长，动态调整数组长度");
        int n = MaxSize / 2;
        T[] Temp = new T[n];
        Array.Copy(Items, 0, Temp, 0, Items.Length);
        Items = Temp;
        for (int i = n; i < MaxSize; i++)
        {
            Destroy(Items[i]);
        }
        MaxSize = n;
        Debug.Log($"调整后长度{ MaxSize }");
    }
}

[Serializable]
public abstract class PoolBase : MonoBehaviour
{
    public bool IsUse;
    public int num;
    public abstract void Reset();
}