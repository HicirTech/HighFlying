using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonController : MonoBehaviour {

    [SerializeField]
    private Button sourceButton;
    [SerializeField]
    private Sprite[] buttonImages = new Sprite[2];
    BackGroundMusicPlay bgm;
    
    public void onSoundPress()
    {
        changeButtonSprite();
        muteAndUnmute();

    }

    private void changeButtonSprite()
    {
        /*/
        if (sourceButton.image.sprite.Equals(buttonImages[0]))
        {
            sourceButton.image.sprite = buttonImages[1];
        }
        else
        {
            sourceButton.image.sprite = buttonImages[0];
        }*/

        sourceButton.image.sprite =sourceButton.image.sprite.Equals(buttonImages[0])? buttonImages[1]:buttonImages[0];

    }
    private void muteAndUnmute()
    {
      AudioListener.volume= (AudioListener.volume==0)?1:0;    
      bgm = FindObjectOfType<BackGroundMusicPlay>();
      this.bgm.switchPlay();
    }
}
