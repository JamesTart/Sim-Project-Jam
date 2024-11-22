using UnityEngine;
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class MoveFirstPersonOnject : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 5f;

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

        Vector3 moveVector = (transform.forward * vInput) + (transform.right * hInput);

        if (moveVector.magnitude > 1f)
            moveVector = moveVector.normalized;

        moveVector *= moveSpeed;

        grounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f) + 0.2f, whatIsGround);
        Debug.DrawRay(transform.position, Vector3.down * playerHeight * 0.5f, Color.blue);

        float verticalspeed = rb.linearVelocity.y;

        if (grounded)
            rb.linearVelocity = new Vector3(moveVector.x, verticalspeed, moveVector.z);
    }
}