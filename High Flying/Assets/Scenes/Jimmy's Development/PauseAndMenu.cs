using UnityEngine;
using UnityEngine.UI;

public class PauseAndMenu : MonoBehaviour {

    [Tooltip("Drag continue button here")][SerializeField]
    private Button btnContinue;
    [Tooltip("Drag back to menu button here")]
    [SerializeField]
    private Button btnBackToMenu;

    private void Start()
    {
        btnContinue.onClick.AddListener(ClickResumeButton);
        btnBackToMenu.onClick.AddListener(ClickBackToMenuButton);
    }

    /// <summary>
    /// when you click result, timescale will be return 1 and game continue
    /// </summary>
    public void ClickResumeButton()
    {
        HidePopup();
        ResumeGame();
    } 

    /// <summary>
    /// back to scene menu
    /// </summary>
    public void ClickBackToMenuButton()
    {
        // do something here
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ShowPopup()
    {
        this.gameObject.SetActive(true);
    }

    public void HidePopup()
    {
        this.gameObject.SetActive(false);
    }
}
