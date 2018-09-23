using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : PanelBase
{
    [SerializeField]
    private GameObject levelComplete, gameOver;
    [SerializeField]
    private GameObject bodyInfo;
    [SerializeField]
    private Text coinsCollectedText, ringCollectedText, RingMultiplierText, DifficultyMultiplierText, totalScoreText;
    [SerializeField]
    private Button mainMenuButton, levelSelectionButton, playAgainButton;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainPlay");
        });

        levelSelectionButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelSelect");
        });

        playAgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    /// <summary>
    // call when level complete
    /// update info about rings, coins, ring multiplier, difficult multiplier, total score
    /// </summary>
    public void LevelComplete(int coinsCollected, int ringsCollected, int ringMultiplier, int difficultyMultiplier)
    {
        print("level compelte");
        SetTop(true);
        SetBody(true);
        UpdateBodyInfo(coinsCollected, ringsCollected, ringMultiplier, difficultyMultiplier);
    }

    /// <summary>
    /// show popup to play again
    /// </summary>
    public void Die()
    {
        SetTop(false);
        SetBody(false);
    }

    private void SetTop(bool isWin)
    {
        levelComplete.SetActive(isWin);
        gameOver.SetActive(!isWin);
    }

    private void SetBody(bool isWin)
    {
        bodyInfo.SetActive(isWin);
    }

    private void UpdateBodyInfo(int coinsCollected, int ringsCollected, int ringMultiplier, int difficultyMultiplier)
    {
        // do something here
        //TODO
    }
}
