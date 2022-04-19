using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//The scene manager Generates the layout of the level as well as handles the transitioning between menus and scenes
public class sceneManager : MonoBehaviour
{
    //a field used for the terrain prefab
    [SerializeField]
    GameObject terrain;

    //a field used for a list of varying track tiles that are in the prefab menu
    [SerializeField] private GameObject[] trackTile;

    //a field used for the prefab that plays a transition animation before loading the next scene
    [SerializeField]
    GameObject transition;

    //a field for the prefab of the start of the level 
    [SerializeField]
    GameObject sceneSpawn;

    //most of these public variables are meant to be private, only reload utilizes the public feature.
    
    //a counting variable that is used to calculate an updated position of leftTerrain, rightTerrain, and tile
    private float z;
    private float leftz;
    private float rightz;
    
    //a boolean reload that if set to true will load the lose Scen tied to the race level; should be renamed to raceLose
    public bool reload;
    
    //an int value used to set how long to generate the level map
    public int numGen;

    //a Vector3 used to keep track of the current spawn position of the Left terrain
    public Vector3 spawnLeft;
    
    //a Vector3 used to keep track of the current spawn position of the Right terrain
    public Vector3 spawnRight;
    
    //a Vector3 used to keep track of the current spawn position of the center track
    public Vector3 spawnCenter;
    
    //three Quaternions used to instantiate the respective terrain/tile rotations
    public Quaternion tileQ;
    public Quaternion terrainLeftQ;
    public Quaternion terrainRightQ;
    
    //an int that is used for random number generation
    private int ran;
    
    //a boolean used to load the winning screen of the race level; should be renamed to raceWin
    public bool loadScene;
    
    //an integer used to specify the upper range for randomly choosing a tile prefab
    private int numFabs;

    
    // Start is called before the first frame update
    
    void Start()
    {
        //an initalizing of the default values used in managing this scene

        numGen = 15;
        numFabs = 18;

        z = 40f;
        leftz = 40f;
        rightz = 60f;
        spawnCenter.Set(0, 0, z);
        spawnLeft.Set(-10, -.5f , leftz);
        spawnRight.Set(10, -.5f , rightz);
        tileQ.Set(0,0,0,1);
        terrainLeftQ.Set(0,0,0,1);
        terrainRightQ.Set(0,1,0,0);
        
        //calls the Generate function to randomly generate a course
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player loses load "RacingLose" scene
        if(reload){
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("RacingLose");
        }
        
        //a reset button for testing purposes as well as softlock prevention
        if(Input.GetKeyDown(KeyCode.R)){
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("Payne");
        }

        //if the player wins load "RacingWin" scene
        if(loadScene){
            SceneManager.LoadScene("RacingWin");
        }
    }

    //for the duration of numGen, generate next terrain/tile set and update the z counting position
    private void Generate(){
        for(int i = 0; i < numGen; i++){
            
            //random number used for next trackTile
            ran = Random.Range(0,numFabs);
            
            //picks a random prefab from the trackTile field and instantiates it
            Instantiate(trackTile[ran], spawnCenter, tileQ);
            
            //increment z by width of track/terrain
            z += 20f;
            spawnCenter.Set(0, 0, z);
            
            //generates a purely cosmetic and repeating terrain prefab to the left and right of the track tiles
            Instantiate(terrain, spawnLeft, terrainLeftQ);
            Instantiate(terrain, spawnRight, terrainRightQ);
            leftz += 40f;
            rightz += 40f;
            spawnLeft.Set(-10, -.5f, leftz);
            spawnRight.Set(10, -.5f, rightz);
            
            //random number used for next trackTile
            ran = Random.Range(0,numFabs);
            
            //generates a second random prefab from the trackTile field
            Instantiate(trackTile[ran], spawnCenter, tileQ);
            
            //increment z by width of track/terrain
            z += 20f;
            spawnCenter.Set(0, 0, z);
        }
        Instantiate(transition, spawnCenter, tileQ);
    }
}
