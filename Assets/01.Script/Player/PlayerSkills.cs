using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerSkills : MonoBehaviour
{
    [HideInInspector] public Player player;

    void Start()
    {
        GetValue();
        SetValue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time >= twoskilltimerover && !isSkillings && !isTwoskilling)
        {
            TwoSkillUiimage.fillAmount = 0;
            TwoSkillUiText.enabled = true;
            StartCoroutine(Skilltwo());
            StartCoroutine(UpdateSkillTwoUi());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time >= oneskilltimerover && !isSkillings)
        {
            SkillUiimage.fillAmount = 0;
            SkillUiText.enabled = true;
            StartCoroutine(Skillone());
            StartCoroutine(UpdateSkillUi());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(Ultimate());
        }
    }

    IEnumerator Skillone()
    {
        isSkillings = true;
        isOneskilling = true;
        oneskilltimerover = Time.time + oneskillTimer;
        player.ani.SetTrigger("Skillone");
        yield return new WaitForSeconds(0.5f);
        float distanceLevel = 10f;
        Vector3 skilldistance = transform.position + transform.forward * distanceLevel;
        var skillone = Instantiate(skillOne, skilldistance, transform.rotation);
        SwordPar.Stop();
        yield return new WaitForSeconds(0.5f);
        isOneskilling = false;
        isSkillings = false;
    }

    IEnumerator Skilltwo()
    {
        isTwoskilling = true;
        isSkillings = true;
        twoskilltimerover = twoskilltimerover + twoskillTimer;
        player.ani.SetTrigger("Skilltwo");
        yield return new WaitForSeconds(0.5f);
        float pluesYpos = 1.5f;
        Vector3 skillUppos = transform.position + transform.up * pluesYpos;
        var skilltwo = Instantiate(skillTwo, skillUppos, transform.rotation);
        isSkillings = false;
        yield return new WaitForSeconds(2f);
        Destroy(skilltwo);
    }

    IEnumerator Ultimate()
    {
        isSkillings = true;
        ultimatering = true;
        player.ani.SetTrigger("Ultimate");

        yield return new WaitForSeconds(1.14f);

        Vector3 skillup = transform.position+ transform.up * 1.5f;
        var snowWould = Instantiate(ultimatePar, transform.position, ultimatePar.transform.rotation);
        isSkillings = false;

        yield return new WaitForSeconds(2.8f);
        for (int i = 0; i < maxSkulls; i++)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);
            Vector3 randomDirection = new Vector3(randomX, 0, randomZ).normalized;

            Vector3 skullSpawnPosition = snowWould.transform.position + randomDirection * 2;

            GameObject skull = Instantiate(ultimateSK, skullSpawnPosition, Quaternion.identity);
            summonedSkulls.Add(skull);
            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(21f);
        foreach (var skull in summonedSkulls)
        {
            Destroy(skull);
        }
        summonedSkulls.Clear();
        Destroy(snowWould);
    }

    IEnumerator UpdateSkillUi()
    {
        float elapsedTime = 0f;
        SkillUiText.text = $"{(int)elapsedTime}";
        while (elapsedTime < oneskillTimer)
        {
            float remainingTime = oneskillTimer - elapsedTime;
            SkillUiText.text = $"{(int)remainingTime}";
            SkillUiimage.fillAmount = elapsedTime / oneskillTimer;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SkillUiimage.fillAmount = 1;
        SkillUiText.enabled = false;
    }

    IEnumerator UpdateSkillTwoUi()
    {
        float elapsedTime = 0f;
        TwoSkillUiText.text = $"{(int)elapsedTime}";
        while (elapsedTime < twoskillTimer)
        {
            float remainingTime = twoskillTimer - elapsedTime;
            TwoSkillUiText.text = $"{(int)remainingTime}";
            TwoSkillUiimage.fillAmount = elapsedTime / twoskillTimer;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        TwoSkillUiimage.fillAmount = 1;
        TwoSkillUiText.enabled = false;
        isTwoskilling = false;
    }

    void SwordSkillEffect()
    {
        SwordPar.Play();
    }
}
