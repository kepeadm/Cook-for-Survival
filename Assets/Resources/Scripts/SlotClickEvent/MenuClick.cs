using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MenuClick : MonoBehaviour, IPointerClickHandler
{
    public MenuClickButton type;
    public enum MenuClickButton{
        openmenu,
        gotomain,
        backtogame,
        textboxnext,
        textboxskip,
    }
    
    public void OnPointerClick(PointerEventData eventData){
        if (type == MenuClickButton.openmenu){
            GameObject.Find("GUI").transform.Find("GUI_menu").gameObject.SetActive(true);
        }
        else if (type == MenuClickButton.gotomain){
            SceneManager.LoadScene("titlescene", LoadSceneMode.Single);
        }
        else if (type == MenuClickButton.backtogame){
            GameObject.Find("GUI").transform.Find("GUI_menu").gameObject.SetActive(false);
        }
        else if (type == MenuClickButton.textboxnext){
            GameObject.Find("GameManager").GetComponent<Gamemanager>().storybox.playscreen();
        }
        else if (type == MenuClickButton.textboxskip){
            GameObject.Find("GameManager").GetComponent<Gamemanager>().storybox.playskip();
        }
    }
}