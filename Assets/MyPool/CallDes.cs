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
            generator.destroy(other.gameObject);
            //generator.pooldestroy(other.gameObject.name, other.GetComponent<PoolBase>());
        }
    }
}
