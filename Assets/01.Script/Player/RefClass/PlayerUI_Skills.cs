using UnityEngine;
using UnityEngine.UI;

public class PlayerUI_Skills : MonoBehaviour
{
    internal Image SkillUiimage;
    internal Image TwoSkillUiimage;
    internal Image UltimateUiimage;
    internal Text SkillUiText;
    internal Text TwoSkillUiText;
    internal Text UltimateUiText;

    

    void Awake()
    {
        SkillUiimage = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        SkillUiText = transform.GetChild(0).GetChild(2).GetComponent<Text>();
        TwoSkillUiimage = transform.GetChild(1).GetChild(1).GetComponent<Image>();
        TwoSkillUiText = transform.GetChild(1).GetChild(2).GetComponent<Text>();
        UltimateUiimage = transform.GetChild(2).GetChild(1).GetComponent<Image>();
        UltimateUiText = transform.GetChild(2).GetChild(2).GetComponent<Text>();
    }
}
