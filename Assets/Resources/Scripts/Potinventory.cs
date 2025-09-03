using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Potinventory : MonoBehaviour
{
    public List<potSlotData> slots = new List<potSlotData>();
    public float cookProcess = 0f;
    public string inventoryName = "";
    private int maxSlot = 4;

    public GameObject slotitemPrefab;
    public GameObject potInventoryPrefab;

    public Potinventory CopyValue(){
        Potinventory data = new Potinventory();
        data.cookProcess = this.cookProcess;
        data.inventoryName = this.inventoryName;
        data.maxSlot = this.maxSlot;
        data.slots = this.slots;

        return data;
    }

    public void Awake(){
        slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");
        potInventoryPrefab = Resources.Load<GameObject>("Prefabs/cook_inventory");
        slots = new List<potSlotData>();

        for(int i = 0; i < maxSlot; i++){
            potSlotData slot = new potSlotData();
            slot.isEmpty = true;
            slot.item = null;
            slot.itemData = null;
            slots.Add(slot);
        }
    }

    public void slotClearAll(){
        slots = new List<potSlotData>();

        for(int i = 0; i < maxSlot; i++){
            potSlotData slot = new potSlotData();
            slot.isEmpty = true;
            slot.item = null;
            slot.itemData = null;
            slots.Add(slot);
        }
    }

    

    public void openInventory()
    {
        GameObject player = GameObject.Find("Player");
        if(player.GetComponent<Inventory>().openInventory == ""){
            player.GetComponent<Inventory>().openInventory = "" + this.gameObject.name;

            GameObject potInventory = Instantiate(potInventoryPrefab, GameObject.Find("GUI").transform, false);
            potInventory.name = "potInventory";

            List<GameObject> slotObj = new List<GameObject>();
            for(int i = 0; i< potInventory.transform.childCount; i++){
                if(potInventory.transform.GetChild(i).gameObject.name.Contains("cookslot_")){
                    slotObj.Add(potInventory.transform.GetChild(i).gameObject);
                }
            }
            Potinventory potinv = potInventory.GetComponent<Potinventory>();
            potinv.slots = this.slots;


            for(int i = 0; i<maxSlot; i++){
                if(this.slots[i].isEmpty == false){
                    GameObject slotItem = Instantiate(slotitemPrefab, slotObj[i].transform, false);
                    slotItem.name = slots[i].itemData.itemName;
                    slotItem.GetComponent<Image>().sprite = slots[i].itemData.itemIcon;
                    slotItem.transform.SetAsFirstSibling();

                    GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                    go_amount.name = "slotamount";

                    GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                    go_durability.name = "slotdurability";
                    if(slots[i].itemData.durability == -1){
                        go_durability.SetActive(false);
                    }
                    slots[i].item = slotItem;
                }
            }
            GameObject slotcanvas = GameObject.Find("slotcanvas");
            slotcanvas.transform.SetAsLastSibling();

            potInventory.GetComponent<Potinventory>().inventoryName = ""+ this.gameObject.name;
        }
    }
    void Update(){
        GameObject player = GameObject.Find("Player");
        if( (player.GetComponent<Inventory>().openInventory == inventoryName) && (player.GetComponent<Inventory>().openInventory != "") ){
            GameObject potInventory = this.gameObject;
            GameObject chestInventory = GameObject.Find(potInventory.GetComponent<Potinventory>().inventoryName);


            chestInventory.GetComponent<Potinventory>().slots = potInventory.GetComponent<Potinventory>().slots;
            for(int i=0; i< maxSlot; i++){
                if(slots[i].item!=null){
                    GameObject slotamountObj = slots[i].item.transform.Find("slotamount").gameObject;
                    GameObject slotdurabilityObj = slots[i].item.transform.Find("slotdurability").Find("bar").gameObject;
                    if(slots[i].itemData.amount>=2) {
                        slotamountObj.GetComponent<TextMeshProUGUI>().text = "" + slots[i].itemData.amount;
                        slotamountObj.transform.SetAsLastSibling();
                    }
                    else{
                        slotamountObj.GetComponent<TextMeshProUGUI>().text = "";
                        slotamountObj.transform.SetAsLastSibling();
                    }
                    if((slots[i].itemData.durability >= 1)&& (slots[i].itemData.maxdurability >= 0)){
                        float dura = slots[i].itemData.durability;
                        float maxdura = slots[i].itemData.maxdurability;
                        float percent = dura / maxdura;
                        slotdurabilityObj.GetComponent<Image>().fillAmount = percent;
                    }
                }
            }
            GameObject PotObj  = GameObject.Find(inventoryName);
            float distance = Vector3.Distance(PotObj.transform.position, player.transform.position);
            if(distance >= 2.0f){
                GameObject.Find("potInventory").transform.Find("closebutton").gameObject.GetComponent<Potclosebutton>().closeInventory();
            }
            
        }
    }
}

[System.Serializable]
public class potSlotData{
    public bool isEmpty;
    public GameObject item;
    public ItemData itemData;
    public potSlotData CopyValue(){
        potSlotData data = new potSlotData();
        data.isEmpty = this.isEmpty;
        data.item = this.item;
        data.itemData = this.itemData;
        

        return data;
    }
}