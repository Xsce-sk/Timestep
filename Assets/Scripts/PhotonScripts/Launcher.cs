using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Header("Settings")]
    public byte maxPlayersPerRoom = 4;
    public string SceneName;
    public bool ConnectOnStart = false;
    public bool PunCallbacks;

    void Start()
    {
        if(ConnectOnStart)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // What the button should call to connect
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    #region PUN Callbacks
    public override void OnConnectedToMaster()
    {
        if(PunCallbacks)
            Debug.Log("OnConnectedToMaster() was called by PUN");

        PhotonNetwork.JoinRoom("Room1");
        // PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (PunCallbacks)
            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (PunCallbacks)
            Debug.Log("OnJoinRoomFailed() was called by PUN");

        PhotonNetwork.CreateRoom("Room1", new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = maxPlayersPerRoom });
        if (PunCallbacks)
            Debug.Log("Created Room: Room1");
    }

    public override void OnJoinedRoom()
    {
        if (PunCallbacks)
            Debug.Log("OnJoinedRoom() was called by PUN");

        #region ErrorChecking
        if (SceneName == null || SceneName == "")
        {
            Debug.LogErrorFormat("Please enter a valid scene name. Scene name: {0}", SceneName);
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(SceneName))
        {
            Debug.LogErrorFormat("Scene: {0} not found. Make sure the scene is addded to the build settings.");
            return;
        }
        #endregion

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            PhotonNetwork.LoadLevel(SceneName);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (PunCallbacks)
            Debug.Log("OCreateRoomFailed() was called by PUN");

        PhotonNetwork.CreateRoom("Room2", new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = maxPlayersPerRoom });

        if (PunCallbacks)
            Debug.Log("Created Room: Room2");
    }
    #endregion
}
