using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class Texttips : MonoBehaviour
{
    public GameObject inventoryTip;
    public GameObject selectTip;
    public List<ObjectTip> objecttip = new List<ObjectTip>();
    public GameObject objectTipPrefab;
    
    public float inventoryTime = 0f;
    public float selectTime = 0f;
    void Awake(){
        inventoryTip = this.transform.Find("inventorytip").gameObject;
        selectTip = this.transform.Find("selecttip").gameObject;
        objectTipPrefab = Resources.Load<GameObject>("Prefabs/ObjectTexttipPrefab");
    }
    public void openinventoryTip(string text){
        inventoryTime = 1f;
        inventoryTip.GetComponent<TextMeshProUGUI>().text = text;
    }
    public void openselectTip(string text){
        selectTime = 1f;
        selectTip.GetComponent<TextMeshProUGUI>().text = text;
    }
    public bool objectTipContains(string name){
        foreach(ObjectTip obj in objecttip){
            if (obj.name == name){
                return true;
            }
        }
        return false;
    }
    public int objectTipIndex(string name){
        for(int i = 0; i < objecttip.Count; i++){
            if(objecttip[i].name == name){
                return i;
            }
        }
        return 0;
    }
    public void openobjectTip(string name, string text, float time = 5f){
        if(objectTipContains(name)){
            objecttip[objectTipIndex(name)].time = time;
            GameObject texttip = GameObject.Find(name).transform.Find("objecttexttip").gameObject;
            texttip.GetComponent<TextMeshPro>().text = text;
            return;
        }
        else{
            GameObject obj = GameObject.Find(name);
            GameObject texttip = (GameObject) Instantiate(objectTipPrefab, obj.transform, false);
            texttip.name = "objecttexttip";
            texttip.GetComponent<TextMeshPro>().text = text;
            ObjectTip objtip = new ObjectTip();
            objtip.name = name;
            objtip.time = 3f;
            objtip.obj = texttip;
            objecttip.Add(objtip);
        }
    }
    void Update(){
        if(inventoryTime > 0f){
            inventoryTime -= 1f * Time.deltaTime;
        }
        else if(inventoryTime <= 0f){
            inventoryTip.GetComponent<TextMeshProUGUI>().text = "";
        }
        if(selectTime > 0f){
            selectTime -= 1f * Time.deltaTime;
        }
        else if(selectTime <= 0f){
            selectTip.GetComponent<TextMeshProUGUI>().text = "";
        }
        int i = 0;
        while(i<objecttip.Count){
            if(objecttip[i].time > 0f){
                objecttip[i].time -= 1f * Time.deltaTime;
            }
            else{
                if(objecttip[i].obj == null){
                    objecttip.RemoveAt(i);
                    i += 1;
                    continue;
                }
                else if(GameObject.Find(objecttip[i].name) == null){
                    objecttip.RemoveAt(i);
                    i += 1;
                    continue;
                }
                Destroy(objecttip[i].obj);
                objecttip.RemoveAt(i);
            }
            i += 1;
        }
    }
}


[System.Serializable]
public class ObjectTip{
    public string name;
    public GameObject obj;
    public float time;
}