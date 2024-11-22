using UnityEngine;

public class DeleteOnContact : MonoBehaviour
{
    // This method is called when the object collides with another collider
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object the current object is colliding with has a Rigidbody and Collider
        if (collision.gameObject != null)

        if (collision.gameObject.CompareTag("Destructible"))
        {
                Destroy(collision.gameObject);
        }

        {
            // Destroy the object that was collided with
            Destroy(collision.gameObject);
        }
    }
}
