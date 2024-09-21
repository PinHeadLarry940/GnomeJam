using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeObject : MonoBehaviour
{
    // Start
    void Start()
    {
        Debug.Log("I, Am a knife");
    }

    /*When we call the stab event we want to see the knife move forward then backward in a stabbing motion
    we would also like for the knife to check what is in front of it and if it is a player we want to kill that player*/
    public void Stab()
    {
        transform.position = new Vector3(1, 0, 1);
    }
    
    //this needs to happen when the stab event is called. This way we are only checking whats in front of us and what it is when we go to stab
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
