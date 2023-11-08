using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    public float jumpForce = 9;
    [SerializeField]
    public float speed = 4;
    [SerializeField]
    public float fallingSpeed = 6;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    bool doubleJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //Get Direction 
        float horizontalDirection = (Input.GetKey(KeyCode.D)? 1 : 0) - (Input.GetKey(KeyCode.A)? 1 : 0);

        //Check Direction of Sprite
        if(horizontalDirection != 0)
        {
            GetComponent<SpriteRenderer>().flipX = horizontalDirection < 0;
        }

        //Move object
        rb.velocity = new Vector2(horizontalDirection * speed, rb.velocity.y);

        //Check jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if(isGrounded)
            {
                Jump();
            }
            else
            {
                if (doubleJump)
                {
                    Jump(doubleJump);
                    doubleJump = false;
                }
            }
        }

        if(isGrounded) {
            doubleJump = true;
        }

        //Check Down direction
        if(Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, - fallingSpeed);
        }

        //Check restart game
        if (Input.GetKeyDown(KeyCode.R))
            transform.position = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Jump(bool isDoubleJump = false) {
        //The second jump is not as high
        rb.velocity = new Vector2(rb.velocity.x, (isDoubleJump? jumpForce * 2 / 3 : jumpForce));
    }
}
