using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    private float sceneStartTime;

    void Awake()
    {
        // Singleton pattern to ensure only one instance of TimeManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent TimeManager from being destroyed when changing scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // Register event listener for scene loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unregister event listener for scene loaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneStartTime = Time.time;
    }

    public float GetTimeSinceSceneLoad()
    {
        return Time.time - sceneStartTime;
    }


}