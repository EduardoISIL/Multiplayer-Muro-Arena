using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

using Photon.Pun;
using UnityEngine.UI;

public class GameController : MonoBehaviourPunCallbacks
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip checkPointSound;
    [SerializeField] private AudioClip sawSound;

    [SerializeField] private AudioSource tuParlante;

    [SerializeField] private TextMeshProUGUI RelojCounterHUD;
    [SerializeField] private int RelojGame = 400;
    [HideInInspector] public bool isGameOver = false;

    //[SerializeField] private TextMeshProUGUI txtScore;
    //int playerScore = 0;
    // Start is called before the first frame update

    int index = 0;
    List<int> Scores = new List<int>(2);
    List<string> Names = new List<string>(2);

    private TextMeshProUGUI txtResultGame;
    private GameObject imgGOResult;
    private Image imgResult;

    void Start()
    {
        //txtScore.text = " x" + playerScore;
        RelojCounterHUD.text = "" + RelojGame;
        InvokeRepeating("ReduceValue", 0, 1);
        //photonView.RPC("End", RpcTarget.AllBufferedViaServer);

        txtResultGame = GameObject.Find("txt End").GetComponent<TextMeshProUGUI>();
        txtResultGame.text = "";
        imgGOResult = txtResultGame.transform.parent.gameObject;
        imgResult = imgGOResult.gameObject.GetComponent<Image>();
        imgResult.color = new Color32(255, 255, 255, 0);


    }
    public void ReduceValue()
    {
        RelojGame--;
        RelojCounterHUD.text = "" + RelojGame;
    }
    private void Update()
    {
        if (RelojGame <= 0 && isGameOver == false)
        {
            isGameOver = true;
            RelojGame = 0;
            print("Fin del juego");
        }
    }
    public void PlaySound(int index)
    {
        switch(index)
        {
            case 1: //coin
                tuParlante.PlayOneShot(coinSound);
                break;
            case 2: //check point
                tuParlante.PlayOneShot(checkPointSound);
                break;
            case 3: //Win
                tuParlante.PlayOneShot(sawSound);
                break;
            case 4: //Saw
                tuParlante.PlayOneShot(sawSound);
                break;

        }
    }


    
    public void End(int points, string name)
    {
        CancelInvoke();
        index++;
        print("index: " + index);
        Scores.Add(points);
        Names.Add(name);
        string winner = "";

        if(index >= 2)
        {
            print("Scores index " + 1 + ": " + Scores[0] + " Player: " + Names[0]); //photon name
            print("Scores index " + 2 + ": " + Scores[1] + " Player: " + Names[1]); //photon name
            if (Scores[0] > Scores[1]) //gano Jugador 1
            {
                print("gano Jugador 1");
                winner = Names[0];
                //GameObject.Find(Names[0]).GetComponent<PlayerController>().PlayerWON();
                //GameObject.Find(Names[1]).GetComponent<PlayerController>().PlayerLOSE();
            }
            else if (Scores[0] < Scores[1]) //gano Jugador 2
            {
                print("gano Jugador 2");
                winner = Names[1];
                //GameObject.Find(Names[1]).GetComponent<PlayerController>().PlayerWON();
                //GameObject.Find(Names[0]).GetComponent<PlayerController>().PlayerLOSE();
            }
            else //empate
            {
                print("Empate");
                winner = "Both players!";
                //GameObject.Find(Names[0]).GetComponent<PlayerController>().PlayerTIE();
                //GameObject.Find(Names[1]).GetComponent<PlayerController>().PlayerTIE();
            }
            imgResult.color = new Color32(0, 0, 0, 100);
            txtResultGame.text = "Resultados: \n " + "El jugador: " + Names[0] + " tuvo " + Scores[0] + " pts \n"
                                                   + "El jugador: " + Names[1] + " tuvo " + Scores[1] + " pts \n"
                                                   + "Winner is " + winner;
        }

    }
}
