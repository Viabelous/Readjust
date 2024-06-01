using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornCoverEffect : MonoBehaviour
{

    void Update()
    {
        transform.position = transform.parent.position;
    }
    void EndAnimation()
    {
        Destroy(gameObject);
    }
}