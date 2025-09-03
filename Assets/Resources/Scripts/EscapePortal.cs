using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class EscapePortal : MonoBehaviour
{
    public void GameClear(){
        List<GameObject> GUI = new List<GameObject>();
        GUI.Add(GameObject.Find("GUI").transform.Find("Joystick").gameObject);
        GUI.Add(GameObject.Find("GUI").transform.Find("GameTimer").gameObject);
        GUI.Add(GameObject.Find("GUI").transform.Find("RecipeButton").gameObject);
        foreach(GameObject obj in GUI){
            obj.SetActive(false);
        }
        List<GameObject> GameOver = new List<GameObject>();
        GameOver.Add(GameObject.Find("SCREEN").transform.Find("GameClear").gameObject);
        GameOver.Add(GameObject.Find("SCREEN").transform.Find("Background").gameObject);
        foreach(GameObject obj in GameOver){
            obj.SetActive(true);
        }
        GameObject Screen = GameObject.Find("SCREEN");
        Screen.SetActive(false);
        Screen.SetActive(true);
        GameObject.Find("GameManager").GetComponent<Gamemanager>().gamePlay = false;
    }
}