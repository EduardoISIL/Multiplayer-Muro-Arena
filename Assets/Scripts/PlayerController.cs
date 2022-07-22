using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

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
    [HideInInspector] public static int playerScore = 0;

    //Position
    Vector3 posSaved;

    private void Awake()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        SpriteRndr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        var lista_textos = FindObjectsOfType<TextMeshProUGUI>();
        txtScore = GameObject.Find("txt Score").GetComponent<TextMeshProUGUI>();
        txtScore.text = " x" + playerScore;

        _transform = GetComponent<Transform>();
        //if (GameObject.Find("Player 1")) _transform.name = "Player 2";
        //else if (GameObject.Find("Player(Clone)")) _transform.name = "Player 1";

        posSaved = GameObject.Find("SpawnPosition").transform.position;

        //PhotonNetwork.LocalPlayer.NickName = "Player " + PhotonNetwork.LocalPlayer.ActorNumber;
        this.transform.name = "Player " + "2";
        if (photonView.IsMine)
        {
            string idText = "" + PhotonNetwork.LocalPlayer.UserId;
            string idText4 = idText.Substring(0, 4);
            //this.transform.name = "Player " + idText4;
            this.transform.name = "Player " + 1;
        }

    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded) Jumping(); //
            }
            float X = Input.GetAxisRaw("Horizontal");
            float Y = Input.GetAxisRaw("Vertical");

            Vector3 mov = _transform.right * X + _transform.forward * Y;
            _transform.position += mov * Speed * Time.deltaTime * canMove; //si termino el juego, canMove se vuelve 0
        }

    }

    /* //public void OnPhotonInstantiate(Photon.Pun.PhotonMessageInfo info)
     //{
     //    Debug.Log("Is this mine?... " + info.Sender.IsLocal.ToString());
     //}*/

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
                playerScore += 1;
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
                //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlaySound(3);
                //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().End(playerScore, PhotonNetwork.LocalPlayer.NickName);
                //Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "Platform")
        {
            if (photonView.IsMine)
            {
                this.transform.parent = collision.transform;
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
    public void SavePosition(Vector3 thisPos) => posSaved = thisPos;
    public void UpdatePosition() => this.transform.position = posSaved;
    public void StopMoving() => canMove = 0;

    public void CallRemoteMethod()
    {
        GetComponent<PhotonView>().RPC(
            "SendScore",
            RpcTarget.All, playerScore, this.transform.name);
    }

    [PunRPC]
    public void SendScore(int PlayerScore, string name)
    {
        if (photonView.IsMine)
        {
            //var hash = PhotonNetwork.LocalPlayer.CustomProperties;
            //hash.Add(this.transform.name, playerScore);
            //PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            //if (!PhotonNetwork.IsMasterClient) return;
            //Debug.Log(PhotonNetwork.LocalPlayer.ToStringFull());
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().End(PlayerScore, name);
        }
    }
    /* public void RecieveScore(int value1, int value2)
     {
         print("value1: " + value1 + " value2: " + value2);
         print("playerScore" + playerScore);

         if (playerScore == value1) // se esta leyendo a si mismo
         {
             // esta leyendo al enemigo
             if (playerScore <= value1) PlayerLOSE();
             else if (playerScore >= value1) PlayerWON();
             else PlayerTIE();
         }
         else if (playerScore == value2) // se esta leyendo a si mismo
         {
             if (playerScore <= value2) PlayerLOSE();
             else if (playerScore >= value2) PlayerWON();
             else PlayerTIE();
         }
     }
    */
    /* public void PlayerWON()
     {
         print("Se activo PlayerWON");
         imgResult.color = new Color32(0, 0, 0, 100);
         txtResultGame.text = "Congratulations you WON!";
     }
     public void PlayerLOSE()
     {
         print("Se activo PlayerLOSE");
         imgResult.color = new Color32(0, 0, 0, 100);
         txtResultGame.text = "Eliminated";
     }
     public void PlayerTIE()
     {
         print("Se activo PlayerTIE");
         imgResult.color = new Color32(0, 0, 0, 100);
         txtResultGame.text = "Tie!";
     }*/
}

