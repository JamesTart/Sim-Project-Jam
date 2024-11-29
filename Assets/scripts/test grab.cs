using UnityEngine;

public class TestGrab : MonoBehaviour
{
    public new Camera camera;               // Camera used for raycasting
    public float grabDistance = 3.0f;       // Maximum distance to grab an object
    public float minimumGrabDistance = 1.0f; // Minimum distance for grabbing
    public float holdDistance = 2.0f;       // Distance at which the object is held

    private Rigidbody grabbedRigidbody = null;
    private Vector3 grabOffset;             // Offset from the grabbed object's position to the camera

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (grabbedRigidbody == null)
                Grab();
            else
                Release();
        }

        // Update the position of the grabbed object while holding it
        if (grabbedRigidbody != null)
        {
            MoveGrabbedObject();
        }
    }

    void Grab()
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (!Physics.Raycast(ray, out hitInfo, grabDistance))
            return;

        Rigidbody hitRigidbody = hitInfo.collider.attachedRigidbody;
        if (hitRigidbody == null)
            return;

        // Check if the object is within valid grabbing range
        float distanceToObject = Vector3.Distance(camera.transform.position, hitInfo.transform.position);
        if (distanceToObject < minimumGrabDistance)
            return;

        // Grab the object
        grabbedRigidbody = hitRigidbody;
        grabbedRigidbody.isKinematic = true; // Disable physics simulation
        grabOffset = camera.transform.position + camera.transform.forward * holdDistance - grabbedRigidbody.position;
    }

    void Release()
    {
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.isKinematic = false; // Re-enable physics
            grabbedRigidbody = null;
        }
    }

    void MoveGrabbedObject()
    {
        // Keep the grabbed object at the designated hold distance from the camera
        Vector3 targetPosition = camera.transform.position + camera.transform.forward * holdDistance - grabOffset;
        grabbedRigidbody.MovePosition(targetPosition);
    }
}
