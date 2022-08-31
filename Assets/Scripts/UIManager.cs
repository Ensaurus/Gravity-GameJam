using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    public GameObject tutorialText;
    public TextMeshProUGUI score;
    public TextMeshProUGUI gameOverText;
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
    // public TextMeshProUGUI nudgeCounter;
    public GameObject nudgeUI;
    public GameObject platformsUI;
    public Image nudgeMask;
    [SerializeField] private const float NEEDLE_MIN_ROTATION = 4.47f;
    [SerializeField] private const float NEEDLE_MAX_ROTATION = -183.77f;

    private bool speedometerMaxed;
    private bool gameRunning;
    public GameObject gameOverScreen;

    private Vector3 initialSpeedometerScale;
    private Vector3 originalNudgeScale;

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

    // Start is called before the first frame update
    void Start()
    {
        originalNudgeScale = nudgeUI.transform.localScale;
        initialSpeedometerScale = speedometer.transform.localScale;
        platformsUI.SetActive(false);
        gameOverScreen.SetActive(false);
        gameRunning = false;
        score.gameObject.SetActive(false);
        speedometer.SetActive(false);
        nudgeUI.SetActive(false);
        PlayerBounding.instance.PlayerEnteredBound.AddListener(EnteredBoundHandler);
        PlayerSpeedTracker.instance.gameOverEvent.AddListener(OnGameOver);
        restartButton.onClick.AddListener(ResetScene);
    }

    private void EnteredBoundHandler()
    {
        platformsUI.SetActive(true);
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
            SetSpeedometerSize();
            SetNeedleRotation();
            GetCurrentNudgeFill();
            score.text = PlayerSpeedTracker.instance.DepthTravelled.ToString("F0");
        }
    }

    private void SetSpeedometerSize()
    {
        float normalizedSpeed = PlayerSpeedTracker.instance.currentVelocity / PlayerSpeedTracker.instance.criticalVelocity;
        speedometer.transform.localScale = Vector3.Lerp(initialSpeedometerScale, initialSpeedometerScale + Vector3.one, normalizedSpeed);
    }

    private void SetNeedleRotation()
    {
        if (speedometerMaxed)
        {
            if (!PlayerSpeedTracker.instance.reachedCriticalVelocity)
            {
                speedometerNeedle.GetComponent<Animation>().Rewind();
                speedometerMaxed = false;
                speedometerFire.Stop();
            }
        }
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
        // nudgeCounter.text = currentNudges.ToString("F0");
        
        if (currentNudges < maxNudges){
            currentNudgeTime += Time.deltaTime;
            fillAmountNudgeBar = currentNudgeTime / timeForNudge;
            if(fillAmountNudgeBar > 1){
                currentNudges += 1;
                fillAmountNudgeBar = 0;
                currentNudgeTime = 0;
                StartCoroutine(HighlightUI(nudgeUI, 2));
            }
            nudgeMask.fillAmount = fillAmountNudgeBar;
        }
        else{
            fillAmountNudgeBar = 1;
            nudgeMask.fillAmount = fillAmountNudgeBar;
        }
        // nudgeCounter.text = currentNudges.ToString("F0");
    }

    // called by event
    public void DecreaseCurrentNudges()
    {
        if (currentNudges > 0)
        {
            currentNudges -= 1;
            nudgeUI.transform.localScale = originalNudgeScale;
            StopAllCoroutines();
        }
    }

    private void OnGameOver()
    {
        gameOverText.text = "Depth:\n" + PlayerSpeedTracker.instance.DepthTravelled.ToString("F0");
        gameOverScreen.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator HighlightUI(GameObject ui, float time)
    {
        ui.SetActive(true);
        Vector3 startingSize = ui.transform.localScale;
        float timer = 0;
        while (timer < time)
        {
            float periodInSeconds = 0.5f;
            float lerpAmount = 0.5f * Mathf.Cos(2 * Mathf.PI / periodInSeconds * timer + Mathf.PI) + 0.5f;
            ui.transform.localScale = Vector3.Lerp(startingSize, startingSize + Vector3.one * 0.5f, lerpAmount);
            timer += Time.deltaTime;
            yield return null;
        }
        ui.transform.localScale = startingSize;
    }
}
