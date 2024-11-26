using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public void Place(Rigidbody grabbedRigidBody, float maxDistance)
    {
        GameObject[] snappingPoints = GameObject.FindGameObjectsWithTag("snappingPoint");


        // Gets the closest snapping point
        GameObject closestSnappingPoint = null;
        float closestSnappingDistance = Mathf.Infinity;

        foreach (GameObject snap in snappingPoints)
        {
            float snappDistance = Vector3.Distance(grabbedRigidBody.position, snap.transform.position);

            if (snappDistance < closestSnappingDistance)
            {
                closestSnappingPoint = snap;
                closestSnappingDistance = snappDistance;
            }
        }

        if (closestSnappingDistance > maxDistance) // cant reach any snapping point
            return;

        grabbedRigidBody.isKinematic = true;
        grabbedRigidBody.transform.parent = closestSnappingPoint.transform;
        grabbedRigidBody.transform.localEulerAngles = Vector3.zero;
        grabbedRigidBody.transform.localPosition = Vector3.zero;
    }
}
