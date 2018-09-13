using UnityEngine;
using UnityEngine.UI;

public class UIBattleController : MonoBehaviour {

    private GameObject character;
    private MovementControl movementControl;

    #region UI_MAIN
    [SerializeField]
    private PauseAndMenu pauseAndMenu;
    [SerializeField]
    private Button btnRightPause, btnLeftPause;
    [SerializeField]
    private Button btnJump;
    #endregion

    private void Start()
    {
        var character = GameObject.FindGameObjectWithTag("Character");
        if (character)
        {
            // get component MovementControl in character
            movementControl = character.GetComponentInChildren<MovementControl>();
        }
        else
        {
            throw new System.Exception("Can't find 'character' in your scene");
        }

        btnRightPause.onClick.AddListener(PauseGame);
        btnLeftPause.onClick.AddListener(PauseGame);

        btnJump.enabled = movementControl.EnableJump;
        btnJump.onClick.AddListener(()=> {
            movementControl.Jump();
        });
    }

    private void PauseGame()
    {
        pauseAndMenu.PauseGame();
        pauseAndMenu.ShowPopup();
    }
}
