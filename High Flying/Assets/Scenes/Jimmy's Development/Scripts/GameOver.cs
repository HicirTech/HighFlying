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
    private Text coinsCollectedText, ringCollectedText, ringMultiplierText, difficultyMultiplierText, totalScoreText;
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
    public void LevelComplete(int coinsCollected, int ringsCollected, int ringMultiplier, int difficultyMultiplier, float totalScore)
    {
        print("level compelte");
        SetTop(true);
        SetBody(true);
        UpdateBodyInfo(coinsCollected, ringsCollected, ringMultiplier, difficultyMultiplier, totalScore);
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

    private void UpdateBodyInfo(int coinsCollected, int ringsCollected, int ringMultiplier, int difficultyMultiplier, float totalScore)
    {
        coinsCollectedText.text = string.Format("{0}/20", coinsCollected);
        ringCollectedText.text = string.Format("{0}/10", ringsCollected);
        ringMultiplierText.text = ringMultiplier.ToString();
        difficultyMultiplierText.text = difficultyMultiplier.ToString();
        totalScoreText.text = totalScore.ToString();
    }
}
