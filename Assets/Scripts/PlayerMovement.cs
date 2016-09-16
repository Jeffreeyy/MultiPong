using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour 
{
    [SerializeField]private GameObject _gameManagerObj;
    [SerializeField]private Transform[] _spawnpoints;
    [SerializeField]private float _movementSpeed;
    [SerializeField]private float _borderMargin;
    private GameManager _gameManager;
    private float x, y;


	void Update () 
    { 
        if(!isLocalPlayer)
            return;


        x = transform.position.x;
        y = Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime;

        transform.Translate(0, y, 0);

        transform.position = new Vector2(x,Mathf.Clamp(transform.position.y,-_borderMargin,_borderMargin));
	}

    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
