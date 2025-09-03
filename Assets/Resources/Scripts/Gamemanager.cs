using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Gamemanager : MonoBehaviour
{
    public float game_time = 20f;
    public bool gamePlay;
    public GameObject timerObj;
    public Language lang;
    public Storybox storybox;
    public void Awake(){
        storybox.guiscreen = GameObject.Find("SCREEN");
        timerObj = GameObject.Find("GUI").transform.Find("GameTimer").Find("GameTimer0").Find("GameTimer1").gameObject;
        storybox.textbox = GameObject.Find("SCREEN").transform.Find("GameTextbox").gameObject;
        storybox.playerimage = storybox.textbox.transform.Find("box").Find("character").Find("box").Find("playerimage").gameObject;
        storybox.npcimage = storybox.textbox.transform.Find("box").Find("character").Find("box").Find("npcimage").gameObject;
    }
    public void Start(){
        storybox.guiscreen.SetActive(false);
        storybox.guiscreen.SetActive(true);
        storybox.textbox.SetActive(true);
        storybox.playscreen();
    }
    public void gamestart(){
        gamePlay = true;
    }
    public void setTimer(){
        int min = (int) game_time;
        int sec = (int) ( (game_time - min) * 60 );
        string text = "" + min + ":" + sec;
        GameObject Timetext = GameObject.Find("GUI").transform.Find("GameTimer").Find("GameTime").gameObject;
        Timetext.GetComponent<TextMeshProUGUI>().text = text;
    }
    public void tipDesire(){
        GameObject Desiretext = GameObject.Find("GUI").transform.Find("DesireTip").Find("DesireText").gameObject;
        Altar altar = GameObject.Find("Altar").GetComponent<Altar>();
        Desiretext.GetComponent<TextMeshProUGUI>().text = altar.desire;
    }
    public void Update(){
        if(gamePlay == true){
            if(game_time >= 0){
                if(game_time == 20f){
                    Altar altar = GameObject.Find("Altar").GetComponent<Altar>();
                    altar.newDesire(game_time);
                }
                else if(game_time == 15f){
                    Altar altar = GameObject.Find("Altar").GetComponent<Altar>();
                    altar.newDesire(game_time);
                }
                else if(game_time == 10f){
                    Altar altar = GameObject.Find("Altar").GetComponent<Altar>();
                    altar.newDesire(game_time);
                }
                else if(game_time == 5f){
                    Altar altar = GameObject.Find("Altar").GetComponent<Altar>();
                    altar.newDesire(game_time);
                }
                game_time -= 1f * Time.deltaTime / 60f;
                timerObj.GetComponent<Image>().fillAmount = 1 - game_time / 20;
                setTimer();
                tipDesire();
            }
            else{
                List<GameObject> GUI = new List<GameObject>();
                GUI.Add(GameObject.Find("GUI").transform.Find("Joystick").gameObject);
                GUI.Add(GameObject.Find("GUI").transform.Find("GameTimer").gameObject);
                GUI.Add(GameObject.Find("GUI").transform.Find("RecipeButton").gameObject);
                foreach(GameObject obj in GUI){
                    obj.SetActive(false);
                }
                List<GameObject> GameOver = new List<GameObject>();
                GameOver.Add(GameObject.Find("SCREEN").transform.Find("GameOver").gameObject);
                GameOver.Add(GameObject.Find("SCREEN").transform.Find("Background").gameObject);
                foreach(GameObject obj in GameOver){
                    obj.SetActive(true);
                }
                GameObject Screen = GameObject.Find("SCREEN");
                Screen.SetActive(false);
                Screen.SetActive(true);
                gamePlay = false;
            }
        }
    }
}

[System.Serializable]
public class Storybox{
    public GameObject guiscreen;
    public GameObject textbox;
    public GameObject playerimage;
    public GameObject npcimage;
    public int scene = -1;

    public void playtext(string type, string text){
        if(type == "player"){
            GameObject guitext = textbox.transform.Find("box").Find("texts").Find("text").gameObject;
            npcimage.SetActive(false);
            playerimage.SetActive(true);
            guitext.GetComponent<typewriterUI>().stopTMP();
            guitext.GetComponent<TextMeshProUGUI>().text = text;
            guitext.GetComponent<typewriterUI>().startTMP();
        }
        else if(type == "npc"){
            GameObject guitext = textbox.transform.Find("box").Find("texts").Find("text").gameObject;
            npcimage.SetActive(true);
            playerimage.SetActive(false);
            guitext.GetComponent<typewriterUI>().stopTMP();
            guitext.GetComponent<TextMeshProUGUI>().text = text;
            guitext.GetComponent<typewriterUI>().startTMP();
        }
    }
    public void playskip(){
        scene = 16;
        textbox.SetActive(false);
        GameObject.Find("GameManager").GetComponent<Gamemanager>().gamestart();
    }
    public void playscreen(){
        if(scene == -1){
            playtext("npc", "이제야 깨어났네");
            scene = 0;
        }
        else if(scene == 0){
            playtext("npc", "이제야 깨어났네");
            scene = 1;
        }
        else if(scene == 1){
            playtext("npc", "서둘러 ! 곧 신의 식사 시간이야.");
            scene = 2;
        }
        else if(scene == 2){
            playtext("npc", "신의 제물이 된 것을 영광으로 여겨라.");
            scene = 3;
        }
        else if(scene == 3){
            playtext("player", "아니 잠깐만. 나는 정말 맛이 없어.");
            scene = 4;
        }
        else if(scene == 4){
            playtext("player", "신께서도 나보다는 더욱 맛있는 걸 먹고싶을 거야.");
            scene = 5;
        }
        else if(scene == 5){
            playtext("npc", "너보다 더 맛있는게 뭐가 있지?");
            scene = 6;
        }
        else if(scene == 6){
            playtext("npc", "더 시간 끌지 말고, 곱게 제단에 올라갈 채비를 해.");
            scene = 7;
        }
        else if(scene == 7){
            playtext("player", "내가 맛있는 음식을 준비해볼게!!!");
            scene = 8;
        }
        else if(scene == 8){
            playtext("npc", "뭐?");
            scene = 9;
        }
        else if(scene == 9){
            playtext("player", "신께서 만족할 만한 음식을 내올게.");
            scene = 10;
        }
        else if(scene == 10){
            playtext("npc", "수작부리지 마.");
            scene = 11;
        }
        else if(scene == 11){
            playtext("player", "정말 맛있는걸 내올게. 기회를 줘.");
            scene = 12;
        }
        else if(scene == 12){
            playtext("npc", "흠...");
            scene = 13;
        }
        else if(scene == 13){
            playtext("npc", "그래. 널 한번 믿어보도록 하지. 시간은 단 20분만 주겠다.");
            scene = 14;
        }
        else if(scene == 14){
            playtext("npc", "시간이 지난다면 널 제단에 반드시 올려놓겠다.");
            scene = 15;
        }
        else if(scene == 15){
            playtext("player", "(20분 안에 신의 만족시키고 섬에서 나갈 방법을 찾아야겠어!)");
            scene = 16;
        }
        else if(scene == 16){
            textbox.SetActive(false);
            GameObject.Find("GameManager").GetComponent<Gamemanager>().gamestart();
        }
    }
}