using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;
using TMPro;

public class PauseController : MonoBehaviour
{

    //variables
    public bool canPause;
    
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //when to pause
    void Pause()
    {
        Debug.Log("the game is paused");
        //if the bool is turned on by either of the inputs then we will pause the game. should turn off the ability to pause again.
        //going to try to use the new input system to make this work
    }
}
