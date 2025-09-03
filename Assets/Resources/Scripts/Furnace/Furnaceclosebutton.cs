using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Furnaceclosebutton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData){
        closeInventory();
    }
    public void closeInventory(){
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Inventory>().openInventory = "";
        GameObject furnace = GameObject.Find("Furnace");
        furnace.GetComponent<Furnace>().furnacegui.SetActive(false);
    }
}