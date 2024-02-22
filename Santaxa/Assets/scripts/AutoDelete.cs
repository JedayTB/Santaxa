using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 1f;
    void Start()
    {
        GameObject.Destroy(this.gameObject, lifeTime);       
    }
}
