using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            print("Pos saved: " + this.transform.position);
            //2 es el index de checkpoint
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlaySound(2); 
            collider.GetComponent<PlayerController>().SavePosition(this.transform.position);
        }
    }
}
