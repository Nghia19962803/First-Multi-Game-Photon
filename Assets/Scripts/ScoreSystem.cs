using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreSystem : MonoBehaviour
{
    int score = 0;
    public Text scoreText;
    PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void AddScore()
    {
        view.RPC("AddScoreRPC", RpcTarget.All);
    }

    [PunRPC]
    void AddScoreRPC()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
