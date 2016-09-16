using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
  
    // the game manager so we can send the position of the ball to the server
    [SerializeField]private GameObject _scoreCounterObj;
    [SerializeField]private GameObject _gameManagerObj;
    private GameManager _gameManager;
    private ScoreCounter _scoreCounter;
    // the speed the ball starts with
    [SerializeField]private float _startSpeed = 5f;

    // the maximum speed of the ball
    [SerializeField]private float _maxSpeed = 20f;

    // how much faster the ball gets with each bounce
    [SerializeField]private float _speedIncrease = 0.25f;

    // the current speed of the ball
    private float _currentSpeed;

    // the current direction of travel
    private Vector2 _currentDir;

    // whether or not the ball is resetting
    private bool _resetting = false;

    void Start()
    {
        _gameManager = _gameManagerObj.GetComponent<GameManager>();
        _scoreCounter = _scoreCounterObj.GetComponent<ScoreCounter>();

        // initialize starting speed
        _currentSpeed = _startSpeed;

        // initialize direction
        _currentDir = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        // don't move the ball if it's resetting
        if( _resetting )
            return;

        // move the ball in the current direction
        Vector2 moveDir = _currentDir * _currentSpeed * Time.deltaTime;
        transform.Translate( new Vector2( moveDir.x, moveDir.y ) );

        _gameManager.serverBallPos = new Vector2(transform.position.x, transform.position.y);
    }

    void OnTriggerEnter2D( Collider2D other )
    {
        if( other.gameObject.tag == "Boundary" )
        {
            // vertical boundary, reverse Y direction
            _currentDir.y *= -1;
            Debug.Log("Hit WALL!!");
        }
        else if (other.gameObject.tag == "Player")
        {
            // player paddle, reverse X direction
            _currentDir.x *= -1;
        }
        else if (other.gameObject.tag == "GoalP1")
        {
            _scoreCounter.RpcAddScore(2);
            StartCoroutine(resetBall());
        }
        else if (other.gameObject.tag == "GoalP2")
        {
            _scoreCounter.RpcAddScore(1);
            StartCoroutine(resetBall());
        }

        // increase speed
        _currentSpeed += _speedIncrease;
    
        // clamp speed to maximum
        _currentSpeed = Mathf.Clamp( _currentSpeed, _startSpeed, _maxSpeed );
    }

    IEnumerator resetBall()
    {
        // reset position, speed, and direction
        _resetting = true;
        transform.position = Vector3.zero;
    
        _currentDir = Vector3.zero;
        _currentSpeed = 0f;
        // wait for 3 seconds before starting the round
        yield return new WaitForSeconds( 3f );

        Start();

        _resetting = false;
    }
}
