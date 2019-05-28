using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Singleton for GameManager. Not sure if we need this yet.
    // public static GameManager Instance;

    [Header("Settings")]
    public GameObject playerPrefab;
    public bool PunCallbacks;

    [Header("Debug")]

    [SerializeField] private GameObject m_Player;
    [SerializeField] private bool m_isConnected;

    void Awake()
    {
        // For Game Manager Singleton. Not sure if we will need this, keeping just in case.
        //if (Instance != null && Instance != this)
        //    Destroy(gameObject);
        //Instance = this;
        //DontDestroyOnLoad(gameObject);

        //isConnected = false;
    }

    void Start ()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
                m_Player = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
                m_Player.GetComponent<MeshRenderer>().enabled = true; // Turn on the mesh renderer for the local player. Probably a better way to do this.
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
            
        }
    }
	
	void Update ()
    {
		
	}

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("PhotonLauncher");
    }

    #region PUN Callbacks
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PunCallbacks)
            Debug.LogFormat("OnPlayerEnteredRoom: {0}", newPlayer);

        if (PunCallbacks)
            Debug.LogFormat("OnPlayerEnteredRoom: IsMasterClient? {0}", PhotonNetwork.IsMasterClient);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (PunCallbacks)
            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }
    #endregion
}
