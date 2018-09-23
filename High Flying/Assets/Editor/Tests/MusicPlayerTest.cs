using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MusicPlayerTest {

    /// <summary>
    /// this will test the player will start with a unplay state
    /// </summary>
    [Test]
    public void MusicPlayerStartTest() {        
        BackGroundMusicPlay player = new BackGroundMusicPlay();
        Assert.IsFalse(player.isPlaying);
    }

    /// <summary>
    /// this will test the game object player will player the music
    /// have the ability to switch between switch play and stop by one key
    /// </summary>
    [Test]
    public void MusicPlayerStopAndStartPlay()
    {
        GameObject gameObjec = new GameObject();
        gameObjec.AddComponent<AudioSource>();
        gameObjec.AddComponent<BackGroundMusicPlay>();
        BackGroundMusicPlay testingPlayer = gameObjec.GetComponent<BackGroundMusicPlay>();
        testingPlayer.setupPlayer();
        bool isCurrentPlayer = testingPlayer.isPlaying;
        Assert.IsTrue(isCurrentPlayer);
        testingPlayer.switchPlay();
        isCurrentPlayer = testingPlayer.isPlaying;
        Assert.IsFalse(isCurrentPlayer);
    }
    
    /// <summary>
    /// this will test the mute button have the ability to mute the game
    /// </summary>
    [Test]
    public void MuteButtonMutesTheAudio()
    {
        AudioListener.volume = 1;
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<SoundButtonController>();
        SoundButtonController muteButton = gameObject.GetComponent<SoundButtonController>();

        Assert.IsNotNull(muteButton);
        Assert.NotZero(AudioListener.volume);
        muteButton.muteAndUnmute();
        Assert.Zero(AudioListener.volume);
        muteButton.muteAndUnmute();
        Assert.NotZero(AudioListener.volume);
    }
    
}
