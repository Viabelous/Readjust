using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    [SerializeField] private List<GameObject> children;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject child in children)
        {
            SpriteRenderer childSpriteRend = child.GetComponent<SpriteRenderer>();
            childSpriteRend.sortingLayerName = spriteRenderer.sortingLayerName;
            childSpriteRend.color = spriteRenderer.color;
        }
    }
}
