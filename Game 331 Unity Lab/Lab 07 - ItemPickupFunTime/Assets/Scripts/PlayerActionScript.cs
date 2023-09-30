using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionScript : MonoBehaviour {

    ItemInventory itemInventory;

    public int MaxMP = 10;
    int CurrentMP;

	// Use this for initialization
	void Start () {
        itemInventory = GetComponent<ItemInventory>();
        CurrentMP = MaxMP;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            itemInventory.SelectPreviousItem();
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            itemInventory.SelectNextItem();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ItemBase currentItem = itemInventory.GetCurrentItem();
            if(currentItem != null)
            {
                currentItem.UseItem(this);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ItemBase currentItem = itemInventory.GetCurrentItem();
            if (currentItem != null)
            {
                DropItem(currentItem);
            }
        }
    }

    public int GetCurrentMP()
    {
        return CurrentMP;
    }

    public void ConsumeMP(int consumeAmount)
    {
        CurrentMP -= consumeAmount;
    }

    public void RestoreMP()
    {
        CurrentMP = MaxMP;
    }

    public void ReceiveItem(ItemBase item)
    {
        ItemBase clone = item.Clone();
        itemInventory.AddItem(clone);
    }

    public void DropItem(ItemBase item)
    {
        itemInventory.RemoveItem(item);

        Vector3 offset = transform.forward * 2.0f;
        GameObject pickupItem = Instantiate(item.PickupItemPrefab, transform.position + offset, transform.rotation);
    }
}
