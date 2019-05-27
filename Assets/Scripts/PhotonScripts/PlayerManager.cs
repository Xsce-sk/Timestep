using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;


    protected Transform m_Transform;
    protected Transform m_HMDTransform = null;

    public GameObject model;
    public bool hideModelOnLocal;


    private void Awake()
    {
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
