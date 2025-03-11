using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static int playerScore=0;
    public static int lifes = 3;
    public GUISkin layout;              
    GameObject Ball;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifes == 0){
            SceneManager.LoadScene("Derrota");
            playerScore = 0;
            lifes = 3;
        }
        if(BricksGenerator.numberOfBricks == 0 && SceneManager.GetActiveScene().name == "Fase1")
        {
            SceneManager.LoadScene("Fase2");
        }
        if(BricksGenerator.numberOfBricks == 0 && SceneManager.GetActiveScene().name == "Fase2")
        {
            SceneManager.LoadScene("Vitoria");
        }
    }

    void OnGUI () {
        Scene scene = SceneManager.GetActiveScene();
        GUI.skin = layout;
        if (scene.name == "Fase1" || scene.name == "Fase2")
        {
            FaseGUI();
        }
        if (scene.name == "Menu")
        {
            MenuGUI();
        }
        if (scene.name == "Derrota")
        {
            DerrotaGUI();
        }
        if (scene.name == "Vitoria")
        {
            VitoriaGUI();
        }
    }


    public static void LoseLife(){
        lifes--;
    }

    public static void Score(){
        playerScore += 50;
    }

    void FaseGUI()
    {
        GUIStyle customStyle = new GUIStyle(GUI.skin.label);
        customStyle.fontSize = 20; 
        customStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(100, 0, 400, 100), "Pontos: " + playerScore, customStyle);
        GUI.Label(new Rect(800, 0, 400, 100), "HP: " + lifes, customStyle);
    }

    void VitoriaGUI(){
        GUIStyle customStyle = new GUIStyle(GUI.skin.label);
        customStyle.fontSize = 70; 
        customStyle.normal.textColor = Color.green; 
        customStyle.alignment = TextAnchor.MiddleCenter; 
        Vector2 textSize = customStyle.CalcSize(new GUIContent("Vitoria"));


        float x = (Screen.width - textSize.x) / 2;
        float y = (Screen.height - textSize.y) / 2; 

        GUI.Label(new Rect(x, y, textSize.x, textSize.y), "Vitoria", customStyle);
    }

    void DerrotaGUI(){
        GUIStyle customStyle = new GUIStyle(GUI.skin.label);
        customStyle.fontSize = 60;
        customStyle.normal.textColor = Color.red;
        customStyle.alignment = TextAnchor.UpperCenter;

        GUI.Label(new Rect(Screen.width / 2 - 200, 50, 400, 100), "Derrota", customStyle);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 30;

        float buttonWidth = 220;
        float buttonHeight = 80;
        float buttonX = (Screen.width - buttonWidth) / 2;
        float buttonY = Screen.height - 90;

        if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Reiniciar", buttonStyle))
        {
            SceneManager.LoadScene("Fase1");
        }
    }

    void MenuGUI(){
        GUIStyle customStyle = new GUIStyle(GUI.skin.label);
        customStyle.fontSize = 70;
        customStyle.normal.textColor = Color.blue;
        customStyle.alignment = TextAnchor.UpperCenter;

        GUI.Label(new Rect(Screen.width / 2 - 200, 50, 400, 100), "ARKANOID", customStyle);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 30;

        float buttonWidth = 220;
        float buttonHeight = 80;
        float buttonX = (Screen.width - buttonWidth) / 2;
        float buttonY = Screen.height - 150;

        if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "COMEÇAR", buttonStyle))
        {
            SceneManager.LoadScene("Fase1");
        }
    }
}
