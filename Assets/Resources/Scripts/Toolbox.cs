using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Toolbox : MonoBehaviour
{
    public GameObject boxObjPrefab;
    public GameObject player;
    public GameObject boxObj;
    public bool active = false;
    public ItemData tool;
    public int toolSlot;
    void Start(){
        boxObjPrefab = Resources.Load<GameObject>("Prefabs/Player select box");
        player = GameObject.Find("Player");
        boxObj = this.transform.Find("playerselectbox").gameObject;
        boxObj.SetActive(true);
        active = true;
    }
    void Update(){
        if(active == false){
            boxObj.SetActive(false);
            active = false;
        }
        else{
            boxObj.SetActive(true);
            active = true;
        }
        float facing = player.GetComponent<PlayerController>().facing;
        if(active == true){
            if(facing == 1){
                boxObj.transform.position = new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z);
            }
            else{
                boxObj.transform.position = new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z);
            }
        }
    }
}
