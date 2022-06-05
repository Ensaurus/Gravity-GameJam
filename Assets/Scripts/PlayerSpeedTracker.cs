using UnityEngine;

public class PlayerSpeedTracker : MonoBehaviour
{
    public static PlayerSpeedTracker instance;

    public Rigidbody rb;
    public ParticleSystem fireSystem;
    public ParticleSystem explosionSystem;
    private Transform myTransform;

    public float currentVelocity;
    public float criticalVelocity { get; private set; } = 40;
        
    public bool reachedCriticalVelocity
    {
        get; 
        private set; 
    }
    private float loggedDepthTravelled=0;
    private float depthTravelled = 0;
    private float currentDepth;
    public float DepthTravelled
    {
        get { return depthTravelled; }
    }

    private bool tracking = false;
    private float lengthOfTile;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayerBounding.instance.PlayerEnteredBound.AddListener(StartTracking);
        PlayerBounding.instance.ReachedBottomBound.AddListener(UpdateLoggedDepth);
        lengthOfTile = Vector3.Distance(PlayerBounding.instance.upperBoundTransform.position, PlayerBounding.instance.lowerBoundTransform.position);
        reachedCriticalVelocity = false;
        rb = GetComponent<Rigidbody>();
        //ps = GetComponentInChildren<ParticleSystem>();
        myTransform = transform;
    }

    void Update()
    {
        if (tracking)
        {
            currentDepth = loggedDepthTravelled + Vector3.Distance(PlayerBounding.instance.upperBoundTransform.position, myTransform.position);
            depthTravelled = currentDepth > depthTravelled ? currentDepth : depthTravelled;
            currentVelocity = Mathf.Abs(rb.velocity.y);
            if (!reachedCriticalVelocity && currentVelocity >= criticalVelocity)
            {
                fireSystem.Play();
                explosionSystem.Play();
                reachedCriticalVelocity = true;
            }
            /*
            else
            {
                ps.Stop();
            }
            */
        }
    }

    private void StartTracking()
    {
        tracking = true;
    }

    private void UpdateLoggedDepth()
    {
        loggedDepthTravelled += lengthOfTile;
    }

    void OnCollisionEnter(Collision collision){
        if(reachedCriticalVelocity){
            explosionSystem.Play();
        }

    }

}
