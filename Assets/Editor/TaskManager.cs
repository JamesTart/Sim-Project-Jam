using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class TaskManagerWindow : EditorWindow
{
    private List<string> tasks = new List<string>();
    private string newTask = "";
    private const string TaskSaveKey = "TaskManager_Tasks"; // Key to store tasks in EditorPrefs

    [MenuItem("Tools/Task Manager")]
    public static void ShowWindow()
    {
        GetWindow<TaskManagerWindow>("Task Manager");
    }

    private void OnEnable()
    {
        LoadTasks(); // Load tasks when the window opens
    }

    private void OnDisable()
    {
        SaveTasks(); // Save tasks when the window closes
    }

    private void OnGUI()
    {
        GUILayout.Label("Task Manager", EditorStyles.boldLabel);

        GUILayout.Space(10);

        GUILayout.Label("Add a New Task:");
        newTask = EditorGUILayout.TextField(newTask);

        if (GUILayout.Button("Add Task") && !string.IsNullOrEmpty(newTask))
        {
            tasks.Add(newTask);
            newTask = "";
        }

        GUILayout.Space(10);

        GUILayout.Label("Task List:");

        if (tasks.Count == 0)
        {
            GUILayout.Label("No tasks available.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"{i + 1}. {tasks[i]}");
                if (GUILayout.Button("Remove"))
                {
                    tasks.RemoveAt(i);
                }
                GUILayout.EndHorizontal();
            }
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Clear All Tasks"))
        {
            tasks.Clear();
        }
    }

    private void SaveTasks()
    {
        string json = JsonUtility.ToJson(new TaskData { Tasks = tasks });
        EditorPrefs.SetString(TaskSaveKey, json);
    }

    private void LoadTasks()
    {
        if (EditorPrefs.HasKey(TaskSaveKey))
        {
            string json = EditorPrefs.GetString(TaskSaveKey);
            TaskData taskData = JsonUtility.FromJson<TaskData>(json);
            if (taskData != null && taskData.Tasks != null)
            {
                tasks = taskData.Tasks;
            }
        }
    }

    [System.Serializable]
    private class TaskData
    {
        public List<string> Tasks;
    }
}
