using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;

public class Cook : MonoBehaviour
{
    public RecipeData data = new RecipeData();
    public void Awake(){
        data = loadJson(data);
        foreach(CookData recipe in data.recipe){
            recipe.makedic();
        }
        makefullRecipe();
    }
    public void setCook(furnaceSlotData furnaceSlot){
        string foodname = getFoodname(furnaceSlot);
        int foodindex = getFoodindex(furnaceSlot);
        Item_manager im = GameObject.Find("GameManager").GetComponent<Item_manager>();
        ItemData data = new ItemData();
        if(foodname == "fail"){
            furnaceSlot.potInventory.slotClearAll();
            data = im.loadItemData("trash", data);
            furnaceSlot.potInventory.slots[0].itemData = data;
            furnaceSlot.potInventory.slots[0].isEmpty = false;
        }
        else{
            furnaceSlot.potInventory.slotClearAll();
            data = im.loadItemData(foodname, data);
            furnaceSlot.potInventory.slots[0].itemData = data;
            furnaceSlot.potInventory.slots[0].isEmpty = false;
            
            Recipe recipe = GameObject.Find("Player").GetComponent<Recipe>();
            PlayerRecipes playerdata = GameObject.Find("Player").GetComponent<Recipe>().playerdata;
            if(!playerdata.ContainFoodname(foodname)){
                PlayerRecipeData newdata = new PlayerRecipeData();
                newdata.foodname = foodname;
                newdata.indexlist.Add(foodindex);
                
                playerdata.recipe.Add(newdata);
                recipe.makeJson(playerdata);
            }
            else{
                if(!playerdata.ContainFoodindex(foodname, foodindex)){
                    playerdata.AddFoodindex(foodname, foodindex);
                    recipe.makeJson(playerdata);
                }
            }
            
        }
    }
    public float getProgress(furnaceSlotData furnaceSlot){
        CookData recipe = getRecipe(getFoodname(furnaceSlot));
        float progress = data.recipe[getFoodindex(furnaceSlot)].progress;
        if(recipe == null){
            return 10f;
        }
        else{
            return progress;
        }
    }
    public void makefullRecipe(){
        PlayerRecipes playerdata = new PlayerRecipes();
        int i = 0;
        foreach(CookData recipe in data.recipe){
            if(!playerdata.ContainFoodname(recipe.name)){
                PlayerRecipeData newdata = new PlayerRecipeData();
                newdata.foodname = recipe.name;
                newdata.indexlist.Add(i);
                newdata.tool = recipe.tool;
                playerdata.recipe.Add(newdata);
            }
            else{
                if(!playerdata.ContainFoodindex(recipe.name, i)){
                    playerdata.AddFoodindex(recipe.name, i);
                }
            }
            i++;
        }
        Recipe playerrecipe = new Recipe();
        playerrecipe.makeJson(playerdata);
    }
    public CookData getRecipe(string name){
        foreach(CookData recipe in data.recipe){
            if(recipe.name == name){
                return recipe;
            }
        }
        return null;
    }
    public string getFoodTool(string name){
        foreach(CookData recipe in data.recipe){
            if(recipe.name == name){
                return recipe.tool;
            }
        }
        return null;
    }
    public List<string> getFoodType(string name){
        foreach(CookData recipe in data.recipe){
            if(recipe.name == name){
                return recipe.type;
            }
        }
        return null;
    }
    public List<Money> getFoodMoney(string name){
        foreach(CookData recipe in data.recipe){
            if(recipe.name == name){
                return recipe.price;
            }
        }
        return null;
    }
    public List<Money> getFoodMoneyType(string name, string type){
        List<Money> price = new List<Money>();
        foreach(CookData recipe in data.recipe){
            if(recipe.name == name){
                foreach(string recipetype in recipe.type){
                    if(recipetype == type){
                        foreach(Money money in recipe.price){
                            Money tMoney = new Money();
                            tMoney.type = money.type;
                            tMoney.amount = money.amount;
                            tMoney.amount = tMoney.amount * 2;
                            price.Add(tMoney);
                        }
                        return price;
                    }
                }
                price = recipe.price;
                return price;
            }
        }
        return null;
    }
    public int getFoodindex(furnaceSlotData furnaceSlot){
        Potinventory potinv = furnaceSlot.potInventory;
        string tool = furnaceSlot.itemData.itemName;
        Dictionary<string, int> potmaterials = new Dictionary<string, int>();
        string name = null;
        int index = -1;
        foreach(potSlotData slot in potinv.slots){
            if(slot.isEmpty == true){
                continue;
            }
            name = slot.itemData.itemName;
            if(potmaterials.ContainsKey(name)){
                potmaterials[name] += 1;
            }
            else{
                potmaterials.Add(name, 1);
            }
        }
        foreach(CookData recipe in data.recipe){
            index +=1;
            if(recipe.tool != tool){
                continue;
            }
            else{
                List<string> cookKeys = new List<string>(recipe.materials.Keys);
                List<string> potKeys = new List<string>(potmaterials.Keys);
                if(cookKeys.Count != potKeys.Count){
                    continue;
                }
                else{
                    int same = 0;
                    foreach(string key in cookKeys){
                        foreach(string potkey in potKeys){
                            if(potkey != key){
                                continue;
                            }
                            if(recipe.materials[key] == potmaterials[key]){
                                same += 1;
                            }
                        }
                    }
                    if(same == cookKeys.Count){
                        return index;
                    }
                    else{
                        continue;
                    }
                }
            }
        }
        return index;
    }
    public string getFoodname(furnaceSlotData furnaceSlot){
        Potinventory potinv = furnaceSlot.potInventory;
        string tool = furnaceSlot.itemData.itemName;
        Dictionary<string, int> potmaterials = new Dictionary<string, int>();
        string name = null;
        foreach(potSlotData slot in potinv.slots){
            if(slot.isEmpty == true){
                continue;
            }
            name = slot.itemData.itemName;
            if(potmaterials.ContainsKey(name)){
                potmaterials[name] += 1;
            }
            else{
                potmaterials.Add(name, 1);
            }
        }
        foreach(CookData recipe in data.recipe){
            if(recipe.tool != tool){
                continue;
            }
            else{
                List<string> cookKeys = new List<string>(recipe.materials.Keys);
                List<string> potKeys = new List<string>(potmaterials.Keys);
                if(cookKeys.Count != potKeys.Count){
                    continue;
                }
                else{
                    int same = 0;
                    foreach(string key in cookKeys){
                        foreach(string potkey in potKeys){
                            if(potkey != key){
                                continue;
                            }
                            if(recipe.materials[key] == potmaterials[key]){
                                same += 1;
                            }
                        }
                    }
                    if(same == cookKeys.Count){
                        return recipe.name;
                    }
                    else{
                        continue;
                    }
                }
            }
        }
        string foodname = "fail";
        return foodname;
    }

    public RecipeData loadJson(RecipeData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/Cook");
            data = JsonUtility.FromJson<RecipeData>(textData.ToString());
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/Resources/Json/Cook" + ".json");
            if (System.IO.File.Exists(path)!=true){
                File.WriteAllText(path, "{}");
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<RecipeData>(jsonData);
            return data;
        }
    }
    public void makeJson(RecipeData data){
        string path = Path.Combine(Application.dataPath + "/Resources/Json/Cook" + ".json");
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }
}




[System.Serializable]
public class RecipeData{
    public List<CookData> recipe = new List<CookData>();
}

[System.Serializable]
public class CookData{
    public string name;
    public string tool;
    public List<string> type = new List<string>();
    public float progress;
    public List<string> materials_key = new List<string>();
    public List<int> materials_data = new List<int>();
    public Dictionary<string, int> materials = new Dictionary<string, int>();
    public List<Money> price = new List<Money>();
    public void makedic(){
        materials = new Dictionary<string, int>();
        for(int i = 0; i < materials_key.Count; i++){
            
            materials.Add(materials_key[i], materials_data[i]);
        }
    }
    public void splitdic(){
        materials_key = new List<string>();
        materials_data = new List<int>();
        foreach(KeyValuePair<string, int> dic in materials){
            materials_key.Add(dic.Key);
            materials_data.Add(dic.Value);
        }
    }
}

[System.Serializable]
public class Money{
    public string type;
    public int amount;
}