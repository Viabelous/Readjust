using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyShadow : MonoBehaviour
{

    public List<GameObject> children;
    private SpriteRenderer spriteRenderer;
    // private DefenseSystem defenseSystem;

    // // untuk skill locking
    // private bool triggeredDamageToggle;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // defenseSystem = GetComponent<DefenseSystem>();
        // triggeredDamageToggle = false;
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D other)
    {
        // karena pada saat diserang, bayangan yang mendeteksi skill
        // sehingga gambar musuh terbang harus mengikuti layer bayangan
        if (other.CompareTag("Damage"))
        {
            if (other.GetComponent<SkillController>().skill.Element != Element.Air)
            {
                return;
            }
            foreach (GameObject child in children)
            {
                SpriteRenderer childSpriteRend = child.GetComponent<SpriteRenderer>();

                // efeknya ketika kena skill
                childSpriteRend.sortingLayerName = spriteRenderer.sortingLayerName;
                childSpriteRend.color = spriteRenderer.color;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            if (other.GetComponent<SkillController>().skill.Element != Element.Air)
            {
                return;
            }

            spriteRenderer.sortingLayerName = "Shadow";

            foreach (GameObject child in children)
            {
                SpriteRenderer childSpriteRend = child.GetComponent<SpriteRenderer>();

                // efeknya setelah kena skill kembali ke awal
                childSpriteRend.sortingLayerName = "Enemy";
                childSpriteRend.color = spriteRenderer.color;
            }
        }
    }


}
