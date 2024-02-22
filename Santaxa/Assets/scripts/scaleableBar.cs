using UnityEngine;

public class scaleableBar : MonoBehaviour
{
    public Transform barTransform;
    public SpriteRenderer barSpriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        barTransform = GetComponent<Transform>();
        if(barTransform ==null){
            Debug.Log($"{this.gameObject.name} is missing it's transform");
        }
        barSpriteRenderer = GetComponent<SpriteRenderer>();
        if(barSpriteRenderer == null){
            Debug.Log($"{this.gameObject.name} is missing it's spriterenderer");
        }
    }
}
