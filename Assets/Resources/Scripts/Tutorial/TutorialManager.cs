using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour, IPointerClickHandler
{
    public string name = "";
    public string scene = "none";
    public GameObject TutorialCanvas;
    public void Awake(){
        TutorialCanvas = GameObject.Find("TutorialCanvas");
    }
    public void Start(){
        GameObject obj;
        if(name == "manager"){
            obj = TutorialCanvas.transform.Find("TutorialScene").gameObject;
            obj.SetActive(true);
            trueBG();
            trueBT();
        }
    }
    public void OnPointerClick(PointerEventData eventData){
        if(name == "button"){
            nextScene();
        }
    }
    public void setScene(string tscene){
        GameObject.Find("GameManager").GetComponent<TutorialManager>().scene = tscene;
        GameObject.Find("TutorialCanvas").transform.Find("TutorialButton").gameObject.GetComponent<TutorialManager>().scene = tscene;
    }
    public void Update(){
        GameObject obj;
        Altar altar = GameObject.Find("Altar").GetComponent<Altar>();
        GameObject player = GameObject.Find("Player");
        Inventory inventory = player.GetComponent<Inventory>();
        if( (scene == "6") && (inventory.CheckHasItem("gold") >= 1) ){
            obj = TutorialCanvas.transform.Find("TutorialScene6").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene7").gameObject;
            obj.SetActive(true);
            setScene("7");
        }
        else if( (scene == "7") && (inventory.GetTool().itemType == ItemData.ItemType.tool)){
            obj = TutorialCanvas.transform.Find("TutorialScene7").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene8").gameObject;
            obj.SetActive(true);
            setScene("8");
        }
        else if( (scene == "8") && (inventory.CheckHasItem("wood") >= 1) ){
            obj = TutorialCanvas.transform.Find("TutorialScene8").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene9").gameObject;
            obj.SetActive(true);
            setScene("9");
            trueBG();
            trueBT();
        }
        else if( (scene == "10") && (inventory.CheckHasItem("seed_potato") >= 1) ){
            obj = TutorialCanvas.transform.Find("TutorialScene10").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene11").gameObject;
            obj.SetActive(true);
            setScene("11");
        }
        else if( (scene == "11") && (GameObject.Find("potato") != null) && (GameObject.Find("potato").gameObject.tag == "garden") ){
            obj = TutorialCanvas.transform.Find("TutorialScene11").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene12").gameObject;
            obj.SetActive(true);
            setScene("12");
        }
        else if( (scene == "12") && (inventory.CheckHasItem("potato") >= 1) ){
            obj = TutorialCanvas.transform.Find("TutorialScene12").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene13").gameObject;
            obj.SetActive(true);
            trueBG();
            trueBT();
            setScene("13_1");
        }
        else if( (scene == "15") && (inventory.CheckHasItem("steak") >= 1) ){
            obj = TutorialCanvas.transform.Find("TutorialScene15").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene16").gameObject;
            obj.SetActive(true);
            setScene("16");
        }
        else if( (scene == "16") && (altar.preFood == "steak") ){
            obj = TutorialCanvas.transform.Find("TutorialScene16").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene17").gameObject;
            obj.SetActive(true);
            trueBT();
            trueBG();
            setScene("17");
        }
    }
    public void nextScene(){
        GameObject obj;
        if(scene == "none"){
            obj = TutorialCanvas.transform.Find("TutorialScene").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene0").gameObject;
            obj.SetActive(true);
            setScene("1_0");
        }
        else if(scene == "1_0"){
            obj = TutorialCanvas.transform.Find("TutorialScene0").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene1").gameObject;
            obj.SetActive(true);
            setScene("1_1");
        }
        else if(scene == "1_1"){
            obj = TutorialCanvas.transform.Find("TutorialScene1").gameObject;
            obj.transform.Find("box").Find("texts").Find("text01").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(true);
            setScene("1_2");
        }
        else if(scene == "1_2"){
            obj = TutorialCanvas.transform.Find("TutorialScene1").gameObject;
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text03").gameObject.SetActive(true);
            setScene("1_3");
        }
        else if(scene == "1_3"){
            obj = TutorialCanvas.transform.Find("TutorialScene1").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene2").gameObject;
            obj.SetActive(true);
            setScene("2");
        }
        else if(scene == "2"){
            obj = TutorialCanvas.transform.Find("TutorialScene2").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene3").gameObject;
            obj.SetActive(true);
            setScene("3");
        }
        else if(scene == "3"){
            obj = TutorialCanvas.transform.Find("TutorialScene3").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene4").gameObject;
            obj.SetActive(true);
            obj.transform.Find("box").Find("texts").Find("text01").gameObject.SetActive(true);
            setScene("4_1");
        }
        else if(scene == "4_1"){
            obj = TutorialCanvas.transform.Find("TutorialScene4").gameObject;
            obj.transform.Find("box").Find("texts").Find("text01").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(true);
            setScene("4_2");
        }
        else if(scene == "4_2"){
            obj = TutorialCanvas.transform.Find("TutorialScene4").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene5").gameObject;
            obj.SetActive(true);
            setScene("5");
        }
        else if(scene == "5"){
            obj = TutorialCanvas.transform.Find("TutorialScene5").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene6").gameObject;
            obj.SetActive(true);
            falseBG();
            falseBT();
            setScene("6");
        }
        else if(scene == "9"){
            obj = TutorialCanvas.transform.Find("TutorialScene9").gameObject;
            obj.transform.Find("box").Find("texts").Find("text01").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(true);
            setScene("9_2");
        }
        else if(scene == "9_2"){
            obj = TutorialCanvas.transform.Find("TutorialScene9").gameObject;
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text03").gameObject.SetActive(true);
            setScene("9_3");
        }
        else if(scene == "9_3"){
            obj = TutorialCanvas.transform.Find("TutorialScene9").gameObject;
            obj.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text03").gameObject.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene10").gameObject;
            obj.SetActive(true);
            falseBG();
            falseBT();
            setScene("10");
        }
        else if(scene == "13_1"){
            obj = TutorialCanvas.transform.Find("TutorialScene13").gameObject;
            obj.transform.Find("box").Find("texts").Find("text01").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(true);
            setScene("13_2");
        }
        else if(scene == "13_2"){
            obj = TutorialCanvas.transform.Find("TutorialScene13").gameObject;
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text03").gameObject.SetActive(true);
            setScene("13_3");
        }
        else if(scene == "13_3"){
            obj = TutorialCanvas.transform.Find("TutorialScene13").gameObject;
            obj.transform.Find("box").Find("texts").Find("text03").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text04").gameObject.SetActive(true);
            setScene("13_4");
        }
        else if(scene == "13_4"){
            obj = TutorialCanvas.transform.Find("TutorialScene13").gameObject;
            obj.transform.Find("box").Find("texts").Find("text04").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text05").gameObject.SetActive(true);
            setScene("13_5");
        }
        else if(scene == "13_5"){
            obj = TutorialCanvas.transform.Find("TutorialScene13").gameObject;
            obj.transform.Find("box").Find("texts").Find("text05").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text06").gameObject.SetActive(true);
            setScene("13_6");
        }
        else if(scene == "13_6"){
            obj = TutorialCanvas.transform.Find("TutorialScene13").gameObject;
            obj.transform.Find("box").Find("texts").Find("text06").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text07").gameObject.SetActive(true);
            setScene("13_7");
        }
        else if(scene == "13_7"){
            obj = TutorialCanvas.transform.Find("TutorialScene13").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene14").gameObject;
            obj.SetActive(true);
            setScene("14_1");
        }
        else if(scene == "14_1"){
            obj = TutorialCanvas.transform.Find("TutorialScene14").gameObject;
            obj.transform.Find("box").Find("texts").Find("text01").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(true);
            setScene("14_2");
        }
        else if(scene == "14_2"){
            obj = TutorialCanvas.transform.Find("TutorialScene14").gameObject;
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text03").gameObject.SetActive(true);
            setScene("14_3");
        }
        else if(scene == "14_3"){
            obj = TutorialCanvas.transform.Find("TutorialScene14").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene15").gameObject;
            obj.SetActive(true);
            falseBG();
            falseBT();
            setScene("15");
        }
        else if(scene == "17"){
            obj = TutorialCanvas.transform.Find("TutorialScene17").gameObject;
            obj.SetActive(false);
            obj = TutorialCanvas.transform.Find("TutorialScene18").gameObject;
            obj.SetActive(true);
            setScene("18_1");
        }
        else if(scene == "18_1"){
            obj = TutorialCanvas.transform.Find("TutorialScene18").gameObject;
            obj.transform.Find("box").Find("texts").Find("text01").gameObject.SetActive(false);
            obj.transform.Find("box").Find("texts").Find("text02").gameObject.SetActive(true);
            setScene("18_2");
        }
        else if(scene == "18_2"){
            SceneManager.LoadScene("game", LoadSceneMode.Single);
        }
    }
    public void falseBG(){
        GameObject obj;
        obj = TutorialCanvas.transform.Find("Background").gameObject;
        obj.SetActive(false);
    }
    public void trueBG(){
        GameObject obj;
        obj = TutorialCanvas.transform.Find("Background").gameObject;
        obj.SetActive(true);
    }
    public void falseBT(){
        GameObject obj;
        obj = TutorialCanvas.transform.Find("TutorialButton").gameObject;
        obj.SetActive(false);
    }
    public void trueBT(){
        GameObject obj;
        obj = TutorialCanvas.transform.Find("TutorialButton").gameObject;
        obj.SetActive(true);
    }
}