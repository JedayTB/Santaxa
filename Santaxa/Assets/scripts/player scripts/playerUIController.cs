using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUIController : MonoBehaviour
{
    // Start is called before the first frame update
    //private SpriteRenderer sprite; //this sprite
    public Transform HpBar; //good for now. Don't need to change colour
    void Start()
    {
        /*
        sprite = GetComponent<SpriteRenderer>();
        if(sprite == null){
            Debug.Log($"{this.gameObject.name} is missing sprite renderer");
        }
        */
        HpBar.localScale = Vector3.zero; // Only show if gerergb
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
    IEnumerator CoolDownBar(Transform otherSize ){
        Vector3 beforeVal = otherSize.localScale;
        beforeVal.y = 0;
        otherSize.localScale = beforeVal;
        Vector3 growSize = beforeVal;
        print("before for loop");
        for(float size = 0; size <= 1; size += 0.1f){
            growSize.y = size;
            otherSize.localScale = growSize;
            print($"yield run {size}");
            yield return null;
        }
    }
    public void hpUIEvent(){
        print("cghec for change");
        StartCoroutine(CoolDownBar(HpBar));
    }
}
