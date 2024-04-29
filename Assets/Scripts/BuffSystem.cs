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
    DEF,
    AGI,
    FOC,
    Thorn
}

public class Buff
{
    public string id;
    public BuffType type;
    public float value;
    public float timer;

    public Buff(string id, BuffType type, float value, float timer)
    {
        this.id = id;
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
    public List<Buff> buffsActive = new List<Buff>();
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

    void Update()
    {
        switch (type)
        {
            case CharacterType.Player:
                // print("Apakah ada buff shield? " + CheckBuff(BuffType.Shield));

                // kalau sedang pakai buff shield
                if (CheckBuff(BuffType.Shield))
                {
                    // cari index buff shield
                    int index = buffsActive.FindIndex(buff => buff.type == BuffType.Shield);

                    // kalau shield yg dipakai saat ini sudah habis, 
                    // maka hapus dari list buff yg sedang dipakai
                    if (((PlayerController)chrController).player.shield <= 0)
                    {
                        RemoveBuff(buffsActive[index]);
                    }
                }
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
        switch (type)
        {
            case CharacterType.Player:
                PlayerController playerController = (PlayerController)chrController;

                switch (buff.type)
                {
                    // will of fire
                    case BuffType.ATK:
                        playerController.player.atk += buff.value;
                        break;

                    case BuffType.DEF:
                        playerController.player.def += buff.value;
                        break;

                    // sacrivert
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

                    // sanare
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

                    // preserve & invitro
                    case BuffType.Shield:
                        int index = buffsActive.FindIndex(buff => buff.type == BuffType.Shield);

                        // kalau sedang menggunakan shield, 
                        // maka hapus buff shield sebelumnya
                        if (index != -1)
                        {
                            buffsActive.RemoveAt(index);
                        }

                        // // tambahkan buff shield baru
                        // buffsActive.Add(buff);

                        playerController.player.maxShield = buff.value;
                        playerController.player.shield = buff.value;

                        break;

                    // thorn cover
                    case BuffType.Thorn:
                        break;
                }

                break;

            case CharacterType.Enemy:
                MobController mobController = (MobController)chrController;
                break;

        }

        buffsActive.Add(buff);
    }

    private void RemoveBuff(Buff buff)
    {
        switch (type)
        {
            case CharacterType.Player:
                PlayerController playerController = (PlayerController)chrController;

                switch (buff.type)
                {
                    // will of fire
                    case BuffType.ATK:
                        playerController.player.atk -= buff.value;
                        break;

                    // fudoshin
                    case BuffType.DEF:
                        playerController.player.def -= buff.value;
                        break;

                }
                break;
        }

        buffsActive.Remove(buff);
    }

    public float GetAllBuffValues(BuffType type)
    {
        List<Buff> buffs = buffsActive.FindAll(buff => buff.type == type);
        float sum = 0;
        foreach (Buff buff in buffs)
        {
            sum += buff.value;
        }
        return sum;
    }



}