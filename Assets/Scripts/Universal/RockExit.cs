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


    public KeyCode explodeKey = KeyCode.K;

    private void Awake()
    {
        instance = this;
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
        if (Input.GetKeyDown(explodeKey))
        {
            Explode();
        }
    }
    
    
}
