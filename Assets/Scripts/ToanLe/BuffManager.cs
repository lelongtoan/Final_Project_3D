using System.Collections;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    
    public GameObject hpBuffUI;
    public GameObject mpBuffUI;
    public GameObject dmgBuffUI;

    public float buffDuration = 300f; //5p

    private float maxHP = 100f;
    private float maxMP = 100f;
    private float dmg = 10f;

    public float currentHP;
    public float currentMP;

    private Coroutine hpBuffCoroutine;
    private Coroutine mpBuffCoroutine;
    private Coroutine dmgBuffCoroutine;
    private void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

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
                    () => { maxHP += item.buffD; currentHP += item.buffD; },
                    () => { maxHP -= item.buffD; currentHP = Mathf.Min(currentHP, maxHP); },
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
                    () => { maxMP += item.buffD; currentMP += item.buffD; },
                    () => { maxMP -= item.buffD; currentMP = Mathf.Min(currentMP, maxMP); },
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
                    () => dmg += item.buffD,
                    () => dmg -= item.buffD,
                    () => dmgBuffUI.SetActive(false)
                ));
                break;
        }
    }

    private IEnumerator ApplyBuff(System.Action applyEffect, System.Action resetEffect, System.Action uiReset)
    {
        applyEffect.Invoke();
        yield return new WaitForSeconds(buffDuration);
        resetEffect.Invoke();
        uiReset.Invoke();
    }

    private void ResetHPBuff(float amount)
    {
        maxHP -= amount;
        currentHP = Mathf.Min(currentHP, maxHP);
        hpBuffUI.SetActive(false);
    }

    private void ResetMPBuff(float amount)
    {
        maxMP -= amount;
        currentMP = Mathf.Min(currentMP, maxMP);
        mpBuffUI.SetActive(false);
    }

    private void ResetDMGBuff(float amount)
    {
        dmg -= amount;
        dmgBuffUI.SetActive(false);
    }
}
