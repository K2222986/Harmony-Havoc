using TMPro;
using UnityEngine;

public class SaveFileScript : MonoBehaviour
{
    public string Name;
    public int TimeElapsed;
    public TextMeshProUGUI SaveText;


    public void LoadName()
    {
        SaveText.text = Name;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }
    public void ChangeTime(int timeElapsed)
    {
        TimeElapsed = timeElapsed;
    }
}
