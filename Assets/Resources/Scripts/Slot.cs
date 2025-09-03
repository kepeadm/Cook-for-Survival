using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void setstartdragInventory(string name){
        GameObject tempobj = GameObject.Find("GameManager");
        tempobj.GetComponent<Variables>().startdraginventory = name;
    }
    public void setdragInventory(string name){
        GameObject tempobj = GameObject.Find("GameManager");
        tempobj.GetComponent<Variables>().draginventory = name;
    }
    public string getstartdragInventory(){
        GameObject tempobj = GameObject.Find("GameManager");
        return tempobj.GetComponent<Variables>().startdraginventory;
    }
    public string getdragInventory(){
        GameObject tempobj = GameObject.Find("GameManager");
        return tempobj.GetComponent<Variables>().draginventory;
    }
    public void setstartdragSlot(int slot){
        GameObject tempobj = GameObject.Find("GameManager");
        tempobj.GetComponent<Variables>().startdragslot = slot;
    }
    public int getstartdragSlot(){
        GameObject tempobj = GameObject.Find("GameManager");
        return tempobj.GetComponent<Variables>().startdragslot;
    }
    public void setdragSlot(int slot){
        GameObject tempobj = GameObject.Find("GameManager");
        tempobj.GetComponent<Variables>().dragslot = slot;
    }
    public int getdragSlot(){
        GameObject tempobj = GameObject.Find("GameManager");
        return tempobj.GetComponent<Variables>().dragslot;
    }
    public void OnPointerClick(PointerEventData eventData){
        setSlotTool();
    }

    public void itemDrop(){
        if((getstartdragSlot()>=0) && (getdragInventory()=="air")){
            GameObject player = GameObject.Find("Player");
            player.GetComponent<Inventory>().dropItem(getstartdragSlot());
            GameObject dragitem = GameObject.Find("DragItem");
            GameObject iteminslot = player.GetComponent<Inventory>().slots[getstartdragSlot()].slotObj;
            iteminslot = iteminslot.transform.GetChild(0).gameObject;

            setstartdragSlot(-1);
            Destroy(iteminslot);
            Destroy(dragitem);
        }
    }


    public void OnEndDrag(PointerEventData eventData){
        if((getstartdragSlot()>=0) && (getdragInventory()=="air")){
            GameObject player = GameObject.Find("Player");
            player.GetComponent<Inventory>().dropItem(getstartdragSlot());
            GameObject dragitem = GameObject.Find("DragItem");
            GameObject iteminslot = player.GetComponent<Inventory>().slots[getstartdragSlot()].slotObj;
            iteminslot = iteminslot.transform.GetChild(0).gameObject;

            setstartdragSlot(-1);
            Destroy(iteminslot);
            Destroy(dragitem);
        }
        else if( (getstartdragSlot() >= 0) && (getdragInventory()=="GUI_Altar") ){
            GameObject altar = GameObject.Find("Altar");
            GameObject player = GameObject.Find("Player");
            Inventory inventory = player.GetComponent<Inventory>();
            GameObject startslotitem = inventory.slots[getstartdragSlot()].item;
            GameObject dragitem = GameObject.Find("DragItem");
            if( (getdragSlot() == 0) && (inventory.slots[getstartdragSlot()].itemData.itemType == ItemData.ItemType.food) ){
                ItemData dragitemData = inventory.slots[getstartdragSlot()].itemData;

                GameObject slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");
                Altar otherinv = GameObject.Find("Altar").GetComponent<Altar>();

                GameObject slotObj = GameObject.Find("altarslot_0");

                if(otherinv.slot.isEmpty == false){
                    inventory.GetItem(otherinv.slot.itemData);
                    foreach(Transform child in slotObj.transform){
                        Destroy(child.gameObject);
                    }
                }

                GameObject slotItem = Instantiate(slotitemPrefab, slotObj.transform, false);


                slotItem.name = dragitemData.itemName;
                slotItem.GetComponent<Image>().sprite = dragitemData.itemIcon;
                slotItem.transform.SetAsFirstSibling();
                slotItem.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);

                GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                go_amount.name = "slotamount";

                GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                go_durability.name = "slotdurability";
                if(dragitemData.durability == -1){
                    go_durability.SetActive(false);
                }


                startslotitem.SetActive(true);
                if(otherinv.slot.isEmpty == true){
                    otherinv.slot.isEmpty = false;
                }
                otherinv.slot.item = slotItem;
                otherinv.slot.itemData = inventory.slots[getstartdragSlot()].itemData.CopyValue();
                otherinv.slot.itemData.amount = 1;
                Destroy(dragitem);
                inventory.slots[getstartdragSlot()].itemData.amount -= 1;

                setstartdragSlot(-1);
            }
            else{
                setdragInventory("air");
                itemDrop();
            }
        }
        else if( (getstartdragSlot() >= 0) && (getdragInventory()=="GUI_Miller") ){
            GameObject miller = GameObject.Find("Windmill");
            GameObject player = GameObject.Find("Player");
            Inventory inventory = player.GetComponent<Inventory>();
            GameObject startslotitem = inventory.slots[getstartdragSlot()].item;
            GameObject dragitem = GameObject.Find("DragItem");
            Miller milling = miller.GetComponent<Miller>();
            if( (getdragSlot() == 0) && (milling.CheckCrops(inventory.slots[getstartdragSlot()].itemData.itemName)) ){
                ItemData dragitemData = inventory.slots[getstartdragSlot()].itemData;

                GameObject slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");

                GameObject slotObj = GameObject.Find("millerslot_0");
                GameObject slotItem = Instantiate(slotitemPrefab, slotObj.transform, false);

                

                slotItem.name = dragitemData.itemName;
                slotItem.GetComponent<Image>().sprite = dragitemData.itemIcon;
                slotItem.transform.SetAsFirstSibling();
                slotItem.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);

                GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                go_amount.name = "slotamount";

                GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                go_durability.name = "slotdurability";
                if(dragitemData.durability == -1){
                    go_durability.SetActive(false);
                }

                Miller otherinv = GameObject.Find("Windmill").GetComponent<Miller>();

                startslotitem.SetActive(true);
                otherinv.slots[getdragSlot()].item = slotItem;
                otherinv.slots[getdragSlot()].itemData = inventory.slots[getstartdragSlot()].itemData.CopyValue();

                otherinv.slots[getdragSlot()].isEmpty = false;
                Destroy(startslotitem);
                Destroy(dragitem);
                inventory.slots[getstartdragSlot()].isEmpty = true;
                inventory.slots[getstartdragSlot()].item = null;
                inventory.slots[getstartdragSlot()].itemData = null;
                setstartdragSlot(-1);
            }
            else{
                setdragInventory("air");
                itemDrop();
            }
        }
        else if( (getstartdragSlot() >= 0) && (getdragInventory()=="GUI_Beehive") ){
            GameObject beehive = GameObject.Find("Beehive");
            GameObject player = GameObject.Find("Player");
            Inventory inventory = player.GetComponent<Inventory>();
            GameObject startslotitem = inventory.slots[getstartdragSlot()].item;
            GameObject dragitem = GameObject.Find("DragItem");
            if( (getdragSlot() == 0) && (inventory.slots[getstartdragSlot()].itemData.itemName.Contains("queenbee")) ){
                ItemData dragitemData = inventory.slots[getstartdragSlot()].itemData;

                GameObject slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");

                GameObject slotObj = GameObject.Find("beehiveslot_0");
                GameObject slotItem = Instantiate(slotitemPrefab, slotObj.transform, false);

                

                slotItem.name = dragitemData.itemName;
                slotItem.GetComponent<Image>().sprite = dragitemData.itemIcon;
                slotItem.transform.SetAsFirstSibling();
                slotItem.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);

                GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                go_amount.name = "slotamount";

                GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                go_durability.name = "slotdurability";
                if(dragitemData.durability == -1){
                    go_durability.SetActive(false);
                }

                Beehive otherinv = GameObject.Find("Beehive").GetComponent<Beehive>();

                startslotitem.SetActive(true);
                otherinv.slots[getdragSlot()].item = slotItem;
                otherinv.slots[getdragSlot()].itemData = inventory.slots[getstartdragSlot()].itemData.CopyValue();

                otherinv.slots[getdragSlot()].isEmpty = false;
                Destroy(startslotitem);
                Destroy(dragitem);
                inventory.slots[getstartdragSlot()].isEmpty = true;
                inventory.slots[getstartdragSlot()].item = null;
                inventory.slots[getstartdragSlot()].itemData = null;
                setstartdragSlot(-1);
            }
            else{
                setdragInventory("air");
                itemDrop();
            }
        }
        else if( (getstartdragSlot() >= 0) && (getdragInventory()=="GUI_Furnace") ){
            GameObject furnace = GameObject.Find("Furnace");
            GameObject player = GameObject.Find("Player");
            Inventory inventory = player.GetComponent<Inventory>();
            GameObject startslotitem = inventory.slots[getstartdragSlot()].item;
            GameObject dragitem = GameObject.Find("DragItem");
            if( (getdragSlot() == 1) && (inventory.slots[getstartdragSlot()].itemData.itemName.Contains("wood")) ){
                ItemData dragitemData = inventory.slots[getstartdragSlot()].itemData;

                GameObject slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");

                GameObject slotObj = GameObject.Find("furnaceslot_1");
                GameObject slotItem = Instantiate(slotitemPrefab, slotObj.transform, false);

                

                slotItem.name = dragitemData.itemName;
                slotItem.GetComponent<Image>().sprite = dragitemData.itemIcon;
                slotItem.transform.SetAsFirstSibling();
                slotItem.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);

                GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                go_amount.name = "slotamount";

                GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                go_durability.name = "slotdurability";
                if(dragitemData.durability == -1){
                    go_durability.SetActive(false);
                }

                Furnace otherinv = GameObject.Find("Furnace").GetComponent<Furnace>();

                startslotitem.SetActive(true);
                otherinv.slots[getdragSlot()].item = slotItem;
                otherinv.slots[getdragSlot()].itemData = inventory.slots[getstartdragSlot()].itemData.CopyValue();

                otherinv.slots[getdragSlot()].isEmpty = false;
                Destroy(startslotitem);
                Destroy(dragitem);
                inventory.slots[getstartdragSlot()].isEmpty = true;
                inventory.slots[getstartdragSlot()].item = null;
                inventory.slots[getstartdragSlot()].itemData = null;
                setstartdragSlot(-1);
            }
            else if( (getdragSlot() == 0) && (inventory.slots[getstartdragSlot()].itemData.itemType == ItemData.ItemType.potitem) ){
                ItemData dragitemData = inventory.slots[getstartdragSlot()].itemData;

                GameObject slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");

                GameObject slotObj = GameObject.Find("furnaceslot_0");
                GameObject slotItem = Instantiate(slotitemPrefab, slotObj.transform, false);

                

                slotItem.name = dragitemData.itemName;
                slotItem.GetComponent<Image>().sprite = dragitemData.itemIcon;
                slotItem.transform.SetAsFirstSibling();
                slotItem.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);

                GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                go_amount.name = "slotamount";

                GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                go_durability.name = "slotdurability";
                if(dragitemData.durability == -1){
                    go_durability.SetActive(false);
                }

                Furnace otherinv = GameObject.Find("Furnace").GetComponent<Furnace>();

                startslotitem.SetActive(true);
                otherinv.slots[getdragSlot()].item = slotItem;
                otherinv.slots[getdragSlot()].itemData = inventory.slots[getstartdragSlot()].itemData.CopyValue();

                otherinv.slots[getdragSlot()].potInventory = inventory.slots[getstartdragSlot()].potInventory;

                otherinv.slots[getdragSlot()].isEmpty = false;
                Destroy(startslotitem);
                Destroy(dragitem);
                inventory.slots[getstartdragSlot()].isEmpty = true;
                inventory.slots[getstartdragSlot()].item = null;
                inventory.slots[getstartdragSlot()].itemData = null;
                otherinv.slots[getdragSlot()].maxcookProgress = GameObject.Find("Furnace").GetComponent<Cook>().getProgress(otherinv.slots[getdragSlot()]);
                otherinv.slots[getdragSlot()].cookProgress = 0f;
                otherinv.slots[getdragSlot()].level = 0;
                setstartdragSlot(-1);
            }
            else{
                setdragInventory("air");
                itemDrop();
            }
        }
        else if((getstartdragSlot() >= 0) && (getstartdragInventory() != getdragInventory()) && (getdragInventory()!="air") && (getdragInventory()!="GUI_Furnace")){
            GameObject otherobj = GameObject.Find(getdragInventory());
            if (otherobj.GetComponent<Potinventory>() != null){
                Potinventory otherinv = otherobj.GetComponent<Potinventory>();

                GameObject player = GameObject.Find("Player");
                Inventory inventory = player.GetComponent<Inventory>();
                if (otherinv.slots[getdragSlot()].isEmpty == true){
                    ItemData dragitemData = inventory.slots[getstartdragSlot()].itemData;
                    GameObject dragitem = GameObject.Find("DragItem");
                    GameObject startslotitem = inventory.slots[getstartdragSlot()].item;

                    GameObject slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");
                    GameObject slotItem = Instantiate(slotitemPrefab, otherobj.transform.GetChild(getdragSlot()), false);
        
                    slotItem.name = dragitemData.itemName;
                    slotItem.GetComponent<Image>().sprite = dragitemData.itemIcon;
                    slotItem.transform.SetAsFirstSibling();

                    GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                    go_amount.name = "slotamount";

                    GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                    go_durability.name = "slotdurability";
                    if(dragitemData.durability == -1){
                        go_durability.SetActive(false);
                    }

                    Destroy(dragitem);
                    startslotitem.SetActive(true);
                    otherinv.slots[getdragSlot()].item = slotItem;
                    otherinv.slots[getdragSlot()].itemData = inventory.slots[getstartdragSlot()].itemData.CopyValue();

                    otherinv.slots[getdragSlot()].itemData.amount = 1;
                    otherinv.slots[getdragSlot()].isEmpty = false;
                    inventory.slots[getstartdragSlot()].itemData.amount -= 1;
                    if(inventory.slots[getstartdragSlot()].itemData.amount<= 0){
                        Destroy(startslotitem);
                        inventory.slots[getstartdragSlot()].isEmpty = true;
                        inventory.slots[getstartdragSlot()].item = null;
                        inventory.slots[getstartdragSlot()].itemData = null;
                    }
                    setstartdragSlot(-1);
                    

                }
            }
        }
        else if((getstartdragSlot() >= 0) && (getstartdragInventory()==getdragInventory())){
            
            string slotA = "Slot_" + getstartdragSlot();
            string slotB = "Slot_" + getdragSlot();

            GameObject guiInventory = GameObject.Find("Inventory");

            
            int indexA = guiInventory.transform.Find(slotA).GetSiblingIndex();
            int indexB = guiInventory.transform.Find(slotB).GetSiblingIndex();
            if(indexA > indexB){
                int indexMin = indexB;
                int indexMax = indexA;
                guiInventory.transform.Find(slotA).SetSiblingIndex(indexMin);
                guiInventory.transform.Find(slotB).SetSiblingIndex(indexMax);
            }
            else{
                int indexMin = indexA;
                int indexMax = indexB;
                guiInventory.transform.Find(slotB).SetSiblingIndex(indexMin);
                guiInventory.transform.Find(slotA).SetSiblingIndex(indexMax);
            }



            GameObject player = GameObject.Find("Player");
            Inventory inventory = player.GetComponent<Inventory>();

            GameObject slotitem = inventory.slots[getstartdragSlot()].item;
            GameObject dragitem = GameObject.Find("DragItem");
            GameObject slotSelecter = GameObject.Find("slotSelecter");

            GameObject tempslotObj = guiInventory.transform.GetChild(inventory.slotToolIndex).gameObject;
            slotSelecter.transform.SetParent(tempslotObj.transform,false);
            
            
            

            Destroy(dragitem);
            slotitem.SetActive(true);
            setstartdragSlot(-1);
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        int clickedSlot = 0;
        string clickedSlotName = ""+this.gameObject.name;
        string[] splitter = clickedSlotName.Split('_');
        clickedSlot = int.Parse(splitter[1]);
        if ((getdragSlot()==clickedSlot)&&(getdragInventory()=="playerinventory")){
            setdragSlot(-1);
            setdragInventory("air");
        }
    }
    public void OnPointerEnter(PointerEventData eventData){
        int clickedSlot = 0;
        string clickedSlotName = ""+this.gameObject.name;
        string[] splitter = clickedSlotName.Split('_');
        clickedSlot = int.Parse(splitter[1]);
        setdragSlot(clickedSlot);
        setdragInventory("playerinventory");
    }
    public void OnDrag(PointerEventData eventData){
        GameObject slotcanvas = GameObject.Find("slotcanvas");
        if (slotcanvas.transform.childCount >= 1){
            GameObject dragitem = GameObject.Find("DragItem");
            dragitem.transform.position = eventData.position;
        }
    }


    public void OnBeginDrag(PointerEventData eventData){
        GameObject tempGUI = GameObject.Find("GUI");
        Canvas canvas = tempGUI.GetComponent<Canvas>();

        int clickedSlot = 0;
        string clickedSlotName = ""+this.gameObject.name;
        string[] splitter = clickedSlotName.Split('_');
        clickedSlot = int.Parse(splitter[1]);

        

        GameObject player = GameObject.Find("Player");
        Inventory inventory = player.GetComponent<Inventory>();

        if (inventory.slots[clickedSlot].item !=null){
            setstartdragSlot(clickedSlot);
            setstartdragInventory("playerinventory");
            GameObject slotitem = inventory.slots[clickedSlot].item;
            GameObject slotcanvas = GameObject.Find("slotcanvas");
            GameObject dragitem = Instantiate(slotitem, slotcanvas.transform, false);
            dragitem.name = "DragItem";
            slotitem.SetActive(false);
        }
    }
    
    void setSlotTool(){
        if (this.gameObject.name.Contains("Slot_")){
            int clickedSlot = 0;
            string clickedSlotName = ""+this.gameObject.name;
            string[] splitter = clickedSlotName.Split('_');
            clickedSlot = int.Parse(splitter[1]);
            
            GameObject player = GameObject.Find("Player");
            Inventory inventory = player.GetComponent<Inventory>();
            GameObject guiInventory = GameObject.Find("Inventory");
            
            inventory.slotToolIndex = guiInventory.transform.Find("Slot_"+clickedSlot).GetSiblingIndex();

            Texttips texttips = GameObject.Find("TextTips").GetComponent<Texttips>();

            if(inventory.slots[clickedSlot].isEmpty == false){
                texttips.openinventoryTip(inventory.slots[clickedSlot].itemData.itemName);
            }
        }
    }
}
