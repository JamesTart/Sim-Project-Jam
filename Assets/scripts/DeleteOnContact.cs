using UnityEngine;

public class DeleteOnContact : MonoBehaviour
{
    // The tag to filter which objects can be destroyed
    public string targetTag = "Destructible";

    // This method is called when the object collides with another collider
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Destroy the object with the matching tag
            Destroy(collision.gameObject);
        }
    }
}
