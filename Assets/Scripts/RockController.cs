using System.Collections;
using UnityEngine;

public class RockController : MonoBehaviour
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
        float timer = 5;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + PlatformSpawner.instance.platformSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
