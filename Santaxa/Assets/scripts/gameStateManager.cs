using UnityEngine;
using UnityEngine.UI;

public class gameStateManager : MonoBehaviour
{
    public GameObject playerReference;
    [SerializeField]
    public Text hpText;
    public  hpEventController playerHpEvent;
    public playerLoadout playerLoadout;
    private static gameStateManager instance;
    public playerUIController playerUICont;
    public static gameStateManager Instance{
        get{return instance;}
    }
    [SerializeField]
    public static waveController waveCont;
    public playerShootLogic shootLogic;
    public playerAOELogic aoeLogic;
    public Rigidbody2D playerRB2D;
    public Animator playerAnimator;
    void Start()
    {
        if(instance != null){
            Destroy(this.gameObject);
        }else{
            instance = this;
        }

        playerReference = GameObject.FindGameObjectWithTag("PLAYER");
        if(playerReference == null){
            Debug.LogError("PLAYER IS NULL");
        }else{
            Debug.Log("gamestate manager got player");
        }

        waveCont = GameObject.FindWithTag("waveManager").GetComponent<waveController>();
        if(waveCont == null){
            Debug.LogError("GameStatemanager wavecontroller null");
        } 

        playerLoadout = playerReference.gameObject.GetComponent<playerLoadout>();
        if(playerLoadout == null){
            Debug.LogError("PLAYER LOADOUIT NULL IN GSM");
        }

        playerUICont = playerReference.gameObject.GetComponent<playerUIController>();
        if(playerUICont == null){
            Debug.LogError("PLAYER UI NULL IN GSM");
        }

        playerHpEvent = playerReference.gameObject.GetComponent<hpEventController>();
        if(playerHpEvent == null){
            Debug.LogError("PLASYER HP EVENT NULL");
        }

        aoeLogic = playerReference.gameObject.GetComponent<playerAOELogic>();
        if(aoeLogic == null)
        {
            Debug.LogError("PLAYER AOE LOGIC IS NULL IN GSM");
        }

        shootLogic = playerReference.gameObject.GetComponent<playerShootLogic>();
        if(shootLogic == null)
        {
            Debug.LogError("PLAYER SHOOT LOGIC IS NULL");
        }
        
        playerRB2D = playerReference.GetComponent<Rigidbody2D>();
        if(playerRB2D == null){
            Debug.LogError("PLAYER RIGID BODY 2D MISSING");
        }

        playerAnimator = playerReference.GetComponent<Animator>();  
        if(playerAnimator == null)
        {
            Debug.LogError("PLAYER ANIMATOR NULL");
        }
        waveCont.waveSpawnLogic(); // Start at beggining of game
    }
    public void playerOnDash(float waitTime){
        playerUICont.dashCoolDown(waitTime);
    }
    public void playerOnHit(Vector2 hpValues){
        playerUICont.hpUIEvent(hpValues);
        //print("player hit");
    }
    public void playerOnAOE(float waitTime)
    {
        playerUICont.AOECoolDown(waitTime);
    }
}
