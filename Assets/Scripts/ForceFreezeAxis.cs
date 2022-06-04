using UnityEngine;

public class ForceFreezeAxis : MonoBehaviour
{
    public enum axis
    {
        x,
        y,
        z
    }
    public axis frozenAxis;
    float initial;
    Transform myTransform;

    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
        switch (frozenAxis){
            case (axis.x):
                initial = transform.position.x;
                break;
            case (axis.y):
                initial = transform.position.y;
                break;
            case (axis.z):
                initial = transform.position.z;
                break;
            default:
                Debug.Log("Set an axis to freeze in inspector");
                break;
        }   
    }

    void FixedUpdate()
    {
        switch (frozenAxis)
        {
            case (axis.x):
                myTransform.position = new Vector3(initial, myTransform.position.y, myTransform.position.z);
                break;
            case (axis.y):
                myTransform.position = new Vector3(myTransform.position.x, initial, myTransform.position.z);
                break;
            case (axis.z):
                myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, initial);
                break;
            default:
                Debug.Log("Set an axis to freeze in inspector");
                break;
        }
    }
}
