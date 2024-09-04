using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    // Default gravity from Project Settings: -9.81
    [SerializeField] GameObject hitbox;
    [SerializeField] Rigidbody rb;
    [SerializeField] int speed;
    Vector2 movement;
    [SerializeField] GameObject player;
    bool isSprinting = false;
    float rotation;
    [SerializeField] int jumpPower;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Look();
        Movement();
    }

    void Movement()
    {
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.y);
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
        if(hitbox.transform.localPosition.y <= 0.315f)
            rb.AddForce(0, jumpPower, 0, ForceMode.Impulse);
    }
}
