using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    PlayerController[] players;
    PlayerController nearestPlayer;
    ScoreSystem score;
    public GameObject vfx;
    PhotonView view;
    public float speed;

    void Start()
    {
        view = GetComponent<PhotonView>();
        players = FindObjectsOfType<PlayerController>();
        speed = 7;
        score = FindObjectOfType<ScoreSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(players.Length == 2)
        {
            float distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
            float distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);

            if (distanceOne < distanceTwo)
            {
                nearestPlayer = players[0];
            }
            else{
                nearestPlayer = players[1];
            }

            if(nearestPlayer != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(PhotonNetwork.IsMasterClient)
        {
            if(other.tag == "GoldRay")
            {
                view.RPC("SpawnVfx",RpcTarget.All);
                score.AddScore();
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    [PunRPC]
    void SpawnVfx()
    {
        Instantiate(vfx,transform.position, Quaternion.identity);
    }
}
