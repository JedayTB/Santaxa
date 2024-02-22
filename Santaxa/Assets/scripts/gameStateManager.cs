using UnityEngine;
using UnityEngine.UI;

public class gameStateManager : MonoBehaviour
{
    public static GameObject playerReference;
    [SerializeField]
    public Text hpText;
    private  hpEventController playerHpEvent;
    public static playerLoadout playerLoadout;
    private static gameStateManager instance;
    private playerUIController playerUICont;
    public static gameStateManager Instance{
        get{return instance;}
    }
    [SerializeField]
    public static waveController waveCont;
    public playerShootLogic shootLogic;
    public playerAOELogic aoeLogic;
    void Start()
    {
        instance = this;
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
