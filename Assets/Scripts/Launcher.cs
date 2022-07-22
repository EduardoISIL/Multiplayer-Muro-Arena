using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Menu
    [SerializeField] private GameObject loadingPnl;
    private bool menuState = false;
    private bool playPressed = false;
    private bool connectState = false;
    [SerializeField] private RectTransform sidePnl = null;
    [SerializeField] private GameObject optionsPnl = null;
    [SerializeField] private GameObject creditsPnl = null;
    [SerializeField] private Vector2 wide;
    //-------------------------------------------------------------------------------

    // Lobby
    [SerializeField] private Sprite[] playerSkins;
    [SerializeField] private SpriteRenderer playerEdit;
    private int tempValue = 0;
    private int skinsCount = 0;
    private bool lobbyState = false;
    protected private bool joinState = false;
    private bool startPressed = false;
    private bool backPressed = false;
    private bool isBack = false;
    private GameObject backBtn = null;
    [SerializeField] private RuntimeAnimatorController[] animController;
    //-------------------------------------------------------------------------------

    // Nivel 1
    private bool inGame = false;
    [SerializeField] private PhotonView playerPref1;
    [SerializeField] private CameraFollower cam;
    [SerializeField] private Transform SpawnDefault;
    private bool roomState = false;
    //-------------------------------------------------------------------------------

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

                    if (connectState == false)
                    {
                        PhotonNetwork.ConnectUsingSettings();
                        connectState = true;
                    }

                    skinsCount = playerSkins.Length;
                    tempValue = 0;

                    if (GameObject.Find("Play") != null)
                    {
                        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(Play);
                    }

                    if (GameObject.Find("Options") != null)
                    {
                        GameObject.Find("Options").GetComponent<Button>().onClick.AddListener(OptionsShow);
                    }

                    if (GameObject.Find("Credits") != null)
                    {
                        GameObject.Find("Credits").GetComponent<Button>().onClick.AddListener(CreditsShow);
                    }

                    if (GameObject.Find("hidePnl") != null)
                    {
                        GameObject.Find("hidePnl").GetComponent<Button>().onClick.AddListener(Hide);
                    }

                    if (GameObject.Find("Quit") != null)
                    {
                        GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(QuitGame);
                    }
                }

                if (isBack == true)
                {
                    lobbyState = false;
                    playPressed = false;
                    loadingPnl = GameObject.Find("LoadingPnl");

                    if (loadingPnl != null)
                    {
                        loadingPnl.SetActive(false);
                        GameObject.Find("SoundManager").GetComponent<MusicManager>().menuSound = false;
                    }

                    if (optionsPnl == null)
                    {
                        optionsPnl = GameObject.Find("OptionsPnl");
                    }

                    if (creditsPnl == null)
                    {
                        creditsPnl = GameObject.Find("CreditsPnl");
                    }

                    if (sidePnl == null)
                    {
                        sidePnl = GameObject.Find("SidePnl").GetComponent<RectTransform>();
                    }

                    if (GameObject.Find("Play") != null)
                    {
                        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(Play);
                    }

                    if (GameObject.Find("Options") != null)
                    {
                        GameObject.Find("Options").GetComponent<Button>().onClick.AddListener(OptionsShow);
                    }

                    if (GameObject.Find("Credits") != null)
                    {
                        GameObject.Find("Credits").GetComponent<Button>().onClick.AddListener(CreditsShow);
                    }

                    if (GameObject.Find("hidePnl") != null)
                    {
                        GameObject.Find("hidePnl").GetComponent<Button>().onClick.AddListener(Hide);
                    }

                    if (GameObject.Find("Quit") != null)
                    {
                        GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(QuitGame);
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

                    if (joinState == false)
                    {
                        PhotonNetwork.JoinLobby();
                    }

                    backBtn = GameObject.Find("BackBtn");

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
                    if (roomState == false)
                    {
                        RoomOptions options = new RoomOptions();
                        options.MaxPlayers = 10;
                        PhotonNetwork.JoinOrCreateRoom("room", options, TypedLobby.Default);
                        roomState = true;
                    }

                    inGame = true;
                    print("Nivel 1 Case");
                    //levelState = true; HABILITAR CUANDO SE PUEDA REGRESAR AL MENU
                    SpawnDefault = GameObject.Find("SpawnPosition").transform;
                    cam = GameObject.Find("Main Camera").GetComponent<CameraFollower>();
                    print("Se creo el Room");
                }

                break;
        }
    }

    public override void OnConnectedToMaster()
    {
        print("Conectado con exito al servidor");
        if (loadingPnl != null)
        {
            loadingPnl.SetActive(false); // Al conectar con el servidor se retira la pantalla de LOADING...
            GameObject.Find("SoundManager").GetComponent<MusicManager>().menuSound = false;
            GameObject.Find("Cinematic").GetComponent<CinematicControl>().inMenu = false;
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

    public void LookAtForPLayer()
    {
        cam.LookOutForThePlayer();
    }

    // Botón de Menú
    public void Play()
    {
        if (playPressed == false)
        {
            BtnSfx();
            playPressed = true;
            SceneManager.LoadScene(1);
            GameObject.Find("SoundManager").GetComponent<MusicManager>().lobbySound = false;
        }
    }

    // Botón de Lobby
    public void StartGame()
    {
        if (startPressed == false)
        {
            BtnSfx();
            GameObject.Find("loadingTxt").GetComponent<Text>().text = "Loading...";
            startPressed = true;
            backBtn.SetActive(false);
            SceneManager.LoadScene(2);
        }
    }

    // Volver al menu
    public void BackMenu()
    {
        if (backPressed == false)
        {
            BtnSfx();
            GameObject.Find("loadingTxt").GetComponent<Text>().text = "Loading...";
            backPressed = true;
            isBack = true;
            GameObject.Find("SoundManager").GetComponent<MusicManager>().menuSound = false;
            SceneManager.LoadScene(0);
        }
    }

    // Selector de Skin
    public void RightSkin()
    {
        if (tempValue < skinsCount - 1)
        {
            BtnSfx();
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

    public void BtnSfx()
    {
        GameObject.Find("SoundManager").GetComponent<MusicManager>().ButtonSFX();
    }

    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(MoveLeft(wide, 1f));
    }

    private IEnumerator MoveLeft(Vector2 target, float duration)
    {
        float current = 0;
        Vector2 originalRT = sidePnl.anchoredPosition;

        while (current < duration)
        {
            sidePnl.anchoredPosition = Vector2.Lerp(originalRT, target, current / duration);
            current += Time.deltaTime;
            yield return null;
        }

        sidePnl.anchoredPosition = target;
        yield return null;
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(MoveLeft(wide * -5, 1f));
    }

    public void OptionsShow()
    {
        optionsPnl.SetActive(true);
        creditsPnl.SetActive(false);
        Show();
    }

    public void CreditsShow()
    {
        optionsPnl.SetActive(false);
        creditsPnl.SetActive(true);
        Show();
    }

    public void QuitGame()
    {
        print("Se cierra el juego");
        Application.Quit();
    }

}

