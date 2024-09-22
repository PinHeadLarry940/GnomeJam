using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
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

   


    public TextMeshProUGUI test;
    public float TimeLeft;
    public bool TimerOn = false;

    PlayerMove playermove;
    PlayerMove2 playermove2;

    


    private void Awake()
    {
        twoplayermode = true;   

        if(twoplayermode) 
            knifeguy = Random.Range(0, 2);

        if (fourlayermode)
            knifeguy = Random.Range(0, 4);
    }

    private void Start()
    {
        TimerOn = true;
        currentRoundTime = roundtimer;
        TimeLeft = knifeTime;
        playermove = Player1.GetComponent<PlayerMove>();
        playermove2 = Player2.GetComponent<PlayerMove2>();

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
