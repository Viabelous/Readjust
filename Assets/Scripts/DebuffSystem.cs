using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DebuffSystem : MonoBehaviour
{
    public CharacterType type;

    [HideInInspector]
    public List<Buff> debuffsActive = new List<Buff>();
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

    public void ActivateDebuff(Buff debuff)
    {
        if (debuff.timer == 0)
        {
            AddDebuff(debuff);
        }
        else
        {
            debuff.coroutine = CoroutineDebuff(debuff);
            StartCoroutine(debuff.coroutine);
        }
    }
    public IEnumerator CoroutineDebuff(Buff debuff)
    {
        AddDebuff(debuff);
        yield return new WaitForSeconds(debuff.timer);

        // kalau misalnya sebelum waktunya habis
        // tapi debuffnya sudah hilang,
        // tidak perlu remove lagi
        // biasanya terjadi saat efek skill te reset 
        // karena player menekan skill sebelum efek skill habis
        if (CheckDebuff(debuff))
        {
            RemoveDebuff(debuff);
        }
    }

    public void DeactivateDebuff(Buff debuff)
    {
        RemoveDebuff(debuff);
    }
    public void DeactivateAllRelatedBuff(string name)
    {
        List<Buff> debuffDetected = debuffsActive.FindAll(debuff => debuff.name == name);

        foreach (Buff debuff in debuffDetected)
        {
            RemoveDebuff(debuff);
        }
    }
    public bool CheckDebuff(BuffType type)
    {
        if (debuffsActive.FindIndex(debuff => debuff.type == type) != -1)
        {
            return true;
        }
        return false;
    }

    public bool CheckDebuff(string name)
    {
        if (debuffsActive.FindIndex(debuff => debuff.name == name) != -1)
        {
            return true;
        }
        return false;
    }

    public bool CheckDebuff(Buff debuff)
    {
        if (debuffsActive.Contains(debuff))
        {
            return true;
        }
        return false;
    }


    private void AddDebuff(Buff debuff)
    {
        switch (type)
        {
            case CharacterType.Player:
                PlayerController playerController = (PlayerController)chrController;

                switch (debuff.type)
                {
                    // holy sonata
                    case BuffType.ATK:
                        playerController.player.Downgrade(Stat.ATK, debuff.value);
                        break;

                    // holy sonata
                    case BuffType.DEF:
                        playerController.player.Downgrade(Stat.DEF, debuff.value);

                        break;
                }

                break;

        }
        debuffsActive.Add(debuff);
    }

    private void RemoveDebuff(Buff debuff)
    {
        if (!debuffsActive.Contains(debuff))
        {
            return;
        }

        switch (type)
        {
            case CharacterType.Player:
                PlayerController playerController = (PlayerController)chrController;

                switch (debuff.type)
                {
                    // holy sonata
                    case BuffType.ATK:
                        playerController.player.Upgrade(Stat.ATK, debuff.value);
                        break;

                    // holy sonata
                    case BuffType.DEF:
                        playerController.player.Upgrade(Stat.DEF, debuff.value);
                        break;

                }
                break;
        }
        debuffsActive.Remove(debuff);
    }

}