using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErisProjectileBehaviour : MonoBehaviour
{
    enum ErisProjectileType
    {
        Eris,
        Xena
    }
    [SerializeField] private ErisProjectileType type;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}