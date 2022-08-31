using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionDetection : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        SoundManager.instance.PlayBonk();
    }
}
