using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [Header("Snapping Settings")]
    public float maxSnapDistance = 2.0f; // Default snapping distance, editable in the Inspector

    public void Place(Rigidbody grabbedRigidbody)
    {
        Place(grabbedRigidbody, maxSnapDistance); // Call the overloaded method
    }

    public void Place(Rigidbody grabbedRigidbody, float maxDistance)
    {
        // Find all snapping points in the scene
        GameObject[] snappingPoints = GameObject.FindGameObjectsWithTag("SnappingPoint");

        // Get the closest snapping point
        GameObject closestSnappingPoint = null;
        float closestSnappingDistance = Mathf.Infinity;

        foreach (GameObject snap in snappingPoints)
        {
            float snapDistance = Vector3.Distance(grabbedRigidbody.position, snap.transform.position);

            if (snapDistance < closestSnappingDistance)
            {
                closestSnappingPoint = snap;
                closestSnappingDistance = snapDistance;
            }
        }

        // Check if the closest snapping point is within the provided maxDistance
        if (closestSnappingDistance > maxDistance)
            return;

        // Snap the object to the closest snapping point
        grabbedRigidbody.isKinematic = true;
        grabbedRigidbody.transform.parent = closestSnappingPoint.transform;
        grabbedRigidbody.transform.localEulerAngles = Vector3.zero;
        grabbedRigidbody.transform.localPosition = Vector3.zero;
    }
}
