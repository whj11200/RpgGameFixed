using UnityEngine;
using UnityEngine.UI;

public partial class PlayerHP : MonoBehaviour
{
    private Image hpImage;
    private Text hpText;
    private float hp;
    private readonly float maxhp = 100f;

    private void SetValue()
    {
        SetHealth(maxhp);
        hpImage = transform.GetChild(7).GetChild(1).GetComponent<Image>();
        hpText = transform.GetChild(7).GetChild(2).GetComponent<Text>();
    }
}