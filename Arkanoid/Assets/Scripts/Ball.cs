using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d; 
    private AudioSource[] audioSources;

    void GoBall(){                      
        float rand = Random.Range(0, 2);
        if(rand < 1)
        {
            rb2d.AddForce(new Vector2(10, -30));
            
        } 
        else 
        {
            rb2d.AddForce(new Vector2(-10, -30));
            
        }
    }

    void Start () {
        audioSources = GetComponents<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>(); 
        Invoke("GoBall", 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb2d.AddForce(new Vector2(0, 5));
        }
    }

    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Player")){
            Vector2 vel;
            vel.y = rb2d.velocity.y;
            vel.x = (rb2d.velocity.x / 2) + (coll.collider.attachedRigidbody.velocity.x / 3);
            rb2d.velocity = vel;
        }
        if(coll.gameObject.tag == "Brick")
        {
            Destroy(coll.gameObject);
            GameManager.Score();
            BricksGenerator.numberOfBricks--;
            audioSources[1].Play();
        }
        if(coll.gameObject.tag == "Bound")
        {
            audioSources[0].Play();
        }
    }

    void ResetBall(){
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }

}
