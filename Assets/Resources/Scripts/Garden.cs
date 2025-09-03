using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Garden : MonoBehaviour
{
    public CropData crop = new CropData();
    public GameObject cropPrefab;
    public GameObject cropObject;

    public void Start(){
        cropPrefab = Resources.Load("Prefabs/cropPrefab") as GameObject;
    }

    public void Update(){
        if( (crop.level >= 1) && (crop.level <= 3) ){
            crop.growth += 10.0f * Time.deltaTime;
            if(crop.growth >= 100f){
                crop.level += 1;
                if(crop.level == 1){
                    cropObject.GetComponent<SpriteRenderer>().sprite = crop.crops_01;
                }
                else if(crop.level == 2){
                    cropObject.GetComponent<SpriteRenderer>().sprite = crop.crops_02;
                }
                else if(crop.level == 3){
                    cropObject.GetComponent<SpriteRenderer>().sprite = crop.crops_03;
                }
                else if(crop.level == 4){
                    cropObject.GetComponent<SpriteRenderer>().sprite = crop.crops_04;
                }
                crop.growth = 0f;

            }
        }
    }
    
    public void getWater(){
        cropObject.GetComponent<SpriteRenderer>().sprite = crop.crops_04;
        crop.growth = 0f;
        crop.level = 4;
    }
    public void getCrop(string name){
        Item_manager im = GameObject.Find("GameManager").GetComponent<Item_manager>();
        ItemData cropData = null ;
        cropData = im.loadItemData(name, cropData);

        cropData.itemIcon = Resources.Load<Sprite>(cropData.spritePath);

        GameObject player = GameObject.Find("Player");

        if(player.GetComponent<Inventory>().CheckGetItem(cropData)){
            player.GetComponent<Inventory>().GetItem(cropData);
            Destroy(this.gameObject);
        }
    }

    public void planting(string name){
        crop.name = name;
        crop.growth = 0;
        crop.level = 1;
        cropResource(crop.name);
        cropObject = Instantiate(cropPrefab, this.transform, false);
        cropObject.GetComponent<SpriteRenderer>().sprite = crop.crops_01;
        this.gameObject.name = crop.name;
        cropObject.name = crop.name;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = crop.soil;

    }
    public void cropResource(string name){

        crop.soil = Resources.Load<Sprite>("Sprites/Entity/Farm/soil_01");


        string path;
        path = "Sprites/Entity/Farm/" + name + "_01";
        crop.crops_01 = Resources.Load<Sprite>(path);

        path = "Sprites/Entity/Farm/" + name + "_02";
        crop.crops_02 = Resources.Load<Sprite>(path);

        path = "Sprites/Entity/Farm/" + name + "_03";
        crop.crops_03 = Resources.Load<Sprite>(path);

        path = "Sprites/Entity/Farm/" + name + "_04";
        crop.crops_04 = Resources.Load<Sprite>(path);


    }
}

[System.Serializable]
public class CropData{
    public float growth;
    public int level = 0;
    public string name;

    public Sprite soil;
    public Sprite crops_01;
    public Sprite crops_02;
    public Sprite crops_03;
    public Sprite crops_04;
}