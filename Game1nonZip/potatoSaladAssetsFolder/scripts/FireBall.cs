using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for the purpose of time and simplicity I made the fireball and manaball different scripts, but
//it would have been just as easy to make it one script that handles both objects.
//I did it this specific way, because I was origianlly not planning on implementing manaballs
//I simply wanted to test the concept by creating a seperate script as a test and it worked exactly as I wanted it to.
//If I needed to work on either gameobject more/ add another variant, I would have created an interface that implements each script.
public class FireBall : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //a debug option that allows the tester to clear all fireball objects from the screen by pressing T
        //This left on during the full build for demonstrative purposes.
        //Originally wanted to make a debug menu, but due to deadline constraints I opted to just leave it active.
        if(Input.GetKeyDown(KeyCode.T)){
            destroyTimer();
        }
    }

    //a checker to destroy either the wizard or the fireball on collision of a set of objects
    private void OnCollisionEnter(Collision other){
        //
        //if the fireball collides with a "protect", lag protecting wall, or a "shield", GameObject spell controled by the player, destroy the fireball.
        if(other.gameObject.tag == "protect" || other.gameObject.tag == "shield"){
            Destroy(this.gameObject);
        }
        //if the fireball collides with a "Wizard", player object, destroy player.
        if(other.gameObject.tag == "Wizard"){
            Destroy(other.gameObject);
        }
    }
    
    //uneccesary function, but I was originally going to put a specific time interval here.
    private void destroyTimer(){
        Destroy(gameObject);
    }
}
