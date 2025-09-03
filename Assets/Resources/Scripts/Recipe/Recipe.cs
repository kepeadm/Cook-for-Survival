using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class Recipe : MonoBehaviour
{
    public PlayerRecipes playerdata = new PlayerRecipes();
    public List<string> recipelist = new List<string>();

    public GameObject listslotPrefab;
    public GameObject recipeGUI;

    public void Awake(){
        playerdata = loadJson(playerdata);
        recipeGUI = GameObject.Find("GUI").transform.Find("GUI_recipe").gameObject;
        listslotPrefab = Resources.Load<GameObject>("Prefabs/Playerrecipe/listslot");
    }
    public void Start(){
        RecipeData data = new RecipeData();
        data = GameObject.Find("Furnace").GetComponent<Cook>().data;
        GUIopenlist(data);
    }

    public bool hasRecipe(string name){
        foreach(PlayerRecipeData recipe in playerdata.recipe){
            if(recipe.foodname == name){
                return true;
            }
        }
        return false;
    }

    public void openGUI(){
        foreach(PlayerRecipeData recipe in playerdata.recipe){
            foreach(string foodname in recipelist){
                if(recipe.foodname == foodname){
                    GameObject foodimage = recipeGUI.transform.Find("recipe_list").Find("recipe_pan").Find(foodname).Find("FoodImage").gameObject;
                    Destroy(foodimage);
                    foodimage = Instantiate(new GameObject(), recipeGUI.transform.Find("recipe_list").Find("recipe_pan").Find(foodname), false);
                    foodimage.name = "FoodImage";
                    foodimage.AddComponent<RectTransform>();
                    foodimage.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 200f);
                    foodimage.AddComponent<Image>();
                    ItemData item = new ItemData();
                    item = GameObject.Find("GameManager").GetComponent<Item_manager>().loadItemData(recipe.foodname, item);
                    foodimage.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.spritePath);
                }
            }
        }
        recipeGUI.SetActive(true);
    }
    public void GUIopenlist(RecipeData data){
        foreach(CookData recipe in data.recipe){
            if(!recipelist.Contains(recipe.name)){
                recipelist.Add(recipe.name);
                ItemData item = new ItemData();
                item = GameObject.Find("GameManager").GetComponent<Item_manager>().loadItemData(recipe.name, item);
                GameObject panel = recipeGUI.transform.Find("recipe_list").Find("recipe_pan").gameObject;
                GameObject slot = Instantiate(listslotPrefab, panel.transform, false);
                slot.name = recipe.name;
                GameObject foodimage = slot.transform.Find("FoodImage").gameObject;
                foodimage.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.spritePath);
                foodimage.GetComponent<Image>().color = new Color32(0,0,0,255);
            }
            else{
                continue;
            }
        }
    }

    public PlayerRecipes loadJson(PlayerRecipes data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            /*TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/PlayerRecipe");
            data = JsonUtility.FromJson<PlayerRecipes>(textData.ToString());
            */
            path = Path.Combine(Application.persistentDataPath+ "PlayerRecipe" + ".json");
            if (System.IO.File.Exists(path)!=true){
                File.WriteAllText(path, "{}");
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<PlayerRecipes>(jsonData);
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/Resources/Json/PlayerRecipe" + ".json");
            if (System.IO.File.Exists(path)!=true){
                File.WriteAllText(path, "{}");
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<PlayerRecipes>(jsonData);
            return data;
        }
    }

    public void makeJson(PlayerRecipes data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            //path = Path.Combine("jar:file://" + Application.dataPath + "!/assets"+ "/Resources/Json/PlayerRecipe" + ".json");
            path = Path.Combine(Application.persistentDataPath+ "PlayerRecipe" + ".json");
        }
        else{
            path = Path.Combine(Application.dataPath + "/Resources/Json/PlayerRecipe" + ".json");
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

}


[System.Serializable]
public class PlayerRecipes{
    public List<PlayerRecipeData> recipe = new List<PlayerRecipeData>();
    public bool ContainFoodname(string foodname){
        foreach(PlayerRecipeData r in recipe){
            if(r.foodname == foodname){
                return true;
            }
        }
        return false;
    }
    public bool ContainFoodindex(string foodname, int index){
        foreach(PlayerRecipeData r in recipe){
            if(r.foodname == foodname){
                foreach(int i in r.indexlist){
                    if(i == index){
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public void AddFoodindex(string foodname, int index){
        foreach(PlayerRecipeData r in recipe){
            if(r.foodname == foodname){
                r.indexlist.Add(index);
                return;
            }
        }
    }
}

[System.Serializable]
public class PlayerRecipeData{
    public string foodname;
    public string tool;
    public List<int> indexlist = new List<int>();
}