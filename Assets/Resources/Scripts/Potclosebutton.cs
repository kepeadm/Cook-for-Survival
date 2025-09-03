using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Potclosebutton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData){
        GameObject player = GameObject.Find("Player");
        string inventoryName = this.gameObject.transform.parent.name;
        GameObject potInventory = this.gameObject.transform.parent.gameObject;
        GameObject chestInventory = GameObject.Find(potInventory.GetComponent<Potinventory>().inventoryName);


        chestInventory.GetComponent<Potinventory>().slots = potInventory.GetComponent<Potinventory>().slots;
        player.GetComponent<Inventory>().openInventory = "";

        Destroy(potInventory);
    }
    public void closeInventory(){
        GameObject player = GameObject.Find("Player");
        string inventoryName = this.gameObject.transform.parent.name;
        GameObject potInventory = this.gameObject.transform.parent.gameObject;
        /*
        GameObject chestInventory = GameObject.Find(potInventory.GetComponent<Potinventory>().inventoryName);


        chestInventory.GetComponent<Potinventory>().slots = potInventory.GetComponent<Potinventory>().slots;
        */
        player.GetComponent<Inventory>().openInventory = "";

        Destroy(potInventory);
    }
}