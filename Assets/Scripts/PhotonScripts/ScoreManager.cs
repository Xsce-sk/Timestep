using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    Dictionary<string, int> scoreBoard = new Dictionary<string, int>(); // (player id, score)
    public static ScoreManager instance;
    public TextMeshProUGUI tmp;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void AddPlayer(string id)
    {
        photonView.RPC("AddPlayerRPC", RpcTarget.AllBuffered, id);
    }

    public void IncrementPlayer(string id)
    {
        photonView.RPC("IncrementPlayerRPC", RpcTarget.AllBuffered, id);
    }

    [PunRPC]
    public void AddPlayerRPC(string id)
    {
        if (scoreBoard.ContainsKey(id))
            return;

        scoreBoard.Add(id, 0);

        UpdateTMP();
    }

    [PunRPC]
    public void IncrementPlayerRPC(string id)
    {
        if (scoreBoard.ContainsKey(id))
            scoreBoard[id]++;

        UpdateTMP();
    }

    public void UpdateTMP()
    {
        tmp.text = "";
        foreach(KeyValuePair<string, int> entry in scoreBoard)
        {
            tmp.text += "Player " + entry.Key[0] + ": " + entry.Value + "\n";
        }
    }
}
