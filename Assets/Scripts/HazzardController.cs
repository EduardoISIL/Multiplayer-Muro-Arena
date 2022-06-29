using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazzardController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag =="Player")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlaySound(3);
            collider.GetComponent<PlayerController>().UpdatePosition();
            //Deal damage to the Destructible we touched!
        }
    }
}
