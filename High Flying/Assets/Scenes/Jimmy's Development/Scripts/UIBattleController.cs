using UnityEngine;
using UnityEngine.UI;

public class UIBattleController : MonoBehaviour {

    private GameObject character;
    private MovementControl movementControl;

    #region UI_MAIN
    [SerializeField]
    private PauseAndMenu pauseAndMenu;
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
}
