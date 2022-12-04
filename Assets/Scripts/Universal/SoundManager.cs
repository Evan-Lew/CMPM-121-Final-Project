using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{

    //Note: To add anyother audio
    /*           1. put your audio into folder called "Resources/SFX"
     *           2. add load in Void Start()
     *           3. add it into PlaySound() at the end of this script
     *           4. call function in other script with ->  SoundManager.PlaySound("sfx_Explosion", 1)  
     */


    [HideInInspector]public static AudioClip sfx_Explosion, sfx_Jump, sfx_Footstep;

    [HideInInspector]public static AudioSource audioSrc;
    [HideInInspector]public static AudioSource audioSrc_footStep;

    void Start()
    {
        //loading
        sfx_Explosion = Resources.Load<AudioClip>("SFX/Explosion");
        sfx_Jump = Resources.Load<AudioClip>("SFX/Jump");
        sfx_Footstep = Resources.Load<AudioClip>("SFX/Footstep");

        audioSrc = GetComponent<AudioSource>();
        audioSrc_footStep = GameObject.Find("SFX/SoundManager/FootStep").GetComponent<AudioSource>();
    }

    //example call in other script
    //SoundManager.PlaySound("sfx_Explosion", 1);



    //clip: which sound u want to play
    //volumn: how loud u want the sound want to be
    public static void PlaySound(string clip, float volumn)
    {
        switch (clip)
        {
            case "sfx_Explosion":
                audioSrc.clip = sfx_Explosion;
                audioSrc.volume = volumn;
                audioSrc.PlayOneShot(audioSrc.clip);
                break;

            case "sfx_Jump":
                audioSrc.clip = sfx_Jump;
                audioSrc.volume = volumn;
                audioSrc.PlayOneShot(audioSrc.clip);
                break;


            //footstep will be paused when it's not moving, so I use another audio source to avoid pause other clips at same time
            case "sfx_Footstep":
                audioSrc_footStep.clip = sfx_Footstep;
                audioSrc_footStep.volume = volumn;
                audioSrc_footStep.Play();
                break;
        }
    }


}
