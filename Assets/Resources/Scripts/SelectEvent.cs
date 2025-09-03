using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEvent : MonoBehaviour
{
    public GameObject colliding;
    public string collidingTag;
    public string outlineName;
    public ObjMenu menu = new ObjMenu();
    public bool openedMenu;
    public GameObject outliner;
    public List<GameObject> collidingList = new List<GameObject>();

    void Start(){
        menu.gui = GameObject.Find("InteractiveButton");
        menu.opener = menu.gui.transform.Find("open").gameObject;
        menu.picker = menu.gui.transform.Find("pickup").gameObject;
        menu.planter = menu.gui.transform.Find("plant").gameObject;
        menu.opener.SetActive(false);
        menu.picker.SetActive(false);
        menu.planter.SetActive(false);
        outliner = GameObject.Find("outlineOBJ");
    }
    void Update(){
        if((colliding != null) && (outlineName == colliding.name)){
            outliner.transform.position = colliding.transform.position;
            createOutline(colliding);
        }
    }
    void enterReEvent(GameObject colliding){
        collidingTag = colliding.tag;
        if(collidingTag == "Interactive"){
            createOutline(colliding);
        }
        else if(collidingTag == "pickup_item"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "pot"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "potitem"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "furnace"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "beehive"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "miller"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "altar"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "garden"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "shop"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "animal"){
            createOutline(colliding);
        }
        else if(collidingTag == "door"){
            createOutline(colliding);
            openMenu();
        }
        else if(collidingTag == "portal"){
            createOutline(colliding);
            openMenu();
        }
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.isTrigger != null){
            if(coll.gameObject.tag == "Interactive"){
                colliding = coll.gameObject;
                collidingTag = "Interactive";
                createOutline(colliding);
            }
            else if(coll.gameObject.tag == "pickup_item"){
                colliding = coll.gameObject;
                collidingTag = "pickup_item";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "pot"){
                colliding = coll.gameObject;
                collidingTag = "pot";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "potitem"){
                colliding = coll.gameObject;
                collidingTag = "potitem";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "farmmanager"){
                colliding = coll.gameObject;
                collidingTag = "farmmanager";
            }
            else if(coll.gameObject.tag == "garden"){
                colliding = coll.gameObject;
                collidingTag = "garden";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "furnace"){
                colliding = coll.gameObject;
                collidingTag = "furnace";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "beehive"){
                colliding = coll.gameObject;
                collidingTag = "beehive";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "miller"){
                colliding = coll.gameObject;
                collidingTag = "miller";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "altar"){
                colliding = coll.gameObject;
                collidingTag = "altar";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "animal"){
                colliding = coll.gameObject;
                collidingTag = "animal";
                createOutline(colliding);
            }
            else if(coll.gameObject.tag == "door"){
                colliding = coll.gameObject;
                collidingTag = "door";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "portal"){
                colliding = coll.gameObject;
                collidingTag = "portal";
                createOutline(colliding);
                openMenu();
            }
            else if(coll.gameObject.tag == "shop"){
                colliding = coll.gameObject;
                collidingTag = "shop";
                createOutline(colliding);
                openMenu();
            }
            //collidingList.Add(coll.gameObject);
            collidinglistadding(coll.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D coll){
        if(outlineName == (string)coll.gameObject.name){
            outlineName = null;
            outliner.GetComponent<SpriteRenderer>().sprite = null;
        }
        if(coll.gameObject == colliding){
            closeMenu();
            collidingList.Remove(colliding);
            colliding = null;
            collidingTag = null;
        }
        for(int i = 0; i < collidingList.Count ; i++){
            if(collidingList[i] == coll.gameObject){
                collidingList.RemoveAt(i);
                break;
            }
        }
        if( (colliding == null) && (collidingList.Count >= 1) ){
            foreach(GameObject collidingTemp in collidingList){
                colliding = collidingTemp;
                enterReEvent(colliding);
                break;
            }
        }
    }
    public void collidinglistadding(GameObject obj){
        if(collidingList.Contains(obj)){
            return;
        }
        else{
            collidingList.Add(obj);
        }
    }
    void createOutline(GameObject colliding){
        outlineName = (string)colliding.name;
        if(outliner.GetComponent<SpriteRenderer>().sprite != colliding.GetComponent<SpriteRenderer>().sprite){
            outliner.GetComponent<SpriteRenderer>().sprite = colliding.GetComponent<SpriteRenderer>().sprite;
        }
        Vector3 position = colliding.transform.position;
        outliner.transform.position = position;
        outliner.transform.localScale = new Vector3(colliding.transform.localScale.x * 1.15f, colliding.transform.localScale.y * 1.15f, colliding.transform.localScale.z);
    }
    void openMenu(){
        Texttips texttip = GameObject.Find("TextTips").GetComponent<Texttips>();
        if(collidingTag == "potitem"){
            menu.opener.SetActive(true);
            menu.picker.SetActive(true);
            menu.planter.SetActive(false);
            texttip.openselectTip(colliding.name);
            texttip.openselectTip(colliding.GetComponent<Item>().itemData.itemName);
        }
        else if(collidingTag == "pickup_item"){
            menu.opener.SetActive(false);
            menu.picker.SetActive(true);
            menu.planter.SetActive(false);
            texttip.openselectTip(colliding.GetComponent<Item>().itemData.itemName);
        }
        else if(collidingTag == "pot"){
            menu.opener.SetActive(true);
            menu.picker.SetActive(false);
            menu.planter.SetActive(false);
        }
        else if(collidingTag == "shop"){
            menu.opener.SetActive(true);
            menu.picker.SetActive(false);
            menu.planter.SetActive(false);
            texttip.openselectTip(colliding.GetComponent<Shop>().data.name);
        }
        else if(collidingTag == "furnace"){
            menu.opener.SetActive(true);
            menu.picker.SetActive(false);
            menu.planter.SetActive(false);
            texttip.openselectTip("화덕");
        }
        else if(collidingTag == "beehive"){
            menu.opener.SetActive(true);
            menu.picker.SetActive(false);
            menu.planter.SetActive(false);
            texttip.openselectTip("벌집");
        }
        else if(collidingTag == "door"){
            menu.opener.SetActive(true);
            menu.picker.SetActive(false);
            menu.planter.SetActive(false);
            texttip.openselectTip("문");
        }
        else if(collidingTag == "miller"){
            menu.opener.SetActive(true);
            menu.picker.SetActive(false);
            menu.planter.SetActive(false);
            texttip.openselectTip("제분 풍차");
        }
        else if(collidingTag == "portal"){
            menu.opener.SetActive(true);
            menu.picker.SetActive(false);
            menu.planter.SetActive(false);
            texttip.openselectTip("탈출 포탈");
        }
        else if(collidingTag == "altar"){
            menu.opener.SetActive(true);
            menu.picker.SetActive(false);
            menu.planter.SetActive(false);
            texttip.openselectTip("제단");
        }
        else if(collidingTag == "garden"){
            menu.opener.SetActive(false);
            menu.picker.SetActive(false);
            menu.planter.SetActive(false);
            if(colliding.GetComponent<Garden>().crop.level == 0) {
                menu.planter.SetActive(true);
                texttip.openselectTip("밭");
            }
            else if(colliding.GetComponent<Garden>().crop.level >= 1) {
                texttip.openselectTip(colliding.name);
                if(colliding.GetComponent<Garden>().crop.level >= 4) {
                    menu.picker.SetActive(true);
                }
            }
        }
        menu.opener.GetComponent<InteractiveClick>().Loading();
        menu.picker.GetComponent<InteractiveClick>().Loading();
        menu.planter.GetComponent<InteractiveClick>().Loading();
    }
    public void closeMenu(){
        menu.opener.SetActive(false);
        menu.picker.SetActive(false);
        menu.planter.SetActive(false);
    }
}

[System.Serializable]
public class ObjMenu{
    public GameObject gui;
    public GameObject opener;
    public GameObject picker;
    public GameObject planter;
}