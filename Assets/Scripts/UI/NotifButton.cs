using UnityEngine;

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