using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


public class UIGameController : MonoBehaviour
{

    private GameObject character;
    private MovementControl movementControl;
    private HitBuildingHandler hitBuldingHandler;
    private HealthSystem healthSystem;
    private PointSystem pointSystem;
    private CoinSystem coinSystem;
    private int isPressedLeftButton = 0, isPressedRightButton = 0;

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
        GameObject character = GameObject.FindGameObjectWithTag("Character");
        if (character)
        {
            //get all subcomponents
            getSubComponents(character);
            // get component HitBuiling to 
            //set trigger  to call action when level complete
            setTriggerLevelCompleted();
            //set trigger to call action when character die
            setTriggerDie();
        }
        else
        {
            throw new System.Exception("Can't find 'character' in your scene"); // if cant find then throw the exception
        }

        btnLeftJump.enabled = btnRightJump.enabled = movementControl.EnableJump; //enable all 3 buttons
        addListenerToButton(); //add listeners to button pause, left and right
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

    //this method is to add Listeners to Buttons pause, left and right
    public void addListenerToButton()
    {
        btnPause.onClick.AddListener(PauseGame);
        btnLeftJump.onClick.AddListener(movementControl.Jump);
        btnRightJump.onClick.AddListener(movementControl.Jump);
    }

    public void onMoveButtonUp(string name)
    {
        switch (name)
        {
            case "Right":
                isPressedRightButton = 0;
                break;
            case "Left":
                isPressedLeftButton = 0;
                break;
        }
        UpdateMove();
    }

    public void onMoveButtonDown(string name)
    {
        switch (name)
        {
            case "Right":
                isPressedRightButton = 1;
                break;
            case "Left":
                isPressedLeftButton = 1;
                break;
        }
        UpdateMove();
    }

    private void UpdateMove()
    {
        var speed = isPressedRightButton - isPressedLeftButton;

        CrossPlatformInputManager.SetAxis("Horizontal", speed);
    }

    private void getMovementControl(GameObject character)
    {
        movementControl = character.GetComponentInChildren<MovementControl>();
        Debug.AssertFormat(movementControl != null, "movementControl cant be null");
    }

    private void gethitBuldingHandler(GameObject character)
    {
        hitBuldingHandler = character.GetComponent<HitBuildingHandler>();
        Debug.AssertFormat(hitBuldingHandler != null, "hitBuldingHandler cant be null");
    }

    private void getComponentHealthSystem(GameObject character)
    {
        healthSystem = character.GetComponent<HealthSystem>();
        Debug.AssertFormat(hitBuldingHandler != null, "healthSystem cant be null");
    }

    private void getComponentPointSystem(GameObject character)
    {
        pointSystem = character.GetComponent<PointSystem>();
        Debug.AssertFormat(pointSystem != null, "pointSystem cant be null");
    }

    private void getComponentCoinSystem(GameObject character)
    {
        coinSystem = character.GetComponent<CoinSystem>();
        Debug.AssertFormat(coinSystem != null, "coinSystem cant be null");
    }

    private void setTriggerLevelCompleted()
    {
        hitBuldingHandler.onLevelComplete += () =>
        {
            gameOver.ShowPopup();
            gameOver.LevelComplete(coinSystem.getCoins(),
                                   pointSystem.getRingsPassedCounter(),
                                   pointSystem.getRingMultiplier(),
                                   healthSystem.difficultyRating,
                                   pointSystem.getPoints());
        };
    }

    private void setTriggerDie()
    {
        healthSystem.onDie = () =>
        {
            gameOver.ShowPopup();
            gameOver.Die();
        };
    }

    private void getSubComponents(GameObject character)
    {
        // get component MovementControl in character
        getMovementControl(character);
        //get all components
        //get componentBuilfingHandler
        gethitBuldingHandler(character);
        // get component HealthSystem 
        getComponentHealthSystem(character);
        // get component PointSystem to get point and ring
        getComponentPointSystem(character);
        // get component CoinSystem to get coins
        getComponentCoinSystem(character);
    }
}
