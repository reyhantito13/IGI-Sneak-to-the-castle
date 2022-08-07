using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float runSpeed;
    public float walkSpeed;
    public float jumpSpeed;

    Rigidbody playerRB;
    Animator playerAnim;

    bool facingRight;
    bool onGround;
    Collider[] groundCollision;
    float groundCheckRadius = 1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        facingRight = true;
        onGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {       
        float move = Input.GetAxis("Horizontal");
        playerAnim.SetFloat("speed", Mathf.Abs(move));

        float sneaking = Input.GetAxisRaw("Fire3");
        playerAnim.SetFloat("sneaking", sneaking);

        if (onGround && Input.GetAxis("Jump") > 0)
        {
            onGround = false;
            playerAnim.SetBool("onGround", onGround);
            playerRB.AddForce(new Vector3(0, jumpHeight, 0));
        }

        groundCollision = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollision.Length > 0) onGround = true;
        else onGround = false;

        playerAnim.SetBool("onGround", onGround);
            
        if (sneaking>0 && onGround)
        {
            playerRB.velocity = new Vector3(move * walkSpeed, playerRB.velocity.y, 0);
        }
        else
        {
            playerRB.velocity = new Vector3(move * runSpeed, playerRB.velocity.y, 0);
        }        

        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.z *= -1;
        transform.localScale = playerScale;
    }
}
