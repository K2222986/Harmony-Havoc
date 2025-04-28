using MyGameUILibrary;
using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public static Combo Instance;
    public int streak;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI multiplierText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        comboText.text = "Combo: " + streak;
        if (streak < 60)
        {
            RadialProgressComponent.Instance.m_RadialProgress.progress = (streak % 20) * 5;
        }
        else
        {
            RadialProgressComponent.Instance.m_RadialProgress.progress = 100;
        }
        if (streak < 20)
        {
            multiplierText.text = "1x";
        }
        else if (streak < 40)
        {
            multiplierText.text = "2x";
        }
        else if (streak < 60)
        {
            multiplierText.text = "3x";
        }
        else
        {
            multiplierText.text = "4x";
        }
    }
}
