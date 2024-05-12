using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;
    public float  startTimeBtwSpawns;
    float timeBtwSpawns;
    
    private void Start() {
        timeBtwSpawns = startTimeBtwSpawns;
    }
    private void Update() {
        if (PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.MaxPlayers != 2)
        {
            return;
        }

       if(timeBtwSpawns <= 0)
       {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
       }else{
            timeBtwSpawns -= Time.deltaTime;
       }
    }
}
