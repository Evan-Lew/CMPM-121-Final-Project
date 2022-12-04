using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        //this.gameObject.GetComponent<Light>().enabled = false;
        this.gameObject.GetComponent<Light>().intensity = Random.Range(5f, 7f);
        timeDelay = Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(timeDelay);
        //this.gameObject.GetComponent<Light>().enabled = true;
        this.gameObject.GetComponent<Light>().intensity = Random.Range(8f, 10f);
        timeDelay = Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
