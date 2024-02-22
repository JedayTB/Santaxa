using UnityEngine;

public class playerShootLogic : MonoBehaviour
{
    public playerBullet currentBullet;

    private playerLoadout currentSettings;
    [SerializeField]
    public float coolDown;
    [SerializeField]
    private float cooldownTimer;

    public static bool bouncyBool = false; // i'm not gonna lie i have NO IDEA what static actually means all i know is it lets me reference it from other scripts LMAO

    private void Start()
    {
        currentSettings = GetComponent<playerLoadout>();
        if(currentSettings == null){
            Debug.LogError($"{this.gameObject.tag} DOES NOT HAVE PLAYER LOADOUT");
        }
        coolDown = currentSettings.shootCoolDown;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && cooldownTimer >= coolDown)
        {
            shootLogic();
            //ineficient. find event way to do.
            cooldownTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.B)) // press b to bouncy
        {
            bouncyBool = !bouncyBool;
            print("Bouncy is " + bouncyBool);
        }
    }
    //To be run inside the playerLoadout Script
    public void updateCoolDown(float newVal)
    {
        this.coolDown = newVal;
        
    }
    private void shootLogic()
    {
        Vector3 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 currentPosition = this.transform.position;

        mousePositon.z = 0f;
        currentPosition.z = 0f;

        Vector3 delta = mousePositon - currentPosition;

        float valueToRoate = Mathf.Atan2(delta.y , delta.x);
        
        Quaternion rotationForBullet = Quaternion.Euler(0, 0, Mathf.Rad2Deg * valueToRoate - 90f);

        //print($"bullet quaternion {rotationForBullet}\n rotate val {valueToRoate}");
        playerBullet temp = Instantiate(currentBullet);

        temp.transform.SetPositionAndRotation(currentPosition, rotationForBullet);
        temp.setBulletAttritbutes(currentSettings);
    }
}
