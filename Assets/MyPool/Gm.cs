using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gm : PoolBase
{
    public override void Reset()
    {
        this.tag = "Untagged";
        this.transform.localPosition = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
        this.GetComponent<Rigidbody>().Sleep();
    }

}
