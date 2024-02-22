using UnityEngine;
using UnityEngine.UI;

public class playerLoadout : MonoBehaviour
{
    public int damage = 1;
    public float speed = 15f; //Base speed
    public int penetration = 1;
    public float shootCoolDown = 0.3f;

    public int damageAOE = 3;
    public Vector2 sizeAOE = new Vector2(6.6f, 3.6f); // goofy ahh starting numbers
    public float coolDownAOE = 7f;

    private int gearPointsAmount = 5;

    public int gpAmount {
        get { return gearPointsAmount; }
        set { gearPointsAmount = value; }
    }
    public const int intAmounts = 1;
    public const float floatAmounts = 1f;

    //text things
    [SerializeField]
    private Text GPtxt;

    [SerializeField]
    private Text speedTxt;
    [SerializeField]
    private Text damageTxt;
    [SerializeField]
    private Text penetetrationTxt;
    [SerializeField]
    private Text coolDownTxt;
    [SerializeField]
    private Text AOEDamageTxt;
    [SerializeField]
    private Text AOESizeTxt;
    [SerializeField]
    private Text AOECoolDownTxt;
    //End of var declaration
    void Start()
    {
        gpAmount = 5;
    }

    public void increaseAtributes(string attribute) {

        if (hasEnoughGearPoints()) {
            switch (attribute) {
                case "damage":
                    damage += 1;
                    break;
                case "pen":
                    penetration += 1;
                    break;
                case "speed":
                    speed += 1f;
                    break;
                case "cd":
                    //As you allocate points to cooldown
                    //Make the cooldownvalue lower
                    shootCoolDown = shootCoolDown <= 0.01f ? shootCoolDown = 0.01f : shootCoolDown -= 0.02f;
                    break;
                case "aoedamage":
                    damageAOE += 1;
                    break;
                case "aoesize":
                    sizeAOE.x += 1;
                    sizeAOE.y += 1;
                    break;
                case "aoecd":
                    coolDownAOE = coolDownAOE <= 3f ? coolDownAOE = 3f : coolDownAOE -= 0.2f;
                    //shootCoolDown = shootCoolDown <= 0.01f ? shootCoolDown = 0.01f : shootCoolDown -= 0.02f;
                    break;
                default:
                    Debug.LogError($"typo. mispelled {attribute}");
                    break;
            }
            gearPointDelta(1);
            handleTextAndCoolDown();
        }
        //Debug.Log("case where player wants to upgrade but can't");
    }
    public void decreaseAttribute(string attribute) {
        bool gearLogic = true;
        switch (attribute) {
            case "damage":
                //Quick explanation.
                //If its less then or equal to 0, set back to 0. if its above, do normal stuff
                damage = damage <= 1 ? damage = 1 : damage -= 1;
                //quick boolean logic
                //if the value is at 0, and you take 1 away from it, that means
                //you shouldn't be able to take more away from it. 
                //in this case, set gear logic to false.
                //Quick update: changed from 0 to 1
                gearLogic = (damage - 1) >= 1;
                break;
            case "pen":
                penetration = penetration <= 0 ? penetration = 0 : penetration -= 1;
                gearLogic = (penetration - 1) >= 0;
                break;
            case "speed":
                speed = speed <= 1 ? speed = 1 : speed -= 1;
                gearLogic = (speed - 1) >= 1;
                break;
            case "cd":
                //if you deallocate points to cooldown
                //Make cooldown higher
                shootCoolDown += 0.02f;
                break;
            case "aoedamage":
                damageAOE = damageAOE <= 1 ? damageAOE = 1 : damageAOE -= 1;
                gearLogic = (damageAOE - 1) >= 1;
                break;
            case "aoesize":
                sizeAOE.x -= 1;
                sizeAOE.y -= 1;
                break;
            case "aoecd":
                coolDownAOE += 0.5f;
                break;
            default:
                Debug.LogError($"typo. mispelled {attribute}");
                break;
        }

        if (gearLogic) {
            gearPointDelta(-1);
        }
        handleTextAndCoolDown();
    }
    private void gearPointDelta(int val) {
        //If player takes off assigment to a value
        //Val would be negative
        //So give the gear point back to player
        if (val < 0) { //less then 0, give.
            gearPointsAmount += intAmounts;
        } else {
            gearPointsAmount -= intAmounts;
        }
    }
    private bool hasEnoughGearPoints() {
        return gearPointsAmount > 0;
    }
    public void handleTextAndCoolDown()
    {
        handleLoadoutText();
        handleCoolDowns();
    }

    private void handleLoadoutText(){
        this.GPtxt.text = $"GP:{this.gearPointsAmount}";
        this.speedTxt.text = $"({this.speed})";
        this.damageTxt.text = $"({this.damage})";
        this.penetetrationTxt.text = $"({this.penetration})";
        this.coolDownTxt.text = $"({this.shootCoolDown})";
        this.AOEDamageTxt.text = $"({this.damageAOE})";
        this.AOESizeTxt.text = $"({this.sizeAOE.x})";
        this.AOECoolDownTxt.text = $"({this.coolDownAOE})";
    }
    private void handleCoolDowns()
    {
        gameStateManager.Instance.shootLogic.updateCoolDown(this.shootCoolDown);
        gameStateManager.Instance.aoeLogic.updateCoolDown(this.coolDownAOE);
    }
}
