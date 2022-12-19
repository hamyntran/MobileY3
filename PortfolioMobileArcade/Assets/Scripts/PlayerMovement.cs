using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int gap = 1;

    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float moveSpeed = .5f;
    [SerializeField] private float rotateSpeed = .5f;

    private Rigidbody playerRG;

    private bool inputForward, inputBackward, inputLeft, inputRight = false;
    private bool hopping = false;

    private Animator _animator;

    public Vector3 nextDir;
    // Start is called before the first frame update
    void Start()
    {
        playerRG = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        transform.eulerAngles = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Movement();
    }
    
    private void HandleInput()
    {
        inputForward = Input.GetKeyDown(KeyCode.W);
        inputBackward = Input.GetKeyDown(KeyCode.S);
        inputLeft = Input.GetKeyDown(KeyCode.A);
        inputRight = Input.GetKeyDown(KeyCode.D);

    }

    private void Movement()
    {
        if(hopping){return;}
        
        if (inputForward)
        {
            float round = 0;
            if (transform.position.x % 1 != 0)
            {
                round = Mathf.Round(transform.position.x) - transform.position.x;
            }
            
            transform.eulerAngles = new Vector3(0, 0, 0);

            
            MovePlayer(new Vector3(round,0,gap));
            
            LaneGenerator.Instance.GenerateLane(false);
        }
        
        if (inputBackward)
        {
            
            float round = 0;
            if (transform.position.x % 1 != 0)
            {
                round = Mathf.Round(transform.position.x) - transform.position.x;
            }
            transform.eulerAngles = new Vector3(0, 180, 0);

            MovePlayer(new Vector3(round,0,-gap));
        }
        
        if (inputLeft)
        {
            float round = 0;
            if (transform.position.z % 1 != 0)
            {
                round = Mathf.Round(transform.position.x) - transform.position.x;
            }
            transform.eulerAngles = new Vector3(0, -90, 0);
            
            MovePlayer(new Vector3(-gap,0,round));
        }
        
        if (inputRight)
        {
            float round = 0;
            if (transform.position.z % 1 != 0)
            {   
                round = Mathf.Round(transform.position.x) - transform.position.x;
            } 
            transform.eulerAngles = new Vector3(0, 90, 0);
            MovePlayer(new Vector3(gap,0,round));

        }
    }

    private void MovePlayer(Vector3 difference)
    {
        _animator.SetTrigger("Move");
        hopping = true;
        
        transform.position += difference;
    }

    private void FinishHopping()
    {
        hopping = false;
    }
}
