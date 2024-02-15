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

    public static gameStateManager Instance{
        get{return instance;}
    }
    [SerializeField]
    public static waveController waveCont;
    
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
        playerHpEvent = playerReference.GetComponent<hpEventController>();
        waveCont.waveSpawnLogic(); // Start at beggining of game
    }
    public void playerOnHit(int playerHp){
        string temp  = $"Hp: {playerHp}";
        hpText.text = temp;
        //print("player hit");
    }
}
