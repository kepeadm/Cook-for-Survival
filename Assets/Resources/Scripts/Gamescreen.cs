using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Gamescreen : MonoBehaviour, IPointerClickHandler
{
    public string triggerName;
    public void OnPointerClick(PointerEventData eventData){
        if(triggerName == "GoToTitle"){
            GoToTitle();
        }
        else if (triggerName == ""){
            
        }
        else if (triggerName == "start"){
            GameStart();
        }
        else if (triggerName == "tutorial"){
            TutorialStart();
        }
    }
    public void GoToTitle(){
        SceneManager.LoadScene("titlescene", LoadSceneMode.Single);
    }
    public void GameStart(){
        SceneManager.LoadScene("game", LoadSceneMode.Single);
    }
    public void TutorialStart(){
        SceneManager.LoadScene("tutorial", LoadSceneMode.Single);
    }
}