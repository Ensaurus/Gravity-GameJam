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

    // Start is called before the first frame update
    void Start()
    {
        runningTutorial = true;
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
        Destroy(gameObject);
    }
}
