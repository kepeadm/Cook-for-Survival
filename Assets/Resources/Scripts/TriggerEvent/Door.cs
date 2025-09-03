using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string keyName = "none";
    public GameObject openObj;
    public GameObject closeObj;
    public GameObject triggerObj;
    public bool open = false;
    public bool consume = false;
    public void openDoor(ItemData key){
        if(keyName == "none"){
            open = true;
            Collider2D collider = this.GetComponent<Collider2D>();
            SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
            collider.enabled = false;
            renderer.sprite = openObj.GetComponent<SpriteRenderer>().sprite;
            SpriteRenderer triggerrenderer = triggerObj.GetComponent<SpriteRenderer>();
            triggerrenderer = renderer;
        }
        else if(key.itemName == keyName){
            if(consume == true){
                key.amount -= 1;
            }
            open = true;
            Collider2D collider = this.GetComponent<Collider2D>();
            SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
            collider.enabled = false;
            renderer.sprite = openObj.GetComponent<SpriteRenderer>().sprite;
            SpriteRenderer triggerrenderer = triggerObj.GetComponent<SpriteRenderer>();
            triggerrenderer = renderer;
        }
        else{
            Texttips texttip = GameObject.Find("TextTips").GetComponent<Texttips>();
            texttip.openobjectTip("Player", "I think, i need the key.");
        }
    }
    public void closeDoor(){
        open = false;
        Collider2D collider = this.GetComponent<Collider2D>();
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        collider.enabled = true;
        renderer.sprite = closeObj.GetComponent<SpriteRenderer>().sprite;
        SpriteRenderer triggerrenderer = triggerObj.GetComponent<SpriteRenderer>();
        triggerrenderer = renderer;
    }
}