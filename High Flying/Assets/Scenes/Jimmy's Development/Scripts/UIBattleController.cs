using UnityEngine;
using UnityEngine.UI;

public class UIBattleController : MonoBehaviour {

    private GameObject character;
    private MovementControl movementControl;
    private HitBuildingHandler hitBuldingHandler;
    private HealthSystem healthSystem;

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

            // get component HitBuiling to call action when level complete
            hitBuldingHandler = character.GetComponent<HitBuildingHandler>();
            hitBuldingHandler.onLevelComplete = () =>
            {
                gameOver.ShowPopup();
                gameOver.LevelComplete(1, 1, 1, 1);
            };

            // get component HealthSystem to call action when spaceship die
            healthSystem = character.GetComponent<HealthSystem>();
            healthSystem.onDie = () =>
            {
                gameOver.ShowPopup();
                gameOver.Die();
            };
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
