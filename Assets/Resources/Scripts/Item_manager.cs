using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

public class Item_manager : MonoBehaviour
{
    public List<ItemList> ItemList = new List<ItemList>();
    public void setIteminit(GameObject item, ItemData data){
        item.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(data.spritePath);
        item.AddComponent<BoxCollider2D>();
        item.GetComponent<BoxCollider2D>().isTrigger = true;
        data.itemIcon = Resources.Load<Sprite>(data.spritePath);
        item.GetComponent<Item>().itemData = data;
    }
    public void dropItem(string name, Vector3 position, int amount = 1, ItemData copydata = null){
        ItemData data = new ItemData();
        data = loadItemData(name,data);
        GameObject dropitemPrefab = Resources.Load("Prefabs/dropitemPrefab") as GameObject;
        GameObject dropitemlist = GameObject.Find("DropItem Object");
        GameObject item = Instantiate(dropitemPrefab, dropitemlist.transform, false);
        item.transform.position = position;
        item.name = "droped item: " + name;
        item.tag = "pickup_item";
        setIteminit(item, data);
        item.GetComponent<Item>().itemData.amount = amount;
        if(copydata != null){
            item.GetComponent<Item>().itemData = copydata;
        }
    }

    public void throwItemPlayer(string name, Vector3 position, int amount = 1, ItemData copydata = null,int slot = 0){
        ItemData data = new ItemData();
        data = loadItemData(name,data);
        GameObject dropitemPrefab = Resources.Load("Prefabs/dropitemPrefab") as GameObject;
        GameObject dropitemlist = GameObject.Find("DropItem Object");
        GameObject item = Instantiate(dropitemPrefab, dropitemlist.transform, false);
        item.transform.position = position;
        string loopname = "droped item: " + name;
        int i = 0;
        while(GameObject.Find(loopname) != null){
            loopname = "droped item: " + name + " ("+ i + ")";
            i += 1;
        }
        item.name = loopname;
        setIteminit(item, data);
        item.GetComponent<Item>().itemData.amount = amount;
        if(copydata != null){
            item.GetComponent<Item>().itemData = copydata;
        }
        if(item.GetComponent<Item>().itemData.hasInventory == true) {
            item.AddComponent<Potinventory>();
            GameObject player = GameObject.Find("Player");
            item.GetComponent<Potinventory>().slots = player.GetComponent<Inventory>().slots[slot].potInventory.slots;
            item.tag = "potitem";
        }
        StartCoroutine(throwAnimation(item));

    }


    public GameObject throwItemPlayerObj(string name, Vector3 position, int amount = 1, ItemData copydata = null,int slot = 0){
        ItemData data = new ItemData();
        data = loadItemData(name,data);
        GameObject dropitemPrefab = Resources.Load("Prefabs/dropitemPrefab") as GameObject;
        GameObject dropitemlist = GameObject.Find("DropItem Object");
        GameObject item = Instantiate(dropitemPrefab, dropitemlist.transform, false);
        item.transform.position = position;
        string loopname = "droped item: " + name;
        int i = 0;
        while(GameObject.Find(loopname) != null){
            loopname = "droped item: " + name + " ("+ i + ")";
            i += 1;
        }
        item.name = loopname;
        setIteminit(item, data);
        item.GetComponent<Item>().itemData.amount = amount;
        if(copydata != null){
            item.GetComponent<Item>().itemData = copydata;
        }
        if(item.GetComponent<Item>().itemData.hasInventory == true) {
            item.AddComponent<Potinventory>();
            GameObject player = GameObject.Find("Player");
            item.GetComponent<Potinventory>().slots = player.GetComponent<Inventory>().slots[slot].potInventory.slots;
            item.tag = "potitem";
        }
        StartCoroutine(throwAnimation(item));
        return item;

    }

    IEnumerator throwAnimation(GameObject item){
        float y = item.transform.position.y;
        float z = item.transform.position.z;
        float facing;
        int count = Random.Range(0, 2);
        string temp = (string)item.tag;
        GameObject player = GameObject.Find("Player");

        facing = player.GetComponent<PlayerController>().facing;

        Physics2D.IgnoreLayerCollision(11, 14, true);
        item.GetComponent<BoxCollider2D>().isTrigger = false;
        for(int i = 0; i<10 ; i++){
            if(item == null){
                break;
            }
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            Vector2 position = new Vector2(item.transform.position.x + 0.1f * facing, rb.position.y);
            rb.MovePosition(position);
            //item.transform.position = new Vector3(item.transform.position.x + 0.2f*facing , y, z);
            yield return new WaitForSeconds(0.0001f);
        }
        item.GetComponent<BoxCollider2D>().isTrigger = true;
        Physics2D.IgnoreLayerCollision(11, 14, false);
        item.tag = temp;
    }



    public void dropItemAni(string name, Vector3 position, int amount = 1, ItemData copydata = null, int facing = 0){
        ItemData data = new ItemData();
        data = loadItemData(name,data);
        GameObject dropitemPrefab = Resources.Load("Prefabs/dropitemPrefab") as GameObject;
        GameObject dropitemlist = GameObject.Find("DropItem Object");
        GameObject item = Instantiate(dropitemPrefab, dropitemlist.transform, false);
        item.transform.position = position;
        item.name = "droped item: " + name;
        setIteminit(item, data);
        item.GetComponent<Item>().itemData.amount = amount;
        if(copydata != null){
            item.GetComponent<Item>().itemData = copydata;
        }
        if(facing == 0){
            int count = Random.Range(0, 2);
            if(count == 0){
                facing = 1;
            }
            else{
                facing = -1;
            }
        }
        StartCoroutine(dropAnimation(item, facing));

    }

    IEnumerator dropAnimation(GameObject item, int facing){
        float y = item.transform.position.y;
        float z = item.transform.position.z;
        item.tag = "pickup_item";
        Physics2D.IgnoreLayerCollision(11, 14, true);
        item.GetComponent<BoxCollider2D>().isTrigger = false;

        for(int i = 0; i<10 ; i++){
            if(item == null){
                break;
            }
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            Vector2 position = new Vector2(rb.position.x + 0.1f * facing, rb.position.y);
            rb.MovePosition(position);
            //item.transform.position = new Vector3(item.transform.position.x - 0.05f*facing , y, z);
            yield return new WaitForSeconds(0.025f);
        }
        item.GetComponent<BoxCollider2D>().isTrigger = true;
        Physics2D.IgnoreLayerCollision(11, 14, false);
    }
    public ItemData loadItemData(string name, ItemData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/Items/" + name);
            data = JsonUtility.FromJson<ItemData>(textData.ToString());
            data.itemIcon =  Resources.Load<Sprite>(data.spritePath);
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/Resources/Json/Items/" + name + ".json");
            if (System.IO.File.Exists(path)!=true){
                File.WriteAllText(path, "{}");
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<ItemData>(jsonData);
            data.itemIcon =  Resources.Load<Sprite>(data.spritePath);
            return data;
        }
    }
    public void makeItemData(string file, string name, int quantity, int durability, int maxdurability, bool stackable, int amount, int itemtype){

        ItemData make_data = new ItemData();
        make_data.itemName = name;
        make_data.objectName = name;
        make_data.itemIcon = Resources.Load<Sprite>("Item/"+file);
        make_data.quantity = quantity;
        make_data.durability = durability;
        make_data.maxdurability = maxdurability;
        make_data.stackable = stackable;
        make_data.amount = amount;
        make_data.itemType = (ItemData.ItemType)itemtype;
        make_data.spritePath = "Item/"+file;

        string path = Path.Combine(Application.dataPath + "/Resources/Json/Items/" + name + ".json");
        string json = JsonUtility.ToJson(make_data, true);
        File.WriteAllText(path, json);

        
    }
}

public class ItemList{
    string name;
}