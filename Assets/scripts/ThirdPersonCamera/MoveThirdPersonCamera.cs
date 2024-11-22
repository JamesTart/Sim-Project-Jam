using UnityEngine;

public class MoveThirdPersonCamera : MonoBehaviour
{

    Rigidbody rb;

    public new Transform camera;

    [Header("Movement")]

    public float moveSpeed = 5f;
    public float rotationSpeed = 600f;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;

    bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }


    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        float vInput = Input.GetAxisRaw("Vertical");

        Vector3 forwardVector = new Vector3(camera.forward.x, 0f, camera.forward.z).normalized;
        Vector3 rightVector = new Vector3(camera.right.x, 0f, camera.right.z).normalized;


        Vector3 moveVector = (forwardVector * vInput) + (rightVector * hInput);

        if (moveVector.magnitude > 1)
            moveVector = moveVector.normalized;

        moveVector *= moveSpeed;

        grounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f) + 0.2f, whatIsGround);

        float verticalspeed = rb.linearVelocity.y;

        if (grounded)
            rb.linearVelocity = new Vector3(moveVector.x, verticalspeed, moveVector.z);

        if (moveVector.magnitude > 0.2f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
          
        }
    }
}
