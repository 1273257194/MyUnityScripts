using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private Transform mTransform;

    void Start()
    {
        mTransform = transform;
    }

    public void Move(Vector3 deltaV3)
    {
        // mTransform.Translate(deltaV3);
        mTransform.position += deltaV3;
    }
}