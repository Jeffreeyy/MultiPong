using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour 
{
    private GameObject[] _players;
    [SerializeField]private GameObject[] _spawnpoints;

    [SyncVar]private Vector2 _serverBallPos;
    public Vector2 serverBallPos
    {
        get { return _serverBallPos; }
        set { _serverBallPos = value; }
    }

    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].transform.position = _spawnpoints[i].transform.position;
        }
    }

}
