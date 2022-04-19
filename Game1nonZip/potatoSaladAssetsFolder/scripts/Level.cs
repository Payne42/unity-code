using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//most of the tasks held in this function could be equated to a scene manager while working in tandem with the camera manager
//If/when I work on this project again, creating a proper scene manager out of this script would be one of my first tasks
public class Level : MonoBehaviour{
    // Start is called before the first frame update
    
    // a bool used to determine if the Main camera is on
    private bool mainCam;

    //a bool used to start the game when set to true and stop the game when set to false
    private bool start;

    //a float used to keep track of the current time. poorly named should be called current/currentTime
    private float value;
    
    //a float used to keep track of the highest score; longest time survived
    private float highScore;
    
    //test trying to get a mana display to work
    //private float mana;
    
    //a Text used to display the current Time while the game is active; should be renamed to timeText
    public Text currTime;
    
    //a text used to try and get a mana display functioning while the game is runnning
    public Text manaValue;

    //a Text used to display the current High score; should be renamed to highScore
    public Text score;

    //a string used for display purposes; changes depending if its the first time played or not; should be renamed
    public string s;
    
    //field for Wizard, mainly used to reset the Wizards position on game reset
    [SerializeField]
    GameObject Wizard;
    
    //field for temporaryPlatform, mainly used on game reset
    [SerializeField]
    GameObject temporaryPlatform;

    //two Vector3s and two Quaternions used for resetting the game; I mistakingly typed out a second Quaternion, q, that Quaternion is never used in this script
    private Vector3 wizardStart;
    private Vector3 platStart;
    private Quaternion setRot;
    private Quaternion q;
    
    //was used for testing purposes while trying to get a functioning mana display working
    private Player test;
    

    
    void Start()
    {
        //a basic set of instantiating base values
        s = "mana power";
        //mana = 10;
        value = 0;
        start = false;
        mainCam = true;
        setRot.Set(0f,0f,0f,1f);
        wizardStart.Set(0f,0f,0f);
        platStart.Set(0f,-1.25f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        // this part of level could have been made much easier with a simple scene manager, but this way is also passable
        // this simply checks the state of the game and either preps the level while on Mainmenu or prepares to swap to Mainmenu, using clearscreen and reset as functions
        // I could have also used public variables with the cameraManager, but I wanted to avoid global variable as much as possible
        // All of this happens when the player presses Z while on the Main menu or while the there are no wizards during the game time.
        if(Input.GetKeyDown(KeyCode.Z)){
            if(mainCam){
                mainCam = false;
                Reset();
                start = true;
            }
            else{
                if(GameObject.FindGameObjectsWithTag("Wizard").Length <= 0){
                    //mana =10;
                    value = 0;
                    clearScreen();
                    mainCam = true;
                }
            }
        }
        
        //This portion of update is used to calculate, keep track, and display the current time while playing, and display/check highscore
        if(start){
            value += Time.deltaTime;
        }
        
        //an attempt at trying to get the wizards current mana level to be displayed
        /*if((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && GameObject.FindGameObjectsWithTag("Wizard").Length > 0){
            if(mana > 0){
                mana--;
                manaValue.text = s + " = " + mana.ToString();
            }
        }*/
        
        
        double b = System.Math.Round(value, 2);
        currTime.text = b.ToString();
        
        //if the Wizard dies record data of the highscore to be displayed and wait for the player to reset the game by pressing R
        if(GameObject.FindGameObjectsWithTag("Wizard").Length == 0){
            start = false;
            if(value > highScore){
                        highScore = value;
                        double e = System.Math.Round(highScore, 2);
                        score.text = "High Score = " + e.ToString();
                    }
            clearScreen();
            if(Input.GetKeyDown(KeyCode.R)){
                if(!mainCam){
                    value = 0;
                    start = true;
                    Reset();
                }
            }
        }
    }
    
    //a function that destroys all "Fireball", "shield", "platform", and "Mana Ball" gameobjects
    public void clearScreen(){
       // Debug.Log("Do you wish to restart");
        GameObject[] list1 = GameObject.FindGameObjectsWithTag("Fireball");
        foreach (GameObject i in list1)
            {
                Destroy(i);
            }
        GameObject[] list2 = GameObject.FindGameObjectsWithTag("shield");
        foreach (GameObject j in list2)
            {
                Destroy(j);
            }
        GameObject[] list3 = GameObject.FindGameObjectsWithTag("platform");
        foreach (GameObject k in list3)
            {
                Destroy(k);
            }
        GameObject[] list4 = GameObject.FindGameObjectsWithTag("Mana Ball");
        foreach (GameObject v in list4)
            {
                Destroy(v);
            }
    }

    //a function that resets the game with a new Wizard and tempPlatform
    public void Reset(){
        Instantiate(Wizard, wizardStart, setRot);
        Instantiate(temporaryPlatform, platStart, setRot);
        //mana = 10;
        //manaValue.text = s + " = " + mana.ToString();
        
       // Debug.Log(GameObject.FindGameObjectsWithTag("Wizard").mana);
    }

}