using UnityEngine;
public abstract class PanelBase : MonoBehaviour
{
    public virtual void ShowPopup()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void HidePopup()
    {
        this.gameObject.SetActive(false);
    }
}
