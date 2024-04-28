using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public enum BuffType
{
    HP,
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
    private MonoBehaviour chrController;

    void Start()
    {
        switch (type)
        {
            case CharacterType.Player:
                chrController = GetComponent<PlayerController>();
                break;
            case CharacterType.Enemy:
                chrController = GetComponent<MobController>();
                break;
        }
    }

    public void ActivateBuff(Buff buff)
    {
        if (buff.timer == 0)
        {
            AddBuff(buff);
        }
        else
        {
            StartCoroutine(CoroutineBuff(buff));
        }
    }

    public IEnumerator CoroutineBuff(Buff buff)
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

        switch (type)
        {
            case CharacterType.Player:
                PlayerController playerController = (PlayerController)chrController;

                switch (buff.type)
                {
                    case BuffType.ATK:
                        playerController.player.atk += buff.value;
                        break;

                    case BuffType.Mana:
                        if (playerController.player.mana + buff.value > playerController.player.maxMana)
                        {
                            playerController.player.mana = playerController.player.maxMana;
                        }
                        else
                        {
                            playerController.player.mana += buff.value;
                        }
                        break;

                    case BuffType.HP:
                        if (playerController.player.hp + buff.value > playerController.player.maxHp)
                        {
                            playerController.player.hp = playerController.player.maxHp;
                        }
                        else
                        {
                            playerController.player.hp += buff.value;
                        }
                        break;
                }

                break;

            case CharacterType.Enemy:
                MobController mobController = (MobController)chrController;
                break;

        }

    }

    private void RemoveBuff(Buff buff)
    {
        buffsActive.Remove(buff);
        switch (type)
        {
            case CharacterType.Player:
                PlayerController playerController = (PlayerController)chrController;

                switch (buff.type)
                {
                    case BuffType.ATK:
                        playerController.player.atk -= buff.value;
                        break;
                }

                break;
        }


    }



}