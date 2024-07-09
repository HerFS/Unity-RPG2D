using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public int id;
    public bool isNpc;
    public int npcId;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        npcId = id;
    }
}
