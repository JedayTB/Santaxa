using System.Collections;
using UnityEngine;
//Doesn't really need to be a "player" script.
//All methods are generic used. Except the two public ones
public class playerUIController : MonoBehaviour
{
    [SerializeField]
    private scaleableBar HpBar; //good for now. Don't need to change colour
    [SerializeField]
    private scaleableBar dashCoolDownBar;
    void Start()
    {
        //Only show in 

        HpBar.gameObject.SetActive(false);
        dashCoolDownBar.gameObject.SetActive(false);
    }
    //sprite.color = new Color (1, 0, 0, 1); 
    IEnumerator fadeOut(scaleableBar sprite, float countDown)
    {        
        sprite.gameObject.SetActive(true);
        Color opacity = sprite.barSpriteRenderer.color;
        opacity.a = 0;
        float count = 0;
        while(count <= countDown){
            count += Time.deltaTime;
            opacity.a = count;
            sprite.barSpriteRenderer.color = opacity;
            yield return null;
        }
        
        sprite.gameObject.SetActive(false);
        StopCoroutine(fadeOut(sprite, countDown));
    }
    IEnumerator fadeIn(SpriteRenderer sprite)
    {        
        Color opacity = new Color(1,1,1, 0);
        for (float alpha = 0f; alpha <= 0; alpha += 0.1f)
        {
            opacity.a = alpha;
            sprite.color = opacity;
            yield return null;
        }
        StopCoroutine(fadeIn(sprite));
    }
    IEnumerator CoolDownBar(Transform targetSize, float coolDownTime){
        targetSize.gameObject.SetActive(true);
        Vector3 beforeVal = targetSize.localScale;
        beforeVal.y = 0;
        targetSize.localScale = beforeVal;
        Vector3 growSize = beforeVal;
        float count = 0f;
        //above can probably be simplified
        while(count <= coolDownTime){
            count += Time.deltaTime;
            growSize.y = count / coolDownTime;
            targetSize.transform.localScale = growSize;
            yield return null;
        }
        targetSize.gameObject.SetActive(false);
        StopCoroutine(CoolDownBar(targetSize, coolDownTime));
    }
    void hpBarLogic(scaleableBar targetBar, Vector2 hpValues){
        //Base value divided by current val
        //(4 / 5) = 0.8 for example
        float newSize = hpValues.y / hpValues.x;
        print(newSize);
        Vector3 newScale = targetBar.transform.localScale;
        newScale.y = newSize;
        targetBar.transform.localScale = newScale;
        StartCoroutine(fadeOut(targetBar, 1f));
    }
    public void dashCoolDown(float coolDownTime){
        StartCoroutine(CoolDownBar(dashCoolDownBar.transform, coolDownTime));
    }

    public void hpUIEvent(Vector2 hpValues){
        //print("cghec for change");
        hpBarLogic(HpBar, hpValues);
    }
}
