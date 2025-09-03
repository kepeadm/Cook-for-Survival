using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Beehiveclosebutton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData){
        closeInventory();
    }
    public void closeInventory(){
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Inventory>().openInventory = "";
        GameObject beehive = GameObject.Find("Beehive");
        beehive.GetComponent<Beehive>().beehivegui.SetActive(false);
    }
}