using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public float knifeTime = 30f;
    private float currentKnifeTime = 0f;

    public float roundtimer = 240f;
    private float currentRoundTime = 0f;

    public float knifeguy;
    private bool twoplayermode;
    private bool fourlayermode;


    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject playerKnife1;
    public GameObject playerKnife2;
    public GameObject playerKnife3;
    public GameObject playerKnife4;

    public Transform playerspawn1;
    public Transform playerspawn2;
    public Transform playerspawn3;
    public Transform playerspawn4;


    public Transform[] Playerspawns;
    private int spawncount;
    private int currentspawn1;
    private int currentspawn2;

    public TextMeshProUGUI test;
    public float TimeLeft;
    public bool TimerOn = false;

    PlayerMove playermove;
    PlayerMove2 playermove2;

    


    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        twoplayermode = true;   

        if(twoplayermode) 
            knifeguy = Random.Range(0, 2);

        if (fourlayermode)
            knifeguy = Random.Range(0, 4);
    }

    private void Start()
    {
        spawncount = Playerspawns.Length;
        

        TimerOn = true;
        currentRoundTime = roundtimer;
        TimeLeft = knifeTime;
        playermove = Player1.GetComponent<PlayerMove>();
        playermove2 = Player2.GetComponent<PlayerMove2>();
        MoveToSpawn();

        if (knifeguy == 0)
        {
            playerKnife2.SetActive(false);

            playermove2 = Player2.GetComponent<PlayerMove2>();
            playermove2.readytoatk = false;
            playermove.readytoatk = true;
            playerKnife1.SetActive(true);

        }


        else if (knifeguy == 1)
        {
            playerKnife1.SetActive(false);

            playermove = Player1.GetComponent<PlayerMove>();
            playermove.readytoatk = false;
            playermove2.readytoatk = true;
            playerKnife2.SetActive(true);
        }


    }

    public void RandomSpawn()
    {
        currentspawn1 = Random.Range(0, spawncount);
        currentspawn2 = Random.Range(0, spawncount);

        if (currentspawn1 == currentspawn2)
            currentspawn1 = currentspawn2 - 1;

        playerspawn1 = Playerspawns[currentspawn1];
        playerspawn2 = Playerspawns[currentspawn2];
    }


    public void MoveToSpawn()
    {
        RandomSpawn();
        //move all the players to their spawn points depending on the gamemode
        if (twoplayermode)
        {
            Rigidbody Player1RB = Player1.GetComponent<Rigidbody>();
            Rigidbody Player2RB = Player2.GetComponent<Rigidbody>();
            Player1RB.MovePosition(playerspawn1.position);
            Player2RB.MovePosition(playerspawn2.position);
        }
        if (fourlayermode)
        {
            Rigidbody Player1RB = Player1.GetComponent<Rigidbody>();
            Rigidbody Player2RB = Player2.GetComponent<Rigidbody>();
            Rigidbody Player3RB = Player3.GetComponent<Rigidbody>();
            Rigidbody Player4RB = Player4.GetComponent<Rigidbody>();
            Player1RB.MovePosition(playerspawn1.position);
            Player2RB.MovePosition(playerspawn2.position);
            Player3RB.MovePosition(playerspawn3.position);
            Player4RB.MovePosition(playerspawn4.position);
        }
    }



   
    private void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
            }
        }
        else
            Invoke(nameof(ResetTimer), 1f);



    }
    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        test.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ResetTimer()
    {
        TimerOn = true;
        TimeLeft = knifeTime;
        if (twoplayermode)
            knifeguy = Random.Range(0, 2);

        else if (fourlayermode)
            knifeguy = Random.Range(0, 4);

        if (knifeguy == 0)
        {
            playerKnife2.SetActive(false);

            playermove2 = Player2.GetComponent<PlayerMove2>();
            playermove2.readytoatk = false;
            playermove.readytoatk = true;
            playerKnife1.SetActive(true);

        }
        

        else if (knifeguy == 1)
        {
            playerKnife1.SetActive(false);

            playermove = Player1.GetComponent<PlayerMove>();
            playermove.readytoatk = false;
            playermove2.readytoatk = true;

            playerKnife2.SetActive(true);
        }
          



    }

}
