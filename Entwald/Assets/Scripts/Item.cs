using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public int itemPower;
	public int itemSpeed;
	public ItemType itemType;

	public enum ItemType {
		Weapon,
		Armor,
		Consumable,
		Throwable,
		Quest
	}

	public Item( int id, string name, string desc, int power, int speed, ItemType type){
		itemID = id;
		itemName = name;
		itemDesc = desc;
		itemIcon = Resources.Load <Texture2D> ("Item Icons/" + name);
		itemPower = power;
		itemSpeed = speed;
		itemType = type;

	}
	public Item(){
		itemID = -1;
	}

}
