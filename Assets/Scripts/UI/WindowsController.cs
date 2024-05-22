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
    public GameObject FocusedButton;
    public int ActiveWindowsID;

    public GameObject[] popUps;
    [HideInInspector] public NotifPopUp popUp;


    void Update()
    {
        if (ActiveWindowsID != -1 && ZoneManager.instance.CurrentState() != ZoneState.OnPopUp)
        {


            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (HoveredButton.GetComponent<Navigation>().Left != null)
                {
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Left;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (HoveredButton.GetComponent<Navigation>().Right != null)
                {
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Right;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (HoveredButton.GetComponent<Navigation>().Up != null)
                {
                    if (HoveredButton.GetComponent<Navigation>().Up.name == "close")
                    {
                        HoveredButton.GetComponent<Navigation>().Up.GetComponent<Navigation>().Down = HoveredButton;
                    }
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Up;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (HoveredButton.GetComponent<Navigation>().Down != null)
                {
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Down;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                HoveredButton.GetComponent<Navigation>().Clicked();
            }
            // else
            // {
            //     HoveredButton.GetComponent<Navigation>().ExclusiveKey();
            // }
        }

    }

    public void toogleWindow(int windows_id, bool doOpenWindow)
    {
        Windows[windows_id].SetActive(doOpenWindow);
        Player.GetComponent<PlayerController>().movementEnable(!doOpenWindow);
        if (doOpenWindow)
        {
            ActiveWindowsID = windows_id;
            HoveredButton = WindowsButtonStartPointNavigation[windows_id];
            HoveredButton.GetComponent<Navigation>().IsHovered(true);
        }
        else
        {
            ActiveWindowsID = -1;
            HoveredButton.GetComponent<Navigation>().IsHovered(false);
            HoveredButton = null;
        }

    }

    public void CloseSkillTree()
    {
        foreach (GameObject obj in SkillTree)
        {
            obj.SetActive(false);
        }
    }

    public void CreatePopUp(string id, PopUpType type, string info)
    {
        GameObject newpopUp = Instantiate(
            popUps[type == PopUpType.OK ? 0 : 1],
            GameObject.Find("UI").transform
        );

        popUp = newpopUp.GetComponent<NotifPopUp>();
        popUp.id = id;
        popUp.info = info;

        ZoneManager.instance.ChangeCurrentState(ZoneState.OnPopUp);
    }
}