using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDetector : MonoBehaviour
{
    public Action OnTouchedGround;


    private void OnTriggerEnter(Collider col)
    {
        OnTouchedGround?.Invoke();
    }
}
