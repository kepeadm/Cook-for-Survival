using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Tree : MonoBehaviour
{
    public float maxhealth = 10;
    public float health;

    void Start(){
        health = maxhealth;
    }

    public void Chop(){
        if(health > 0){
            StartCoroutine(ShakeTree());
            health = health - 4;
        }
        if(health <= 0){
            GameObject gm = GameObject.Find("GameManager");
            gm.GetComponent<Item_manager>().dropItemAni("wood", this.transform.position, 1);
            Destroy(this.gameObject);
        }
    }

    IEnumerator ShakeTree(){
        float y = transform.position.y;
        float z = transform.position.z;
        this.transform.position = new Vector3(this.transform.position.x - 0.05f , y, z);
        yield return new WaitForSeconds(0.2f);
        this.transform.position = new Vector3(this.transform.position.x + 0.1f , y, z);
        yield return new WaitForSeconds(0.2f);
        this.transform.position = new Vector3(this.transform.position.x - 0.1f , y, z);
        yield return new WaitForSeconds(0.2f);
        this.transform.position = new Vector3(this.transform.position.x + 0.05f , y, z);
    }
}
