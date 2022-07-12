using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{

    // Menu
    [SerializeField] private GameObject loadingPnl;
    private bool menuState = false;
    private bool playPressed = false;

    // Lobby
    [SerializeField] private Sprite[] playerSkins;
    [SerializeField] private SpriteRenderer playerEdit;
    private int tempValue = 0;
    private int skinsCount = 0;
    private bool lobbyState = false;
    //private bool levelState = false; HABILITAR CUANDO SE PUEDA REGRESAR AL MENU
    private bool startPressed = false;
    private bool backPressed = false;
    private bool isBack = false;
    private GameObject backBtn = null;
    [SerializeField] private RuntimeAnimatorController[] animController;

    // Nivel 1
    private bool inGame = false;
    [SerializeField] private PhotonView playerPref1;
    [SerializeField] private CameraFollower cam;
    [SerializeField] private Transform SpawnDefault;

    // Propiedad DON'T DESTROY ON LOAD
    public static Launcher launcherStatic; //comparte esta variable con todas las escenas

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
    //---------------------

    public override void OnConnectedToMaster()
    {
        print("Conectado con exito al servidor"); 
        if (loadingPnl != null)
        {
            loadingPnl.SetActive(false); // Al conectar con el servidor se retira la pantalla de LOADING...
        }
    }

    private void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":

                if (isBack == false && menuState == false)
                {
                    print("Menu Case");
                    lobbyState = false;
                    playPressed = false;
                    menuState = true;
                    PhotonNetwork.ConnectUsingSettings();

                    skinsCount = playerSkins.Length;
                    tempValue = 0;
                }

                if (isBack == true)
                {
                    lobbyState = false;
                    playPressed = false;
                    loadingPnl = GameObject.Find("LoadingPnl");
                    if (loadingPnl != null)
                    {
                        loadingPnl.SetActive(false);
                    }
                    if (GameObject.Find("Play") != null)
                    {
                        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(Play);
                    }
                }

                break;

            case "Lobby":

                if (lobbyState == false)
                {
                    print("Lobby Case");

                    menuState = false;
                    isBack = false; 
                    backPressed = false;
                    lobbyState = true;

                    PhotonNetwork.JoinLobby();
                    backBtn = GameObject.Find("BackBtn").gameObject;

                    tempValue = 0;

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

                    if (GameObject.Find("BackBtn") != null)
                    {
                        GameObject.Find("BackBtn").GetComponent<Button>().onClick.AddListener(BackMenu);
                    }

                    if (GameObject.Find("loadingTxt") != null)
                    {
                        GameObject.Find("loadingTxt").GetComponent<Text>().text = "";
                    }

                    if (GameObject.Find("PlayerEdit") != null)
                    {
                        playerEdit = GameObject.Find("PlayerEdit").GetComponent<SpriteRenderer>();
                    }
                }

                if (playerEdit != null) playerEdit.sprite = playerSkins[tempValue];

                break;

            case "Nivel 1":

                if (inGame == false)
                {
                    inGame = true;
                    print("Nivel 1 Case");
                    PhotonNetwork.JoinRandomOrCreateRoom();
                    //levelState = true; HABILITAR CUANDO SE PUEDA REGRESAR AL MENU
                    SpawnDefault = GameObject.Find("SpawnPosition").transform;
                    cam = GameObject.Find("Main Camera").GetComponent<CameraFollower>();
                    print("Se creo el Room");
                }

                break;
        }
    }

    // Paso 5 - Al ingresar a la sala
    public override void OnJoinedRoom()
    {
        Debug.Log("Ingreso a sala exitosa");
        print(tempValue);
        
        if (playerPref1 != null && cam != null && SpawnDefault != null)
        {
            PhotonNetwork.Instantiate(playerPref1.name, SpawnDefault.position, Quaternion.identity);
            playerPref1.GetComponent<Animator>().runtimeAnimatorController = animController[tempValue];
            LookAtForPLayer();
        }
    }

    // Por DEFINIR
    public void LookAtForPLayer()
    {
        cam.LookOutForThePlayer();
    }

    // Bot�n de Men�
    public void Play()
    {
        if (playPressed == false)
        {
            playPressed = true;
            SceneManager.LoadScene(1);
        }
    }

    // Bot�n de Lobby
    public void StartGame()
    {
        if (startPressed == false)
        {
            GameObject.Find("loadingTxt").GetComponent<Text>().text = "Loading...";
            startPressed = true;
            backBtn.SetActive(false);
            SceneManager.LoadScene(2);
        }
    }

    public void BackMenu()
    {
        if (backPressed == false)
        {
            GameObject.Find("loadingTxt").GetComponent<Text>().text = "Loading...";
            backPressed = true;
            isBack = true;
            PhotonNetwork.LeaveLobby();
            SceneManager.LoadScene(0);
        }
    }

    // Selector de Skin
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

