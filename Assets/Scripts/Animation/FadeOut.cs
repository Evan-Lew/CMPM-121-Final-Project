using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
   // public bool enableFadeOut;
    GameObject Player;
    PlayerMovement Player_script_playermovement;
    public Animator anim;

    //if u change this varibaAnimator anim;le, u need to mannually edit the animation as well
    float fadeInTime = 3;
    float fadeWaitTime = 2;
    float fadeOutTime = 3;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        Player_script_playermovement = Player.GetComponent<PlayerMovement>();


    }


    [SerializeField] public void FadingEvent()
    {
        StartCoroutine(fadeControl());
    }


    [HideInInspector]
    public void teleport()
    {
        Player.transform.position = Player_script_playermovement.startPos;

    }

    private IEnumerator fadeControl()
    {
        anim.Play("Fading");
        Player_script_playermovement.enableControl = false;
        //call teleport while in the dark
        yield return new WaitForSeconds(fadeInTime + (fadeWaitTime / 2));
        teleport();
        yield return new WaitForSeconds(fadeOutTime);

        //reenable control and pitbottom trigger
        Player_script_playermovement.enableControl = true;
        Player_script_playermovement.enablePitFallFeature = true;
        gameObject.SetActive(false);
    }


}
