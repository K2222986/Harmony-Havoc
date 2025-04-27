using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public static Combo Instance;
    public int streak;
    public TextMeshProUGUI comboText;

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
    }
}
