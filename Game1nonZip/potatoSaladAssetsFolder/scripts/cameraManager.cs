using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    // a camera used to display exclusively the Mainmenu UI
    public GameObject cam1;
    
    // a camera used to display exclusively the active game
    public GameObject cam2;
    
    //a private boolean to check to see if the main menu is currently active
    private bool menuOn;
    
    void Start()
    {
        //set the Main menu camera as active
        cam1.SetActive(true);
        
        //set the game camera to inactive
        cam2.SetActive(false);
        
        //set the private boolean to true, in order to showcase that the Mainmenu is Active
        menuOn = true;
        
    }

    // Update is called once per frame
    //works well as a camera manager but a proper scene manager would probably work better.
    //though for as simple of a state the game is currently in, changing scenes may cause uneccesary lag
    void Update()
    {
        //if the player presses Z while on the MainMenu start the game and set all cameras to their respective states
        //if the player has died and is still on the game camera put them back to the main menu and set all cameras to there respective states
        if(Input.GetKeyDown(KeyCode.Z)){
            //if camera main menu is on
            if(menuOn){
                cam1.SetActive(false);
                cam2.SetActive(true);
                menuOn = false;
            }
            //if game menu is on and player is dead
            else{
                if(GameObject.FindGameObjectsWithTag("Wizard").Length == 0){
                    cam1.SetActive(true);
                    cam2.SetActive(false);
                    menuOn = true;
                }
            }
        }
    }
}
