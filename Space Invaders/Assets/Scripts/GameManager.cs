using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SpaceInvaders"); // Troque "GameScene" pelo nome da sua cena principal
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SpaceInvaders"); // Reinicia a cena atual
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Volta para o menu principal
    }
}
