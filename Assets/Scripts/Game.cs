using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    [Header("Component")]
    public Controller playerManager;
    public Animator transtion;
    public Animator boxAnimator;
    public Transform currentRespawn;

    [Header("Event")]
    public float timeBeforeOpen;
    public float timeStayOpen;
    private bool isOpen;
    private float timing;

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
        timing+= Time.deltaTime;

        if (isOpen)
        {
            if(timing >= timeStayOpen)
            {
                Close();
                timing = 0;
            }
        } else
        {
            if (timing >= timeBeforeOpen)
            {
                Open();
                timing = 0;
            }
        }

        if(isOpen && !playerManager.isFakeDeath())
        {
            playerManager.FakeDead();
            StartCoroutine(Transition(0.5f, "Hide"));
            StartCoroutine(BeforeRespawn(1.5f));
            StartCoroutine(Transition(1.5f, "Show"));
        }
    }

    void Open()
    {
        boxAnimator.SetBool("Open", true);
        StartCoroutine(waitAnim()); ;
    }

    IEnumerator waitAnim()
    {
        yield return new WaitForSeconds(1.5f);
        isOpen = true;
        timing = 0;
    }

    void Close()
    {
        boxAnimator.SetBool("Open", false);
        isOpen = !isOpen;
    }



    void Finish()
    {
        StartCoroutine(Transition(1f,"Hide"));
        SceneManager.LoadScene(2);
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

    }

    IEnumerator BeforeRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        Respawn();

    }


}
