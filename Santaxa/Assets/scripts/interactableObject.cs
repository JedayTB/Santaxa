using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class interactableObject : MonoBehaviour
{
    SpriteRenderer interactionSprite;
    //[SerializeField] private UnityEvent onInteraction;
    //Maybe use in playerloadout with
    //unityevent instead?
    [SerializeField]
    private GameObject loadoutUI;
    //Not optimal either. 
    playerLoadout currentPlayerLoadout;
    bool interactionInterfaceOn = false;
    
    float delayForInteraction = 0;
    float delayWait = 0.5f;
    private void Start()
    {
        interactionSprite = GetComponent<SpriteRenderer>();
        interactionSprite.enabled = false;

        loadoutUI.SetActive(false);

        GameObject playerReference = gameStateManager.playerReference;
        currentPlayerLoadout = playerReference.gameObject.GetComponent<playerLoadout>();
    }
    private void Update()
    {
        if(interactionSprite.enabled == true && Input.GetKeyDown(KeyCode.F))
        {
            onPlayerInteraction();
            interactionInterfaceOn = true;
        }
        if(interactionInterfaceOn && Input.GetKeyDown(KeyCode.F) && delayForInteraction > delayWait){
            print("not inisde func yet");
            onPlayerTurnOff();
        }
        if(interactionInterfaceOn){
            delayForInteraction += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PLAYER"))
        {
            interactionSprite.enabled = true;
            //Debug.Log($"{this.name} is within range to be interacted by with player");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PLAYER"))
        {
            interactionSprite.enabled = false;
            //Debug.Log($"{this.name} is within range to be interacted by with player");
            if(loadoutUI.activeSelf == true){
                loadoutUI.SetActive(false);
            }
        }
    }
    void onPlayerTurnOff(){
        delayForInteraction = 0f;
        if(loadoutUI.activeSelf == true){
            loadoutUI.SetActive(false);
        }
        interactionInterfaceOn = false;
    }
    void onPlayerInteraction()
    {
        Debug.Log($"player has interacted with {this.gameObject.name}");
        loadoutUI.SetActive(true);
        currentPlayerLoadout.handleLoadoutText();
    }
}