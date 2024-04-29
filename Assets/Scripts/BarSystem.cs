using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    hp,
    mana,
    shield
}

public class BarSystem : MonoBehaviour
{
    public BarType type;
    public GameObject bar, player;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (type)
        {
            case BarType.hp:
                bar.GetComponent<Image>().fillAmount = playerController.player.hp / playerController.player.maxHp;
                break;

            case BarType.mana:
                bar.GetComponent<Image>().fillAmount = playerController.player.mana / playerController.player.maxMana;
                break;

            case BarType.shield:


                if (gameObject.activeInHierarchy)
                {
                    bar.GetComponent<Image>().fillAmount = playerController.player.shield / playerController.player.maxShield;
                }
                break;
        }
    }
}
