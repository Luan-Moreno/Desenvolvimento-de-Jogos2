using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed = 5.0f;
    private bool laserActive;

    // Referência ao ScoreManager
    private ScoreManager scoreManager;

    private void Start()
    {
        // Obtém a referência ao ScoreManager na cena
        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager não encontrado na cena.");
        }
    }

    private void Update()
    {
        // Movimentação
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        // Atirar
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // Pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void Shoot()
    {
        if (!laserActive)
        {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            
            projectile.direction = Vector3.up;
            
            projectile.destroyed += LaserDestroyed;
            
            laserActive = true;
        }
    }

    private void LaserDestroyed()
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se colidir com um invader ou um míssil, perder o jogo
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
            other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            SceneManager.LoadScene("Lose"); // Carrega a tela de derrota
        }
    }

    // Pausar e retomar o jogo
    private void TogglePause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f; // Pausar o jogo
        }
        else
        {
            Time.timeScale = 1f; // Retomar o jogo
        }
    }

    // Incrementa o score e informa o ScoreManager
    public void AddScore(int points)
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(points);  // Chama o método AddScore no ScoreManager
        }
    }
}
