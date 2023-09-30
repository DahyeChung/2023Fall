using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Wand : ItemBase {

    public GameObject BulletPrefab;
    public int MPCost = 2;

    protected override void OnItemUsed(PlayerActionScript playerActionScript)
    {
        if(playerActionScript.GetCurrentMP() >= MPCost)
        {
            playerActionScript.ConsumeMP(MPCost);
            GameObject bullet = Instantiate(BulletPrefab);
            bullet.transform.position = playerActionScript.transform.position;
            bullet.GetComponent<ProjectileScript>().MovementVector = playerActionScript.transform.forward;
        }
    }
}
