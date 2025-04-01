using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    #region Public Variables
    public GameObject playerBulletPrefab;
    public bool playerIsImmortal = false;
    public int playerLives = 5;
    public bool isGameOver = false;
    public float bulletOffsetX = 0.5f;
    public float bulletOffsetY = 0f;

    public bool isBulletTimeActive = false;
    public float bulletTimeSlowdownFactor = 0.5f;
    public float bulletTimeDuration = 5f; 
    public float bulletTimeCooldown = 10f;
    #endregion

    #region Private Variables
    [SerializeField] private float _pushUpForce = 6.0f;
    [SerializeField] private float _pushDownForce = 6.0f;
    [SerializeField] private float _timeBetweenShots = 0.3f;
    private float _timestamp; 
    private Rigidbody2D _rb;
    private GUI _gui;

    private float _bulletTimeEndTime;
    private float _bulletTimeNextReadyTime;
    private float _unscaledTimestamp; 
    #endregion

    #region Unity Methods

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null) Debug.LogError("Rigidbody2D not found on " + nameof(playerController));

        if (Camera.main != null)
        {
            _gui = Camera.main.GetComponent<GUI>();
        }

        _unscaledTimestamp = Time.unscaledTime;
    }

    void Update()
    {
        HandleInput();

        if (isGameOver && Input.GetButtonDown("Fire1"))
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            LoseLife();
        }

        HandleBulletTime();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisão detectada com: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            PlayerDidCollide();
        }
    }

    #endregion

    #region Private Methods

    private void HandleInput()
    {
        Vector2 force = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            force.y += _pushUpForce;
        }
        if (Input.GetKey(KeyCode.S))
        {
            force.y -= _pushDownForce;
        }

        if (_rb != null) _rb.velocity = force;

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.unscaledTime >= _unscaledTimestamp)
        {
            if (playerBulletPrefab != null)
            {
                Instantiate(playerBulletPrefab, transform.position + new Vector3(bulletOffsetX, bulletOffsetY, 0), Quaternion.identity);
                _unscaledTimestamp = Time.unscaledTime + _timeBetweenShots; // Usa o tempo SEM escala
            }
        }
    }

    private void PlayerDidCollide()
    {
        LoseLife();
    }

    private void LoseLife()
    {
        if (playerLives > 0 && !playerIsImmortal)
        {
            playerLives--;
        }
        Debug.Log("Player tomou dano! Vidas restantes: " + playerLives);
    }


    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleBulletTime()
    {
        // Ativar
        if (Input.GetKeyDown(KeyCode.P) && !isBulletTimeActive && Time.time >= _bulletTimeNextReadyTime)
        {
            isBulletTimeActive = true;
            _bulletTimeEndTime = Time.time + bulletTimeDuration;
            Time.timeScale = bulletTimeSlowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        // Desativar
        if (isBulletTimeActive && Time.time >= _bulletTimeEndTime)
        {
            isBulletTimeActive = false;
            _bulletTimeNextReadyTime = Time.time + bulletTimeCooldown;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
    #endregion
}