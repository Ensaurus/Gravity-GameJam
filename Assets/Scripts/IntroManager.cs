using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class IntroManager : MonoBehaviour
{
    public static IntroManager instance { get;  private set; }
    public bool runningTutorial;
    private bool waitingForJump;
    public UnityEvent StartGame;
    public TextMeshProUGUI introTutorialText;
    public float runTutorialSpeed;

    public UnityEvent tutorialFinished;

    private Coroutine activeTutorial;

    public bool doingTutorial = true;

    public GameObject nudgeUI;
    public GameObject platformUI;
    public GameObject speedometerUI;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        waitingForJump = true;
        runningTutorial = true;
        introTutorialText.color = Color.black;
        introTutorialText.text = "Hold D to run.";
        introTutorialText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && waitingForJump)
        {
            StopAllCoroutines();
            runningTutorial = false;
            waitingForJump = false;
            StartGame.Invoke();
            introTutorialText.gameObject.SetActive(false);
        }
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
        yield return null;
    }

    public void StartActualTutorial()
    {
        if (activeTutorial == null && doingTutorial)
        {
            activeTutorial = StartCoroutine(ActualTutorial());
        }   
        else if (activeTutorial == null && !doingTutorial) 
        {
            activeTutorial = StartCoroutine(SkippingTutorial());
        }
    }

    IEnumerator SkippingTutorial()
    {
        introTutorialText.color = Color.black;
        introTutorialText.text = "";
        introTutorialText.gameObject.SetActive(true);
        tutorialFinished.Invoke();
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
            introTutorialText.text = i.ToString();
        }
        yield return new WaitForSeconds(1f);
        introTutorialText.text = "GOOD LUCK!";
        yield return new WaitForSeconds(1);
        introTutorialText.gameObject.SetActive(false);
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

        int nudgesRequired = 10;
        introTutorialText.text = "Oh well.\nUse WASD to nudge yourself. Give it a try. \n" + nudgesRequired;
        
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                nudgesRequired--;
                introTutorialText.text = "Oh well.\nUse WASD to nudge yourself. Give it a try. \n" + nudgesRequired;
                if (nudgesRequired <= 0)
                {
                    break;
                }
            }
            yield return null;
        }
        introTutorialText.text = "Nice!";
        yield return new WaitForSeconds(2);

        introTutorialText.text = "Note you can only nudge up every once in a while.";
        yield return StartCoroutine(UIManager.instance.HighlightUI(nudgeUI, 5));

        introTutorialText.text = "Also, while falling, use spacebar to smack yourself into a platform like this.";
        yield return StartCoroutine(UIManager.instance.HighlightUI(platformUI, 5));

        introTutorialText.text = "Try not to go too fast...";
        yield return StartCoroutine(UIManager.instance.HighlightUI(speedometerUI, 5));

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
