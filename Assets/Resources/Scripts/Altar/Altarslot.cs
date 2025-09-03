using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Altarslot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
        GameObject altarObj = GameObject.Find("Altar");
        Altar altarinv = altarObj.GetComponent<Altar>();

        if (altarinv.slot.isEmpty == false){

            GameObject player = GameObject.Find("Player");
            Inventory inventory = player.GetComponent<Inventory>();

            if(inventory.CheckGetItem(altarinv.slot.itemData) == true){
                inventory.GetItem(altarinv.slot.itemData);
                altarinv.slot.isEmpty = true;
                altarinv.slot.item = null;
                altarinv.slot.itemData = null;
                
                Destroy(this.transform.GetChild(0).gameObject);
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData){
        int clickedSlot = 0;
        if ((getdragSlot()==clickedSlot)&&(getdragInventory()=="GUI_Altar")){
            setdragSlot(-1);
            setdragInventory("air");
        }
    }
    public void OnPointerEnter(PointerEventData eventData){
        int clickedSlot = 0;
        setdragSlot(clickedSlot);
        setdragInventory("GUI_Altar");
    }

}
