using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
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
    List<int> Scores = new List<int>();
    List<string> Names = new List<string>();

    void Start()
    {
        //txtScore.text = " x" + playerScore;
        RelojCounterHUD.text = "" + RelojGame;
        InvokeRepeating("ReduceValue", 0, 1);
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
            //StartCoroutine(gameOver("GameOverJ"));
            //SceneManager.LoadScene("GameOverJ");
        }
    }
    //IEnumerator gameOver(string name)
    //{
    //    print("Se inicio Co-rutina Game Over");
    //    yield return new WaitForSeconds(1);
    //    SceneManager.LoadScene(name);
    //}

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
        index++;
        Scores.Add(points);
        Names.Add(name);
        print("Scores index " + index + ": " + points + " Player: " + name); //photon name

        if(index >= 2)
        {
            if (Scores[0] > Scores[1]) //gano Jugador 1
            {
                print("gano Jugador 1");
                GameObject.Find("Player 1").GetComponent<PlayerController>().PlayerWON();
                GameObject.Find("Player 2").GetComponent<PlayerController>().PlayerLOSE();
            }
            else if (Scores[0] < Scores[1]) //gano Jugador 2
            {
                print("gano Jugador 2");
                GameObject.Find("Player 2").GetComponent<PlayerController>().PlayerWON();
                GameObject.Find("Player 1").GetComponent<PlayerController>().PlayerLOSE();
            }
            else //empate
            {
                print("Empate");
                GameObject.Find("Player 1").GetComponent<PlayerController>().PlayerTIE();
                GameObject.Find("Player 2").GetComponent<PlayerController>().PlayerTIE();
            }
        }

    }
}
