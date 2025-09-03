using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Miller : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public List<millerSlotData> slots = new List<millerSlotData>();

    public float millingProgress = 0f;

    public GameObject millergui;

    void Start(){
        millergui = GameObject.Find("GUI").transform.Find("GUI_miller").gameObject;
        millergui.SetActive(false);

        for(int i = 0; i<2; i++){
            millerSlotData slot = new millerSlotData();
            slot.isEmpty = true;
            slot.itemData = null;
            slots.Add(slot);
        }
    }

    public void slotClear(int i){
        string slotname = "millerslot_" + i;
        GameObject slot = millergui.transform.Find(slotname).gameObject;
        Destroy(slot.transform.GetChild(0).gameObject);
        slots[i].isEmpty = true;
        slots[i].itemData = null;
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
            player.GetComponent<Inventory>().openInventory = "GUI_Miller";
            millergui.SetActive(true);

            
        }
    }
    public bool CheckCrops(string name){
        if( (name == "cabbage") || (name == "carrot") || (name == "potato") || (name == "wheat") || (name =="onion") || (name =="garlic") || (name =="redpepper") || (name =="tomato") ){
            return true;
        }
        return false;
    }

    public void UpdatemillingProgress(){
        GameObject millerbar = millergui.transform.Find("millerbar").gameObject;
        millerbar.GetComponent<Image>().fillAmount = millingProgress / 100;
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
        if(slots[0].isEmpty == true){
            millingProgress = 0f;
            return;
        }
        else if(!CheckCrops(slots[0].itemData.itemName)){
            millingProgress = 0f;
            return;
        }
        if (millingProgress <= 100f){
            millingProgress += 9f * Time.deltaTime;
        }
        if (millingProgress >= 100f){
            millingProgress = 0f;
            slots[0].itemData.amount -= 1;
            if(slots[1].isEmpty == false){
                if(slots[1].itemData.itemName.Contains("flour")){
                    slots[1].itemData.amount +=1;
                }
            }
            else{
                Item_manager im = new Item_manager();
                ItemData data = new ItemData();
                data = im.loadItemData("flour", data);

                slots[1].itemData = data;

                GameObject player = GameObject.Find("Player");
                GameObject slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");

                GameObject slotObj = millergui.transform.Find("millerslot_1").gameObject;
                GameObject slotItem = Instantiate(slotitemPrefab, slotObj.transform, false);

                

                slotItem.name = slots[1].itemData.itemName;
                slotItem.GetComponent<Image>().sprite = Resources.Load<Sprite>(slots[1].itemData.spritePath);;
                slotItem.transform.SetAsFirstSibling();
                slotItem.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);

                GameObject go_amount = Instantiate(player.GetComponent<Inventory>().slotamountPrefab, slotItem.transform, false);
                go_amount.name = "slotamount";

                GameObject go_durability = Instantiate(player.GetComponent<Inventory>().slotdurabilityPrefab, slotItem.transform, false);
                go_durability.name = "slotdurability";
                if(slots[1].itemData.durability == -1){
                    go_durability.SetActive(false);
                }
                slots[1].item = slotItem;
                slots[1].isEmpty = false;
                slots[1].itemData = data;

                Miller otherinv = GameObject.Find("Windmill").GetComponent<Miller>();
            }
        }
    }
    
    public void Updatecrops(){
        int i = 0;
        if(slots[i].isEmpty == true){
            return;
        }
        if(slots[i].itemData.amount < 1){
            slots[i].isEmpty = true;
            slots[i].item = null;
            slots[i].itemData = null;
            GameObject iteminslot = millergui.transform.Find("millerslot_0").gameObject;
            iteminslot = iteminslot.transform.GetChild(0).gameObject;
            Destroy(iteminslot);
            }
        }

    public void Updatewindmill(){
        if(millingProgress < 1){
            return;
        }
        GameObject millerObj = GameObject.Find("Windmill");
        GameObject windmill = millerObj.transform.Find("windmill_2").gameObject;
        windmill.transform.Rotate(0.0f, 0.0f, 100f*Time.deltaTime, Space.Self);
    }
    void Update(){
        UpdatemillingProgress();
        Updatecrops();
        Updatewindmill();
        
        GameObject millerObj = GameObject.Find("Windmill");
        GameObject player = GameObject.Find("Player");

        if(player.GetComponent<Inventory>().openInventory =="GUI_Miller"){
            float distance = Vector3.Distance(millerObj.transform.position, player.transform.position);
            if(distance >= 2.0f){
                millergui.transform.Find("millerexit").gameObject.GetComponent<Millerclosebutton>().closeInventory();
            }
        }

        
    }
}


[System.Serializable]
public class millerSlotData{
    public bool isEmpty;
    public GameObject item;
    public ItemData itemData;
}