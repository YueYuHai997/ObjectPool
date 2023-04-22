using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDes : MonoBehaviour
{
    public Generator generator;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ignore"))
        {
            //Destroy(other.gameObject);
            //generator.destroy(other.gameObject);
            //generator.pooldestroy(other.gameObject.name, other.GetComponent<PoolBase>());

            generator.Unitypooldestroy(other.gameObject.name, other.GetComponent<Gm>());
        }
    }
}
