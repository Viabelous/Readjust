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
    public BuffType buffType;
    public float value;
    public float timer;

    public Buff(BuffType buffType, float value, float timer)
    {
        this.buffType = buffType;
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

        for (int i = 0; i < buffsActive.Count; i++)
        {
            buffsActive[i].timer -= Time.deltaTime;
            if (buffsActive[i].timer <= 0)
            {
                buffsActive.Remove(buffsActive[i]);
            }
        }

        // switch (type)
        // {
        //     case CharacterType.Player:
        //         break;
        //     case CharacterType.Enemy:
        //         break;
        // }

    }

    public void AddBuff(Buff buff)
    {
        buffsActive.Add(buff);
    }

    public bool CheckBuff(BuffType buffType)
    {

        if (buffsActive.FindIndex(buff => buff.buffType == buffType) != -1)
        {
            return true;
        }
        return false;
    }

    public float GetBuffValues(BuffType buffType)
    {

        float totalValue = 0;
        List<Buff> buffs = buffsActive.FindAll(buff => buff.buffType == buffType);

        foreach (Buff buff in buffs)
        {
            totalValue += buff.value;
        }

        return totalValue;

    }



}