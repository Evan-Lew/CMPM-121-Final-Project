using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public SnapshotCamera snapCam;
    public KeyCode snapShotKey;
    void Update()
    {
        if (Input.GetKeyDown(snapShotKey))
        {
            snapCam.CallTakeSnapshot();
        }
    }
}
