using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    //The thing we want to follow
    private Transform followed;
    public void LookOutForThePlayer()
    {
        followed = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        //Set our position equal to the position of the player
        if (followed != null)
        {
            Vector3 newPosition = followed.position;
            newPosition.z = transform.position.z;

            //Set our position equal to the new position
            transform.position = newPosition;
        }
    }
}
