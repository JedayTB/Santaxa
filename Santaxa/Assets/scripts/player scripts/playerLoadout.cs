using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class playerLoadout : MonoBehaviour
{
    public int damage = 1;
    public float speed = 15f; //Base speed
    public int penetration = 1;
    public float coolDown = 0.3f;
    private int gearPointsAmount = 5;
    public int gpAmount{
        get{return gearPointsAmount;}
        set{gearPointsAmount = value;}   
    }

    public const int intAmounts = 1;
    public const float floatAmounts = 1f;
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
//End of var declaration
    void Start()
    {
        gpAmount = 5;
    }

    public void increaseAtributes(string attribute){
        
        if(hasEnoughGearPoints()){
            switch(attribute){
            case "damage":
                damage += 1;
                break;
            case "pen":
                penetration  += 1;
                break;
           case "speed":
                speed += 1f;
                break;
            case "cd":
            //As you allocate points to cooldown
            //Make the cooldownvalue lower
                coolDown = coolDown <= 0.01f?  coolDown = 0.01f: coolDown -= 0.02f;
                break;
            default:
                Debug.LogError($"typo. mispelled {attribute}");
                break;
            }
            gearPointDelta(1);
            handleLoadoutText();
        }
        //Debug.Log("case where player wants to upgrade but can't");
    }
    public void decreaseAttribute(string attribute){
        bool gearLogic = true;
        switch(attribute){
            case "damage":
                damage = damage <= 0? damage = 0: damage -=1;
                gearLogic = (damage -1) >= 0;
                break;
            case "pen":
                penetration = penetration <= 0? penetration = 0: penetration -=1;  
                gearLogic = (penetration -1) >= 0;
                break;
            case "speed":
                speed = speed <= 0? speed = 0: speed -=1 ;
                gearLogic = (speed - 1) >= 0;
                break;
            case "cd":
            //if you deallocate points to cooldown
            //Make cooldown higher
                coolDown += 0.02f;
                break;
            default:
                Debug.LogError($"typo. mispelled {attribute}");
                break;
        }

        if(gearLogic){
            gearPointDelta(-1);
        }
        handleLoadoutText();
    }
    private void gearPointDelta(int val){
           //If player takes off assigment to a value
        //Val would be negative
        //So give the gear point back to player
        if(val < 0){ //less then 0, give.
            gearPointsAmount += intAmounts;
        }else{
            gearPointsAmount -= intAmounts;
        }
    }
    private bool hasEnoughGearPoints(){
        return gearPointsAmount > 0;
    }
    public void handleLoadoutText(){
        this.GPtxt.text = $"GP:{this.gearPointsAmount}";
        this.speedTxt.text = $"({this.speed})";
        this.damageTxt.text = $"({this.damage})";
        this.penetetrationTxt.text = $"({this.penetration})";
        this.coolDownTxt.text = $"({this.coolDown})";
    }
}
