using UnityEngine;

public class DoorFixed : MonoBehaviour
{
    public bool closed = true;
    public float openDegrees = 90f;
    public float openSpeed = 60f;

    // Private Variables
    float startAngle;
    float currentOffset;
    float closedOffset = 0f;
    float openOffset = 90f;


    void Start()
    {
        startAngle = transform.localEulerAngles.y;
    }

    void Update()
    {
        if (closed)
            currentOffset = Mathf.MoveTowards(currentOffset, closedOffset, openSpeed * Time.deltaTime);
        else
            currentOffset = Mathf.MoveTowards(currentOffset, openOffset, openSpeed * Time.deltaTime);

        transform.localEulerAngles = new Vector3(0f, startAngle + currentOffset, 0f);
    }

    public void ToggleOpen()
    {
        closed = !closed;
    }
}
