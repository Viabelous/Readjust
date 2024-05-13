using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    [SerializeField] private RewardType type;
    [SerializeField] private GameObject player;
    [SerializeField] private Text displayText;

    void Update()
    {
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