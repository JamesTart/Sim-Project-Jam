using UnityEngine;

public class GrabObject : MonoBehaviour
{

    public new Camera camera;
    public Transform grabTransform;
    public float grabDistance = 3.0f;

    public PlaceObject placeObjectComponent;

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

        if (!Physics.Raycast(ray, out hitInfo, 3f))
            return;

        if (!hitInfo.transform.CompareTag("Grabbable"))
            return;

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
        

        if (placeObjectComponent != null)
            placeObjectComponent.Place(grabbedRigidbody, 3f);

        grabbedRigidbody = null;
    }
}