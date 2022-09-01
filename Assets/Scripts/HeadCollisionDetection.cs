using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionDetection : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Editor"))
            SoundManager.instance.PlayBonk();
    }
}
