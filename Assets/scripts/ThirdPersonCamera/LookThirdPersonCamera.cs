using UnityEngine;

public class LookThirdPersonCamera : MonoBehaviour
{
    public Transform cameraFocus;
    public Vector3 offset;

    void Update()
    {
        transform.LookAt(cameraFocus);
        transform.position = Vector3.Lerp(transform.position, cameraFocus.position + offset, 0.8f * Time.deltaTime);
    }
}
