using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] int speed;
    Vector2 movement;
    [SerializeField] GameObject player;
    bool isSprinting = false;
    float rotation;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        var velocity = move * speed;
        //velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    void Look()
    {
        if (movement.x == 1)
            rotation = 90;
        else if (movement.x == -1)
            rotation = 270;
        else if (movement.y == 1)
            rotation = 0;
        else if (movement.y == -1)
            rotation = 180;
        else if (movement.x > 0 && movement.y > 0)
            rotation = 45;
        else if (movement.x < 0 && movement.y < 0)
            rotation = 225;
        else if (movement.x > 0 && movement.y < 0)
            rotation = 135;
        else if (movement.x < 0 && movement.y > 0)
            rotation = 315;
        player.transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        isSprinting =! isSprinting;
    }
}
