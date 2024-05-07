using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private MobController mobController;
    private GameObject player;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mobController = GetComponent<MobController>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y > player.transform.position.y && !mobController.onSkillTrigger)
        {
            spriteRenderer.sortingLayerName = "Enemy Back";
        }
        else
        {
            spriteRenderer.sortingLayerName = "Enemy";
        }
    }


}
