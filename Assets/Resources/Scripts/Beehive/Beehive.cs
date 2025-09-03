using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Beehive : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public List<beehiveSlotData> slots = new List<beehiveSlotData>();

    public float honeyProgress = 0f;

    public GameObject beehivegui;

    void Start(){
        beehivegui = GameObject.Find("GUI").transform.Find("GUI_beehive").gameObject;
        beehivegui.SetActive(false);

        for(int i = 0; i<2; i++){
            beehiveSlotData slot = new beehiveSlotData();
            slot.isEmpty = true;
            slot.itemData = null;
            slots.Add(slot);
        }
    }

    public void slotClear(int i){
        string slotname = "beehiveslot_" + i;
        GameObject slot = beehivegui.transform.Find(slotname).gameObject;
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
            player.GetComponent<Inventory>().openInventory = "GUI_Beehive";
            beehivegui.SetActive(true);

            
        }
    }

    public void UpdatehoneyProgress(){
        GameObject beehivehoneybar = beehivegui.transform.Find("beehivehoneybar").gameObject;
        beehivehoneybar.GetComponent<Image>().fillAmount = honeyProgress / 100;
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
            honeyProgress = 0f;
            return;
        }
        else if(!slots[0].itemData.itemName.Contains("queenbee")){
            honeyProgress = 0f;
            return;
        }
        if (honeyProgress <= 100f){
            honeyProgress += 5f * Time.deltaTime;
        }
        if (honeyProgress >= 100f){
            honeyProgress = 0f;
            slots[0].itemData.durability -= 1;
            if(slots[1].isEmpty == false){
                if(slots[1].itemData.itemName.Contains("honey")){
                    slots[1].itemData.amount +=1;
                }
            }
            else{
                Item_manager im = new Item_manager();
                ItemData data = new ItemData();
                data = im.loadItemData("honey", data);

                slots[1].itemData = data;

                GameObject player = GameObject.Find("Player");
                GameObject slotitemPrefab = Resources.Load<GameObject>("Prefabs/slotitemPrefab");

                GameObject slotObj = beehivegui.transform.Find("beehiveslot_1").gameObject;
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

                Beehive otherinv = GameObject.Find("Beehive").GetComponent<Beehive>();
            }
        }
    }
    
    public void Updatequeenbee(){
        int i = 0;
        if(slots[i].isEmpty == true){
            return;
        }
        if((slots[i].itemData.durability <= 0)&& (slots[i].itemData.maxdurability >= 0)){
            if(slots[i].itemData.amount > 1){
                slots[i].itemData.durability = slots[i].itemData.maxdurability;
                slots[i].itemData.amount -= 1;
            }
            else{
                slots[i].isEmpty = true;
                slots[i].item = null;
                slots[i].itemData = null;
                GameObject iteminslot = beehivegui.transform.Find("beehiveslot_0").gameObject;
                iteminslot = iteminslot.transform.GetChild(0).gameObject;
                Destroy(iteminslot);
            }
        }
    }
    void Update(){
        UpdatehoneyProgress();
        Updatequeenbee();
        
        GameObject BeehiveObj = GameObject.Find("Beehive");
        GameObject player = GameObject.Find("Player");

        if(player.GetComponent<Inventory>().openInventory =="GUI_Beehive"){
            float distance = Vector3.Distance(BeehiveObj.transform.position, player.transform.position);
            if(distance >= 2.0f){
                beehivegui.transform.Find("beehiveexit").gameObject.GetComponent<Beehiveclosebutton>().closeInventory();
            }
        }

        
    }

    
}


[System.Serializable]
public class beehiveSlotData{
    public bool isEmpty;
    public GameObject item;
    public ItemData itemData;
}