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
    Thorn,
    Fire,
    Earth,
    Water,
    Air,
    Custom
}

public class Buff
{
    public string id;
    public string name;
    public BuffType type;
    public float value;
    public float timer;
    public IEnumerator coroutine;

    public Buff(string id, string name, BuffType type, float value, float timer)
    {
        this.id = id;
        this.name = name;
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
            buff.coroutine = CoroutineBuff(buff);
            StartCoroutine(buff.coroutine);
        }
    }

    public IEnumerator CoroutineBuff(Buff buff)
    {
        AddBuff(buff);
        yield return new WaitForSeconds(buff.timer);

        // kalau misalnya sebelum waktunya habis
        // tapi buffnya sudah hilang,
        // tidak perlu remove lagi
        // biasanya terjadi saat efek skill te reset 
        // karena player menekan skill sebelum efek skill habis
        if (CheckBuff(buff))
        {
            RemoveBuff(buff);
        }
    }

    public bool CheckBuff(BuffType type)
    {
        if (buffsActive.FindIndex(buff => buff.type == type) != -1)
        {
            return true;
        }
        return false;
    }

    public bool CheckBuff(string name)
    {
        if (buffsActive.FindIndex(buff => buff.name == name) != -1)
        {
            return true;
        }
        return false;
    }

    public bool CheckBuff(Buff buff)
    {
        if (buffsActive.Contains(buff))
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

                // untuk semua buff yg memiliki waktu,
                // maka buff waktu akan direset
                // dan efek dari buff tidak akan berganda
                if (buff.timer > 0)
                {
                    ResetSimiliarBuff(buff);
                }

                switch (buff.type)
                {
                    // will of fire
                    case BuffType.ATK:
                        playerController.player.atk += buff.value;
                        break;

                    case BuffType.DEF:
                        playerController.player.def += buff.value;
                        break;

                    // light step
                    case BuffType.AGI:
                        playerController.player.agi += buff.value;
                        break;

                    // calm
                    case BuffType.FOC:
                        playerController.player.foc += buff.value;
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
                        // int index = buffsActive.FindIndex(buff => buff.type == BuffType.Shield);

                        // kalau sedang menggunakan shield, 
                        // maka hapus buff shield sebelumnya
                        ResetSimiliarBuff(buff);

                        playerController.player.maxShield = buff.value;
                        playerController.player.shield = buff.value;

                        break;

                    // thorn cover
                    case BuffType.Thorn:
                        break;


                }

                break;

                // case CharacterType.Enemy:
                //     MobController mobController = (MobController)chrController;
                //     break;

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

                    // calm
                    case BuffType.FOC:
                        playerController.player.foc -= buff.value;
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

    private void ResetSimiliarBuff(Buff buff)
    {
        // kalau sedang menggunakan shield, 
        // maka hapus buff shield sebelumnya
        if (CheckBuff(buff.name))
        {
            int prevBuffIndex = buffsActive.FindIndex(buffActive => buffActive.name == buff.name);
            RemoveBuff(buffsActive[prevBuffIndex]);
            // StopCoroutine(buff.coroutine);
        }

    }

    private bool BuffisSkill(Buff buff)
    {
        if (buff.id.Contains("skill"))
        {
            return true;
        }
        return false;

    }

}