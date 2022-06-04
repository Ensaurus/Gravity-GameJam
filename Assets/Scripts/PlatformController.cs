using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    Coroutine activeCoroutine;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && activeCoroutine == null)
        {
            activeCoroutine = StartCoroutine(MoveAway());
        }
    }

    IEnumerator MoveAway()
    {
        float timer = PlatformSpawner.instance.platformLifeTime;
        while (timer > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + PlatformSpawner.instance.platformSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
