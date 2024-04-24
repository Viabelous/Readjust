using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public enum BuffType
{
    Heal,
    Mana,
    Shield,
    ATK,
    AGI,
    FOC
}

public class Buff
{
    public BuffType type;
    public float value;
    public float timer;

    public Buff(BuffType type, float value, float timer)
    {
        this.type = type;
        this.value = value;
        this.timer = timer;
    }
}

// dikasih ke skill
public class BuffSystem : MonoBehaviour
{

    public CharacterType type;

    [HideInInspector]
    private List<Buff> buffsActive = new List<Buff>();

    void Start()
    {

    }

    void Update()
    {
        // buffsActive.Skip(35).ToList().ForEach(
        //     buff => buff.timer -= Time.deltaTime
        // );

        // for (int i = 0; i < buffsActive.Count; i++)
        // {
        //     buffsActive[i].timer -= Time.deltaTime;
        //     if (buffsActive[i].timer <= 0)
        //     {
        //         buffsActive.Remove(buffsActive[i]);
        //     }
        // }

        // switch (type)
        // {
        //     case CharacterType.Player:
        //         break;
        //     case CharacterType.Enemy:
        //         break;
        // }

    }

    public IEnumerator ActivateBuff(Buff buff)
    {
        AddBuff(buff);
        yield return new WaitForSeconds(buff.timer);
        RemoveBuff(buff);
    }

    public bool CheckBuff(BuffType type)
    {
        if (buffsActive.FindIndex(buff => buff.type == type) != -1)
        {
            return true;
        }
        return false;
    }

    public float GetBuffValues(BuffType type)
    {
        float totalValue = 0;
        List<Buff> buffs = buffsActive.FindAll(buff => buff.type == type);

        foreach (Buff buff in buffs)
        {
            totalValue += buff.value;
        }

        return totalValue;
    }

    private void AddBuff(Buff buff)
    {
        buffsActive.Add(buff);
    }

    private void RemoveBuff(Buff buff)
    {
        buffsActive.Remove(buff);
    }



}