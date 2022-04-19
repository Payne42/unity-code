using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMovement : MonoBehaviour
{
    // field to spawn the anvil prefab used for the transitioning scene
    [SerializeField]
    private GameObject anvil;

    // field used to for a transform above the car in order to spawn the anvil at any position; doesn't need to properly exist
    [SerializeField]
    private GameObject spawner;

    //field used to access the sceneManager
    [SerializeField]
    private sceneManager s;


    //all of these variable are meant to be private except for fail

    //limiter for how fast the boost pad can increase speed
    public int speedLimit = 20;

    //base speed for the car
    public float speed = 16f;

    //a float used to represent moving left or right
    public float horizontal; 

    //a bookeeping float to keep track of the original starting speed
    private float initial;

    //a boolean that when true will slowly adjust the speed of the car to go back to its inital speed
    private bool decay = false;
    
    //a float that represents the rate at which the speed of decay is adjusted
    private float decayRate;

    //a boolean that is used to determine if the player has passed the level
    public bool success = false;
    
    // a boolean that when true will slowly increase the players speed to their inital speed
    public bool needSpeed = false;
    
    //a boolean that will tell the scene manager that the player has failed and load the "RacingLose" Scene
    public bool fail = false;


    
    
    // Start is called before the first frame update
    void Start()
    {
        // a set of basic intializations for speed and decayRate
        initial = speed;
        decayRate = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //while the player has neither won nor lost allow the player to change their horizontal position
        if(!success){
            horizontal = Input.GetAxis("Horizontal");
        }
        //if decay is true, call speedReturn to bring the PLayers speed back to normal
        if(decay){
            speedReturn();
        }
        //Debug.Log(speed);
    }

    private void FixedUpdate(){
        //change Players position based around horizontal and speed
        GetComponent<Rigidbody>().velocity = new Vector3(horizontal * 2.25f, GetComponent<Rigidbody>().velocity.y, speed);
        //call speedReturn if the Player has touched a boost pad
        if(needSpeed){
            speedReturn();

        }
    }

    void OnTriggerEnter(Collider ob){
        //if the player collides with a "pad" increase its speed by 2
        if(ob.tag == "pad"){
            if(speed < speedLimit){
                speed  += 2;
            }
        }
        //if the player collides with "ending freeze its position and drop an anvil on it"
        if(ob.tag == "ending"){
            Rigidbody a = GetComponent<Rigidbody>();
            speed = 0;
            success = true;
            a.constraints = RigidbodyConstraints.FreezePositionX;
            Instantiate(anvil, spawner.transform.position, Quaternion.identity);
        }
        //if the PLayer collides with an anvil set flag to load "RaceWin" scene
        if(ob.tag == "Anvil"){
            s.loadScene = true;

        }
    }
        
    //on exit trigger with "pad" and "wall" objects set decay and needSpeed to true respectively
    //I realized after the fact that decay and needSpeed do the exact same thing so this is mildly redundant
    void OnTriggerExit(Collider ob){
        if(ob.tag == "pad"){
            decay = true;
        }
        if(ob.tag == "wall"){
            Destroy(ob.gameObject);
            speed = 0f;
            needSpeed = true;
        }
    }

    // a function that slowly adjusts the Players speed back to its inital speed
    private void speedReturn(){
        if(speed < (initial - .5f)){
            speed += 0.2f;
        }
        else{
            if(speed > (initial + .5f)){
            speed -= 0.2f;
        }
        else
            speed = initial;
            decay = false;
            needSpeed = false;
        }
    }

}
