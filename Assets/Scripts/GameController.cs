using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip checkPointSound;
    [SerializeField] private AudioClip sawSound;

    [SerializeField] private AudioSource tuParlante;

    [SerializeField] private TextMeshProUGUI txtScore;
    int playerScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        txtScore.text = " x" + playerScore;
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
            case 3: //Saw
                tuParlante.PlayOneShot(sawSound);
                break;

        }
    }
    public void UpdateScore()
    {
        playerScore++;
        txtScore.text = " x" + playerScore;
    }
}
