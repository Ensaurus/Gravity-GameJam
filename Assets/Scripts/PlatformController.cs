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
        spawnedBeforeGame = !PlayerBounding.instance.boundIsActive;
        PlayerBounding.instance.SwitchedActiveTile.AddListener(SwitchTileHandler);
        //PlayerBounding.instance.FirstUpwardShift.AddListener(FirstShiftHandler);
    }
    /*
    private void FirstShiftHandler()
    {
        if (spawnedBeforeGame)
        {
            Destroy(gameObject);
        }
    }
    */

    private void OnEnable()
    {
        transform.parent = null;
        activeCoroutine = null;
        spawnedBeforeGame = false;
        currentTile = null;
    }

    private void SwitchTileHandler(GameObject tile)
    {
        if (currentTile && tile == currentTile)
        {
            gameObject.SetActive(false);
            // Destroy(gameObject);
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

        // attach self to active tile
        transform.SetParent(PlayerBounding.instance.activeTile.transform);
        currentTile = transform.parent.gameObject;
    
        float timer = 0;
        float finishTime = 15;
        Material[] materials = GetComponent<MeshRenderer>().materials;
        while (timer < finishTime)
        {
            timer += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + PlatformSpawner.instance.platformSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }
}
