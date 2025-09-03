using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Recipebutton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData){
        openRecipe();
    }
    public void openRecipe(){
        Recipe playerrecipe = GameObject.Find("Player").GetComponent<Recipe>();
        playerrecipe.openGUI();

    }
}