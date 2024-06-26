using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public GameState state;
    [SerializeField] private RewardType type;
    [SerializeField] private GameObject player;
    [SerializeField] private Text displayText;

    void Update()
    {
        if (state == GameState.OnStage)
        {
            gameObject.SetActive(
                StageManager.instance.CurrentState() == StageState.Play ||
                StageManager.instance.CurrentState() == StageState.Pause
            );
        }

        switch (type)
        {
            case RewardType.Aerus:
                displayText.text = player.GetComponent<PlayerController>().player.aerus.ToString();
                break;
            case RewardType.ExpOrb:
                displayText.text = player.GetComponent<PlayerController>().player.exp.ToString();
                break;
            case RewardType.Venetia:
                displayText.text = player.GetComponent<PlayerController>().player.venetia.ToString();
                break;
        }
    }
}