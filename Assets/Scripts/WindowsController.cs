using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class windowsController : MonoBehaviour
{

    public GameObject Player;
    public GameObject[] Windows;
    public bool anyWindowsEnabled;
    public GameObject[] SkillSelectionWindowsInteractable;
    
    public GameObject[] SkillWindowsInteractable;


    private int SelectedIndex;

    private enum elements
    {
        fire, earth,
        water, air
    }

    void Start()
    {
        SelectedIndex = 0;
    }

    void Update(){
        if(Windows[1].activeInHierarchy)
        {
            if(SelectedIndex == 0)
            {
                if(Input.GetKeyDown("q")) toogleWindow(1, false);
            }
        }
        else if(Windows[2].activeInHierarchy)
        {
            if(SelectedIndex == 0)
            {
                if(Input.GetKeyDown("q")) toogleWindow(2, false);
            }
        }
    }

    public void toogleWindow(int windows_id, bool doOpenWindow)
    {
        Windows[windows_id].SetActive(doOpenWindow);
        Player.GetComponent<PlayerController>().movementEnable(!doOpenWindow);
        SetAnyWindowsEnabled(doOpenWindow);
    }

    public void SetAnyWindowsEnabled(bool state)
    {
        anyWindowsEnabled = state;
    }

}