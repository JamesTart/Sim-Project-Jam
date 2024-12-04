using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDisplayInScene : MonoBehaviour
{
    public GameObject taskPrefab;    // Prefab for task UI
    public Transform taskListParent; // Parent object for the task UI list

    private List<TaskItem> tasks = new List<TaskItem>();
    private const string TaskSaveKey = "TaskManager_Tasks";

    [System.Serializable]
    public class TaskItem
    {
        public string Description;
        public bool IsCompleted;
        public GameObject ObjectToTrack;
    }

    void Start()
    {
        LoadTasks();
        CreateTaskUI();
    }

    void Update()
    {
        // Check if any tasks should be marked as completed
        foreach (var task in tasks)
        {
            if (!task.IsCompleted && task.ObjectToTrack == null) // Object is deleted
            {
                task.IsCompleted = true;
                UpdateTaskUI(task);
                SaveTasks();
            }
        }
    }

    private void LoadTasks()
    {
        if (PlayerPrefs.HasKey(TaskSaveKey))
        {
            string json = PlayerPrefs.GetString(TaskSaveKey);
            tasks = JsonUtility.FromJson<TaskData>(json).Tasks;
        }
    }

    private void CreateTaskUI()
    {
        foreach (var task in tasks)
        {
            GameObject taskObj = Instantiate(taskPrefab, taskListParent);
            Toggle toggle = taskObj.GetComponentInChildren<Toggle>();
            Text taskText = taskObj.GetComponentInChildren<Text>();

            if (toggle != null && taskText != null)
            {
                taskText.text = task.Description;
                toggle.isOn = task.IsCompleted;

                toggle.onValueChanged.AddListener((isCompleted) =>
                {
                    task.IsCompleted = isCompleted;
                    SaveTasks();
                });
            }
        }
    }

    private void UpdateTaskUI(TaskItem task)
    {
        foreach (Transform child in taskListParent)
        {
            Toggle toggle = child.GetComponentInChildren<Toggle>();
            Text taskText = child.GetComponentInChildren<Text>();

            if (taskText.text == task.Description && toggle != null)
            {
                toggle.isOn = task.IsCompleted;
                break;
            }
        }
    }

    private void SaveTasks()
    {
        TaskData taskData = new TaskData { Tasks = tasks };
        PlayerPrefs.SetString(TaskSaveKey, JsonUtility.ToJson(taskData));
    }

    [System.Serializable]
    public class TaskData
    {
        public List<TaskItem> Tasks;
    }
}
