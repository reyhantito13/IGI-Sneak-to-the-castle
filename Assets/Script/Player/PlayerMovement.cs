using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("For Jumping Shits")]
    public float jumpForceVertical = 5f;
    public float jumpForceHorizontal = 5f;
    private Vector3 newJumpForce = new Vector3(1f,1f,0f);

    [Header("For Ground Checking")]
    public Transform groundCheck;
    public float groundCheckLength;
    public LayerMask whatIsGround;

    [Header("For Animation Shits")]
    //Variables for animation
    private bool isWalking;
    private bool isFacingRight;

    [Header("For Character Stats")]
    [SerializeField] private float movementSpeed = 10f ;
    private float move;
    private Rigidbody playerRb;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        newJumpForce.x = jumpForceHorizontal;
        newJumpForce.y = jumpForceVertical;
        playerAnim = gameObject.GetComponent<Animator>();
        playerRb = gameObject.GetComponent<Rigidbody>();
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayAnimation();
        CheckForFlip();
        CheckForJump();
    }

    private void FixedUpdate()
    {
        move = Input.GetAxisRaw("Horizontal");
        HorizontalMove();
    }

    bool CheckForGround()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, groundCheckLength, whatIsGround);
    }

    void CheckForJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (CheckForGround())
        {
            playerRb.AddForce(new Vector3(newJumpForce.x * move, newJumpForce.y, 0), ForceMode.Impulse);
        }
    }

    void HorizontalMove()
    {
        if (Input.GetButton("Horizontal"))
        {
            playerRb.velocity = new Vector3(movementSpeed * move, playerRb.velocity.y, 0);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    void CheckForFlip()
    {
        if (move == 1 && !isFacingRight)
        {
            Flip();
        }
        else if (move == -1 && isFacingRight)
        {
            Flip();
        }
    }

    void PlayAnimation()
    {
        playerAnim.SetBool("IsWalking", isWalking);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        gameObject.transform.Rotate(0.0f, 180.0f, 0.00f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckLength, 0));
    }
}
