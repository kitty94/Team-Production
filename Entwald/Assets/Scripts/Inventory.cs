using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item> ();
	private ItemDatabase database;
	public bool showInventory;

	//Tool tip Variables
	private bool showTooltip;
	private string tooltip;

	// Drag Item Variables
	private bool draggingItem;
	private Item draggedItem;

	private int draggedIndex; // for dragging and dropping items;

	// Inventory Window Size;
	private Rect windowSize = new Rect ((Screen.width - 150) / 2, (Screen.height - 20) / 2, 180, 160);

	//public int inventorySize = 25;
	// Use this for initialization
	void Start () {
		for (int i=0; i<slotsX*slotsY; i++) {
			slots.Add (new Item());
			inventory.Add (new Item());
		}
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase>();

		LoadInventory ();
		AddItem (1);
		AddItem (0);
		AddItem (2);
		AddItem (3);
		//RemoveItem (1);

		//print (InventoryContains(1));
	}

	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Inventory")){
			showInventory = !showInventory;
		}

	}

	void OnGUI() {
		GUI.depth = 0;
		if(GUI.Button (new Rect(40,400,100,40), "Save"))
			SaveInventory();
		if(GUI.Button (new Rect(40,450,100,40), "Load"))
			LoadInventory();
//		if (GUI.Button (new Rect (40, 500, 100, 40), "Save Game Data"))
//			PPSerialization.Save ();
//		if (GUI.Button (new Rect (40, 550, 100, 40), "Load Game Data"))
//			PPSerialization.Load ();


		GUI.skin = skin;
		if (showInventory) {
			if (showTooltip)
				GUI.Box (new Rect(Event.current.mousePosition.x+20,Event.current.mousePosition.y+20,150,200), tooltip, skin.GetStyle("Tooltip"));
				//DrawInventory();
			DrawInventory();
//			if (showTooltip)
//				GUI.Box (new Rect(Event.current.mousePosition.x+20,Event.current.mousePosition.y+20,150,200), tooltip, skin.GetStyle("Tooltip"));
		}

		if(draggingItem){
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x,Event.current.mousePosition.y,30,30), draggedItem.itemIcon);
		}

		//Resets the tooltip when not hovered on
		tooltip = "";

	}

	void DrawInventory(){
		//windowSize = GUI.Window (0, windowSize, GenerateInventory, "Inventory");

		windowSize = GUI.Window(0,windowSize,GenerateInventory,"Inventory"); windowSize.x = Mathf.Clamp(windowSize.x,0,Screen.width-windowSize.width); windowSize.y = Mathf.Clamp(windowSize.y,0,Screen.height-windowSize.height);

	}

	void GenerateInventory(int id){
		GUILayout.BeginArea (new Rect (5,19,200,400));
		GUILayout.BeginHorizontal ();
		InventoryWindow ();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		if (!draggingItem) {
			GUI.DragWindow ();
		}
	}

	void InventoryWindow(){
		// Stores current Event 
		Event e = Event.current;
		int i = 0;
		// Creating the Inventory Grid;
		for( int y=0; y<slotsY; y++){
			for (int x=0; x<slotsX; x++) {
				Rect slotRect = new Rect(x*34,y*34,34,34);
				GUI.Box (slotRect, "", skin.GetStyle("Slot"));
				slots[i] = inventory[i];

				// Stores the current item with that value
				Item item = slots[i];

				// Checks if there is an item at this specific location of the inventory;
				if( slots[i].itemName != null){
					GUI.DrawTexture(slotRect,slots[i].itemIcon);

					//Mouse Position of this area ( Mouse Hovering )
					if(slotRect.Contains(e.mousePosition)){
						tooltip = CreateTooltip(slots[i]);
						//GUI.depth = 0;
						showTooltip = true;

						// Mouse Drag Area - If the mouse isn't already dragging an item
						if(e.button == 0 && e.type == EventType.mouseDrag && !draggingItem){ 
							draggingItem = true;
							draggedIndex = i;
							// Setting temporary variable to store the dragged item's data.
							draggedItem = slots[i];
							// Setting the current item that was dragged to another inventory slot.
							inventory[i] = new Item();
						}

						// If the mouse is up and is dragging an item
						if(e.type == EventType.mouseUp && draggingItem){
							inventory[draggedIndex] = item;
							inventory[i] = draggedItem;
							draggingItem = false;
							// No other item being dragged once you let go...
							draggedItem = null;
						}

						// Using COnsumables here
						if(e.isMouse && e.type == EventType.mouseDown && e.button == 1){
							//print ("Clicked " + inventory[i].itemName);
							if(item.itemType == Item.ItemType.Consumable){
								//print ("Used Consumable");
								UseConsumable(slots[i],i,true);
							}
						}
					}
				} else {
					if(slotRect.Contains(e.mousePosition)){
						if(e.type == EventType.mouseUp && draggingItem){
							//inventory[prevIndex] = inventory[i]; Not putting this here because there is nothing here.
							inventory[i] = draggedItem;
							draggingItem = false;
							// No other item being dragged once you let go...
							draggedItem = null;
						}
					}
				}
				if(tooltip == ""){
					showTooltip = false;
				}
				i++;
			}
		}
	}

	string CreateTooltip(Item item){
		if (item.itemType == Item.ItemType.Weapon) {
			tooltip = 
				"<color=#ff0000>" + item.itemName + "</color>\n\n" +
				"<color=#ffffff>Description: " + item.itemDesc + "</color>\n\n" +
				"<color=#ffffff>Power: " + item.itemPower + "</color>\n\n" +
				"<color=#ffffff>Speed: " + item.itemSpeed + "</color>\n\n" +
				"<color=#ffffff>Type: " + item.itemType + "</color>";
		} else if (item.itemType == Item.ItemType.Consumable){
			tooltip = 
				"<color=#ff0000>" + item.itemName + "</color>\n\n" +
					"<color=#ffffff>Description: " + item.itemDesc + "</color>\n\n" +
					"<color=#ffffff>Heal: " + item.itemPower + "</color>\n\n" +
					"<color=#ffffff>Type: " + item.itemType + "</color>";
		}
		return tooltip;
	}


	void AddItem(int id){
		for (int i=0; i<inventory.Count; i++) {
			if(inventory[i].itemName == null){
				for(int j=0; j<database.items.Count; j++){
					if(database.items[j].itemID == id){
						inventory[i] = database.items[j];
					}
				}
				break;
			}
		}
	}

	void RemoveItem(int id){
		for (int i=0; i<inventory.Count; i++) {
			if(inventory[i].itemID == id){
				inventory[i] = new Item();
				break;
			}
		}
	}

	bool InventoryContains(int id){
		bool result = false;
		for (int i=0; i<inventory.Count; i++){
			result = inventory[i].itemID == id;
			if(result){
				break;
			}
		}
		return result;
	}

	private void UseConsumable(Item item, int slot, bool deleteItem){
		switch(item.itemID){
		case 1:
		{
			print ("USED CONSUMABLE: " + item.itemName);
			//PlayerStats.IncreaseStat(3,15,30f);
			break;
		}
		case 2:
		{
			print ("USED CONSUMABLE: " + item.itemName);
			//PlayerStats.IncreaseStat(3,15,30f);
			break;
		}
		}

		if (deleteItem)
			inventory[slot] = new Item();

		}

	void SaveInventory(){

		for(int i=0; i<inventory.Count; i++){
			PlayerPrefs.SetInt ("Inventory " + i, inventory[i].itemID);
		}
	}

	void LoadInventory(){
		for (int i = 0; i < inventory.Count; i++) {
			inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? database.GetItem(PlayerPrefs.GetInt("Inventory " + i)): new Item();
		}
	}

}
