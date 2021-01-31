using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    [Header("Component")]
    public Controller playerManager;
    public Animator transtion;
    public Transform currentRespawn;

    [Header("Event")]
    public float timing;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        Respawn();

        playerManager.detector.OnTouchedCheckpoint += SetCheckpoint;
        playerManager.detector.OnFinish += Finish;
    }


    // Update is called once per frame
    void Update()
    {

    }

    void Finish()
    {
        StartCoroutine(Transition(1f,"Hide"));
    }
    void Respawn()
    {
        playerManager.SetPosition(currentRespawn);
    }

    void SetCheckpoint(Transform point)
    {
        currentRespawn = point;
    }


    IEnumerator Transition(float time, string type)
    {
        yield return new WaitForSeconds(time);
        transtion.SetTrigger(type);
        yield return new WaitForSeconds(time);


    }


}
