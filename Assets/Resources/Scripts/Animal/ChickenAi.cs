using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAi : MonoBehaviour {
    public float moveSpeed = 1f;
    public float originSpeed = 1f;
    public float facing = 1f;
    
    public float time = 0f;
    public float eggtime = 0f;

    public int health = 20;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    public void Death(){
        Item_manager im = GameObject.Find("GameManager").GetComponent<Item_manager>();
        im.dropItem("meat", this.transform.position, 3);
        
        StopCoroutine(beDam());
        Destroy(this.gameObject);
    }

    public void Damaged(){
        StartCoroutine(beDam());
    }
    IEnumerator beDam(){
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        makeWalk();
        moveSpeed = 3f;
        for(int i = 0; i<3; i++){
            renderer.color = new Color32(210,95,95,255);
            yield return new WaitForSeconds(0.1f);
            renderer.color = new Color32(255,255,255,255);
            yield return new WaitForSeconds(0.1f);
        }
        moveSpeed = originSpeed;

    }

    void Update()
    {
        time = time + Time.deltaTime;
        eggtime = eggtime + Time.deltaTime;
        if(time >= 3){
            time = 0f;
            int i = Random.Range(0, 2);
            if(i == 0){
                makeWalk();
            }
            else{
                makeIdle();
            }
        }
        if(eggtime >= 120){
            eggtime = 0f;
            int i = Random.Range(0, 100);
            if(i <= 50){
                beEgg();
            }
        }
        setFacing();
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("Facing", facing);
        if(health <= 0){
            Death();
        }
        
    }
    public void beEgg(){
        Item_manager im = GameObject.Find("GameManager").GetComponent<Item_manager>();
        im.dropItem("egg", this.transform.position, 1);
    }
    public void makeWalk(){
        movement.x = Random.Range(-1.0f, 1.0f);
        movement.y = Random.Range(-1.0f, 1.0f);
    }
    public void makeIdle(){
        movement.x = 0f;
        movement.y = 0f;
    }
    void setFacing(){
        if (movement.x >= 0)
            {
            facing = 1;
            }
        else
            {
            facing = -1;
            }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
