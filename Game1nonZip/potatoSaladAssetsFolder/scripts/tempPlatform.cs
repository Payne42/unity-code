using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is uneeded and can very simply be moved to the Level script
//The only reason why this script is still here is that I needed to focus my time and effort to polishing the game
//rather than cleaning up functioning code.
//This script will not cause any noticable performance or gameplay issues, so I spent my time making sure that the game was functional
public class tempPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //destroys the temporary block that the player spawns on after 3 seconds
        Destroy(gameObject, 3);
    }
}
