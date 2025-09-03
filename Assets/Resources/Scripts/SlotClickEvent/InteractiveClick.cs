using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractiveClick : MonoBehaviour, IPointerClickHandler
{
    public string clickType;
    public GameObject player;
    public GameObject playerselectbox;
    public GameObject colliding;
    
    public void Loading(){
        player = GameObject.Find("Player");
        playerselectbox = GameObject.Find("playerselectbox");
        if(playerselectbox.GetComponent<SelectEvent>().colliding != null){
            colliding = playerselectbox.GetComponent<SelectEvent>().colliding;
        }
    }
    public void OnPointerClick(PointerEventData eventData){
        if (clickType == "open"){
            if(colliding.tag == "potitem"){
                colliding.GetComponent<Potinventory>().openInventory();
            }
            else if(colliding.tag == "furnace"){
                GameObject furnace = GameObject.Find("Furnace");
                furnace.GetComponent<Furnace>().openGUI();
            }
            else if(colliding.tag == "beehive"){
                GameObject beehive = GameObject.Find("Beehive");
                beehive.GetComponent<Beehive>().openGUI();
            }
            else if(colliding.tag == "miller"){
                GameObject miller = GameObject.Find("Windmill");
                miller.GetComponent<Miller>().openGUI();
            }
            else if(colliding.tag == "altar"){
                GameObject altar = GameObject.Find("Altar");
                altar.GetComponent<Altar>().openGUI();
            }
            else if(colliding.tag == "portal"){
                GameObject portal = GameObject.Find("Escape Portal");
                portal.GetComponent<EscapePortal>().GameClear();
            }
            else if(colliding.tag == "shop"){
                colliding.GetComponent<Shop>().openShop();
            }
            else if(colliding.tag == "door"){
                if(colliding.transform.parent.gameObject.GetComponent<Door>().open == false){
                    if(player.GetComponent<Inventory>().toolData != null){
                        colliding.transform.parent.gameObject.GetComponent<Door>().openDoor(player.GetComponent<Inventory>().toolData);
                    }
                }
            }
        }
        else if (clickType == "pickup"){
            colliding = playerselectbox.GetComponent<SelectEvent>().colliding;
            if(colliding.tag == "garden"){
                colliding.GetComponent<Garden>().getCrop(colliding.GetComponent<Garden>().crop.name);
            }
            else if (colliding.tag == "potitem"){
                colliding.GetComponent<Item>().PickUp(player);
            }
            else{
                colliding.GetComponent<Item>().PickUp(player);
            }
        }
        else if (clickType == "plant"){
            colliding = playerselectbox.GetComponent<SelectEvent>().colliding;
            if(player.GetComponent<Inventory>().toolData != null){
                string toolName = player.GetComponent<Inventory>().toolData.itemName;

                if( toolName.Contains("seed_") ){
                    string splitName = ""+ toolName;
                    string[] splitter = splitName.Split('_');
                    toolName = splitter[1];
                    colliding.GetComponent<Garden>().planting(toolName);

                    player.GetComponent<Inventory>().toolData.amount -=1;
                    
                    playerselectbox.GetComponent<SelectEvent>().closeMenu();
                }


            }
        }
    }
    
}