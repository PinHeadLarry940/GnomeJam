using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumbleSounds : MonoBehaviour
{
    public AudioClip[] mumbles;
    private AudioClip currentmumble;
    private AudioSource audioSource;
    private int mumblecount;
    private int mumblerandom;

    private int mumblertime;


    // Start is called before the first frame update
    void Start()
    {
        mumblecount = mumbles.Length;
        audioSource = GetComponent<AudioSource>();
        Invoke(nameof(WaitforRandom), mumblertime);




    }

    private void WaitforRandom()
    {
        Debug.Log("play mumble");
        mumblertime = Random.Range(5, 9);
        mumblerandom = Random.Range(0, mumblecount);
        currentmumble = mumbles[mumblerandom];
        audioSource.clip = currentmumble;
        audioSource.Play();
        Invoke(nameof(WaitforRandom), mumblertime);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
