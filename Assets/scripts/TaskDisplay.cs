using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskDisplayInScene : MonoBehaviour
{
    [Header("UI References")]
    public GameObject taskPrefab; // Prefab with Toggle + Text
    public Transform taskListParent; // Parent for task items

    private List<TaskItem> tasks = new List<TaskItem>();
    private const string TaskSaveKey = "TaskManager_Tasks"; // Key to store tasks persistently

    void Start()
    {
        LoadTasks();
        CreateTaskUI();
    }

    private void LoadTasks()
    {
        // Load tasks from PlayerPrefs or EditorPrefs (if shared with Editor scripts)
        if (PlayerPrefs.HasKey(TaskSaveKey))
        {
            string json = PlayerPrefs.GetString(TaskSaveKey);
            TaskData taskData = JsonUtility.FromJson<TaskData>(json);
            if (taskData != null && taskData.Tasks != null)
            {
                tasks = taskData.Tasks;
            }
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

                // Update the task's completion status when toggled
                toggle.onValueChanged.AddListener((isCompleted) =>
                {
                    task.IsCompleted = isCompleted;
                    SaveTasks();
                });
            }
        }
    }

    private void SaveTasks()
    {
        TaskData taskData = new TaskData { Tasks = tasks };
        string json = JsonUtility.ToJson(taskData);
        PlayerPrefs.SetString(TaskSaveKey, json);
    }

    [System.Serializable]
    public class TaskItem
    {
        public string Description;
        public bool IsCompleted;
    }

    [System.Serializable]
    public class TaskData
    {
        public List<TaskItem> Tasks = new List<TaskItem>();
    }
}
