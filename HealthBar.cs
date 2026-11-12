using UnityEngine;
//a simple heatlh bar that tracks the health of an enemey
public class HealthBar : MonoBehaviour
{
    public EnemyAI enemy;
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        //update the size, position, and color of the health bar based on the enemy's health
        transform.localScale = new Vector3((float)1.5 * enemy._Health / enemy._MaxHealth, (float)0.25, 1);
        transform.position = enemy.transform.position + new Vector3(-0.12f*(enemy._MaxHealth - enemy._Health), 0.64f, 0f);
        if(enemy._Health > enemy._MaxHealth / 2.0)
        {
            
            spriteRenderer.color = new Vector4(0, 1, 0, 1);
        } else
        {
            
            spriteRenderer.color = new Vector4(1, enemy._Health / enemy._MaxHealth * 2.0f, 0, 1);
        }
        
    }
}
