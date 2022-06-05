using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Score;

    // Start is called before the first frame update
    void Start()
    {
        Score.gameObject.SetActive(false);
        PlayerBounding.instance.PlayerEnteredBound.AddListener(EnteredBoundHandler);    
    }

    private void EnteredBoundHandler()
    {
        Score.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = PlayerSpeedTracker.instance.DepthTravelled.ToString();
    }
}
