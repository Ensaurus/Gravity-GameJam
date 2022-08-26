using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformController : MonoBehaviour
{
    private GameObject currentTile;

    private bool spawnedBeforeGame;

    public UnityEvent playerCollidedWithStarterPlatform;

    private void Start()
    {
        // avoid clipping
        GetComponent<MeshRenderer>().materials[0].renderQueue = 3001;
        spawnedBeforeGame = !PlayerBounding.instance.boundIsActive;
        PlayerBounding.instance.SwitchedActiveTile.AddListener(SwitchTileHandler);
        PlayerBounding.instance.FirstUpwardShift.AddListener(FirstShiftHandler);
    }

    private void FirstShiftHandler()
    {
        if (spawnedBeforeGame)
        {
            Destroy(gameObject);
        }
    }

    private void SwitchTileHandler(GameObject tile)
    {
        if (currentTile && tile == currentTile)
        {
            Destroy(gameObject);
        }
    }

    Coroutine activeCoroutine;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && activeCoroutine == null)
        {
            if (!spawnedBeforeGame)
            {
                activeCoroutine = StartCoroutine(MoveAway());
            }
            else
            {
                playerCollidedWithStarterPlatform.Invoke();
            }
        }
    }

    public void StartMoving()
    {
        activeCoroutine = StartCoroutine(MoveAway());
    }

    IEnumerator MoveAway()
    {
        if (PlayerBounding.instance.boundIsActive)
        {
            // attach self to active tile
            transform.SetParent(PlayerBounding.instance.activeTile.transform);
            currentTile = PlayerBounding.instance.activeTile;
        }
        float timer = 0;
        float finishTime = 5;
        Material[] materials = GetComponent<MeshRenderer>().materials;
        Color curColor;
        while (timer < finishTime)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                curColor = materials[i].color;
                curColor.a = Mathf.Lerp(1, 0, timer / finishTime);
                materials[i].color = curColor;
            }
            timer += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + PlatformSpawner.instance.platformSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
