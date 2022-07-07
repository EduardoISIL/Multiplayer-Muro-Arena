using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

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

    //new
    [SerializeField] private TextMeshProUGUI txtScore;
    int playerScore = 0;

    Vector3 posSaved;
    private void Awake()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        SpriteRndr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        var lista_textos = FindObjectsOfType<TextMeshProUGUI>();
        txtScore = lista_textos[0];

        //txtScore = GameObject.Find("txt Score").GetComponent<TextMeshProUGUI>();
        txtScore.text = " x" + playerScore;

        _transform = GetComponent<Transform>();
        if (GameObject.Find("Player 1")) _transform.name = "Player 2";
        else if (GameObject.Find("Player(Clone)")) _transform.name = "Player 1";

        posSaved = GameObject.Find("SpawnPosition").transform.position;

    }
    void Update()
    {
        if (photonView.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
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
    public void SavePosition(Vector3 thisPos)
    {
        posSaved = thisPos;
    }

    public void UpdatePosition()
    {
        this.transform.position = posSaved;
    }

    private void FixedUpdate() // handle physics stuff here
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = collision.transform;
        }

        if (collision.gameObject.tag == "Coin")
        {
            if (photonView.IsMine)
            {
                playerScore++;
                txtScore.text = " x" + playerScore;

                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlaySound(1);
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
    }
}

