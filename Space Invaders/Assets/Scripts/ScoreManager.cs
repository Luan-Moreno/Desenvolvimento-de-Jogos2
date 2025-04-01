using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtMsg;  // Campo de texto para exibir a mensagem de pausa

    private void Start()
    {
        score = 0;

        if (txtScore == null)
        {
            Debug.LogError("txtScore não está atribuído no Inspector.");
            return;
        }

        if (txtMsg != null)
        {
            txtMsg.gameObject.SetActive(false);  // Oculta a mensagem de pausa inicialmente
        }

        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (txtScore != null)
        {
            txtScore.text = "Score: " + score.ToString();
            Debug.Log("Score atualizado na UI: " + score);
        }
        else
        {
            Debug.LogError("scoreText não está atribuído no Inspector.");
        }
    }

    public void PauseGame()
    {
        // Pausa ou retoma o jogo ao pressionar a tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)  // Se o jogo estiver rodando
            {
                Time.timeScale = 0f;  // Pausar
                if (txtMsg != null)
                {
                    txtMsg.text = "PAUSED \n \n Press ESC to continue";
                    txtMsg.gameObject.SetActive(true);  // Mostra a mensagem de pausa
                }
            }
            else
            {
                Time.timeScale = 1f;  // Retomar
                if (txtMsg != null)
                {
                    txtMsg.gameObject.SetActive(false);  // Esconde a mensagem de pausa
                }
            }
        }
    }

    private void Update()
    {
        PauseGame();  // Verifica se o jogo deve ser pausado ou retomado
    }
}
