using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDetector : MonoBehaviour
{
    public Action OnTouchedGround;
    public Action<Transform> OnTouchedCheckpoint;
    public Action OnFinish;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Checkpoint"))
        {
            OnTouchedCheckpoint?.Invoke(col.transform);
        }
        OnTouchedGround?.Invoke();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag.Equals("End"))
        {
            if (!col.gameObject.GetComponent<End>().isOcurred)
            {
                OnFinish?.Invoke();
            }
            
        }
    }
}
