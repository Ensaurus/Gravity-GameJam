using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject gameOverText;
    public GameObject speedometer;
    public Transform speedometerNeedle;
    public ParticleSystem speedometerFire;
    [SerializeField] private const float NEEDLE_MIN_ROTATION = 4.47f;
    [SerializeField] private const float NEEDLE_MAX_ROTATION = -183.77f;

    private bool speedometerMaxed;


    // Start is called before the first frame update
    void Start()
    {
        score.gameObject.SetActive(false);
        speedometer.SetActive(false);
        PlayerBounding.instance.PlayerEnteredBound.AddListener(EnteredBoundHandler);
        PlayerSpeedTracker.instance.gameOverEvent.AddListener(OnGameOver);
    }

    private void EnteredBoundHandler()
    {
        speedometer.SetActive(true);
        score.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SetNeedleRotation();
        score.text = PlayerSpeedTracker.instance.DepthTravelled.ToString("F0");
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

    private void OnGameOver()
    {
        gameOverText.SetActive(true);
    }
}
