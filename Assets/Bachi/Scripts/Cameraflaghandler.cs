using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraflaghandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera Cameraobj;
    public CameraClearFlags currentcamerflag;
    public Color Applycolortocamera;

    private void Start()
    {
        return;
        
            Cameraobj.clearFlags = currentcamerflag;
            Cameraobj.backgroundColor = Applycolortocamera;
        
    }
}
