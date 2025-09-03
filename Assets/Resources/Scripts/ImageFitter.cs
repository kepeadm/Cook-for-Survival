using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageFitter : MonoBehaviour
{
    RectTransform rct;
    public void Update(){
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}