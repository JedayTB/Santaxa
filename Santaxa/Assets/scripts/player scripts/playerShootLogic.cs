using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class playerShootLogic : MonoBehaviour
{
    public playerBullet currentBullet;

    private playerLoadout currentSettings;
    private float coolDown;
    [SerializeField]
    private float cooldownTimer;

    private void Start()
    {
        currentSettings = GetComponent<playerLoadout>();
        if(currentSettings == null){
            Debug.LogError($"{this.gameObject.tag} DOES NOT HAVE PLAYER LOADOUT");
        }
        coolDown = currentSettings.coolDown;
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
    }
    private void shootLogic()
    {
        Vector3 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 currentPosition = this.transform.position;

        mousePositon.z = 0f;
        currentPosition.z = 0f;

        Vector3 delta = mousePositon - currentPosition;

        float valueToRoate = Mathf.Atan2(delta.y , delta.x);
        
        Quaternion rotationForBullet = Quaternion.Euler(0, 0, Mathf.Rad2Deg * valueToRoate);

        //print($"bullet quaternion {rotationForBullet}\n rotate val {valueToRoate}");
        playerBullet temp = Instantiate(currentBullet);

        temp.transform.SetPositionAndRotation(currentPosition, rotationForBullet);
        temp.setBulletAttritbutes(currentSettings);
    }
}
