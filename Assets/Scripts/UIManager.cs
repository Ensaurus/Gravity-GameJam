using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject tutorialText;
    public TextMeshProUGUI score;
    public GameObject gameOverText;
    public Button restartButton;
    public GameObject speedometer;
    public Transform speedometerNeedle;
    public ParticleSystem speedometerFire;
    public int maxNudges = 5;
    //public int startNudges = 0;
    public NudgeHandler NudgeHandler;
    public int currentNudges = 5;
    public float timeForNudge = 3f;
    private float currentNudgeTime = 0f;
    private float fillAmountNudgeBar = 0f;
    public TextMeshProUGUI nudgeCounter;
    public GameObject nudgeUI;
    public Image nudgeMask;
    [SerializeField] private const float NEEDLE_MIN_ROTATION = 4.47f;
    [SerializeField] private const float NEEDLE_MAX_ROTATION = -183.77f;

    private bool speedometerMaxed;
    private bool gameRunning;


    // Start is called before the first frame update
    void Start()
    {
        gameRunning = false;
        score.gameObject.SetActive(false);
        speedometer.SetActive(false);
        nudgeUI.SetActive(false);
        PlayerBounding.instance.PlayerEnteredBound.AddListener(EnteredBoundHandler);    
        PlayerBounding.instance.PlayerEnteredBound.AddListener(EnteredBoundHandler);
        PlayerSpeedTracker.instance.gameOverEvent.AddListener(OnGameOver);
        restartButton.onClick.AddListener(ResetScene);
    }

    private void EnteredBoundHandler()
    {
        speedometer.SetActive(true);
        score.gameObject.SetActive(true);
        nudgeUI.SetActive(true);
        gameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            SetNeedleRotation();
            GetCurrentNudgeFill();
            score.text = PlayerSpeedTracker.instance.DepthTravelled.ToString("F0");
        }
    }

    private void SetNeedleRotation()
    {
        if (!speedometerMaxed)
        {
            float normalizedSpeed = PlayerSpeedTracker.instance.currentVelocity / PlayerSpeedTracker.instance.criticalVelocity;
            float currentZ = Mathf.Lerp(NEEDLE_MIN_ROTATION, NEEDLE_MAX_ROTATION, normalizedSpeed);
            speedometerNeedle.rotation = Quaternion.Euler(speedometerNeedle.rotation.x, speedometerNeedle.rotation.y, currentZ);
            if (PlayerSpeedTracker.instance.reachedCriticalVelocity)
            {
                speedometerMaxed = true;
                speedometerNeedle.GetComponent<Animation>().Play();
                speedometerFire.Play();
            }
        }
    }

    void GetCurrentNudgeFill(){
        nudgeCounter.text = currentNudges.ToString("F0");
        
        if (currentNudges < maxNudges){
            currentNudgeTime += Time.deltaTime;
            fillAmountNudgeBar = currentNudgeTime / timeForNudge;
            if(fillAmountNudgeBar > 1){
                currentNudges += 1;
                fillAmountNudgeBar = 0;
                currentNudgeTime = 0;
            }
            nudgeMask.fillAmount = fillAmountNudgeBar;
        }
        else{
            fillAmountNudgeBar = 1;
            nudgeMask.fillAmount = fillAmountNudgeBar;
        }

        if (currentNudges > 0){
            if (NudgeHandler.nudgeUsed == true){
                Debug.Log("Nudge used.");
                currentNudges -= 1;
                nudgeCounter.text = currentNudges.ToString("F0");
            }
        }
        nudgeCounter.text = currentNudges.ToString("F0");
    }

    private void OnGameOver()
    {
        gameOverText.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
