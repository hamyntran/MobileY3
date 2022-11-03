using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int gap = 1;

    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float moveSpeed = .5f;
    [SerializeField] private float rotateSpeed = .5f;

    private Rigidbody playerRG;

    public Vector3 nextDir;
    // Start is called before the first frame update
    void Start()
    {
        playerRG = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    private void HandleInput() {

        if (Input.GetKeyDown(KeyCode.W)) {
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
                //Move(new Vector3(-1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
                //Move(new Vector3(1, 0, 0));
        }
    }
}
