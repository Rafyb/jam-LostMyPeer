using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public GameObject CanvasTuto;

    // Update is called once per frame
    public void Play()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        transition.SetTrigger("Hide");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Close ()
    {
        CanvasTuto.SetActive(false);
    }
}
