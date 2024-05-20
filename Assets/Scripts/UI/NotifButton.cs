using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifButton : MonoBehaviour
{


    [SerializeField] private PopUpBtnType type;
    [SerializeField] private NotifPopUp popUp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (popUp.GetCurrentBtn().name == gameObject.name)
            {
                popUp.SetClickedBtn(type);
            }
        }
    }
}