using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	public List<Item> items = new List<Item>(); 

	void Start(){
		items.Add (new Item (0,"Sword","A pointy sword made to kill.",5,3,Item.ItemType.Weapon));
		items.Add (new Item (1,"Apple","A luscious red apple.",0,0,Item.ItemType.Consumable));
		items.Add (new Item (2,"Power Potion","A potion that increases your power",6,0,Item.ItemType.Consumable));
		items.Add (new Item (3,"Mushroom", "A mushroom that you can eat", 10, 0, Item.ItemType.Throwable));
	}

	public Item GetItem(int id){
		for (int i=0; i<items.Count; i++) {
			if(items[i].itemID == id){
				return items[i];
			}
		}
		return null;
	}﻿
}