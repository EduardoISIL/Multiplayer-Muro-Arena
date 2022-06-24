using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float Speed = 10;
    private Transform _transform;

    private Rigidbody2D rgbd2D;
    private SpriteRenderer SpriteRndr;

    [SerializeField] private float Jump;

    bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundlayer;

    private void Awake()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        SpriteRndr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        if (GameObject.Find("Player 1")) _transform.name = "Player 2";
        else if (GameObject.Find("Player(Clone)")) _transform.name = "Player 1";

    }
    void Update()
    {
        if (photonView.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                print("Espacio");
                if(isGrounded) Jumping(); //
            }
            float X = Input.GetAxisRaw("Horizontal");
            float Y = Input.GetAxisRaw("Vertical");

            Vector3 mov = _transform.right * X + _transform.forward * Y;
            _transform.position += mov * Speed * Time.deltaTime;
        }

    }
    public void Jumping()
    {
        rgbd2D.velocity = Vector2.up * Jump;
    }
}

