using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class GameStart : MonoBehaviour
{
    public GameObject Player;
    private PlayerMovement _script_PlayerMovement;
    private float waitTime ;


    [SerializeField] private Animator fade;

    private void Awake()
    {
        _script_PlayerMovement = Player.GetComponent<PlayerMovement>();
    }

    void Start()
    {
        //game start with control disabled
        _script_PlayerMovement.enableControl = false;
        //at 0 is fade in
        waitTime = fade.runtimeAnimatorController.animationClips[0].length;
        StartCoroutine(startTheScene(waitTime));
    }

    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator startTheScene(float waitTime)
    {
        //play fade in
        fade.Play(fade.runtimeAnimatorController.animationClips[0].name);

        yield return new WaitForSeconds(waitTime);
        _script_PlayerMovement.enableControl = true;
    }

}
