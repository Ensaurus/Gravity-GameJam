using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    private bool follow = true;

    private void Start()
    {
        PlayerSpeedTracker.instance.gameOverEvent.AddListener(OnGameOver);
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.LookAt(target);
        }
    }
    
    
    private void OnGameOver()
    {
        follow = false;
    }
}
