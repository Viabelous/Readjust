using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


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