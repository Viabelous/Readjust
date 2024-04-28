using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// dikasih ke skill
public class windowsController : MonoBehaviour
{
    public GameObject[] windows;

    public void openWindows(int windows_id)
    {
        windows[windows_id].SetActive(true);
    }

    public void closeWindows(int windows_id)
    {
        windows[windows_id].SetActive(false);
    }

}