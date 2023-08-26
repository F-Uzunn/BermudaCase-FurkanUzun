using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Destroy : MonoBehaviour
{
    //For particle destroy
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }
}
