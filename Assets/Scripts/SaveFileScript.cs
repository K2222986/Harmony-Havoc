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

    public void SetName(string name)
    {
        Name = name;
    }
    public string GetName()
    {
        return Name;
    }
    public void ChangeTime(int timeElapsed)
    {
        TimeElapsed = timeElapsed;
    }
}
