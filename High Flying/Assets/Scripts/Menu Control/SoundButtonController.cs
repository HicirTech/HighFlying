using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonController : MonoBehaviour {

    [SerializeField]
    private Button sourceButton;
    [SerializeField]
    private Sprite[] buttonImages = new Sprite[2];

    public void onSoundPress()
    {
        changeButtonSprite();
    }

    private void changeButtonSprite()
    {
        if (sourceButton.image.sprite.Equals(buttonImages[0]))
        {
            sourceButton.image.sprite = buttonImages[1];
        }
        else
        {
            sourceButton.image.sprite = buttonImages[0];
        }
    }
}
