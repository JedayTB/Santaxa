using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUIController : MonoBehaviour
{

    [SerializeField]
    private Transform HpBar; //good for now. Don't need to change colour
    
    [SerializeField]
    private Transform dashCoolDownBar;
    void Start()
    {
        //Only show in 

        HpBar.gameObject.SetActive(false);
        dashCoolDownBar.gameObject.SetActive(false);
    }
    //sprite.color = new Color (1, 0, 0, 1); 
    // Update is called once per frame
    public IEnumerator fadeOut(SpriteRenderer sprite)
    {        
        Color opacity = new Color(1,1,1,1);
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            opacity.a = alpha;
            sprite.color = opacity;
            yield return null;
        }
    }
    public IEnumerator fadeIn(SpriteRenderer sprite)
    {        
        Color opacity = new Color(1,1,1, 0);
        for (float alpha = 0f; alpha <= 0; alpha += 0.1f)
        {
            opacity.a = alpha;
            sprite.color = opacity;
            yield return null;
        }
    }
    IEnumerator CoolDownBar(Transform otherSize, float coolDownTime){
        otherSize.gameObject.SetActive(true);
        Vector3 beforeVal = otherSize.localScale;
        beforeVal.y = 0;
        otherSize.localScale = beforeVal;
        Vector3 growSize = beforeVal;
        //Yield Will Run every frame and complete one iteration of the for loop
        //Without WaitForSeconds function, will run for 10 frames. 
        float count = 0f;
        print(coolDownTime);
        while(count <= coolDownTime){
            count += Time.deltaTime;
            growSize.y = count / coolDownTime;
            print(growSize.y);
            otherSize.transform.localScale = growSize;
            yield return null;
        }
        otherSize.gameObject.SetActive(false);
        StopCoroutine(CoolDownBar(otherSize, coolDownTime));
    }
    public void dashCoolDown(float coolDownTime){
        StartCoroutine(CoolDownBar(dashCoolDownBar, coolDownTime));
    }
    public void hpUIEvent(){
        //print("cghec for change");
    }
}
