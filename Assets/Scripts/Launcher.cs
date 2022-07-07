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

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        skinsCount = playerSkins.Length;
        tempValue = 0;
        print(tempValue);
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
        PhotonNetwork.JoinRandomOrCreateRoom();
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
            playerEdit.sprite = playerSkins[tempValue];
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

