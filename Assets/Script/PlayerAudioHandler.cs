using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour {

    public AudioSource[] audios;

    public bool soundIsMuted = false;

    // Use this for initialization
    void Start () {
        AudioSource[] audios = GetComponents<AudioSource>();
        //audios[0] coinCollect
        //audios[1] jump
        //audios[2] bgMusic
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ToggleMuteSound()
    {
        soundIsMuted = !soundIsMuted;

        foreach (AudioSource a in audios)
        {
            a.mute = soundIsMuted;
        }
    }

    public void PlaySound(string name)
    {
        switch (name)
        {
            case "coin":
                if(!audios[0].isPlaying)
                    audios[0].Play();
                break;
            case "jump":

                break;
            case "bgMusic":

                break;
            default:
                Debug.Log(string.Format("{0} is an invalid sound name.",name));
                break;

        }
    }
}
