using UnityEngine;
using UnityEngine.SceneManagement;

public enum ZoneState
{
    Idle,
    OnDialog,
    OnWindow,
    OnPopUp
}

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

    public GameObject dialogPanel;

    private ZoneState state;
    [SerializeField] LevelChanger levelChanger;


    void Awake()
    {
        instance = this;
        GameManager.selectedMap = Map.None;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        switch (state)
        {
            case ZoneState.Idle:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    levelChanger.Transition("MainMenu");
                }

                if (dialogPanel.activeInHierarchy)
                {
                    ChangeCurrentState(ZoneState.OnDialog);
                }

                // if (GameObject.FindWithTag("Window") != null)
                // {
                //     ChangeCurrentState(ZoneState.OnWindow);
                // }

                break;

            case ZoneState.OnDialog:
                if (!dialogPanel.activeInHierarchy)
                {
                    // player.GetComponent<PlayerController>().movementEnable(true);
                    ChangeCurrentState(ZoneState.Idle);
                }
                // player.GetComponent<PlayerController>().movementEnable(false);
                break;

            // case ZoneState.OnWindow:
            //     if (GameObject.FindWithTag("Window") == null)
            //     {
            //         player.GetComponent<PlayerController>().movementEnable(true);
            //         ChangeCurrentState(ZoneState.Idle);
            //     }
            //     player.GetComponent<PlayerController>().movementEnable(false);
            //     break;

            case ZoneState.OnPopUp:

                break;
        }
    }

    public ZoneState CurrentState()
    {
        return state;
    }

    public void ChangeCurrentState(ZoneState state)
    {
        this.state = state;
    }

}