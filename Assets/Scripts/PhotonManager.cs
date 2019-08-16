using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour
{

    public bool isHost = false;
    public string packageString = "default";
    public string finalString = "Nothing Yet!";

    bool isConnected = false;
    PhotonView PV;
    string appToLoad;
    ExitGames.Client.Photon.Hashtable syncValues = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
        PV = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.connected)
        {
            finalString = (string)PhotonNetwork.room.CustomProperties["appToLoad"];
        }
    }
    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby!!!");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 21 }, TypedLobby.Default);
    }
    void OnJoinedRoom()
    {
        Debug.Log("Joined Room!!!");
        isConnected = true;
        if (isHost == true)
        {
            StartCoroutine(SetVar());
        }
    }

    [PunRPC]

    private IEnumerator SetVar()
    {
        while (PhotonNetwork.connected)
        {
            syncValues["appToLoad"] = packageString;
            PhotonNetwork.room.SetCustomProperties(syncValues);

            yield return new WaitForSeconds(.5f);
        }

        yield break;
    }
}
