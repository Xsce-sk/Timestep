using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable, IDamageable
{
    [Header("Settings")]
    public static GameObject LocalPlayerInstance;
    public bool showDebug;

    [Header("Debug")]
    [SerializeField]
    private int health = 1; // Maybe we can do this better if you only have one health? Not sure but this should work for now


    private void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }

        DontDestroyOnLoad(this.gameObject);;
    }

    void Start ()
    {

    }

    void Update()
    {
        if (!photonView.IsMine)
            return;
    }
     
    public void LoseHealth(int deltaHealth)
    {
        if(showDebug)
        {
            Debug.Log(this.gameObject.name + " now has " + health);
        }
        health -= 1;
    }

    // Streams player health.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            this.health = (int)stream.ReceiveNext();
        }
    }
}
