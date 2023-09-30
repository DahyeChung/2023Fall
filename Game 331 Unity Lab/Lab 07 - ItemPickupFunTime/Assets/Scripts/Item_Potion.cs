using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Potion : ItemBase
{

    protected override void OnItemUsed(PlayerActionScript playerActionScript)
    {
        playerActionScript.RestoreMP();
    }
}
