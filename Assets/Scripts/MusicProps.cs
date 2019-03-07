using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicProps : MonoBehaviour
{

    public static AudioSource myMusic;
    public static AudioClip backgroundMusic, jumpSound, enemyDeathSound, ringSound, transformationSound;

    // Update is called once per frame

    void Start()
    {
        
        myMusic = GetComponent<AudioSource>();

        jumpSound = Resources.Load<AudioClip>("jumpSound");
        enemyDeathSound = Resources.Load<AudioClip>("enemyDeathSound");
        ringSound = Resources.Load<AudioClip>("ringSound");
        transformationSound = Resources.Load<AudioClip>("superSonicYell");
        backgroundMusic = Resources.Load<AudioClip>("theme");

        myMusic.volume = PlayerPrefs.GetFloat("MusicVolume");

    }

    void Update()
    {  
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "enemyDeathSound":
                myMusic.PlayOneShot(enemyDeathSound);
                break;

            case "jumpSound":
                myMusic.PlayOneShot(jumpSound);
                break;

            case "ringSound":
                myMusic.PlayOneShot(ringSound, 0.05f);
                break;

            case "superSonicYell":
                myMusic.PlayOneShot(transformationSound, 0.3f);
                break;

            case "theme":
                myMusic.PlayOneShot(backgroundMusic);
                break;
        }
    }
}
