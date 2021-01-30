using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDetector : MonoBehaviour
{
    public Action OnTouchedGround;


    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Touched");
        OnTouchedGround?.Invoke();
    }
}
