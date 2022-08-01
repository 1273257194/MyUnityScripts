using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public ILRuntimeInit ilRuntimeInit;
    public Action startAction;
    void Start()
    {
        //ilRuntimeInit.appdomain.Invoke("UnityHotFix.Properties.InstanceClass", "Start", null, null);
    }

    // Update is called once per frame
    void Update()
    {
        
        ilRuntimeInit.appdomain.Invoke("UnityHotFix.Properties.InstanceClass", "Update", null, null);
    }

    private void FixedUpdate()
    { 
        ilRuntimeInit.appdomain.Invoke("UnityHotFix.Properties.InstanceClass", "FixedUpdate", null, null);
    }
}
