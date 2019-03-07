using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour {

    public Slider Volume;
    public AudioSource myMusic;
    
	// Update is called once per frame
    void Start()
    {
        if(PlayerPrefs.GetFloat("MusicVolume") != 0)
            Volume.value = PlayerPrefs.GetFloat("MusicVolume");
    }

	void Update () {
        myMusic.volume = Volume.value;
        PlayerPrefs.SetFloat("MusicVolume", myMusic.volume);
	}
}
