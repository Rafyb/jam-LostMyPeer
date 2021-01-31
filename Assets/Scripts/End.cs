using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public bool isOcurred = true;
    public List<GameObject> obscurred;

    public void OnTriggerEnter(Collider other)
    {
        obscurred.Add(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        obscurred.Remove(other.gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        if (obscurred.Count == 2 && other.gameObject.tag.Equals("Player")) Debug.Log("Gagné");
    }
}
