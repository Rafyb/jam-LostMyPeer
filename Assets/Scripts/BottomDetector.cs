using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDetector : MonoBehaviour
{
    public Action OnTouchedGround;
    public Action<Transform> OnTouchedCheckpoint;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Checkpoint"))
        {
            OnTouchedCheckpoint?.Invoke(col.transform);
        }
        OnTouchedGround?.Invoke();


    }
}
