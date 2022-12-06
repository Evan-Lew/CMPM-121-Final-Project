using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockExit : MonoBehaviour
{

    public static RockExit instance;
    private Rigidbody[] rocks;
    private Vector3 explodeCenter;
    private const float explodeForce = 10f;
    private const float rockFadeDelay = 3f;
    private bool isExploded = false;

    public KeyCode explodeKey = KeyCode.K;

    //refer to list build script
    InteractiveObjectManager _script_interactiveObjectManager;

    private void Awake()
    {

        instance = this;
        _script_interactiveObjectManager = GameObject.Find("Interactable Objects").GetComponent<InteractiveObjectManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rocks = transform.Find("Rocks").GetComponentsInChildren<Rigidbody>();
        explodeCenter = transform.Find("ExplodePoint").transform.position;
        foreach (Rigidbody rb in rocks)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void Explode()
    {
        SoundManager.PlaySound("sfx_Explosion", 0.3f);
        StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        foreach (Rigidbody rb in rocks)
        {
            Vector3 movementDir = (rb.transform.position - explodeCenter).normalized;
            rb.AddForce(movementDir * explodeForce, ForceMode.Impulse);
            //rb.velocity = movementDir * explodeForce;
            rb.constraints = RigidbodyConstraints.None;
        }

        yield return new WaitForSeconds(rockFadeDelay);
        foreach (Rigidbody rb in rocks)
        {
            Destroy(rb.gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {
        //key designed for programmer only
        if (Input.GetKeyDown(explodeKey) || _script_interactiveObjectManager.isFoundAllKey)
        {
            if (!isExploded)
            {
                ExitColliderController.instance.GetComponent<BoxCollider>().enabled = false;
                Explode();
                isExploded = true;
            }
        }
    }


}
