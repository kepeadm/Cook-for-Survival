using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Recipeslot : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData){
        openRecipe();
    }
    public void openRecipe(){
        Recipe playerrecipe = GameObject.Find("Player").GetComponent<Recipe>();
        RecipeData cookdata = GameObject.Find("Furnace").GetComponent<Cook>().data;
        Cook cook = GameObject.Find("Furnace").GetComponent<Cook>();
        GameObject recipeGUI = playerrecipe.recipeGUI;
        GameObject detailViewPrefab = Resources.Load<GameObject>("Prefabs/Playerrecipe/recipe_detail_view01");
        if(recipeGUI.transform.Find("recipe_detail").Find("recipe_detail_view01")){
            Destroy(recipeGUI.transform.Find("recipe_detail").Find("recipe_detail_view01").gameObject);
        }
        recipeGUI.transform.Find("recipe_detail").Find("recipe_detail_view02").gameObject.SetActive(false);
        if(playerrecipe.hasRecipe(this.gameObject.name)){

            GameObject recipePrefab = Resources.Load<GameObject>("Prefabs/Playerrecipe/recipePrefab");

            string foodname = this.gameObject.name;

            ItemData fooditem = new ItemData();
            fooditem = GameObject.Find("GameManager").GetComponent<Item_manager>().loadItemData(foodname, fooditem);
            GameObject detailView = Instantiate(detailViewPrefab, recipeGUI.transform.Find("recipe_detail"), false);
            detailView.name = "recipe_detail_view01";
            detailView.SetActive(true);

            detailView.transform.Find("Foodname").gameObject.GetComponent<TextMeshProUGUI>().text = foodname;

            List<string> types = cook.getFoodType(foodname);
            string foodtype = "";
            for(int i = 0; i < types.Count; i++){
                if(i == 0){
                    foodtype = foodtype + types[i];
                }
                else{
                    foodtype = foodtype + ", " +types[i];
                }
            }
            detailView.transform.Find("Foodtype").gameObject.GetComponent<TextMeshProUGUI>().text = foodtype;

            detailView.transform.Find("FoodImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(fooditem.spritePath);


            List<Money> price = cook.getFoodMoney(foodname);
            foreach(Money money in price){
                detailView.transform.Find("Foodprice").Find(money.type).gameObject.SetActive(true);
                detailView.transform.Find("Foodprice").Find(money.type).Find("amount").gameObject.GetComponent<TextMeshProUGUI>().text = "" + money.amount;
            }

            foreach(PlayerRecipeData precipe in playerrecipe.playerdata.recipe){
                if(precipe.foodname == foodname){
                    foreach(int index in precipe.indexlist){
                        List<string> materials = new List<string>();
                        foreach(string key in cookdata.recipe[index].materials_key){
                            int a = 0;
                            while(a<cookdata.recipe[index].materials[key]){
                                materials.Add(key);
                                a++;
                            }
                        }
                        GameObject recipe = Instantiate(recipePrefab, detailView.transform.Find("recipe_detail_recipe").Find("recipe_detail_rect").Find("recipe_detail_pan"), false);
                        int i = 0;
                        foreach(string material in materials){
                            string slotname = "recipe_detail_material_" + i;
                            ItemData slotitem = new ItemData();
                            slotitem = GameObject.Find("GameManager").GetComponent<Item_manager>().loadItemData(material, slotitem);
                            recipe.transform.Find(slotname).Find("FoodImage").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(slotitem.spritePath);
                            recipe.transform.Find(slotname).gameObject.SetActive(true);
                            i++;
                        }
                    }
                    
                }
                
            }

            GameObject recipe_tray = detailView.transform.Find("recipe_tray").gameObject;
            if(cook.getFoodTool(foodname) == "oven_tray"){
                recipe_tray.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/tray");
            }
            else if(cook.getFoodTool(foodname) == "fripan"){
                recipe_tray.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/fripan");
            }
            else if(cook.getFoodTool(foodname) == "pot"){
                recipe_tray.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/pot");
            }
            else if(cook.getFoodTool(foodname) == "cutting_board"){
                recipe_tray.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/cuttingboard");
            }
            
        }
        else{
            recipeGUI.transform.Find("recipe_detail").Find("recipe_detail_view02").gameObject.SetActive(true);
        }
    }
}