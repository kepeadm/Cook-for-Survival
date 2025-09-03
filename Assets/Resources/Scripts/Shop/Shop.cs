using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class Shop : MonoBehaviour
{
    public ShopData data = new ShopData();
    public GameObject shopGUI;

    public void Start(){
        shopGUI = GameObject.Find("GUI").transform.Find("shop_main").gameObject;
    }
    
    public ShopData loadJson(string name, ShopData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/Shops/" + name);
            data = JsonUtility.FromJson<ShopData>(textData.ToString());
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/Resources/Json/Shops/" + name + ".json");
            if (System.IO.File.Exists(path)!=true){
                File.WriteAllText(path, "{}");
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<ShopData>(jsonData);
            return data;
        }
    }

    public void openShop(){
        data = loadJson(data.name, data);
        shopGUI.SetActive(true);
        GameObject.Find("shop_exit").gameObject.GetComponent<Shopclosebutton>().shopper = this.gameObject;
        GameObject shop_rect = GameObject.Find("shop_rect").gameObject;
        GameObject shop_panel = GameObject.Find("shop_pan").gameObject;
        if (shop_panel.transform.childCount >= 1){
            foreach(Transform shopslot in shop_panel.gameObject.transform){
                Destroy(shopslot.gameObject);
            }
        }
        Sprite ruby = Resources.Load<Sprite>("Item/ruby");
        Sprite emerald = Resources.Load<Sprite>("Item/emerald");
        Sprite saphire = Resources.Load<Sprite>("Item/saphire");
        Sprite opal = Resources.Load<Sprite>("Item/opal");
        Sprite gold = Resources.Load<Sprite>("Item/gold");

        
        if(data.buyshop.Count >= 1){
            int slot = 0;
            GameObject shopslotPrefab = Resources.Load<GameObject>("Prefabs/shopslotprefab");
            foreach(ShopData_Buy buyshop in data.buyshop){
                GameObject slotObj = Instantiate(shopslotPrefab, shop_panel.transform, false);

                slotObj.GetComponent<Shopslot>().buyshop = buyshop;
                
                slotObj.name = "shopslot_" + slot;

                buyshop.itemData.itemIcon = Resources.Load<Sprite>(buyshop.itemData.spritePath);
                GameObject shopslotitem = slotObj.transform.Find("shopitemslot").Find("shopslotitem").gameObject;
                GameObject itemname = slotObj.transform.Find("itemname").gameObject;
                GameObject money = slotObj.transform.Find("itemprice").Find("money").gameObject;
                GameObject amount = slotObj.transform.Find("itemprice").Find("amount").gameObject;
                shopslotitem.GetComponent<Image>().sprite = Resources.Load<Sprite>(buyshop.itemData.spritePath);
                itemname.GetComponent<TextMeshProUGUI>().text = buyshop.itemData.itemName;
                if( buyshop.price.type == "ruby" ){
                    money.GetComponent<Image>().sprite = ruby;
                }
                else if( buyshop.price.type == "gold" ){
                    money.GetComponent<Image>().sprite = gold;
                }
                else if( buyshop.price.type == "saphire" ){
                    money.GetComponent<Image>().sprite = saphire;
                }
                else if( buyshop.price.type == "emerald" ){
                    money.GetComponent<Image>().sprite = emerald;
                }
                else if( buyshop.price.type == "opal" ){
                    money.GetComponent<Image>().sprite = opal;
                }
                amount.GetComponent<TextMeshProUGUI>().text = "" + buyshop.price.amount;

                slot +=1;

            }
        }
    }

}

[System.Serializable]
public class ShopData{
    public string name;
    public List<ShopData_Buy> buyshop = new List<ShopData_Buy>();

}

[System.Serializable]
public class ShopData_Buy{
    public ItemData itemData;
    public ShopData_Money price = new ShopData_Money();
}

[System.Serializable]
public class ShopData_Money{
    public string type;
    public int amount;
}