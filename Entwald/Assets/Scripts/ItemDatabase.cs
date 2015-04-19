using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	public List<Item> items = new List<Item>(); 

	void Start(){
		items.Add (new Item ("Sword",0,"A pointy sword made to kill.",5,3,Item.ItemType.Weapon));
		items.Add (new Item ("Apple",1,"A luscious red apple.",0,0,Item.ItemType.Consumable));
		items.Add (new Item ("Power Potion",2,"A potion that increases your power",6,0,Item.ItemType.Consumable));
		items.Add (new Item ("Mushroom", 3, "A mushroom that you can eat", 0, 0, Item.ItemType.Consumable));
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