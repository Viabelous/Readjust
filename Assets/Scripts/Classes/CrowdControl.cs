using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrowdControlType
{
    Normal,
    Slide,
    KnockBack,
    Slow,
}

public class CrowdControl
{
    public string id;
    public CrowdControlType type;


    // public CrowdControl()
    // {
    //     id = "cc" + UnityEngine.Random.Range(0, 99999);
    // }

}

public class CCSlide : CrowdControl
{
    public float speed;
    public float range;
    public Vector3 initialPos;
    public Vector2 backward;

    public CCSlide(string id, float speed, float range, Vector3 initialPos, Vector2 backward) : base()
    {
        this.id = id;
        this.type = CrowdControlType.Slide;
        this.speed = speed;
        this.range = range;
        this.initialPos = initialPos;
        this.backward = backward;
    }

}

public class CCKnockBack : CrowdControl
{
    public float speed;
    public float range;
    public Vector3 initialPos;

    public Vector3 direction;

    public CCKnockBack(string id, float speed, float range, Vector3 initialPos, Vector3 direction) : base()
    {
        this.id = id;
        this.type = CrowdControlType.KnockBack;
        this.speed = speed;
        this.range = range;
        this.initialPos = initialPos;
        this.direction = direction;
    }
}
public class CCSlow : CrowdControl
{
    public float slow;
    public float initialSpeed;
    public float timer;

    public CCSlow(string id, float slow, float timer, float initialSpeed) : base()
    {
        this.id = id;
        this.type = CrowdControlType.Slow;
        this.slow = slow;
        this.timer = timer;
        this.initialSpeed = initialSpeed;
    }

    public IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(this.timer);
    }
}


