using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Altarclosebutton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData){
        closeInventory();
    }
    public void closeInventory(){
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Inventory>().openInventory = "";
        GameObject altar = GameObject.Find("Altar");
        altar.GetComponent<Altar>().altargui.SetActive(false);
        if(altar.GetComponent<Altar>().slot.isEmpty == false ){
            altar.GetComponent<Altar>().dedicateFood();
        }
    }
}