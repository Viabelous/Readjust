using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    
    [SerializeField] StageManager stageManager;

    void Start()
    {
        if(GameManager.player.GetProgress(Player.Progress.Story) != 0)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(GameManager.player.GetProgress(Player.Progress.Story) != 0)
        {
            gameObject.SetActive(false);
        } else
        {
            if(stageManager.time >= 8)
            {
                gameObject.GetComponent<Animator>().Play("tutorial_animation_fadeOut");
            }
        }
    }
}
