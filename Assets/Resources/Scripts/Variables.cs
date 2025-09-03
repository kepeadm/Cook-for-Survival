using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Variables : MonoBehaviour
{
    public GameObject GardenPrefab;

    public string startdraginventory = "air";
    public string draginventory = "air";
    public int startdragslot = -1;
    public int dragslot = -1;

    public void Start(){
        GardenPrefab = Resources.Load("Prefabs/GardenPrefab") as GameObject;
    }
}
