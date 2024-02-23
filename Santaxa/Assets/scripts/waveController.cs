using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class waveController : MonoBehaviour
{
    [SerializeField]
    private List<zombieScript> enemyPrefrabs;
   
    [SerializeField]
    private int waveNumber = 1;

    //the static 
    public int enemiesAliveThisWave;
    [SerializeField]
    private bool countDownActivated = false;
    [SerializeField]
    private  float timeCount;
    [SerializeField]
    private  float countDownStart = 20f;
    
    public Text countDownText;
    [SerializeField]
    float generalOffset = 15f;
    float offsetVariation = 5f;

    public GameObject musicController;
    musicController music;

    private int ballssack{  // ayo?????
        get{return ballssack;}
    }
    // Start is called before the first frame update
    void Start()
    {  
        countDownText.gameObject.SetActive(false);
        music = musicController.GetComponent<musicController>();
    }
    void Update()
    {
        //Could be moved to a function
        //but like...
        if(countDownActivated){
            //Find out how to only set once
            //okay for now
            countDownText.gameObject.SetActive(true);
            timeCount -= Time.deltaTime;
            if(timeCount <= 0){
                waveSpawnLogic();
                resetCountDown();
                music.SwitchToMain();
            }
            countDownText.text = $"Time Until Next Wave\n {timeCount.ToString("##.##")}";
        }
    }
    //Make Sure Enemies trigger
    //hittableObject.subtracthealth
    //To have proper logic and decrement enemiesAlive.
    public void checkIfWaveOver(){
        enemiesAliveThisWave--;
        if(enemiesAliveThisWave == 0){
            //print("THIS RUNS! HEY HEY JKOELS CAN'T FREESTYLE FOR TSHIT");
            activateCountDown();
            updateGearPointReward();
            waveNumber++;
        }
    }
    void updateGearPointReward(){ 
        int gpReward = waveNumber * 2;
        gameStateManager.Instance.playerLoadout.gpAmount += gpReward;
    }
    void activateCountDown(){
        countDownActivated = true;
        timeCount = countDownStart;
        music.SwitchToBetween();
    }
    void resetCountDown(){
        countDownActivated = false;
        timeCount = countDownStart;
        countDownText.gameObject.SetActive(false);
    }
    public void waveSpawnLogic(){
        //Base amount is how many enemies to spawn on
        //first wave
        int baseAmount = 25;
        int firstRndNum = Random.Range(baseAmount, (waveNumber * 5) + baseAmount);
        int secondRndNum = Random.Range(-5, 5);
        int waveSpawnAmt = firstRndNum + secondRndNum;
        List<zombieScript> enem;
        enem = enemyPrefrabs;
        for(int i = 0; i < waveSpawnAmt; i++){
            spawnRndEnemy();
        }
        enemiesAliveThisWave = waveSpawnAmt;
    }
    private void spawnRndEnemy(){      
        int rndNum;
        int probability;
        bool repeatUntilMakeEnemy = true;
        while(repeatUntilMakeEnemy){
            rndNum = Random.Range(0, enemyPrefrabs.Count);
            probability = Random.Range(0, 101);
            if(probability < enemyPrefrabs[rndNum].chanceToSpawn){
                zombieScript temp = Instantiate(enemyPrefrabs[rndNum]);
                temp.transform.position = getRandomPositionOffscreenOfPlayer();
                repeatUntilMakeEnemy = false;
            }
        }
    }
    private Vector3 getRandomPositionOffscreenOfPlayer(){
        Vector3 newPos = new Vector3();
        int UpDownLeftOrRight = Random.Range(0, 4);
        Vector3 playerPos = gameStateManager.Instance.playerReference.transform.position;
        switch(UpDownLeftOrRight){
            case 0: //up
                newPos.y = playerPos.y + generalOffset;
                newPos.x = Random.Range(playerPos.x - generalOffset, playerPos.x + generalOffset);
                newPos.y += Random.Range(0, offsetVariation);
                break;
            case 1: //down
                newPos.y = playerPos.y - generalOffset;
                newPos.x = Random.Range(playerPos.x - generalOffset, playerPos.x + generalOffset);
                newPos.y += Random.Range(0, offsetVariation);
                break;
            case 2: //left
                newPos.x = playerPos.x - generalOffset;
                newPos.y = Random.Range(playerPos.y - generalOffset, playerPos.y + generalOffset );
                newPos.x += Random.Range(0, offsetVariation);
                break;
            case 3: //right
                newPos.x = playerPos.x + generalOffset;
                newPos.y = Random.Range(playerPos.y - generalOffset, playerPos.y + generalOffset );
                newPos.x += Random.Range(0, offsetVariation);
                break;
        }
        return newPos;
    }
}
