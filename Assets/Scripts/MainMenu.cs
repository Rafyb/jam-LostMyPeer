using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;

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
}
