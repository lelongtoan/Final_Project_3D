using System.Collections;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    
    public GameObject hpBuffUI;
    public GameObject mpBuffUI;
    public GameObject dmgBuffUI;

    public float buffDuration = 15f; //5p


    public Coroutine hpBuffCoroutine;
    public Coroutine mpBuffCoroutine;
    public Coroutine dmgBuffCoroutine;

    public void ActivateBuff(Item item)
    {
        switch (item.buff)
        {
            case Buff.HP:
                if (hpBuffCoroutine != null)
                {
                    StopCoroutine(hpBuffCoroutine);
                    ResetHPBuff(item.buffD);
                }
                hpBuffUI.SetActive(true);
                hpBuffCoroutine = StartCoroutine(ApplyBuff(
                    () => { GameInstance.instance.playerInfor.UpMaxHP(item.buffD); GameInstance.instance.playerInfor.healthPoint += item.buffD; },
                    () => { GameInstance.instance.playerInfor.UpMaxHP(-item.buffD); GameInstance.instance.playerInfor.healthPoint = Mathf.Min(GameInstance.instance.playerInfor.healthPoint, GameInstance.instance.playerInfor.maxHP); },
                    () => hpBuffUI.SetActive(false)
                ));
                break;

            case Buff.MP:
                if (mpBuffCoroutine != null)
                {
                    StopCoroutine(mpBuffCoroutine);
                    ResetMPBuff(item.buffD);
                }
                mpBuffUI.SetActive(true);
                mpBuffCoroutine = StartCoroutine(ApplyBuff(
                    () => { GameInstance.instance.playerInfor.UpMaxMP(item.buffD); GameInstance.instance.playerInfor.manaPoint += item.buffD; },
                    () => { GameInstance.instance.playerInfor.UpMaxMP(-item.buffD); GameInstance.instance.playerInfor.manaPoint = Mathf.Min(GameInstance.instance.playerInfor.manaPoint, GameInstance.instance.playerInfor.maxMP); },
                    () => mpBuffUI.SetActive(false)
                ));
                break;

            case Buff.Dmg:
                if (dmgBuffCoroutine != null)
                {
                    StopCoroutine(dmgBuffCoroutine);
                    ResetDMGBuff(item.buffD);
                }
                dmgBuffUI.SetActive(true);
                dmgBuffCoroutine = StartCoroutine(ApplyBuff(
                    () => GameInstance.instance.playerInfor.UpDame((int)item.buffD),
                    () => GameInstance.instance.playerInfor.UpDame((int)-item.buffD),
                    () => dmgBuffUI.SetActive(false)
                ));
                break;
        }
    }

    private IEnumerator ApplyBuff(System.Action applyEffect, System.Action resetEffect, System.Action uiReset)
    {
        Debug.Log("Buff started");
        applyEffect.Invoke();
        yield return new WaitForSeconds(buffDuration);
        Debug.Log("Buff ended");
        resetEffect.Invoke();
        uiReset.Invoke();
    }

    private void ResetHPBuff(float amount)
    {
        GameInstance.instance.playerInfor.UpMaxHP(-amount);
        GameInstance.instance.playerInfor.healthPoint = Mathf.Min(GameInstance.instance.playerInfor.healthPoint, GameInstance.instance.playerInfor.maxHP);
        hpBuffUI.SetActive(false);
    }

    private void ResetMPBuff(float amount)
    {
        GameInstance.instance.playerInfor.UpMaxMP(-amount);
        GameInstance.instance.playerInfor.manaPoint = Mathf.Min(GameInstance.instance.playerInfor.manaPoint, GameInstance.instance.playerInfor.maxMP);
        mpBuffUI.SetActive(false);
        mpBuffUI.SetActive(false);
    }

    private void ResetDMGBuff(float amount)
    {
        GameInstance.instance.playerInfor.UpDame((int)-amount);
        dmgBuffUI.SetActive(false);
    }
}
