using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (PlayerSpeedTracker.instance.reachedCriticalVelocity)
        {
            PlayerSpeedTracker.instance.explosionSystem.Play();
            SoundManager.instance.Explode();
            PlayerSpeedTracker.instance.gameOverEvent.Invoke();
        }
        SoundManager.instance.PlayCollisionSound();
    }
}
