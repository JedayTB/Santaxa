using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

// bim bim bam bam
public class playerAOELogic : MonoBehaviour
{
    public playerAOEAttack currentAOEAttack;
    private float coolDown = 1;
    public float coolDownTimer = 0;

    private playerLoadout currentSettings;

    // Start is called before the first frame update
    void Start()
    {
        currentSettings = GetComponent<playerLoadout>();
        coolDown = currentSettings.coolDownAOE;
        coolDownTimer = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime;
        if (Input.GetMouseButton(1) && coolDownTimer >= coolDown)
        {
            createAOEAttack();
            coolDownTimer = 0;
            gameStateManager.Instance.playerOnAOE(coolDown);
        }
    }

    private void createAOEAttack()
    {
        Vector3 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition); // stole your code cuz it applies here :)
        Vector3 currentPosition = this.transform.position;

        mousePositon.z = 0f;
        currentPosition.z = 0f;

        Vector3 delta = mousePositon - currentPosition;

        float valueToRoate = Mathf.Atan2(delta.y, delta.x);

        Quaternion rotationForAttack = Quaternion.Euler(0, 0, Mathf.Rad2Deg * valueToRoate - 90f);

        playerAOEAttack temp = Instantiate(currentAOEAttack);
        temp.transform.parent = transform;

        temp.transform.SetPositionAndRotation(transform.position, rotationForAttack);
        temp.SetAOEAttributes(currentSettings);
    }
}
