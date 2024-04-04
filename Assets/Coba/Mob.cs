using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{

    public float hp = 100;
    public GameObject mobObject;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 0)
        {
            Destroy(mobObject, 1f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            hp -= 100;
            print(hp);
            spriteRenderer.color = Color.red;
            // print("Kenaa wehh");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            spriteRenderer.color = Color.white;
            // print("selesee");
        }
    }

}
