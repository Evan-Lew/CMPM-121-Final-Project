using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCollision : MonoBehaviour
{
    [SerializeField] private Animator fade;
    private float waitTime;

    public GameObject Player;
    private PlayerMovement _script_PlayerMovement;

    private void Start()
    {
        //at 1 is fade out
        waitTime = fade.runtimeAnimatorController.animationClips[1].length;
        _script_PlayerMovement = Player.GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(endTheScene(waitTime));
    }


    IEnumerator endTheScene(float waitTime)
    {
        //unable playercontrol
        _script_PlayerMovement.enableControl = false;

        //play fade out
        fade.Play(fade.runtimeAnimatorController.animationClips[1].name);
        yield return new WaitForSeconds(waitTime);
        //load the end scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);

    }



}
