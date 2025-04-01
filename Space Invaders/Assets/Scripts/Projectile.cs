using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed; 
    public System.Action destroyed;

    private void Update()
    {
        // Movimento do projétil
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Invoca o callback para informar que o projétil foi destruído
        if (this.destroyed != null)
        {
            this.destroyed.Invoke();
        }

        // Destrói o projétil
        Destroy(this.gameObject);
    }
}
