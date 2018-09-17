using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MusicPlayerTest
{

    GameObject MusicPlayer;

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator MuiscPlayerPlaysMusicTest()
    {
        //    var musicPlayerPrefab = Resources.Load("");
        MusicPlayer = GameObject.FindWithTag("MusicPlayer");
        Assert.IsNotNull(MusicPlayer);
        AudioSource testObject = MusicPlayer.GetComponent(typeof(AudioSource)) as AudioSource;
        testObject.Play();
        yield return null;
        Assert.Equals(testObject.isPlaying, true);
    }

    [UnityTest]
    public IEnumerator MusicPlayerMuteTest()
    {
        var musicPlayerPrefab = Resources.Load("");
        MusicPlayer = GameObject.FindWithTag("MusicPlayer");
        AudioSource testObject = MusicPlayer.GetComponent(typeof(AudioSource)) as AudioSource;
        yield return null;
        testObject.Play();
        Assert.Equals(testObject.isPlaying, true);
        yield return null;// now music should play

        // some kind of mute method

        Assert.Equals(testObject.isPlaying, false);


    }
}
