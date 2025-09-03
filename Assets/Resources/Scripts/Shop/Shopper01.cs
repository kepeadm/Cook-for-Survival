using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class Shopper01 : MonoBehaviour
{
    public float facing = 1f;
    public Animator animator;
    public void Start(){
        animator = this.GetComponent<Animator>();
    }

    public float getFacing(){
        GameObject player = GameObject.Find("Player");
        float distance = player.transform.position.x - this.transform.position.x;
        if(distance >= 0){
            return 1f;
        }
        else{
            return -1f;
        }
    }
    public void Update(){
        facing = getFacing();
        animator.SetFloat("Facing", facing);
    }
}