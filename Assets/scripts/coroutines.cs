using System.Collections;
using UnityEngine;

public class Coroutines : MonoBehaviour
{

    // IEnumerator: 


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(DelayedExecution());

        if (Input.GetKeyDown(KeyCode.Alpha2))
            StartCoroutine(RepeatingExecution());

        if (Input.GetKeyDown(KeyCode.Alpha3))
            StartCoroutine(RepeatingExecDuration());
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
            StartCoroutine(ConditionalExecution());
    }


    IEnumerator DelayedExecution(float delayseconds = 5f)
    {
        yield return new WaitForSeconds(delayseconds);

        Debug.Log("Time is up!");
    }

    IEnumerator RepeatingExecution(float intervalSeconds = 1f)
    {
        while (true)
        {
            Debug.Log($"Executing with an interval of {intervalSeconds} seconds");

            yield return new WaitForSeconds(intervalSeconds);
        }

    }

    IEnumerator RepeatingExecDuration(float intervalSeconds = 1f, float durationSeconds = 10f)
    {
        float timeElapsed = 0f;

        while (timeElapsed < durationSeconds)
        {
            Debug.Log($"Executing with an interval of {intervalSeconds} seconds");

            timeElapsed += intervalSeconds;
            yield return new WaitForSeconds(intervalSeconds);

        }

        Debug.Log("Time is up!");
    }
    // döp den till en relevant funkion
    bool Somecondition()
    {
        bool result = Input.GetKeyDown(KeyCode.Space);

        return result;

    }

    IEnumerator ConditionalExecution()
    {
        Debug.Log("Time to work!");

        yield return new WaitUntil(Somecondition);
        yield return null; // waits one frame

        Debug.Log("worker did work!");

        yield return new WaitUntil(Somecondition);
        yield return null;

        Debug.Log("worker did work!");

        yield return new WaitUntil(Somecondition);
        yield return null;

        Debug.Log("worker finished the job!");

    }

}
