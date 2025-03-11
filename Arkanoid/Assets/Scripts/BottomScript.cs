using Unity.VisualScripting;
using UnityEngine;

public class BottomScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Ball"){
            GameManager.LoseLife();
            hitInfo.gameObject.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }
    }
}
