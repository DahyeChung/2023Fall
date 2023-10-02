using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Mushroom : ItemBase
{

    protected override void OnItemUsed(PlayerActionScript playerActionScript)
    {
        playerActionScript.transform.localScale *= 2.0f;
    }
}
