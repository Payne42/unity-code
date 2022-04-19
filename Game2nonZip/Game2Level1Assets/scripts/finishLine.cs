using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLine : MonoBehaviour
{
    
    //speed that the finish line moves; should be renamed to finish line speed; also should be private
    public float scale = 10f;
    
    //field for the carMovement script to record failure
    [SerializeField]
    carMovement x;
    
    //field to get the cars RigidBody for freezing its position
    [SerializeField]
    GameObject car;
    
    //field to set the reload boolean to true in order to load "RacingLose" scene
    [SerializeField]
    sceneManager scene;

    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate(){
        
        //moves the finish line at a set speed that is slightly slower than the players base speed
        GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, scale);
        
        //if the player fails freeze both the car and finish line
        //was used to demonstrate fail before the raceLose scene was implemented
        if(x.fail){
            Rigidbody a = car.GetComponent<Rigidbody>();
            Rigidbody b = GetComponent<Rigidbody>();

            a.constraints = RigidbodyConstraints.FreezePositionX;
            a.constraints = RigidbodyConstraints.FreezePositionY;
            a.constraints = RigidbodyConstraints.FreezePositionZ;
            b.constraints = RigidbodyConstraints.FreezePositionX;
            b.constraints = RigidbodyConstraints.FreezePositionY;
            b.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }

    //when the player collides with the finish line set fail to true and the car's reload to true
    //not the best way to handle the fail but functional given the time constraints
    void OnTriggerEnter(Collider ob){
        if(ob.tag == "Player"){
            x.fail = true;
            scene.reload = true;
        }
    }
}
