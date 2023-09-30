using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : ItemBase
{

    protected override void OnItemUsed(PlayerActionScript playerActionScript)
    {
        Door[] doors = FindObjectsOfType<Door>();
        foreach(Door door in doors)
        {
            Vector3 vectorToPlayer = playerActionScript.gameObject.transform.position - door.transform.position;
            float distanceToPlayer = vectorToPlayer.magnitude;
            if (distanceToPlayer < 2.0f)
            {
                Destroy(door.gameObject);
            }
        }
    }
}
