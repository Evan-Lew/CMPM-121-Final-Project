using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockExit : MonoBehaviour
{

    public static RockExit instance;
    private Rigidbody[] rocks;
    private Vector3 explodeCenter;
    private const float explodeForce = 5f;
    private const float rockFadeDelay = 3f;

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rocks = transform.Find("Rocks").GetComponentsInChildren<Rigidbody>();
        explodeCenter = transform.Find("ExplodePoint").transform.position;
    }

    public void Explode()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        foreach (Rigidbody rb in rocks)
        {
            rb.AddForce((rb.transform.position - explodeCenter).normalized * explodeForce, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(rockFadeDelay);
        foreach (Rigidbody rb in rocks)
        {
            rb.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Explode();
        }
    }
    
    
}
