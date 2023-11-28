using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject follow;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = follow.transform.position;
        transform.position = new Vector3(pos.x, pos.y + 3, pos.z - 10);
    }
}
