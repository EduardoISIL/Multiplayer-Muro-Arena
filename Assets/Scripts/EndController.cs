using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlaySound(3);
            var lista_players = FindObjectsOfType<PlayerController>();
            for (int i = 0; i < lista_players.Length; i++)
            {
                lista_players[i].SendScore();
            }
            Destroy(gameObject);
        }
    }
}
