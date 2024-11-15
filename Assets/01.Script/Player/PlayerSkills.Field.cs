using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerSkills : MonoBehaviour
{
    private GameObject skillOne;
    private GameObject skillTwo;
    private GameObject ultimatePar;
    private GameObject ultimateSK;

    private Image SkillUiimage;
    private Image TwoSkillUiimage;
    
    private Image UltimateUiimage;
    private Text SkillUiText;
    private Text TwoSkillUiText;

    private Text UltimateUiText;

    private ParticleSystem SwordPar;

    private List<GameObject> Sk_List;

    private float oneskillTimer = 20f;
    private float twoskillTimer = 5f;
    private float ultimateTimer = 50f;
    
    private float twoskilltimerover;
    private float oneskilltimerover;
    private float ultimatetimerover;
  
    public bool isSkillings;

    private List<GameObject> summonedSkulls = new List<GameObject>();
    private const int maxSkulls = 3;

    private void GetValue()
    {
        skillOne = Resources.Load<GameObject>("Skill/SkillOne");
        skillTwo = Resources.Load<GameObject>("Skill/Sleash");
        ultimatePar = Resources.Load<GameObject>("Skill/SkillThree");
        ultimateSK = Resources.Load<GameObject>("Skill/Skeleton");
        
        PlayerUI_Skills skills = transform.GetComponentInChildren<PlayerUI_Skills>();
        SkillUiimage = skills.SkillUiimage;         // SkillUiimage = transform.GetChild(7).GetChild(3).GetChild(0).GetChild(1).GetComponent<Image>();
        SkillUiText = skills.SkillUiText;           // SkillUiText = transform.GetChild(7).GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        TwoSkillUiimage = skills.TwoSkillUiimage;   // TwoSkillUiimage = transform.GetChild(7).GetChild(3).GetChild(1).GetChild(1).GetComponent<Image>();
        TwoSkillUiText = skills.TwoSkillUiText; 
        UltimateUiimage = skills.UltimateUiimage;    // TwoSkillUiText = transform.GetChild(7).GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        UltimateUiText = skills.UltimateUiText;
        SwordPar = player.Attack.SwordBox.transform.GetChild(0).GetComponent<ParticleSystem>();
        // SwordPar = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
    }

    private void SetValue()
    {
        SwordPar.Stop();
        SkillUiText.enabled = false;
        TwoSkillUiText.enabled = false;
        UltimateUiText.enabled = false;
    }
}