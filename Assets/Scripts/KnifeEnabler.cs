using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnifeEnabler : MonoBehaviour
{
    public GameObject Knife;
    public KnifeObject KnifeObject;
    /* If this is the correct player to start with the knife we will enable the knife
    If this is the correct player then we also want to start a counter on them for roughly 60 seconds, at the end of this timer the knife gets disabled,
    then we send a message to another player for their knife to be turned on*/
    void Start()
    {
        Knife.SetActive(true);
    }

    //When the player clicks their mouse we will send the message to the knife to start the stabbing
    void OnClick()
    {
        Debug.Log("stab");
        KnifeObject.Stab();
    }
}
