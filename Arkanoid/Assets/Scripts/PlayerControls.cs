using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode moveRight = KeyCode.D;      
    public KeyCode moveLeft= KeyCode.A;    
    public float speed = 10.0f;            
    public float boundX = 5.25f;           
    private Rigidbody2D rb2d;   

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        Move();
    }

    void Move(){
        var vel = rb2d.velocity;                
        if (Input.GetKey(moveRight)) {             
            vel.x = speed;
        }
        else if (Input.GetKey(moveLeft)) {      
            vel.x = -speed;                    
        }
        else {
            vel.x = 0;                          
        }
        rb2d.velocity = vel;                    

        var pos = transform.position;           
        if (pos.x > boundX) {                  
            pos.x = boundX;                     
        }
        else if (pos.x < -boundX) {
            pos.x = -boundX;                    
        }
        transform.position = pos;  
    }
}
