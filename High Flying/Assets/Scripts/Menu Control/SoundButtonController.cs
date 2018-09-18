using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonController : MonoBehaviour {

    [SerializeField] private Button sourceButton;
    [SerializeField] private Sprite[] buttonImages = new Sprite[2];
    private BackGroundMusicPlay[] bgm;
    
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

    public void muteAndUnmute()
    {
      AudioListener.volume= (AudioListener.volume==0)?1:0;    
      try{
        bgm = FindObjectsOfType<BackGroundMusicPlay>();
        if(bgm.Length!=0)
        {
            foreach(BackGroundMusicPlay e in bgm)
            {
                e.switchPlay();
            }
        }
      }catch(UnassignedReferenceException)
      {
          print("no music player found, only mute and unmute the game");
      }
    }
}
