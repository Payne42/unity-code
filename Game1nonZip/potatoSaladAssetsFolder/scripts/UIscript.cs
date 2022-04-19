using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIscript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static UIscript instance;

    void Awake(){
        instance = this;
    }

    public Text manaShow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
