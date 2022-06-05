using UnityEngine;
using UnityEngine.Events;

public class PlayerBounding : MonoBehaviour
{
    public static PlayerBounding instance;
    public UnityEvent<GameObject> SwitchedActiveTile;
    public UnityEvent ReachedBottomBound;
    public UnityEvent PlayerEnteredBound;

    public Transform upperBoundTransform;
    public Transform lowerBoundTransform;
    public Transform leftBoundTransform;
    public Transform rightBoundTransform;

    public GameObject activeTile;
    public GameObject upperTile;
    public GameObject lowerTile;
    public GameObject leftTile;
    public GameObject rightTile;
    
    private float upperBound;
    private float lowerBound;
    private float leftBound;
    private float rightBound;

    // note should be Ragdoll's root
    public Transform playerTransform;

    public float spawnBuffer;

    private bool boundIsActive;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        upperBound = upperBoundTransform.position.y;
        lowerBound = lowerBoundTransform.position.y;
        leftBound = leftBoundTransform.position.x;
        rightBound = rightBoundTransform.position.x;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            boundIsActive = true;
            PlayerEnteredBound.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boundIsActive)
        {
            if (playerTransform.position.y < lowerBound)
            {
                playerTransform.position = new Vector3(playerTransform.position.x, upperBound - spawnBuffer, playerTransform.position.z);
                ShiftUp();
            }
            /*
            else if (playerTransform.position.y > upperBound)
            {
                playerTransform.position = new Vector3(playerTransform.position.x, lowerBound + spawnBuffer, playerTransform.position.z);
                ShiftDown();
            }
            */
            if (playerTransform.position.x < leftBound)
            {
                playerTransform.position = new Vector3(rightBound - spawnBuffer, playerTransform.position.y, playerTransform.position.z);
                ShiftRight();
            }
            else if (playerTransform.position.x > rightBound)
            {
                playerTransform.position = new Vector3(leftBound + spawnBuffer, playerTransform.position.y, playerTransform.position.z);
                ShiftLeft();
            }
        }
    }


    // I'm sure there's a way to make this all 1 function, but don't want to think about it too much rn
    private void ShiftUp()
    {
        Vector3 activeTilePos = activeTile.transform.position;
        activeTile.transform.position = upperTile.transform.position;
        upperTile.transform.position = lowerTile.transform.position;
        lowerTile.transform.position = activeTilePos;

        GameObject oldActive = activeTile;
        activeTile = lowerTile;
        lowerTile = upperTile;
        upperTile = oldActive;
        SwitchedActiveTile.Invoke(lowerTile);
        ReachedBottomBound.Invoke();
    }

    private void ShiftDown()
    {
        Vector3 activeTilePos = activeTile.transform.position;
        activeTile.transform.position = lowerTile.transform.position;
        lowerTile.transform.position = upperTile.transform.position;
        upperTile.transform.position = activeTilePos;

        GameObject oldActive = activeTile;
        activeTile = upperTile;
        upperTile = lowerTile;
        lowerTile = oldActive;
        SwitchedActiveTile.Invoke(upperTile);
    }

    private void ShiftLeft()
    {
        Vector3 activeTilePos = activeTile.transform.position;
        activeTile.transform.position = leftTile.transform.position;
        leftTile.transform.position = rightTile.transform.position;
        rightTile.transform.position = activeTilePos;

        GameObject oldActive = activeTile;
        activeTile = rightTile;
        rightTile = leftTile;
        leftTile = oldActive;
        SwitchedActiveTile.Invoke(rightTile);
    }

    private void ShiftRight()
    {
        Vector3 activeTilePos = activeTile.transform.position;
        activeTile.transform.position = rightTile.transform.position;
        rightTile.transform.position = leftTile.transform.position;
        leftTile.transform.position = activeTilePos;

        GameObject oldActive = activeTile;
        activeTile = leftTile;
        leftTile = rightTile;
        rightTile = oldActive;
        SwitchedActiveTile.Invoke(leftTile);
    }
}
