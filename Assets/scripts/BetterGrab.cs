using UnityEngine;

public class BetterGrab : MonoBehaviour
{
    public new Camera camera;
    public Transform grabTransform;
    public float grabDistance = 3.0f;
    public float minimumGrabDistance = 1.0f; // Minimum distance for grabbing an object

    private Rigidbody grabbedRigidbody = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            if (grabbedRigidbody == null)
                Grab();
            else
                Release();
    }

    void Grab()
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (!Physics.Raycast(ray, out hitInfo, grabDistance))
            return;

        if (!hitInfo.transform.CompareTag("Grabbable"))
            return;

        // Calculate the distance from the camera to the hit object
        float distanceToObject = Vector3.Distance(camera.transform.position, hitInfo.transform.position);

        // Only grab the object if the distance is greater than the minimum required
        if (distanceToObject < minimumGrabDistance)
            return;  // Prevent grabbing if the object is too close

        grabbedRigidbody = hitInfo.collider.attachedRigidbody;

        grabbedRigidbody.isKinematic = true;
        grabbedRigidbody.position = grabTransform.position;
        grabbedRigidbody.transform.parent = camera.transform;
        grabbedRigidbody.transform.localRotation = grabTransform.localRotation;
        grabbedRigidbody.GetComponent<Collider>().enabled = true;
    }

    void Release()
    {
        grabbedRigidbody.GetComponent<Collider>().enabled = true;
        grabbedRigidbody.isKinematic = false;
        grabbedRigidbody.transform.parent = null;
        grabbedRigidbody = null;
    }
}
