using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class Shopslot : MonoBehaviour, IPointerClickHandler
{
    public ShopData_Buy buyshop;
    public void OnPointerClick(PointerEventData eventData){
        buyItem();
    }

    public void buyItem(){
        GameObject player = GameObject.Find("Player");
        if ( player.GetComponent<Inventory>().CheckItemAmount(buyshop.price.type) >= buyshop.price.amount ){
            if ( player.GetComponent<Inventory>().CheckGetItem(buyshop.itemData) == true ){
                if(buyshop.itemData.hasInventory == true){
                    Potinventory potinv = new Potinventory();
                    potinv.Awake();
                    player.GetComponent<Inventory>().GetItem(buyshop.itemData, potinv);
                }
                else{
                    player.GetComponent<Inventory>().GetItem(buyshop.itemData);
                }
                player.GetComponent<Inventory>().RemoveItemAmount(buyshop.price.type, buyshop.price.amount);
            }
        }
    }
}