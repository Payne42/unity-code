using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a cannon script attached to a cannon prefab; that allows it to randomly shoot manaballs and fireballs
public class Cannon : MonoBehaviour
{
    //a boolean that's used to determine if the camera is on the correct screen
    private bool mainCam;
    
    // a private float used to control the rate at which projectiles spawn
    private float spawnInterval;
    
    //a private float used to control the speed that projectiles move
    private float launchSpeed;
    
    // a field used to access a fireball prefab in order to allow the respective object
    [SerializeField]
    GameObject Fireball;
    
    // a field used to access a manaball prefab in order to allow the respective object
    [SerializeField]
    GameObject ManaBall;

    //a base Quaternion to allow all projectile gameobjects to be instantiated in the same orientation
    private Quaternion q;

    //a base vector3 to allow all projectile gameobjects to be instantiated at the same position in respect to its cannon
    private Vector3 shootPos;
    

    // Start is called before the first frame update
    void Start()
    {
        // intializes the fields of the above variables to this particular stat block
        mainCam = true;
        launchSpeed = 100;
        spawnInterval = Random.Range(1f,5f);
        shootPos = transform.position;
        shootPos.z -= 1.5f;   
        q.Set(0f,0.707106829f,0f,0.707106829f);
        
    }

    // Update is called once per frame
    void Update(){
        //a check to see if the player is on the main menu or not, can be improved with a scene manager
        if(Input.GetKeyDown(KeyCode.Z)){
            if(mainCam){
                mainCam = false;
            }
            else{
                if(GameObject.FindGameObjectsWithTag("Wizard").Length <= 0){
                    mainCam = true;
                }
            }
        }
     }

    private void FixedUpdate(){
            // while there is a wizard alive and game is active, cannons will constantly shoot projectiles
            // can be greatly improved with a coroutine, but a dealine prevented me from doing this the proper way
            if(GameObject.FindGameObjectsWithTag("Wizard").Length > 0 && !mainCam){
                //decrementing spawninterval each frame as a timer for each shot the cannon makes
                //unless it is zero or less, in which case the cannon will shoot and reset spawnInterval randomly.
                if(spawnInterval <= 0){
                    Shoot();
                    
                    //randomize the spawn interval for more variability
                    spawnInterval = Random.Range(3f, 5f);
                }
                else{
                    spawnInterval -= Time.deltaTime;
                }
            }
    }

    //a function to destroy a gameobject after a set time has passed, mildly uneccesary but still slightly useful
    private void destroyTimer(GameObject x){
        Destroy(x, 30);
    }
    //a function that will randomly shoot fireballs with a 20% chance to replace a fireball with a manaball 
    public void Shoot(){
            int z = Random.Range(1, 10);
            if(z <= 2){
                GameObject x = Instantiate(ManaBall, shootPos, q);
                x.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(launchSpeed, 0, 0));
                //destruction timer to prevent lag
                destroyTimer(x);
            }
            else{
                GameObject x = Instantiate(Fireball, shootPos, q);
                x.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(launchSpeed, 0, 0));
                //destruction timer to prevent lag
                destroyTimer(x);
            }
            //can be cleaned up slightly, but is still functional
    }

    
}
