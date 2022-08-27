using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DecisionZone : MonoBehaviour
{
    public UnityEvent skippingTutorialEvent;
    public float timeRate;
    public GameObject tutorialChoiceUI;
    private bool firstTime = true;

    private void OnTriggerEnter(Collider other)
    {
        if (firstTime)
        {
            Time.timeScale = timeRate;
            tutorialChoiceUI.SetActive(true);
            StartCoroutine(WaitForInput());
            firstTime = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Time.timeScale = 1;
        tutorialChoiceUI.SetActive(false);
        StopAllCoroutines();
    }

    IEnumerator WaitForInput()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                IntroManager.instance.doingTutorial = false;
                Time.timeScale = 1;
                tutorialChoiceUI.SetActive(false);
                skippingTutorialEvent.Invoke();
                break;
            }
            yield return null;
        }
    }
}
