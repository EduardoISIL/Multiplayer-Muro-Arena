using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private GameController gameController; 
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            //1 es el index de coin
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlaySound(1);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().UpdateScore();
            Destroy(gameObject);
        }
    }
}
