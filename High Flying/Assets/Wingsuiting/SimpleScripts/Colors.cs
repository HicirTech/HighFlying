using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Colors : MonoBehaviour 
{
	[SerializeField] private Material[] materials;
	[SerializeField] private string[] ids = new string[]{ "_Color" };
	public Color[] savedColors;
	private int id;

    void GradientTool_OnSelectColor (Color color)
    {
		materials[id].SetColor(ids[id], color);
    }
        
	void Start()
	{
        savedColors = new Color[materials.Length];
        for (int i = 0; i < savedColors.Length; i++) 
		{
			savedColors[i] = materials[i].GetColor(ids[i]);
			Debug.Log (ids[i]);
		}
		id = 0;
		GradientTool.OnSelectColor += GradientTool_OnSelectColor;
	}
    void OnDisable()
    {
        GradientTool.OnSelectColor -= GradientTool_OnSelectColor;
        Resset();
    }
    public void Resset()
    {
        for (int i = 0; i < savedColors.Length; i++) 
        {
			materials[i].SetColor(ids[i], savedColors[i]);
        }
    }
	public void OnColorsChanged(Dropdown dropdown)
	{
		id = dropdown.value;
	}
}
