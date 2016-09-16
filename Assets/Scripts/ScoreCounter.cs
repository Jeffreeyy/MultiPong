using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreCounter : NetworkBehaviour
{
    [SerializeField]private int _maxScore;
    [SerializeField]private int _p1Score;
    [SerializeField]private int _p2Score;
    [SerializeField]private Text _p1ScoreText;
    [SerializeField]private Text _p2ScoreText;
    [SerializeField]private Text _winText;

    [ClientRpc]
    public void RpcAddScore(int player)
    {
        switch(player)
        {
            case 1:
                _p1Score += 1;
                break;
            case 2:
                _p2Score += 1;
                break;
        }
        UpdateScoreBoard();
        CheckScore();
    }

    private void UpdateScoreBoard()
    {
        _p1ScoreText.text = _p1Score.ToString();
        _p2ScoreText.text = _p2Score.ToString();
    }

    private void CheckScore()
    {
        if(_p1Score == _maxScore)
        {
            StartCoroutine(Win(1, 3));
        }
        if(_p2Score == _maxScore)
        {
            StartCoroutine(Win(2, 3));
        }
    }

    private IEnumerator Win(int player, float waitTime)
    {
        _winText.text = "Player " + player + " wins!";
        _winText.enabled = true;
        yield return new WaitForSeconds(waitTime);
        _winText.enabled = false;
        NetworkLobbyManager.Shutdown();
    }
}
