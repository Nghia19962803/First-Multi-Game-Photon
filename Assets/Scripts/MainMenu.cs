using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField jointInput;
    public InputField nickName;

    public void ChangeName()
    {
        PhotonNetwork.NickName = nickName.text;
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();    
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.text,roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(jointInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
