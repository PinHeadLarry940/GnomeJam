using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnifeEnabler : MonoBehaviour
{
    public GameObject Knife;
    // Start is called before the first frame update
    void Start()
    {
        Knife.SetActive(true);
    }

    void OnClick()
    {
        Debug.Log("stab");

    }
}
