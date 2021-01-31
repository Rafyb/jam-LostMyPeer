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

    // Start is called before the first frame update
    void Start()
    {
        Respawn();

        playerManager.detector.OnTouchedCheckpoint += SetCheckpoint;
    }

    void Respawn()
    {
        playerManager.SetPosition(currentRespawn);
    }

    void SetCheckpoint(Transform point)
    {
        currentRespawn = point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay()
    {
        StartCoroutine(Transition(1f));
    }

    IEnumerator Transition(float time)
    {
        yield return new WaitForSeconds(time);
        transtion.SetTrigger("blackScreenOn");
        yield return new WaitForSeconds(time);


    }


}
