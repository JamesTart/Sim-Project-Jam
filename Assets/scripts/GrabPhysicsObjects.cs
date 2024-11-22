using UnityEngine;

public class ColliderGrab : MonoBehaviour
{
    public new Camera camera;
    public Transform grabTransform;

    private Rigidbody grabbedRigidbody = null;
    private Vector3 originalLocalPosition;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (grabbedRigidbody == null)
                Grab();
            else
                Release();
        }

        // Continuously move the grabbed object to the grab position
        if (grabbedRigidbody != null)
        {
            HoldObject();
        }
    }

    void Grab()
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (!Physics.Raycast(ray, out hitInfo, 6f))
            return;

        if (!hitInfo.transform.CompareTag("Grabbable"))
            return;

        grabbedRigidbody = hitInfo.collider.attachedRigidbody;

        // Cache the original local position (optional)
        originalLocalPosition = grabbedRigidbody.transform.localPosition;

        grabbedRigidbody.isKinematic = false; // Keep physics active
        grabbedRigidbody.transform.parent = null; // Prevent snapping to the camera
    }

    void HoldObject()
    {
        // Smoothly move the object toward the grab position
        Vector3 targetPosition = grabTransform.position;
        Vector3 currentPosition = grabbedRigidbody.position;
        grabbedRigidbody.linearVelocity = (targetPosition - currentPosition) * 10f; // Adjust speed multiplier as needed

        // Optionally, maintain rotation
        Quaternion targetRotation = grabTransform.rotation;
        grabbedRigidbody.MoveRotation(Quaternion.Slerp(grabbedRigidbody.rotation, targetRotation, Time.deltaTime * 10f)); // Smooth rotation
    }

    void Release()
    {
        grabbedRigidbody.linearVelocity = Vector3.zero; // Stop object movement
        grabbedRigidbody.angularVelocity = Vector3.zero; // Stop object rotation
        grabbedRigidbody.transform.parent = null; // Detach from camera or grab transform
        grabbedRigidbody = null; // Clear reference
    }
}
