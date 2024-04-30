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
    public GameObject[] WindowsButtonStartPointNavigation;
    public GameObject[] SkillTree;
    public GameObject HoveredButton;
    public int ActiveWindowsID;

    void Update(){
        if(ActiveWindowsID != -1)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(HoveredButton.GetComponent<Navigation>().Left != null)
                {   
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Left;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }

            } else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(HoveredButton.GetComponent<Navigation>().Right != null)
                {   
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Right;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }

            } else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(HoveredButton.GetComponent<Navigation>().Up != null)
                {   
                    if(HoveredButton.GetComponent<Navigation>().Up.name == "close")
                    {
                        HoveredButton.GetComponent<Navigation>().Up.GetComponent<Navigation>().Down = HoveredButton;
                    }
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Up;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }

            } else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(HoveredButton.GetComponent<Navigation>().Down != null)
                {   
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Down;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }


            } else if(Input.GetKeyDown(KeyCode.Q))
            {
                HoveredButton.GetComponent<Navigation>().Clicked();
            } else
            {
                HoveredButton.GetComponent<Navigation>().ExclusiveKey();
            }
        }
    }

    public void toogleWindow(int windows_id, bool doOpenWindow)
    {
        Windows[windows_id].SetActive(doOpenWindow);
        Player.GetComponent<PlayerController>().movementEnable(!doOpenWindow);
        if(doOpenWindow)
        {
            ActiveWindowsID = windows_id;
            HoveredButton = WindowsButtonStartPointNavigation[windows_id];
            HoveredButton.GetComponent<Navigation>().IsHovered(true);
        } else
        {
            ActiveWindowsID = -1;
            HoveredButton.GetComponent<Navigation>().IsHovered(false);
            HoveredButton = null;
        }
        
    }

    public void CloseSkillTree()
    {
        foreach(GameObject obj in SkillTree)
        {
            obj.SetActive(false);
        }
    }

}