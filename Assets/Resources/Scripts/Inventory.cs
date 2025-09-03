using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<SlotData> slots = new List<SlotData>();
    private int maxSlot = 5;
    public GameObject slotPrefab;
    public GameObject slotamountPrefab;
    public GameObject slotdurabilityPrefab;
    public GameObject slotSelecterPrefab;
    public ItemData toolData;
    public int slotTool = 0;
    public string slotToolName;
    public int slotToolIndex = 0;

    public string openInventory = "";
    

    private void Start()
    {
        GameObject slotPanel = GameObject.Find("Inventory");
        for(int i = 0; i< maxSlot; i++)
        {   
            GameObject go = Instantiate(slotPrefab, slotPanel.transform, false);
            go.name = "Slot_" + i;


            SlotData slot = new SlotData();
            slot.isEmpty = true;
            slot.slotObj = go;
            slot.item = null;
            slot.potInventory = new Potinventory();
            slots.Add(slot);


            

            
        }
        GameObject slotSelecter = Instantiate(slotSelecterPrefab, slots[0].slotObj.transform, false);
        slotSelecter.name = "slotSelecter";

        GameObject tempObj = new GameObject("tempEmpty");
        GameObject guiPanel = GameObject.Find("GUI");
        GameObject go2 = Instantiate(tempObj, guiPanel.transform, false);
        Destroy(tempObj);
        go2.name = "slotcanvas";
        go2.gameObject.transform.localScale = new Vector3(1.25f,1.25f,1);
        go2.gameObject.AddComponent<CanvasGroup>();
        go2.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public ItemData GetTool(){
        string toolName = slotToolName;
        if((toolName != null) && (toolName.Contains("_"))){
            List<SlotData> newslots = slots;
            int toolindex = 0;
            string toolSlotName = ""+toolName;
            string[] splitter = toolSlotName.Split('_');
            toolindex = int.Parse(splitter[1]);
            ItemData playertool = newslots[toolindex].itemData;
            return playertool;
        }
        return null;
    }

    public void GetItem(ItemData item, Potinventory potinv = null){
        bool gettingitem = false;
        for(int i = 0; i< maxSlot; i++){
            if(slots[i].isEmpty == false){
                if( (slots[i].itemData.maxdurability == item.maxdurability) && (slots[i].itemData.stackable == true) ){
                    if(slots[i].itemData.itemName == item.itemName){
                        slots[i].itemData.amount = slots[i].itemData.amount + item.amount;
                        if(slots[i].itemData.durability != -1){
                            float duraset = (slots[i].itemData.durability * slots[i].itemData.amount) + (item.durability * item.amount);
                            duraset = duraset / (slots[i].itemData.amount + item.amount);
                            slots[i].itemData.durability = (int) duraset;
                        }
                        gettingitem = true;
                        break;
                    }
                }
            }
        }
        if (gettingitem == false){
            List<int> temp_a = new List<int>();
            GameObject inv_obj = GameObject.Find("Inventory");
            for(int a = 0; a< inv_obj.transform.childCount; a++){
                if(inv_obj.transform.GetChild(a).gameObject.name.Contains("Slot_")){
                    int clickedSlot = 0;
                    string clickedSlotName = ""+inv_obj.transform.GetChild(a).gameObject.name;
                    string[] splitter = clickedSlotName.Split('_');
                    clickedSlot = int.Parse(splitter[1]);
                    temp_a.Add(clickedSlot);
                }
            }
            foreach(int i in temp_a){
                if(slots[i].isEmpty == true){
                    int b = i;
                    GameObject slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");
                    GameObject slotItem = Instantiate(slotitemPrefab, slots[b].slotObj.transform, false);

                    slotItem.name = item.itemName;
                    slotItem.GetComponent<Image>().sprite = item.itemIcon;
                    slotItem.transform.SetAsFirstSibling();

                    GameObject player = GameObject.Find("Player");
                    GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                    go_amount.name = "slotamount";

                    GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                    go_durability.name = "slotdurability";
                    if(item.durability == -1){
                        go_durability.SetActive(false);
                    }


                    if(item.hasInventory == true){
                        slots[b].potInventory = potinv;
                        slots[b].cookProcess = potinv.cookProcess;
                    }
                    slots[b].item = slotItem;
                    slots[b].itemData = item.CopyValue();
                    slots[b].isEmpty = false;
                    break;
                }
            }
        }
    }

    public void dropTool(){
        string toolName = slotToolName;
        if((toolName != null) && (toolName.Contains("_"))){
            List<SlotData> newslots = slots;
            int toolindex = 0;
            string toolSlotName = ""+toolName;
            string[] splitter = toolSlotName.Split('_');
            toolindex = int.Parse(splitter[1]);
            GameObject iteminslot = slots[toolindex].slotObj;
            iteminslot = iteminslot.transform.GetChild(0).gameObject;
            dropItem(toolindex);
            Destroy(iteminslot);
        }
        return;
    }

    public GameObject dropToolObj(){
        string toolName = slotToolName;
        GameObject dropeditem;
        if((toolName != null) && (toolName.Contains("_"))){
            List<SlotData> newslots = slots;
            int toolindex = 0;
            string toolSlotName = ""+toolName;
            string[] splitter = toolSlotName.Split('_');
            toolindex = int.Parse(splitter[1]);
            GameObject iteminslot = slots[toolindex].slotObj;
            iteminslot = iteminslot.transform.GetChild(0).gameObject;
            if(toolindex<= maxSlot){
                if (slots[toolindex].isEmpty == true){
                    return null;
                }
                GameObject gm = GameObject.Find("GameManager");

                Vector3 pos = this.gameObject.transform.position;
                pos.x = this.gameObject.transform.position.x;

                string name = slots[toolindex].itemData.itemName;
                int amount = slots[toolindex].itemData.amount;
                dropeditem = gm.GetComponent<Item_manager>().throwItemPlayerObj(name, pos, amount, slots[toolindex].itemData, toolindex);


                slots[toolindex].isEmpty = true;
                slots[toolindex].item = null;
                slots[toolindex].itemData = null;
                Destroy(iteminslot);
                return dropeditem;
            }
        }
        return null;
    }

    public void dropItem(int slot){
        if(slot<= maxSlot){
            if (slots[slot].isEmpty == true){
                return;
            }
            GameObject gm = GameObject.Find("GameManager");

            Vector3 pos = this.gameObject.transform.position;
            pos.x = this.gameObject.transform.position.x;

            string name = slots[slot].itemData.itemName;
            int amount = slots[slot].itemData.amount;
            gm.GetComponent<Item_manager>().throwItemPlayer(name, pos, amount, slots[slot].itemData, slot);


            slots[slot].isEmpty = true;
            slots[slot].item = null;
            slots[slot].itemData = null;
            string slotName = "Slot_" + slot;

            

        }
    }

    public void RemoveItemAmount(string name, int amount){
        for(int i = 0; i< maxSlot; i++){
            if(slots[i].isEmpty == false){
                if(slots[i].itemData.itemName == name){
                    slots[i].itemData.amount -= amount;
                }
            }
            if(slots[i].isEmpty == true){
            }
        }
        return;
    }

    public int CheckItemAmount(string name){
        for(int i = 0; i< maxSlot; i++){
            if(slots[i].isEmpty == false){
                if(slots[i].itemData.itemName == name){
                    return slots[i].itemData.amount;
                }
            }
        }
        return 0;
    }
    public int CheckHasItem(string name){
        for(int i = 0; i< maxSlot; i++){
            if(slots[i].isEmpty == false){
                if(slots[i].itemData.itemName == name){
                    return slots[i].itemData.amount;
                }
            }
        }
        return 0;
    }

    public bool CheckGetItem(ItemData item){
        for(int i = 0; i< maxSlot; i++){
            if(slots[i].isEmpty == false){
                if(slots[i].itemData.itemName == item.itemName){
                    return true;
                }
            }
            if(slots[i].isEmpty == true){
                return true;
            }
        }
        return false;
    }
    void Update(){
        GameObject guiInventory = GameObject.Find("Inventory");
        if (slotTool != slotToolIndex){
            GameObject slotSelecter = GameObject.Find("slotSelecter");
            GameObject tempslotObj = guiInventory.transform.GetChild(slotToolIndex).gameObject;
            slotSelecter.transform.SetParent(tempslotObj.transform,false);
            slotSelecter.transform.SetAsLastSibling();
            slotTool = slotToolIndex;
            slotToolName = tempslotObj.name;
        }
        if(slotToolName != guiInventory.transform.GetChild(slotToolIndex).gameObject.name){
            slotToolName = guiInventory.transform.GetChild(slotToolIndex).gameObject.name;
        }
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
                if((slots[i].itemData.durability >= 0)&& (slots[i].itemData.maxdurability >= 0)){
                    float dura = slots[i].itemData.durability;
                    float maxdura = slots[i].itemData.maxdurability;
                    float percent = dura / maxdura;
                    slotdurabilityObj.GetComponent<Image>().fillAmount = percent;
                }
                if((slots[i].itemData.durability <= 0)&& (slots[i].itemData.maxdurability >= 0)){
                    if(slots[i].itemData.amount > 0){
                        slots[i].itemData.amount -= 1;
                        slots[i].itemData.durability = slots[i].itemData.maxdurability;
                    }
                    else{
                        slots[i].isEmpty = true;
                        slots[i].item = null;
                        slots[i].itemData = null;
                        GameObject iteminslot = slots[i].slotObj;
                        iteminslot = iteminslot.transform.GetChild(0).gameObject;
                        Destroy(iteminslot);
                    }
                }
            }
            if(slots[i].item != null){
                if(slots[i].itemData.amount <= 0){
                    slots[i].isEmpty = true;
                    slots[i].item = null;
                    slots[i].itemData = null;
                    GameObject iteminslot = slots[i].slotObj;
                    iteminslot = iteminslot.transform.GetChild(0).gameObject;
                    Destroy(iteminslot);
                }

            }
        }
        string splitName = ""+ slotToolName;
        string[] splitter = splitName.Split('_');
        int toolslot = int.Parse(splitter[1]);
        toolData = slots[toolslot].itemData;
    }
}

[System.Serializable]
public class SlotData{
    public bool isEmpty;
    public GameObject item;
    public ItemData itemData;
    public GameObject slotObj;
    public int maxSize;

    public Potinventory potInventory;
    public float cookProcess;
}