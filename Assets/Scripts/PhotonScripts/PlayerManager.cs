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
    public GameObject bloodParts;

    [Header("Debug")]
    [SerializeField] private int health = 1; // Maybe we can do this better if you only have one health? Not sure but this should work for now
    [SerializeField] private bool alive = true;
    private MeshRenderer mesh;
    private BoxCollider boxColl;
    private Coroutine DeathCoroutine;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }

        DontDestroyOnLoad(this.gameObject);;

        mesh = GetComponent<MeshRenderer>();
        boxColl = GetComponent<BoxCollider>();
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
        health -= deltaHealth;

        if(health == 0)
        {
            if(DeathCoroutine== null)
                DeathCoroutine = StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    { 
        alive = false;

        photonView.RPC("BloodRPC", RpcTarget.All, this.transform.position);

        mesh.enabled = false;

        photonView.RPC("RespawnRPC", RpcTarget.All, this.gameObject);

        yield return new WaitForSeconds(2);
      
        mesh.enabled = true;
        alive = true;
        health = 1;
        DeathCoroutine = null;
    }

    [PunRPC]
    public void BloodRPC(Vector3 pos)
    {
        GameObject blood = Instantiate(bloodParts, pos, Quaternion.identity);
        Destroy(blood, 2);
    }

    [PunRPC]
    public void RespawnRPC(GameObject Player)
    {
        if (!Player.GetPhotonView().IsMine)
            return;

        Player.transform.position = GameManager.Instance.spawnPoints[Random.Range(0, GameManager.Instance.spawnPoints.Count)];
    }



    // Streams player health.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(alive);
        }
        else
        {
            this.health = (int)stream.ReceiveNext();
            this.alive = (bool)stream.ReceiveNext();
        }
    }

    public bool IsAlive()
    {
        return alive;
    }
}
