using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public ItemData itemData;
    public GameObject slotitemPrefab;
    public float freshing = 0f;
    public void Update(){
        if(itemData.itemType == ItemData.ItemType.food){
            if(itemData.durability >= 1){
                freshing = freshing + ( 0.2f * Time.deltaTime );
                if(freshing >= 1){
                    itemData.durability -=1;
                    freshing -=1;
                }
                if(itemData.durability <= 0){
                    Item_manager im = GameObject.Find("GameManager").GetComponent<Item_manager>();
                    itemData = im.loadItemData("trash", itemData);
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Item/trash");
                }
            }
        }
    }
    public void PickUp(GameObject Player){
        if ((Player.tag.Equals("Player")) && (this.tag.Contains("item")))
        {
            if(Player.GetComponent<Inventory>().CheckGetItem(itemData)){
                if(this.tag == "potitem"){
                    GameObject player = GameObject.Find("Player");
                    if( (player.GetComponent<Inventory>().openInventory == "" + this.gameObject.name ) && (player.GetComponent<Inventory>().openInventory != "GUI_Furnace") ){
                        GameObject cin = GameObject.Find("potInventory");
                        cin = cin.transform.Find("closebutton").gameObject;
                        cin.GetComponent<Potclosebutton>().closeInventory();
                    }
                }
                Player.GetComponent<Inventory>().GetItem(itemData, this.gameObject.GetComponent<Potinventory>());
                Destroy(this.gameObject);
            }
            /*
            bool getting = false;
            if(this.tag == "potitem"){
                GameObject player = GameObject.Find("Player");
                if( (player.GetComponent<Inventory>().openInventory != "") && (player.GetComponent<Inventory>().openInventory != "GUI_Furnace") ){
                    GameObject cin = GameObject.Find("potInventory");
                    cin = cin.transform.Find("closebutton").gameObject;
                    cin.GetComponent<Potclosebutton>().closeInventory();
                }
            }
            Inventory inventory = Player.GetComponent<Inventory>();
            for(int i = 0; i < inventory.slots.Count; i++){
                if(inventory.slots[i].isEmpty != true){
                    if ((inventory.slots[i].itemData.itemName == this.itemData.itemName)&&(inventory.slots[i].itemData.stackable) && (inventory.slots[i].itemData.durability == this.itemData.durability)){
                        inventory.slots[i].itemData.amount += this.itemData.amount;
                        Destroy(this.gameObject);
                        getting = true;
                        break;
                    }
                }
            }
            if(getting == false){
                for(int i = 0; i < inventory.slots.Count; i++){
                    if(inventory.slots[i].isEmpty){
                        GameObject slotItem = Instantiate(slotitemPrefab, inventory.slots[i].slotObj.transform, false);
                        slotItem.name = itemData.itemName;
                        slotItem.GetComponent<Image>().sprite = itemData.itemIcon;
                        slotItem.transform.SetAsFirstSibling();

                        GameObject player = GameObject.Find("Player");
                        GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                        go_amount.name = "slotamount";

                        GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                        go_durability.name = "slotdurability";
                        if(this.itemData.durability == -1){
                            go_durability.SetActive(false);
                        }

                        inventory.slots[i].item = slotItem;
                        inventory.slots[i].itemData = itemData;
                        if(itemData.hasInventory == true){
                            inventory.slots[i].potInventory = this.GetComponent<Potinventory>().slots;
                            inventory.slots[i].cookProcess = this.GetComponent<Potinventory>().cookProcess;
                        }
                        inventory.slots[i].isEmpty = false;
                        Destroy(this.gameObject);
                        getting = true;
                        break;
                    }
                }
            }
            */
        }
    }
}

[System.Serializable]
public class ItemData{
    
    public string itemName;
    public string itemNameKor;
    public string objectName;
    public Sprite itemIcon;
    public int quantity;
    public int durability;
    public int maxdurability;
    public bool stackable;
    public int amount;
    public ItemType itemType;
    public string spritePath;
    
    public bool hasInventory;

    public enum ItemType{
        food,
        item,
        tool,
        potitem
    }
    
    public ItemData CopyValue(){
        ItemData data = new ItemData();
        data.itemName = this.itemName;
        data.objectName = this.objectName;
        data.itemIcon = this.itemIcon;
        data.quantity = this.quantity;
        data.durability = this.durability;
        data.maxdurability = this.maxdurability;
        data.stackable = this.stackable;
        data.amount = this.amount;
        data.itemType = this.itemType;
        data.spritePath = this.spritePath;
        data.hasInventory = this.hasInventory;

        return data;
    }
}
