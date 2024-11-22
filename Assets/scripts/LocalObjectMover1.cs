using UnityEngine;

public class LocalObjectMover : MonoBehaviour
{
    public Vector3 newLocalPosition;
    public float speed = 10f;
    public bool smoothDamp;

    //private variables

    bool isNewPositionActive = false;
    Vector3 oldLocalPosition;

    Vector3 targetLocalPosition;

    Vector3 currentSpeed; //For SmoothDamp
    void Start()
    {
        oldLocalPosition = transform.localPosition;
        targetLocalPosition = oldLocalPosition;
    }

    void Update()
    {
        Vector3 currentPosition = transform.localPosition;

        if (smoothDamp)
            transform.localPosition = Vector3.SmoothDamp(currentPosition, targetLocalPosition, ref currentSpeed, 2f / speed);
        else
            transform.localPosition = Vector3.MoveTowards(currentPosition, targetLocalPosition, speed * Time.deltaTime);
    }

    public void MoveObject()
    {
        isNewPositionActive = !isNewPositionActive;


        if (isNewPositionActive)
            targetLocalPosition = newLocalPosition;

        else targetLocalPosition = oldLocalPosition;
    }
}