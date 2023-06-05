using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    /*----------Movement setting---------*/
    [FormerlySerializedAs("_jumpForce")]
    [Header("Movement setting")]
    [FormerlySerializedAs("_moveSpeed")] [SerializeField] private float moveSpeed = .5f;
    [SerializeField] private float jumpHeight = 3;
    
    /*----------References---------*/
    private PlayerBehaviour _playerBehaviourScr;
    [SerializeField] Transform _playerHolder;
    private Vector3 _onGroundPosition = new Vector3();
    /*----------Private variables---------*/
    private bool _inputForward, _inputBackward, _inputLeft, _inputRight, _attackIput = false;
    private bool _hopping = false;
    private Vector3 _nextDir;
    private int _gap => InGameManager.Instance.Generator.LaneWidth;

    private bool _recieveInput;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _recieveInput = false;
        InGameManager.Instance.OnGameStart += () => { _recieveInput = true; };
        _playerBehaviourScr = GetComponent<PlayerBehaviour>();
        transform.eulerAngles = new Vector3();

        _onGroundPosition = _playerHolder.position;
    }

    /*
    private void OnDisable()
    {
        InGameManager.Instance.OnGameStart -= () => { _recieveInput = false; };
    }
    */

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Movement();
        Attack();
    }
    
    private void HandleInput()
    {
        if(!_recieveInput) {return;}
        _inputForward = Input.GetKeyDown(KeyCode.W);
        _inputBackward = Input.GetKeyDown(KeyCode.S);
        _inputLeft = Input.GetKeyDown(KeyCode.A);
        _inputRight = Input.GetKeyDown(KeyCode.D);
        _attackIput = Input.GetKeyDown(KeyCode.Space);
    }

    private void Movement()
    {
        if(_hopping){return;}
        
        if (_inputForward)
        {
            float round = 0;
            if (transform.position.x % 1 != 0)
            {
                round = Mathf.Round(transform.position.x) - transform.position.x;
            }
            
            transform.eulerAngles = new Vector3(0, 0, 0);

            
            MovePlayer(new Vector3(round,0,_gap));
            
            InGameManager.Instance.Generator.GenerateLane(false);
        }
        
        if (_inputBackward)
        {
            float round = 0;
            if (transform.position.x % 1 != 0)
            {
                round = Mathf.Round(transform.position.x) - transform.position.x;
            }
            transform.eulerAngles = new Vector3(0, 180, 0);

            MovePlayer(new Vector3(round,0,-_gap));
        }
        
        if (_inputLeft)
        {
            float round = 0;
            if (transform.position.z % 1 != 0)
            {
                round = Mathf.Round(transform.position.x) - transform.position.x;
            }
            transform.eulerAngles = new Vector3(0, -90, 0);
            
            MovePlayer(new Vector3(-_gap,0,round));
        }
        
        if (_inputRight)
        {
            float round = 0;
            if (transform.position.z % 1 != 0)
            {   
                round = Mathf.Round(transform.position.x) - transform.position.x;
            } 
            transform.eulerAngles = new Vector3(0, 90, 0);
            MovePlayer(new Vector3(_gap,0,round));

        }
    }

    private void Attack()
    {
        if (_attackIput)
        {
            _playerBehaviourScr.playerAnimation.TriggerPlayerAnimation(PlayerAnimationState.PlayerAttack);
        }
    }

    private void MovePlayer(Vector3 difference)
    {
        DOHopAnimation();
        
        _playerBehaviourScr.playerAnimation.TriggerPlayerAnimation(PlayerAnimationState.PlayerJump);
        
        _hopping = true;
        
        transform.position += difference;
    }

    private void FinishHopping()
    {
        _hopping = false;
    }

    private void DOHopAnimation()
    {
        var onGroundY = _playerHolder.position.y;
        var jumpY = onGroundY + jumpHeight;

        _playerHolder.DOMoveY(jumpY, moveSpeed).OnComplete(() =>
        {
            _playerHolder.DOMoveY(onGroundY, moveSpeed).OnComplete(() =>
            {
                FinishHopping();
            });
        });
    }
}
