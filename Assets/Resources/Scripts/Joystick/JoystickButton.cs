using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject player;
    public void Awake(){
        player = GameObject.Find("Player");
    }
    public float maxdistance = 120f;
    RectTransform rectTransform;
    public void OnEndDrag(PointerEventData eventData){
        RectTransform rect = this.gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        player.GetComponent<PlayerController>().setMove(0f, 0f);
        player.GetComponent<PlayerController>().Joystick = false;
    }
    public void OnDrag(PointerEventData eventData){
        GameObject joyButton = this.gameObject;
        RectTransform rect = this.gameObject.GetComponent<RectTransform>();
        joyButton.transform.position = eventData.position;
        float distance = Vector2.Distance(Vector2.zero, rect.anchoredPosition);
        if(distance > maxdistance){
            var joyvec = rect.anchoredPosition;
            rect.anchoredPosition = joyvec.normalized * maxdistance;
        }
        Vector2 movement = rect.anchoredPosition.normalized;
        player.GetComponent<PlayerController>().setMove(movement.x, movement.y);
    }


    public void OnBeginDrag(PointerEventData eventData){
        player.GetComponent<PlayerController>().Joystick = true;
        GameObject joyButton = this.gameObject;
        RectTransform rect = this.gameObject.GetComponent<RectTransform>();
        joyButton.transform.position = eventData.position;
        float distance = Vector2.Distance(Vector2.zero, rect.anchoredPosition);
        if(distance > maxdistance){
            var joyvec = rect.anchoredPosition;
            rect.anchoredPosition = joyvec.normalized * maxdistance;
        }
        Vector2 movement = rect.anchoredPosition.normalized;
        player.GetComponent<PlayerController>().setMove(movement.x, movement.y);
    }
}