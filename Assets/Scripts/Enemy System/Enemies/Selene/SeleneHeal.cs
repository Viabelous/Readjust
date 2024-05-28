using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleneHeal : MonoBehaviour
{

    Transform selene;

    void Start()
    {
        selene = transform.parent;
        transform.position = selene.position;
    }

    void Update()
    {
        transform.position = selene.position;
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }
}
