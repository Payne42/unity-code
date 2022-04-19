using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// a script that controls the attributes of the player
//when I first started this game I created a wizard object as a prefab and tied the Player script to that prefab
//this gave me pleanty of issues when developing the project, and prevented me from implementing several features
//If/when I come back to this project the first change I would make would be correcting this mistake.
public class Player : MonoBehaviour
{
    //a boolean that determines if the player has jumped
    private bool jump;

    //a boolean used to spawn a platform
    private bool plat;

    //a boolean used to instantiate a shield
    private bool protect;

    //a float that is adjusted negatively by pressing 'A' and positvely by pressing 'D'
    //used to adjust the players current position
    private float horizontal;
    
    //a float that was intended to control the player position similarly to horizontal
    //is a remenent of a feature that I decided to cut in order to better the game's design
    private float depth;

    //an int used to limit the mana a player can hold, specifically fo balance reasons
    private int manaLimit;

    //a Vector3 used for instantiating a platform. should be renamed to platPos or platSpawn
    private Vector3 currPos;

    //a Vector3 used for instantiating a shield. should be renamed to shieldPos or shieldSpawn
    private Vector3 procPos;
    
    //an unused Vector3 that was meant to help fix a design mistake
    private Vector3 spawn;

    //a basic Quaternion used for every instantiation
    private Quaternion q;
    
    //an int used to measure the current amount of mana a player has
    public int mana;
    
    //a field used for the platform prefab
    [SerializeField]
    private GameObject _platform;
    
    //a field used for the Wizard prefab
    [SerializeField]
    private GameObject Wizard;

    //a field used for the shield prefab
    [SerializeField]
    private GameObject _shield;

    //a field for a transform used to stop the player from getting stuck on geometry.
    //this doesn't need to be a field since the transform is attached to the player, but I was in the middle of testing SerializedField
    [SerializeField]
    private Transform checker;
    
    // a field for a layermask used in addition with checker to stop the player from getting stuck on geometry.
    [SerializeField]
    private LayerMask platLayer; 

    // Start is called before the first frame update
    
    //sets the manaLimit to 30 and give the player a slight bit of mana to survive on
    void Start()
    {
        manaLimit = 30;
        mana = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //sets the jump bool to true when space is pressed
        if(Input.GetKeyDown(KeyCode.Space)){
                jump = true;
        } 
        //sets the plat bool to true and prepares instantiation when pressing E
        //also checks to see if the player has the right amount of mana to cast spell
        if(Input.GetKeyDown(KeyCode.E)){
            if(mana > 0){
                mana--;
                plat = true;
                currPos = transform.position;
                currPos.y = -1.7f;
            }
        }
        //sets the protect bool to true and prepares instantiation when pressing Q
        //also checks to see if the player has the right amount of mana to cast spell
        if(Input.GetKeyDown(KeyCode.Q)){
            if(mana > 0){
                mana--;
                protect = true;
                procPos = transform.position;
                procPos.z += 1.5f;
            }
        }  
        //updates the horizontal and depth every frame in order to change Players position
        horizontal = Input.GetAxis("Horizontal");
        depth = Input.GetAxis("Vertical");
    }


    private void FixedUpdate(){
        
        //adjusts the player/Wizard's position
        GetComponent<Rigidbody>().velocity = new Vector3(horizontal * 2, GetComponent<Rigidbody>().velocity.y, depth * 2);

        //checks to see if the player can jump or not and executes the appriate response
        if(Physics.OverlapSphere(checker.position, 0.1f, platLayer).Length > 0){
            if(jump){
                GetComponent<Rigidbody>().AddForce(Vector3.up * 4.5f, ForceMode.VelocityChange);
                jump = false;
            }
        }

        //uses the plat bool to spawn a platform at the currPos location
        if(plat){
            GameObject x = Instantiate(_platform, currPos, Quaternion.identity);
            destroyTimer(x);
            plat = false;
        } 

        //uses the protect bool to spawn a shield at the procPos
        if(protect){
            //I mistakingly put this q.set() in the wrong location
            q.Set(0.707106829f,0,0,0.707106829f);
            GameObject x = Instantiate(_shield, procPos, q);
            destroyTimer(x);
            protect = false;
        } 
    }

    //a timer to destroy shields and platforms after 5 seconds.
    //the five should also be variable, to make debugging and balancing easier
    private void destroyTimer(GameObject x){
        if(x != Wizard){
            Destroy(x, 5);
        }
    }
    
    //a collision checker that checks to see if the Wizard collides with a "KillWizard" object and if so destroys Player object
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "killWizard"){
            Destroy(gameObject);
        }
    }
    
    //a trigger checker that checks to see if the Wizard collides with a "Mana Ball" object and if so adds mana to player object
    private void OnTriggerEnter(Collider other){
            if(other.gameObject.tag == "Mana Ball"){
                if(mana < manaLimit){
                    mana += 5;
                }
                //debug function that prints current mana in order to see if the mana bar was working
                Debug.Log(mana);
            }
    }
    
    //a unused playerDeath function that was going to cause an event to happen on player death, such as moving to a losing screen
    private void playerDeath(){

    }

}
