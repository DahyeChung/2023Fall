using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : MonoBehaviour {

    public ItemBase InventoryItem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        PlayerActionScript player = collision.gameObject.GetComponent<PlayerActionScript>();
        if(player != null)
        {
            player.ReceiveItem(InventoryItem);
            //player.ReceiveItem(Instantiate(InventoryItem));
            Destroy(gameObject);
        }
    }
}
