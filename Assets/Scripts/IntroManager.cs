using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class IntroManager : MonoBehaviour
{
    public bool runningTutorial;
    public UnityEvent StartGame;
    public TextMeshProUGUI introTutorialText;
    public float runTutorialSpeed;

    public UnityEvent tutorialFinished;

    private Coroutine activeTutorial;

    // Start is called before the first frame update
    void Start()
    {
        runningTutorial = true;
        introTutorialText.color = Color.black;
        introTutorialText.text = "Hold D to run.";
        introTutorialText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (runningTutorial && Input.GetKey(KeyCode.D))
        {
            introTutorialText.alpha -= Time.deltaTime * runTutorialSpeed;
            if (introTutorialText.alpha <= 0.1)
            {
                introTutorialText.color = Color.green;
                introTutorialText.text = "Great Job!";
                introTutorialText.alpha = 1;
                runningTutorial = false;
                StartCoroutine(JumpTutorial());
            }
        }
    }

    IEnumerator JumpTutorial()
    {
        yield return new WaitForSeconds(3);
        introTutorialText.text = "Press Space to Jump!";
        introTutorialText.color = Color.red;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame.Invoke();
                introTutorialText.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }

    public void StartActualTutorial()
    {
        // Debug.Log("starting tutorial");
        if (activeTutorial == null)
        {
            activeTutorial = StartCoroutine(ActualTutorial());
        }   
    }

    IEnumerator ActualTutorial()
    {
        introTutorialText.color = Color.black;
        introTutorialText.text = "Uh oh, looks like you've had a bit of a tumble";
        introTutorialText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        for (int i=0; i<3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            introTutorialText.text += ".";
        }

        yield return new WaitForSeconds(3);

        introTutorialText.text = "Oh well.\nUse A S and D to nudge yourself. Give it a try.";

        int nudgeCounter = 0; 
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                nudgeCounter += 1;
                if (nudgeCounter > 10)
                {
                    break;
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds(2);

        introTutorialText.text = "Also, while falling, use spacebar to smack yourself into a platform like this.";
        yield return new WaitForSeconds(5);

        introTutorialText.text = "See how low you can go";
        tutorialFinished.Invoke();
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            introTutorialText.text += ".";
        }
        yield return new WaitForSeconds(1f);
        introTutorialText.text += "\nGOOD LUCK!";
        yield return new WaitForSeconds(5);
        introTutorialText.gameObject.SetActive(false);
    }
}
