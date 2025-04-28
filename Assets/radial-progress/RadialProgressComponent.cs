using MyGameUILibrary;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class RadialProgressComponent : MonoBehaviour
{
    public RadialProgress m_RadialProgress;
    public static RadialProgressComponent Instance;
    public int posx;
    public int posy;

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

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        m_RadialProgress = new RadialProgress()
        {
            style = {
                position = Position.Absolute,
                left = posx, top = posy, width = 200, height = 200,
                fontSize = 48, alignSelf = Align.Center, unityTextAlign = TextAnchor.LowerCenter, justifyContent = Justify.Center,
                unityFontStyleAndWeight = FontStyle.Bold,
            }
        };
        root.Add(m_RadialProgress);
    }


}
