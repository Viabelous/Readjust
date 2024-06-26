using System.Collections;
using UnityEngine;


public class windowsController : MonoBehaviour
{

    public GameObject Player;
    public LevelChanger levelChanger;
    public GameObject[] Windows;
    public GameObject[] WindowsButtonStartPointNavigation;
    [HideInInspector] public GameObject[] SkillTree;
    public GameObject HoveredButton;
    public GameObject FocusedButton;
    public int ActiveWindowsID;

    public GameObject[] popUps;
    [HideInInspector] public NotifPopUp popUp;
    [HideInInspector] public bool isScrolling = false;
    [Header("Sound")]
    [SerializeField] private AudioSource audioSrc = new AudioSource() { };
    [Tooltip("Sesuaikan dengan nomor windows di property Windows")]
    public AudioClip[] clickButtonSound;
    [Tooltip("Sesuaikan dengan nomor windows di property Windows")]
    public AudioClip[] navigateButtonSound;
    [Tooltip("index 0 untuk scroll shop, index 1 untuk scroll storage")]
    public AudioClip[] scrollButtonSound;


    void Update()
    {
        if (isScrolling)
        {
            HoveredButton.GetComponent<Navigation>().ExclusiveKey();
        }

        else if (ActiveWindowsID != -1 && ZoneManager.instance.CurrentState() != ZoneState.OnPopUp)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (HoveredButton.GetComponent<Navigation>().Left != null)
                {
                    PlaySound(navigateButtonSound[ActiveWindowsID]);
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Left;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (HoveredButton.GetComponent<Navigation>().Right != null)
                {
                    PlaySound(navigateButtonSound[ActiveWindowsID]);
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Right;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (HoveredButton.GetComponent<Navigation>().Up != null)
                {
                    PlaySound(navigateButtonSound[ActiveWindowsID]);
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
                PlaySound(navigateButtonSound[ActiveWindowsID]);
                if (HoveredButton.GetComponent<Navigation>().Down != null)
                {
                    HoveredButton.GetComponent<Navigation>().IsHovered(false);
                    HoveredButton = HoveredButton.GetComponent<Navigation>().Down;
                    HoveredButton.GetComponent<Navigation>().IsHovered(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                PlaySound(clickButtonSound[ActiveWindowsID]);
                HoveredButton.GetComponent<Navigation>().Clicked();
            }
        }

    }

    public IEnumerator ToogleWindow(int windowsID, bool doOpenWindow)
    {
        yield return new WaitForSeconds(0.1f);
        Windows[windowsID].SetActive(doOpenWindow);
        Player.GetComponent<PlayerController>().movementEnable(!doOpenWindow);
        if (doOpenWindow)
        {
            ActiveWindowsID = windowsID;
            HoveredButton = WindowsButtonStartPointNavigation[windowsID];
            HoveredButton.GetComponent<Navigation>().IsHovered(true);
        }
        else
        {
            ActiveWindowsID = -1;
            HoveredButton.GetComponent<Navigation>().IsHovered(false);
            HoveredButton = null;
        }
    }

    public IEnumerator TransitionWindows(int toBeClosedWindowsID, int toBeOpenedWindowsID)
    {
        yield return new WaitForSeconds(0.1f);
        Windows[toBeClosedWindowsID].SetActive(false);
        ActiveWindowsID = -1;
        HoveredButton.GetComponent<Navigation>().IsHovered(false);
        HoveredButton = null;

        Windows[toBeOpenedWindowsID].SetActive(true);
        ActiveWindowsID = toBeOpenedWindowsID;
        HoveredButton = WindowsButtonStartPointNavigation[toBeOpenedWindowsID];
        HoveredButton.GetComponent<Navigation>().IsHovered(true);
    }

    public void openShop()
    {
        isScrolling = true;
        HoveredButton = WindowsButtonStartPointNavigation[7];
        StartCoroutine(ToogleWindow(7, true));
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

    public void PlaySound(AudioClip audio)
    {
        audioSrc.clip = audio;
        audioSrc.Play();
    }

}