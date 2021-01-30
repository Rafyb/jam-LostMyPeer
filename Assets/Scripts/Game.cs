using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public Controller playerManager;
    public GameObject menu;
    public Animator transtion;

    public Transform currentRespawn;


    // Start is called before the first frame update
    void Start()
    {
        OpenMenu();
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
        StartCoroutine(Transition(1f,0));
    }

    void CloseMenu()
    {
        menu.SetActive(false);
    }

    void OpenMenu()
    {
        //menu.SetActive(true);
    }

    IEnumerator Transition(float time,int end)
    {
        yield return new WaitForSeconds(time);
        transtion.SetTrigger("blackScreenOn");
        yield return new WaitForSeconds(time);


        if (end == 0) CloseMenu();
        if (end == 1) OpenMenu();

    }


}
