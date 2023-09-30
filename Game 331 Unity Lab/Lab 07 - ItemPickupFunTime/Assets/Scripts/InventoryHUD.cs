using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHUD : MonoBehaviour {

    public ItemInventory targetInventory;
    public GameObject itemPanelPrefab;

    public Vector3 itemStartPosition = new Vector3(-160.0f, -24.0f, 0.0f);
    public float itemXSpacing = 48.0f;

    List<ItemPanelHUD> itemPanelHuds;

	// Use this for initialization
	void Start () {

        itemPanelHuds = new List<ItemPanelHUD>();
        BuildInventoryPanelsFromTargetInventory();
	}
	
	// Update is called once per frame
	void Update () {

        if(targetInventory.ItemList.Count != itemPanelHuds.Count)
        {
            BuildInventoryPanelsFromTargetInventory();
        }

        System.Diagnostics.Debug.Assert(targetInventory.ItemList.Count == itemPanelHuds.Count);
        for (int i = 0; i < targetInventory.ItemList.Count; ++i)
        {
            ItemBase item = targetInventory.ItemList[i];
            ItemPanelHUD itemPanelHUD = itemPanelHuds[i];

            itemPanelHUD.ItemName.text = item.ItemName;
            itemPanelHUD.ItemIcon.sprite = item.ItemHUDSprite;

            //itemPanelHUD.ItemBorder.color = (item == targetInventory.GetCurrentItem()) ? Color.yellow : Color.white;
            itemPanelHUD.ItemBorder.color = (i == targetInventory.GetCurrentItemIndex()) ? Color.yellow : Color.white;

            itemPanelHUD.ItemBG.color = (item.GetCurrentCooldownSeconds() > 0.0f) ? Color.gray : Color.cyan;
        }
	}

    void BuildInventoryPanelsFromTargetInventory()
    {
        
        foreach (ItemPanelHUD itemPanelHud in itemPanelHuds)
        {
            Destroy(itemPanelHud.gameObject);
        }
        itemPanelHuds = new List<ItemPanelHUD>();

        Vector3 currItemPosition = itemStartPosition;
        foreach (ItemBase item in targetInventory.ItemList)
        {
            GameObject itemPanelObject = Instantiate<GameObject>(itemPanelPrefab);
            ItemPanelHUD itemPanelHud = itemPanelObject.GetComponent<ItemPanelHUD>();
            
            itemPanelObject.transform.SetParent(transform);
            itemPanelObject.transform.localPosition = currItemPosition;
            currItemPosition.x = currItemPosition.x + itemXSpacing;

            itemPanelHuds.Add(itemPanelHud);
        }
    }
}
