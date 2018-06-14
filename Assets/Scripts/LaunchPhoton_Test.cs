using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPhoton_Test : Photon.PunBehaviour {

    private string gameVersion = "Game_0";

    private void Awake ( )
    {
        PhotonNetwork.ConnectToRegion ( CloudRegionCode.usw, gameVersion );
        PhotonNetwork.automaticallySyncScene = true;

        // Auto join lobby is true by default.  I don't know what this does but I guess we don't need it.
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
    }

    // Use this for initialization
    void Start () {
        Connect ( );
	}

    public void Connect ( )
    {
        if ( PhotonNetwork.connected )
        {
            PhotonNetwork.JoinRandomRoom ( );
        }else
        {
            PhotonNetwork.ConnectUsingSettings ( gameVersion );
        }
    }

    public override void OnPhotonRandomJoinFailed ( object [ ] codeAndMsg )
    {
        base.OnPhotonRandomJoinFailed ( codeAndMsg );
        PhotonNetwork.CreateRoom ( null, new RoomOptions ( ) { MaxPlayers = 4 }, null );
    }

    public override void OnConnectedToMaster ( )
    {
        base.OnConnectedToMaster ( );
        PhotonNetwork.JoinRandomRoom ( );
    }

    public override void OnDisconnectedFromPhoton ( )
    {
        base.OnDisconnectedFromPhoton ( );
    }

    public override void OnJoinedRoom ( )
    {
        base.OnJoinedRoom ( );
        Debug.Log ( "DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room." );
        PhotonNetwork.Instantiate ( "Player_Test", Vector3.zero, Quaternion.identity, 0 );
    }
}
