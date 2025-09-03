using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shopclosebutton : MonoBehaviour, IPointerClickHandler
{
    public GameObject shopper;
    public GameObject player;
    public void Start(){
        player = GameObject.Find("Player");
    }
    public void OnPointerClick(PointerEventData eventData){
        closeInventory();
    }
    public void closeInventory(){
        GameObject shopGUI = GameObject.Find("GUI").transform.Find("shop_main").gameObject;
        shopGUI.SetActive(false);
    }
    public void Update(){
        float distance = Vector3.Distance(shopper.transform.position, player.transform.position);
        if(distance >= 2.0f){
                closeInventory();
            }
    }
}