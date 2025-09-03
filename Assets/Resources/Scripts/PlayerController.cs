using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float originSpeed = 4f;
    public float facing = 1f;

    public bool CanUse = true;
    public bool Joystick = false;

    public bool CanFish = false;

    public string behave = "";

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Joystick == false){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        FishCancel();
        
        setFacing();
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("Facing", facing);
        if(Input.GetKeyDown(KeyCode.Q)){
            if(CanUse == true){
                UseTool();
            }
        }
        
    }

    public void setMove(float x, float y){
        movement.x = x;
        movement.y = y;
    }

    void FishCancel(){
        float fishing = animator.GetFloat("Fishing");
        if(fishing != 3f){
            if(this.gameObject.transform.Find("fishIcon")){
                Destroy(this.gameObject.transform.Find("fishIcon").gameObject);
            }
        }
        if(fishing >= 1f){
            if ( (movement.x !=0) || (movement.y != 0) ){
                animator.SetFloat("Fishing", 0f);
                return;
            }
            if(GetTool() == null){
                animator.SetFloat("Fishing", 0f);
                return;
            }
            else if(!GetTool().itemName.Contains("fishing_rod")){
                animator.SetFloat("Fishing", 0f);
                return;
            }
        }
    }
    public void UseTool(){
        if(CanUse != true){
            return;
        }
        string toolName = this.gameObject.GetComponent<Inventory>().slotToolName;
        if(toolName!=null){
            List<SlotData> slots = this.gameObject.GetComponent<Inventory>().slots;
            int toolindex = 0;
            string toolSlotName = ""+toolName;
            string[] splitter = toolSlotName.Split('_');
            toolindex = int.Parse(splitter[1]);
            ItemData playertool = slots[toolindex].itemData;
            float fishing = animator.GetFloat("Fishing");
            if((playertool != null)&&(playertool.itemType == ItemData.ItemType.tool)){
                if((playertool.itemName.Contains("axe")) && (playertool.itemName.Contains("pickaxe")!= true)){
                    StartCoroutine(beAxe(playertool));
                }
                else if(playertool.itemName.Contains("sword")){
                    StartCoroutine(beSword(playertool));
                }
                else if(playertool.itemName.Contains("shovel")){
                    StartCoroutine(beShovel(playertool));
                }
                else if(playertool.itemName.Contains("watering_can")){
                    StartCoroutine(beWater(playertool));
                }
                else if((playertool.itemName.Contains("fishing_rod")) && (CanFish== true) && ( (movement.x == 0) || (movement.y == 0) ) && (fishing <= 0f) ){
                    StartCoroutine(beFishing());
                }
                else if( (fishing >= 2f) && (fishing < 3f) && (playertool.itemName.Contains("fishing_rod")) ){
                    animator.SetFloat("Fishing", 0f);
                }
                else if( (fishing == 3f) && (playertool.itemName.Contains("fishing_rod")) ){
                    animator.SetFloat("Fishing", 4f);
                    StartCoroutine(useFishCatch(GetTool()));
                }
            }
            else if(playertool.itemType == ItemData.ItemType.potitem){
                GameObject potInvObj = this.gameObject.GetComponent<Inventory>().dropToolObj();
                potInvObj.GetComponent<Potinventory>().openInventory();
            }
        }
    }

    ItemData GetTool(){
        string toolName = this.gameObject.GetComponent<Inventory>().slotToolName;
        if(toolName != null){
            List<SlotData> slots = this.gameObject.GetComponent<Inventory>().slots;
            int toolindex = 0;
            string toolSlotName = ""+toolName;
            string[] splitter = toolSlotName.Split('_');
            toolindex = int.Parse(splitter[1]);
            ItemData playertool = slots[toolindex].itemData;
            return playertool;
        }
        return null;
    }
    IEnumerator beFishing(){
        if ( (movement.x !=0) || (movement.y != 0) ){
            yield break;
        }
        animator.SetFloat("Fishing", 1f);
        yield return new WaitForSeconds(0.75f);
        if(!GetTool().itemName.Contains("fishing_rod")){
            animator.SetFloat("Fishing", 0f);
            yield break;
        }
        animator.SetFloat("Fishing", 2f);
        StartCoroutine(useFishWaiting());
    }

    IEnumerator useFishWaiting(){
        float fishing = animator.GetFloat("Fishing");
        float reeling = 0;
        while((fishing == 2f) && (CanFish == true) ){
            yield return new WaitForSeconds(0.55f);
            fishing = animator.GetFloat("Fishing");
            reeling = Random.Range(0f, 10f);
            if((reeling >= 9) && (fishing == 2f)){
                animator.SetFloat("Fishing", 3f);
            }
        }
        fishing = animator.GetFloat("Fishing");
        if(fishing == 3f){
            GameObject fishiconPrefab = Resources.Load<GameObject>("Prefabs/fishiconPrefab");
            GameObject player = this.gameObject;
            GameObject fishicon = Instantiate(fishiconPrefab, player.transform, false);
            fishicon.name = "fishIcon";
            StartCoroutine(useFishReeling());
        }
    }
    IEnumerator useFishReeling(){
        yield return new WaitForSeconds(2f);
        float fishing = animator.GetFloat("Fishing");
        if(fishing == 3f){
            animator.SetFloat("Fishing", 2f);
            StartCoroutine(useFishWaiting());
        }
    }
    IEnumerator useFishCatch(ItemData tool){
        float fishing = animator.GetFloat("Fishing");
        if(fishing == 4f){
            animator.SetTrigger("FishCatch");
            moveSpeed = 0f;
        }
        yield return new WaitForSeconds(0.75f);
        if(fishing == 4f){
            tool.durability -= 1;
            ItemData data = new ItemData();
            GameObject gm = GameObject.Find("GameManager");
            data = gm.GetComponent<Item_manager>().loadItemData("fish", data);
            this.gameObject.GetComponent<Inventory>().GetItem(data);
            moveSpeed = originSpeed;
            animator.SetFloat("Fishing", 0f);
        }
    }

    IEnumerator beSword(ItemData tool){
        CanUse = false;
        animator.SetTrigger("Sword");
        yield return new WaitForSeconds(0.45f);
        useSword(tool);
        yield return new WaitForSeconds(0.25f);
        CanUse = true;
    }
    IEnumerator beWater(ItemData tool){
        CanUse = false;
        animator.SetTrigger("Water");
        yield return new WaitForSeconds(0.15f);
        useWater(tool);
        yield return new WaitForSeconds(0.25f);
        CanUse = true;
    }

    void useWater(ItemData tool){
        GameObject selectbox = GameObject.Find("playerselectbox");
        if(selectbox.GetComponent<SelectEvent>().colliding != null){
            foreach(GameObject colliding in selectbox.GetComponent<SelectEvent>().collidingList){
                string collidingTag = colliding.tag;
                if(collidingTag == "garden"){
                    Garden crop = colliding.GetComponent<Garden>();
                    crop.getWater();
                    tool.durability = tool.durability-1;
                }
            }
        }
    }

    void useSword(ItemData tool){
        GameObject selectbox = GameObject.Find("playerselectbox");
        if(selectbox.GetComponent<SelectEvent>().colliding != null){
            foreach(GameObject colliding in selectbox.GetComponent<SelectEvent>().collidingList){
                string collidingTag = colliding.tag;
                if(collidingTag == "animal"){
                    if(colliding.name.Contains("chicken")){
                        ChickenAi chicken = colliding.GetComponent<ChickenAi>();
                        chicken.health -= 7;
                        chicken.Damaged();
                        tool.durability = tool.durability-1;
                    }
                }
            }
        }
    }



    IEnumerator beShovel(ItemData tool){
        CanUse = false;
        animator.SetTrigger("Shovel");
        yield return new WaitForSeconds(0.55f);
        useShovel(tool);
        yield return new WaitForSeconds(0.25f);
        CanUse = true;
    }
    IEnumerator beAxe(ItemData tool){
        CanUse = false;
        animator.SetTrigger("Axe");
        yield return new WaitForSeconds(0.55f);
        useAxe(tool);
        yield return new WaitForSeconds(0.25f);
        CanUse = true;
    }
    void useShovel(ItemData tool){
        GameObject selectbox = GameObject.Find("playerselectbox");
        if(selectbox.GetComponent<SelectEvent>().colliding == null){
            Vector3 garden_pos = selectbox.transform.position;
            GameObject gardenprefab = GameObject.Find("GameManager").GetComponent<Variables>().GardenPrefab;
            GameObject gardenObj = Instantiate(gardenprefab, garden_pos, Quaternion.identity);
            gardenObj.name = "Garden";
            tool.durability = tool.durability-1;
        }
    }
    void useAxe(ItemData tool){
        GameObject selectbox = GameObject.Find("playerselectbox");
        if(selectbox.GetComponent<SelectEvent>().colliding != null){
            if(selectbox.GetComponent<SelectEvent>().colliding.name.Contains("Tree")){
                GameObject tree = selectbox.GetComponent<SelectEvent>().colliding;
                tree.GetComponent<Tree>().Chop();
                tool.durability = tool.durability-1;
            }
        }
    }
    void setFacing()
    {
        if (movement.x == 0){
            return;
        }
        else if (movement.x > 0){
            facing = 1;
        }
        else{
            facing = -1;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "ladder_trigger")
        {
            Physics2D.IgnoreLayerCollision(11,8, true);
            CanFish = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "ladder_trigger")
        {
            Physics2D.IgnoreLayerCollision(11, 8, false);
            CanFish = false;
        }
    }
}
