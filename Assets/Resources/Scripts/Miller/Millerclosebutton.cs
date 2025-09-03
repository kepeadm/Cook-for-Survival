using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Millerclosebutton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData){
        closeInventory();
    }
    public void closeInventory(){
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Inventory>().openInventory = "";
        GameObject miller = GameObject.Find("Windmill");
        miller.GetComponent<Miller>().millergui.SetActive(false);
    }
}