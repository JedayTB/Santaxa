using UnityEngine;

public class scaleableBar : MonoBehaviour
{
    public Transform barTransform;
    public SpriteRenderer barSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        barTransform = GetComponent<Transform>();
        barSpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
