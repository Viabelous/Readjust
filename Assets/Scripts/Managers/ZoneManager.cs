using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// digunakan dalam stage 
public class ZoneManager : MonoBehaviour
{
    public static ZoneManager instance;

    [Header("Player")]
    public GameObject player;

    [Header("Developer Zone")]
    // batasan map --------------------------
    public Vector2 minMap;
    public Vector2 maxMap;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    // void Update()
    // {

    // }

}