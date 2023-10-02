using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : ItemBase
{

    protected override void OnItemUsed(PlayerActionScript playerActionScript)
    {
        Door[] doors = FindObjectsOfType<Door>(); //모든 문찾기
        foreach(Door door in doors)
        {
            Vector3 vectorToPlayer = playerActionScript.gameObject.transform.position - door.transform.position;
            float distanceToPlayer = vectorToPlayer.magnitude;
            if (distanceToPlayer < 2.0f)
            {
                Destroy(door.gameObject); //문을 찾으면 Destory
            }
        }
    }
}
