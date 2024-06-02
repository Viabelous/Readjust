using UnityEngine;
public class NPCBehaviour : MonoBehaviour
{
    [SerializeField] private int progress;
    private bool unlocked;

    void Start()
    {
        unlocked = GameManager.player.GetProgress(Player.Progress.Story) >= progress;
        gameObject.SetActive(unlocked);
    }
}