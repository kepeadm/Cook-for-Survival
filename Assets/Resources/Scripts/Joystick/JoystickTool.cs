using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class JoystickTool : MonoBehaviour, IPointerClickHandler {
    public GameObject toolIcon;
    public GameObject player;
    public void Awake(){
        player = GameObject.Find("Player");
    }
    public void OnPointerClick(PointerEventData eventData){
        player.GetComponent<PlayerController>().UseTool();
    }
    public void FixedUpdate(){
        ItemData playertool = new ItemData();
        GameObject item;
        playertool = GetTool();
        item = GetToolObj();
        if(playertool == null){
            toolIcon.GetComponent<Image>().sprite = null;
            toolIcon.GetComponent<Image>().color = new Color32(255,255,255,0);
            toolIcon.name = "null";
        }
        else if(toolIcon.name != playertool.itemName){
            toolIcon.GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
            toolIcon.GetComponent<Image>().color = new Color32(255,255,255,255);
            toolIcon.name = playertool.itemName;
        }
    }
    ItemData GetTool(){
        string toolName = player.gameObject.GetComponent<Inventory>().slotToolName;
        if((toolName != null) && (toolName.Contains("_"))){
            List<SlotData> slots = player.gameObject.GetComponent<Inventory>().slots;
            int toolindex = 0;
            string toolSlotName = ""+toolName;
            string[] splitter = toolSlotName.Split('_');
            toolindex = int.Parse(splitter[1]);
            ItemData playertool = slots[toolindex].itemData;
            return playertool;
        }
        return null;
    }
    GameObject GetToolObj(){
        string toolName = player.gameObject.GetComponent<Inventory>().slotToolName;
        if((toolName != null) && (toolName.Contains("_"))){
            List<SlotData> slots = player.gameObject.GetComponent<Inventory>().slots;
            int toolindex = 0;
            string toolSlotName = ""+toolName;
            string[] splitter = toolSlotName.Split('_');
            toolindex = int.Parse(splitter[1]);
            GameObject playertool = slots[toolindex].item;
            return playertool;
        }
        return null;
    }
}