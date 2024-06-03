using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : Navigation
{
    public bool isUnlocked;
    public bool firstAccess;
    public Map stage;
    public GameObject popUp;

    void Start()
    {
        isUnlocked = stage == Map.Stage5 ? false : popUp.GetComponent<StageDescription>().GetFocusedMap().HasUnlocked();
        if (Right != null && !Right.GetComponent<StageSelection>().popUp.GetComponent<StageDescription>().GetFocusedMap().HasUnlocked())
        {
            Right = null;
        }

        if (firstAccess)
        {
            IsHovered(true);
        }
    }

    void Update()
    {
        if (ZoneManager.instance.CurrentState() == ZoneState.OnPopUp)
        {
            switch (WindowsController.popUp.id)
            {
                case "load_stage_failed":
                    if (WindowsController.popUp.GetClickedBtn() == PopUpBtnType.OK)
                    {
                        Destroy(WindowsController.popUp.gameObject);
                        ZoneManager.instance.ChangeCurrentState(ZoneState.Idle);
                    }
                    break;
            }
        }
    }

    public override void IsHovered(bool state)
    {

        // else
        // {
        // }

        if (state)
        {
            GetComponent<Image>().sprite = HoverSprite;
            popUp.SetActive(true);
        }
        else
        {
            GetComponent<Image>().sprite = BasicSprite;
            popUp.SetActive(false);
        }



    }

    public override void Clicked()
    {
        if (isUnlocked)
        {
            GameManager.selectedMap = stage;
            WindowsController.levelChanger.Transition("" + stage);
        }
        else
        {
            WindowsController.CreatePopUp(
                "load_stage_failed",
                PopUpType.OK,
                "Menangkan stage sebelumnya untuk membuka stage ini."
            );
        }
    }

    public override void ExclusiveKey()
    {

    }

}
