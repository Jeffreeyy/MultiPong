using UnityEngine;
using System.Collections;

public class BallFollow : MonoBehaviour 
{

    [SerializeField]private GameObject _gameManagerObj;
    private GameManager _gameManager;
    [SerializeField]private float _damping;

    void Start()
    {
        _gameManager = _gameManagerObj.GetComponent<GameManager>();
    }

	void LateUpdate () 
    {
        transform.position = Vector2.Lerp(transform.position, _gameManager.serverBallPos, _damping);
	}
}
