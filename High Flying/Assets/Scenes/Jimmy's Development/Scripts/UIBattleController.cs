using UnityEngine;
using UnityEngine.UI;

public class UIBattleController : MonoBehaviour {

    private GameObject character;
    private MovementControl movementControl;
    private HitBuildingHandler hitBuldingHandler;
    private HealthSystem healthSystem;
    private PointSystem pointSystem;
    private CoinSystem coinSystem;

    #region UI_MAIN
    [SerializeField]
    private PauseAndMenu pauseAndMenu;
    [SerializeField]
    private GameOver gameOver;
    [SerializeField]
    private Button btnPause;
    [SerializeField]
    private Button btnRightJump, btnLeftJump;
    #endregion

    private void Start()
    {
        var character = GameObject.FindGameObjectWithTag("Character");
        if (character)
        {
            // get component MovementControl in character
            movementControl = character.GetComponentInChildren<MovementControl>();
            Debug.AssertFormat(movementControl != null, "movementControl cant be null");

            // get component HitBuiling to call action when level complete
            hitBuldingHandler = character.GetComponent<HitBuildingHandler>();
            Debug.AssertFormat(hitBuldingHandler != null, "hitBuldingHandler cant be null");
            hitBuldingHandler.onLevelComplete = () =>
            {
                gameOver.ShowPopup();
                gameOver.LevelComplete(coinSystem.coins, pointSystem.getRingsPassedCounter(), 1, healthSystem.difficultyRating, pointSystem.points);
            };

            // get component HealthSystem to call action when character die
            healthSystem = character.GetComponent<HealthSystem>();
            Debug.AssertFormat(hitBuldingHandler != null, "healthSystem cant be null");
            healthSystem.onDie = () =>
            {
                gameOver.ShowPopup();
                gameOver.Die();
            };

            // get component PointSystem to get point and ring
            pointSystem = character.GetComponent<PointSystem>();
            Debug.AssertFormat(pointSystem != null, "pointSystem cant be null");

            // get component CoinSystem to get coins
            coinSystem = character.GetComponent<CoinSystem>();
            Debug.AssertFormat(coinSystem != null, "coinSystem cant be null");
        }
        else
        {
            throw new System.Exception("Can't find 'character' in your scene");
        }

        btnPause.onClick.AddListener(PauseGame);

        btnLeftJump.enabled = btnRightJump.enabled = movementControl.EnableJump;
        btnLeftJump.onClick.AddListener(movementControl.Jump);
        btnRightJump.onClick.AddListener(movementControl.Jump);
    }

    private void PauseGame()
    {
        pauseAndMenu.PauseGame();
        pauseAndMenu.ShowPopup();
    }

    public PauseAndMenu getPauseAndMenu() //this method used to initialise pauseAndMenu Object
    {
        pauseAndMenu = new PauseAndMenu();
        return pauseAndMenu;
    }
}
