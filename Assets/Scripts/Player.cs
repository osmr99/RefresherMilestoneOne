using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    // Default gravity from Project Settings: -9.81
    [SerializeField] GameObject hitbox;
    [SerializeField] Rigidbody rb;
    [SerializeField] int speed;
    [SerializeField] int jumpPower;
    [SerializeField] float sprintMultiplier;
    [SerializeField] GameObject player;
    [SerializeField] Animator animator;
    Vector2 movement;
    bool onGround = false;
    bool isSprinting = false;
    float rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.localPosition = new Vector3(0, 0.32f, 0);

        if (movement.x == 0 && movement.y == 0)
            animator.SetFloat("moveSpeed", 0);
        else
        {
            if (isSprinting)
                animator.SetFloat("moveSpeed", 2);
            else
                animator.SetFloat("moveSpeed", 1);
        }
            

        if (hitbox.transform.localPosition.y <= -0.45f)
        {
            animator.SetBool("falling", true);
        }
    }

    private void FixedUpdate()
    {
        Look();
        Movement();
    }

    void Movement()
    {
        //animator.SetFloat("moveSpeed", rb.velocity.x);
        if (isSprinting)
        {
            rb.velocity = new Vector3(movement.x * sprintMultiplier, rb.velocity.y, movement.y * sprintMultiplier);
            //Debug.Log("Sprinting");
        }
        else
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.y);
            //Debug.Log("Not sprinting");
        }
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;

        //var velocity = move * speed;
        //velocity.y = rb.velocity.y;
        //rb.velocity = velocity;

    }

    void Look()
    {
        if (movement.x * speed > 0 && movement.y * speed > 0)
            rotation = 45;
        else if (movement.x * speed < 0 && movement.y * speed < 0)
            rotation = 225;
        else if (movement.x * speed > 0 && movement.y * speed < 0)
            rotation = 135;
        else if (movement.x * speed < 0 && movement.y * speed > 0)
            rotation = 315;
        else if (movement.x >= 1)
            rotation = 90;
        else if (movement.x <= -1)
            rotation = 270;
        else if (movement.y >= 1)
            rotation = 0;
        else if (movement.y <= -1)
            rotation = 180;

        player.transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>() * speed;
    }

    public void OnSprint(InputValue value)
    {
        isSprinting =! isSprinting;
    }

    public void OnJump(InputValue value)
    {
        //if(hitbox.transform.localPosition.y >= 0.3145f && hitbox.transform.localPosition.y <= 0.315f)
        if(onGround)
        {
            onGround = false;
            animator.SetBool("onGround", false);
            rb.AddForce(0, jumpPower, 0, ForceMode.Impulse);
            if (movement.x == 0 && movement.y == 0)
            {
                animator.SetBool("standingJump", true);
                animator.SetBool("fastJump", false);
            }
            else
            {
                animator.SetBool("fastJump", true);
                animator.SetBool("standingJump", false);
            }


        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            onGround = true;
            animator.SetBool("onGround", true);
            animator.SetBool("falling", false);
        }
            
        if (collision.gameObject.name == "Respawn")
            hitbox.transform.localPosition = new Vector3(0, 2, 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            onGround = false;
            animator.SetBool("onGround", false);
        }
            
    }
}
