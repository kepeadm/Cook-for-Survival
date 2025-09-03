using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Furnace : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public List<furnaceSlotData> slots = new List<furnaceSlotData>();

    public float fireProgress = 0f;

    public GameObject furnacegui;

    public Sprite furnace_on;
    public Sprite furnace_off;

    void Start(){
        furnacegui = GameObject.Find("GUI").transform.Find("GUI_furnace").gameObject;
        furnacegui.SetActive(false);

        for(int i = 0; i<2; i++){
            furnaceSlotData slot = new furnaceSlotData();
            slot.isEmpty = true;
            slot.itemData = null;
            slot.potInventory = null;
            slot.cookProgress = 0f;
            slot.maxcookProgress = -1f;
            slot.level = 0;
            slots.Add(slot);
        }
    }

    public void slotClear(int i){
        string slotname = "furnaceslot_" + i;
        GameObject slot = furnacegui.transform.Find(slotname).gameObject;
        foreach(Transform child in slot.transform){
            Destroy(child.gameObject);
        }
        slots[i].isEmpty = true;
        slots[i].itemData = null;
        slots[i].potInventory = null;
        slots[i].cookProgress = 0f;
        slots[i].maxcookProgress = 0f;
        slots[i].level = 0;
    }
    
    public void OnPointerClick(PointerEventData eventData){
    }
    public void OnPointerExit(PointerEventData eventData){
    }
    public void OnPointerEnter(PointerEventData eventData){
    }

    public void openGUI(){
        GameObject player = GameObject.Find("Player");
        if(player.GetComponent<Inventory>().openInventory ==""){
            player.GetComponent<Inventory>().openInventory = "GUI_Furnace";
            furnacegui.SetActive(true);

            
        }
    }

    void setSmoke(){
        if( (slots[0].level == 0) && (slots[0].isEmpty == false) ){
            if(GameObject.Find("Furnace_smoke0") == null){
                GameObject.Find("Furnace").transform.Find("Furnace_smoke0").gameObject.SetActive(true);
            }
        }
        else{
            if(GameObject.Find("Furnace_smoke0") != null){
                GameObject.Find("Furnace_smoke0").SetActive(false);
            }
        }

        if( (slots[0].level == 1) && (slots[0].isEmpty == false) ){
            if(GameObject.Find("Furnace_smoke1") == null){
                GameObject.Find("Furnace").transform.Find("Furnace_smoke1").gameObject.SetActive(true);
            }
        }
        else{
            if(GameObject.Find("Furnace_smoke1") != null){
                GameObject.Find("Furnace_smoke1").SetActive(false);
            }
        }

        if( (slots[0].level == 2) && (slots[0].isEmpty == false) ){
            if(GameObject.Find("Furnace_smoke2") == null){
                GameObject.Find("Furnace").transform.Find("Furnace_smoke2").gameObject.SetActive(true);
            }
        }
        else{
            if(GameObject.Find("Furnace_smoke2") != null){
                GameObject.Find("Furnace_smoke2").SetActive(false);
            }
        }
    }

    void Update(){
        if (fireProgress > 0f){
            if(this.GetComponent<SpriteRenderer>().sprite != furnace_on){
                this.GetComponent<SpriteRenderer>().sprite = furnace_on;
            }
            fireProgress -= 1f * Time.deltaTime;
            if( (slots[0].isEmpty == false) && (slots[0].level < 2) ){
                slots[0].cookProgress +=3f * Time.deltaTime;
                float maxprogress = slots[0].maxcookProgress;
                if((slots[0].cookProgress >= maxprogress ) && (slots[0].level == 0) ){
                    furnaceSlotData cookslot = slots[0];
                    slots[0].level = 1;
                    this.gameObject.GetComponent<Cook>().setCook(cookslot);
                }
                else if( ( slots[0].cookProgress >= (maxprogress * 2) ) && (slots[0].level == 1) ){
                    furnaceSlotData cookslot = slots[0];
                    slots[0].level = 2;
                    this.gameObject.GetComponent<Cook>().setCook(cookslot);
                }
            }
            setSmoke();
        }
        if (fireProgress <= 0f){
            if(this.GetComponent<SpriteRenderer>().sprite != furnace_off){
                this.GetComponent<SpriteRenderer>().sprite = furnace_off;
            }
            int i = 1;
            if(slots[i].isEmpty == false){
                if (slots[i].itemData.itemName.Contains("wood")){
                        
                    fireProgress += 100f;
                    slots[i].itemData.amount -=1;
                    if(slots[i].itemData.amount <= 0){
                        slotClear(i);
                    }
                }
            }
        }
        GameObject furnacefirebar = furnacegui.transform.Find("furnacefirebar").gameObject;
        furnacefirebar.GetComponent<Image>().fillAmount = fireProgress / 100;
        for(int i=0; i< 2; i++){
            if(slots[i].isEmpty==false){
                GameObject slotamountObj = slots[i].item.transform.Find("slotamount").gameObject;
                GameObject slotdurabilityObj = slots[i].item.transform.Find("slotdurability").Find("bar").gameObject;
                if(slots[i].itemData.amount>=2) {
                    slotamountObj.GetComponent<TextMeshProUGUI>().text = "" + slots[i].itemData.amount;
                    slotamountObj.transform.SetAsLastSibling();
                }
                else if(slots[i].itemData.amount<2) {
                    slotamountObj.GetComponent<TextMeshProUGUI>().text = "";
                    slotamountObj.transform.SetAsLastSibling();
                }
                if((slots[i].itemData.durability >= 0)&& (slots[i].itemData.maxdurability >= 0)){
                    float dura = slots[i].itemData.durability;
                    float maxdura = slots[i].itemData.maxdurability;
                    float percent = dura / maxdura;
                    slotdurabilityObj.GetComponent<Image>().fillAmount = percent;
                }
            }
        }

        GameObject FurnaceObj = GameObject.Find("Furnace");
        GameObject player = GameObject.Find("Player");

        if(player.GetComponent<Inventory>().openInventory =="GUI_Furnace"){
            float distance = Vector3.Distance(FurnaceObj.transform.position, player.transform.position);
            if(distance >= 2.5f){
                furnacegui.transform.Find("furnaceexit").gameObject.GetComponent<Furnaceclosebutton>().closeInventory();
            }
        }

        
    }

    
}


[System.Serializable]
public class furnaceSlotData{
    public bool isEmpty;
    public GameObject item;
    public ItemData itemData;
    public Potinventory potInventory = new Potinventory();
    public float cookProgress;
    public int level;
    public float maxcookProgress;
}