using UnityEngine;

public class elevator : MonoBehaviour
{
    public float targetHeight = 10f;
    public float speed = 3f;


    Vector3 startPosition;
    bool reachTarget;

    void Start()
    {
        startPosition = transform.position;

    }

  
    void Update()
    {
        if (reachTarget)
        {
            Vector3 targetPoistion = new Vector3(startPosition.x, targetHeight, startPosition.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPoistion, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
        }
    }
    public void ToggleReachTarget()
    {
        reachTarget = !reachTarget;
    }
}
