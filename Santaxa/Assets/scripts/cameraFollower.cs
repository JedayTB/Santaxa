using UnityEngine;

public class cameraFollower : MonoBehaviour
{

    [Tooltip("Object the camera is tracking")]
    public Transform Target;

    [Space(10)]
    [Tooltip("Minimum Constraint value for our camera")]
    public Vector2 minXandY;


    [Space(10)]
    [Tooltip("Maximum Constraint value for our camera")]
    public Vector2 maxXandY;

    [Space(10)]
    [Tooltip("The distance from the target that our camera will start moving")]
    public Vector2 Margins;

    [Tooltip("How much, as a decimal percentage our camera tries to follow every frame")]
    public Vector2 Easing;
    void Start()
    {
        //Set to max size of level
        maxXandY.x = 400;
        maxXandY.y = 400;

        minXandY.x = -400;
        minXandY.y = -400;

        Margins.x = 0.5f;
        Margins.y = 0.5f;
        Easing.x = 0.1f;
        Easing.y = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        //Initial position of target object
        float targetX = Target.position.x;
        float targetY = Target.position.y;

        //Clamp forces our value to be between the 2 defined extremes
        //In this case targetX/Y will always be the same or bigger then minxandy
        //or smaller then maxxandy
        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        //lerp (linear interpolation)
        //(B-A)* t + A
        // Mathf.Lerp(2,4, 0.5f);

        //2         3        4
        //|---------0--------|

        // (4-2) * 0.5f + 2
        // 2* 0.5 + 2
        // 1 + 2
        // 3

        //From ethan. I have no idea what the above means. 

        //if camera is beyond margins, move 

        if (Mathf.Abs(transform.position.x - targetX) > Margins.x)
        {
            targetX = Mathf.Lerp(transform.position.x, targetX, Easing.x);
        }
        else
        {
            targetX = transform.position.x;
        }
        if (Mathf.Abs(transform.position.y - targetY) > Margins.y)
        {
            targetY = Mathf.Lerp(transform.position.y, targetY, Easing.y);
        }
        else
        {
            targetY = transform.position.y;
        }

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}