using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GateSelect : MonoBehaviour
{
    [field: SerializeField] private List<GameObject> gates;
    private void Awake()
    {
        int x = Random.Range(0, 2);
        if (x == 0)
        {
            Vector3 tempPos = gates[0].transform.localPosition;
            gates[0].transform.localPosition = gates[1].transform.localPosition;
            gates[1].transform.localPosition = tempPos;
        }
    }
}
