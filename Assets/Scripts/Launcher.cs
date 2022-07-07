using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView playerPref1;
    [SerializeField] private CameraFollower cam;
    [SerializeField] private Transform SpawnDefault;

    [SerializeField] private GameObject loadingPnl;
    [SerializeField] private Sprite[] playerSkins;
    [SerializeField] private SpriteRenderer playerEdit;
    private int tempValue = 0;
    private int skinsCount = 0;
    // Start is called before the first frame update

    public static Launcher launcherStatic;//comparte esta variable con todas las escenas

    private void Awake()
    {
        if (launcherStatic != null && launcherStatic != this)
        {
            Destroy(gameObject);
            return;
        }
        launcherStatic = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        skinsCount = playerSkins.Length;
        tempValue = 0;
        print(tempValue);

        if (GameObject.Find("PlayerEdit") != null)
        {
            playerEdit = GameObject.Find("PlayerEdit").GetComponent<SpriteRenderer>();
        }

        if (SceneManager.GetActiveScene().name == "2")
        {
            print("Se detecto la escena");
            playerPref1 = GameObject.Find("MC 2").GetComponent<PhotonView>();
            cam = GameObject.Find("Main Camera").GetComponent<CameraFollower>();
            SpawnDefault = GameObject.Find("SpawnPosition").GetComponent<Transform>();
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }

    public override void OnConnectedToMaster()
    {
        print("Conectado con exito al servidor");
        if (loadingPnl != null)
        {
            loadingPnl.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Ingreso a sala exitosa");

        if (playerPref1 != null && cam != null && SpawnDefault != null)
        {
            PhotonNetwork.Instantiate(playerPref1.name, SpawnDefault.position, Quaternion.identity);
            LookAtForPLayer();
        }
    }

    public void LookAtForPLayer()
    {
        cam.LookOutForThePlayer();
    }

    private void Update()
    {
        if (GameObject.Find("PlayerEdit") != null)
        {
            playerEdit = GameObject.Find("PlayerEdit").GetComponent<SpriteRenderer>();
        }

        if (playerEdit != null) playerEdit.sprite = playerSkins[tempValue];

        if (GameObject.Find("Left") != null)
        {
            GameObject.Find("Left").GetComponent<Button>().onClick.AddListener(LeftSkin);
        }

        if (GameObject.Find("Right") != null)
        {
            GameObject.Find("Right").GetComponent<Button>().onClick.AddListener(RightSkin);
        }

        if (GameObject.Find("StartBtn") != null)
        {
            GameObject.Find("StartBtn").GetComponent<Button>().onClick.AddListener(StartGame);
        }
    }

    public void RightSkin()
    {
        if (tempValue < skinsCount - 1)
        {
            tempValue++;
            print(tempValue);
        }
    }

    public void LeftSkin()
    {
        if (tempValue > 0)
        {
            tempValue--;
            print(tempValue);
        }
    }
}

