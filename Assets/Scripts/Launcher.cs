using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView playerPref1;
    [SerializeField] private CameraFollower cam;
    [SerializeField] private Transform SpawnDefault;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Conectado con exito al servidor");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Ingreso a sala exitosa");

        PhotonNetwork.Instantiate(playerPref1.name, SpawnDefault.position, Quaternion.identity);
        LookAtForPLayer();
    }
    public void LookAtForPLayer()
    {
        cam.LookOutForThePlayer();
    }
}

