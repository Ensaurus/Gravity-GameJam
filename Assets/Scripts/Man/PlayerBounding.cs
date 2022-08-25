using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBounding : MonoBehaviour
{
    public static PlayerBounding instance;
    public UnityEvent<GameObject> SwitchedActiveTile;
    public UnityEvent ReachedBottomBound;
    public UnityEvent PlayerEnteredBound;
    public UnityEvent FirstUpwardShift;
    private bool hasShiftedUp;

    public Transform upperBoundTransform;
    public Transform lowerBoundTransform;
    public Transform leftBoundTransform;
    public Transform rightBoundTransform;

    public GameObject activeTile;
    public GameObject upperTile;
    public GameObject lowerTile;
    public GameObject leftTile;
    public GameObject rightTile;
    public GameObject upperLeftTile;
    public GameObject upperRightTile;
    public GameObject lowerLeftTile;
    public GameObject lowerRightTile;


    private float upperBound;
    private float lowerBound;
    private float leftBound;
    private float rightBound;

    // note should be Ragdoll's root
    public Transform playerTransform;

    public float spawnBuffer;

    public bool boundIsActive;

    // Start is called before the first frame update
    void Awake()
    {
        boundIsActive = false;
        instance = this;
        upperBound = upperBoundTransform.position.y;
        lowerBound = lowerBoundTransform.position.y;
        leftBound = leftBoundTransform.position.x;
        rightBound = rightBoundTransform.position.x;
    }

    private void Start()
    {
        PlayerSpeedTracker.instance.gameOverEvent.AddListener(OnGameOver);
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
            
            else if (playerTransform.position.y > upperBound)
            {
                playerTransform.position = new Vector3(playerTransform.position.x, lowerBound + spawnBuffer, playerTransform.position.z);
                ShiftDown();
            }

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
        if (!hasShiftedUp)
        {
            hasShiftedUp = true;
            FirstUpwardShift.Invoke();
        }
        Shift(ref lowerLeftTile, ref leftTile, ref upperLeftTile);
        Shift(ref lowerTile, ref activeTile, ref upperTile);
        Shift(ref lowerRightTile, ref rightTile, ref upperRightTile);

        SwitchedActiveTile.Invoke(lowerTile);
        ReachedBottomBound.Invoke();
    }

    private void ShiftDown()
    {
        Shift(ref upperLeftTile, ref leftTile, ref lowerLeftTile);
        Shift(ref upperTile, ref activeTile, ref lowerTile);
        Shift(ref upperRightTile, ref rightTile, ref lowerRightTile);

        SwitchedActiveTile.Invoke(upperTile);
    }

    private void ShiftLeft()
    {
        Shift(ref upperRightTile, ref upperTile, ref upperLeftTile);
        Shift(ref rightTile, ref activeTile, ref leftTile);
        Shift(ref lowerRightTile, ref lowerTile, ref lowerLeftTile);

        SwitchedActiveTile.Invoke(rightTile);
    }

    private void ShiftRight()
    {
        Shift(ref upperLeftTile, ref upperTile, ref upperRightTile);
        Shift(ref leftTile, ref activeTile, ref rightTile);
        Shift(ref lowerLeftTile, ref lowerTile, ref lowerRightTile);

        SwitchedActiveTile.Invoke(leftTile);
    }


    private void Shift(ref GameObject newMiddle, ref GameObject curMiddle, ref GameObject other)
    {
        // move transforms
        Vector3 middleTilePos = curMiddle.transform.position;
        curMiddle.transform.position = other.transform.position;
        other.transform.position = newMiddle.transform.position;
        newMiddle.transform.position = middleTilePos;
        // change fields (references)
        GameObject oldMiddle = curMiddle;
        curMiddle = newMiddle;
        newMiddle = other;
        other = oldMiddle;
    }


    // to remove bounds, so body doesn't fly around
    private void OnGameOver()
    {
        Destroy(this);
    }
}
