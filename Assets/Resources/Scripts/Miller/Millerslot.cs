using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Millerslot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
        GameObject millerObj = GameObject.Find("Windmill");
        Miller millerinv = millerObj.GetComponent<Miller>();

        int clickedSlot = 0;
        string clickedSlotName = ""+this.gameObject.name;
        string[] splitter = clickedSlotName.Split('_');
        clickedSlot = int.Parse(splitter[1]);
        if (millerinv.slots[clickedSlot].isEmpty == false){

            GameObject player = GameObject.Find("Player");
            Inventory inventory = player.GetComponent<Inventory>();

            if(inventory.CheckGetItem(millerinv.slots[clickedSlot].itemData) == true){
                inventory.GetItem(millerinv.slots[clickedSlot].itemData);
                millerinv.slots[clickedSlot].isEmpty = true;
                millerinv.slots[clickedSlot].item = null;
                millerinv.slots[clickedSlot].itemData = null;
                
                Destroy(this.transform.GetChild(0).gameObject);
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData){
        int clickedSlot = 0;
        string clickedSlotName = ""+this.gameObject.name;
        string[] splitter = clickedSlotName.Split('_');
        clickedSlot = int.Parse(splitter[1]);
        if ((getdragSlot()==clickedSlot)&&(getdragInventory()=="GUI_Miller")){
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
        setdragInventory("GUI_Miller");
    }

}
