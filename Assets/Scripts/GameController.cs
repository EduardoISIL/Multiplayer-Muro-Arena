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
            case 3: //Saw
                tuParlante.PlayOneShot(sawSound);
                break;

        }
    }

    //public void UpdateScore()
    //{
    //    playerScore++;
    //    txtScore.text = " x" + playerScore;
    //}
}
