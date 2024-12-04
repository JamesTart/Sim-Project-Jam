using UnityEngine;

[System.Serializable]
public class TaskItem : MonoBehaviour
{
    public string Description;     // Task description
    public bool IsCompleted;       // Completion status
    public GameObject ObjectToTrack; // The object associated with this task
}
