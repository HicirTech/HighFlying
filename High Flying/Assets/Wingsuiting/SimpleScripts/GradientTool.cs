using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void GradientToolSelectColorHandler(Color color);

public class GradientTool : MonoBehaviour
{
    public static event GradientToolSelectColorHandler OnSelectColor;

    [SerializeField] private Slider colorGradient;
    [SerializeField] private Slider colorLerp;
    [SerializeField] private Texture2D colorGradientTexture;
    [SerializeField]  private float maxWhite = 1.0f;

    private float lerp;
    private static Color color;

    void Awake()
    {
        lerp = 0.0f;
        colorGradient.onValueChanged.AddListener(OnColorChanged);
        colorLerp.onValueChanged.AddListener(OnColorLerp);
    }
    public static void SetStartColor(Color value)
    {
        color = value;
    }
    void OnColorChanged(float value)
    {
        if (OnSelectColor != null)
        {
            color = colorGradientTexture.GetPixelBilinear(value, 0.0f);
            OnSelectColor(Color.Lerp(color, Color.white, lerp));
        }
    }
    void OnColorLerp(float value)
    {
        if (OnSelectColor != null)
        {
            lerp = Mathf.Clamp01(value * maxWhite);
            OnSelectColor(Color.Lerp(color, Color.white, lerp));
        }
    }
}
