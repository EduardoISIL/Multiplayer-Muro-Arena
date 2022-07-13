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
    int canMove = 1;

    //new
    private TextMeshProUGUI txtScore;
    int playerScore = 0;

    private TextMeshProUGUI txtResultGame;
    private GameObject imgResult;

    //Position
    Vector3 posSaved;
    private void Awake()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        SpriteRndr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //txtScore = GameObject.Find("txt Score").GetComponent<TextMeshProUGUI>();
        var lista_textos = FindObjectsOfType<TextMeshProUGUI>();
        txtScore = lista_textos[1];
        txtScore.text = " x" + playerScore;

        txtResultGame = lista_textos[2];
        txtResultGame.text = "";
        imgResult = txtResultGame.transform.parent.gameObject;
        imgResult.SetActive(false);

        

        _transform = GetComponent<Transform>();
        if (GameObject.Find("Player 1")) _transform.name = "Player 2";
        else if (GameObject.Find("Player(Clone)")) _transform.name = "Player 1";

        posSaved = GameObject.Find("SpawnPosition").transform.position;

        PhotonNetwork.LocalPlayer.NickName = "Player " + PhotonNetwork.LocalPlayer.ActorNumber;
        this.transform.name = "Player " + PhotonNetwork.LocalPlayer.ActorNumber;
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
            _transform.position += mov * Speed * Time.deltaTime * canMove; //si termino el juego, canMove se vuelve 0
        }

    }
    public void Jumping()
    {
        rgbd2D.velocity = Vector2.up * Jump;
    }
    private void FixedUpdate() // handle physics stuff here
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            if (photonView.IsMine)
            {
                playerScore +=1;
                txtScore.text = " x " + playerScore;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlaySound(1);
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Checkpoint")
        {
            if (photonView.IsMine)
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlaySound(2);
                this.SavePosition(this.transform.position);
            }
        }
        if (collision.gameObject.tag == "End")
        {
            if (photonView.IsMine)
            {
                playerScore += 5;
                txtScore.text = " x " + playerScore;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlaySound(3);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().End(playerScore, PhotonNetwork.LocalPlayer.NickName);
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
    }
    public void SavePosition(Vector3 thisPos)
    {
        print("Se activo el SavePosition");
        posSaved = thisPos;
    }
    public void UpdatePosition()
    {
        this.transform.position = posSaved;
    }
    public void StopMoving()
    {
        canMove = 0;
    }
    public void PlayerWON()
    {
        imgResult.SetActive(true);
        txtResultGame.text = "Congratulations you WON!";
    }
    public void PlayerLOSE()
    {
        imgResult.SetActive(true);
        txtResultGame.text = "Eliminated";
    }
    public void PlayerTIE()
    {
        imgResult.SetActive(true);
        txtResultGame.text = "Tie!";
    }
}

