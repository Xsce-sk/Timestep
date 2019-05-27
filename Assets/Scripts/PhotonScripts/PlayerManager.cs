using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;



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
       
}
