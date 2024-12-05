using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDisplayInScene : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        public string description;
        public GameObject objectToTrack; // The object to track if it's destroyed
        public bool isCompleted;        // Tracks whether the task is completed
    }

    public List<Task> tasks = new List<Task>();  // The list of tasks
    public Transform taskListParent;            // The parent object for displaying tasks
    public GameObject taskPrefab;               // The prefab for a single task item

    private List<Toggle> taskToggles = new List<Toggle>();

    void Start()
    {
        InitializeTasks();
    }

    void Update()
    {
        UpdateTasks();
    }

    private void InitializeTasks()
    {
        // Clear any existing task UI
        foreach (Transform child in taskListParent)
        {
            Destroy(child.gameObject);
        }

        taskToggles.Clear();

        // Create a task entry for each task in the list
        foreach (var task in tasks)
        {
            GameObject taskItem = Instantiate(taskPrefab, taskListParent);
            Toggle toggle = taskItem.GetComponent<Toggle>();
            Text label = taskItem.GetComponentInChildren<Text>();

            if (label != null)
                label.text = task.description;

            taskToggles.Add(toggle);

            // Ensure tasks start as not completed
            task.isCompleted = false;
            toggle.isOn = false;
        }
    }

    private void UpdateTasks()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            Task task = tasks[i];
            if (task.objectToTrack == null && !task.isCompleted)
            {
                // Mark task as completed if the object is destroyed
                task.isCompleted = true;
                taskToggles[i].isOn = true;
            }
        }
    }
}
