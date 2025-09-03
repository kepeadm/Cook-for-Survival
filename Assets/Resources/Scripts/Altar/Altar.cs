using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class Altar : MonoBehaviour
{
    public AltarSlotData slot = new AltarSlotData();
    public string desire;
    public GameObject altargui;
    public GameObject guislot;
    public string preFood;
    public float twitTime = 0f;

    void Start(){
        altargui = GameObject.Find("GUI").transform.Find("GUI_altar").gameObject;
        guislot = altargui.transform.Find("altarslot_0").gameObject;
        altargui.SetActive(false);
        
        slot.isEmpty = true;
        slot.itemData = null;
        slot.item = null;
    }

    public void openGUI(){
        GameObject player = GameObject.Find("Player");
        if(player.GetComponent<Inventory>().openInventory ==""){
            player.GetComponent<Inventory>().openInventory = "GUI_Altar";
            altargui.SetActive(true);
        }
    }
    public void dedicateFood(){
        Cook cook = GameObject.Find("Furnace").GetComponent<Cook>();
        Item_manager im = new Item_manager();
        List<Money> price = new List<Money>();
        price = cook.getFoodMoneyType(slot.itemData.itemName, desire);
        if(slot.itemData.itemName == "escape_portal"){
            //클리어..
            GameObject portal = GameObject.Find("Interactive Object").transform.Find("Escape Portal").gameObject;
            portal.SetActive(true);

            Texttips texttip = GameObject.Find("TextTips").GetComponent<Texttips>();
            texttip.openobjectTip("Fanatic", "세상에.. 신이 무너졌다.. 균열이 열렸어..");
            
        }
        else if(price == null){
            Texttips texttip = GameObject.Find("TextTips").GetComponent<Texttips>();
            texttip.openobjectTip("Fanatic", "그는 요리가 아니면 먹지 않는다.");
        }
        else{
            Vector3 position = this.gameObject.transform.position;
            position.y = position.y - 1.5f;
            foreach(Money money in price){
                im.dropItem(money.type, position, money.amount);
            }
            preFood = (string) slot.itemData.itemName;
            slot.isEmpty = true;
            slot.itemData = null;
            slot.item = null;
            GameObject iteminslot = guislot.transform.GetChild(0).gameObject;
            Destroy(iteminslot);
            Gamemanager gm = GameObject.Find("GameManager").GetComponent<Gamemanager>();
            newDesire(gm.game_time);
        }
    }
    public void newDesire(float gametime){
        int time = (int)gametime;
        List<string> Desires = new List<string>();
        Desires.Add("채식");
        Desires.Add("면");
        Desires.Add("육식");
        Desires.Add("생선");
        Desires.Add("빵");
        Desires.Add("에피타이저");
        Desires.Add("디저트");
        Desires.Add("발효");
        desire = Desires[time%8];
        Texttips texttip = GameObject.Find("TextTips").GetComponent<Texttips>();
        string text = null;
        text = "그가 " + desire + "종류의 음식을 대단히 갈망하고 있어.";
        texttip.openobjectTip("Fanatic", text);
    }
    public void randomtwit(){
        int count = Random.Range(0, 4);
        Texttips texttip = GameObject.Find("TextTips").GetComponent<Texttips>();
        string text = null;
        if(count == 0){
            text = "아직도 " + desire + "를 준비 안 했어? 너무 느리잖아!";
            texttip.openobjectTip("Fanatic", text);
        }
        else if(count == 1){
            text = "손이 아주 느리구나.." + desire + "대신에 너를 제단에 올려야겠어.";
            texttip.openobjectTip("Fanatic", text);
        }
        else if(count == 2){
            text = "까먹었을 까봐 알려주는건데 위대하신 그 분은 " + desire + "을 원하고 있어. 난 정말 친절하다니까.";
            texttip.openobjectTip("Fanatic", text);
        }
        else if(count == 3){
            text = "친절히 알려주는건 이번까지만이야." + desire + "을 어서 내와.";
            texttip.openobjectTip("Fanatic", text);
        }
        else if(count == 4){
            text = "이 정도로 늦게 나오다니. 아주 맛있는 " + desire + "을 내오지 않기만 해 봐.";
            texttip.openobjectTip("Fanatic", text);
        }
    }
    void Update(){
        GameObject altarObj = GameObject.Find("Altar");
        GameObject player = GameObject.Find("Player");
        twitTime = twitTime + Time.deltaTime;
        if(twitTime >= 60f){
            twitTime = twitTime - 60f;
            randomtwit();
        }

        if(slot.isEmpty==false){
            GameObject slotamountObj = slot.item.transform.Find("slotamount").gameObject;
            GameObject slotdurabilityObj = slot.item.transform.Find("slotdurability").Find("bar").gameObject;
            if((slot.itemData.durability >= 0)&& (slot.itemData.maxdurability >= 0)){
                float dura = slot.itemData.durability;
                float maxdura = slot.itemData.maxdurability;
                float percent = dura / maxdura;
                slotdurabilityObj.GetComponent<Image>().fillAmount = percent;
            }
        }

        if(player.GetComponent<Inventory>().openInventory =="GUI_Altar"){
            float distance = Vector3.Distance(altarObj.transform.position, player.transform.position);
            if(distance >= 3.0f){
                altargui.transform.Find("altarexit").gameObject.GetComponent<Altarclosebutton>().closeInventory();
            }
        }
    }
}

[System.Serializable]
public class AltarSlotData{
    public bool isEmpty;
    public GameObject item;
    public ItemData itemData;
}