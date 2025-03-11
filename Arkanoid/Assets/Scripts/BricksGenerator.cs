using UnityEngine;
using UnityEngine.SceneManagement;

public class BricksGenerator : MonoBehaviour
{
    public GameObject blueBrick;
    public GameObject redBrick;
    public GameObject greenBrick;

    public static int numberOfBricks = 0;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Fase1")
        {
            Fase1();
        }
        if (SceneManager.GetActiveScene().name == "Fase2")
        {
            Fase2();
        }
    }

    void Fase1()
    {
        GameObject[] bricks = { blueBrick, redBrick, greenBrick };
        int colorIndex = 0;

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 16; i++)
            {
                GameObject brickToInstantiate = bricks[colorIndex];
                Instantiate(brickToInstantiate, new Vector2(-5 + 0.64f * i, 3 - j), Quaternion.identity);
                numberOfBricks++;

                colorIndex = (colorIndex + 1) % bricks.Length;
            }
        }
    }

    void Fase2()
    {
        GameObject[] bricks = { blueBrick, redBrick, greenBrick };
        int colorIndex = 0;

        for (int j = 0; j < 4; j++) 
        {
            for (int i = 0; i < 12; i++) 
            {
                GameObject brickToInstantiate = bricks[colorIndex];

                float offsetX = (j % 2 == 0) ? 0.32f : 0; 
                float posX = -4.5f + 0.64f * i + offsetX;
                float posY = 3 - 2*j * 0.5f; 

                Instantiate(brickToInstantiate, new Vector2(posX, posY), Quaternion.identity);
                numberOfBricks++;

                colorIndex = (colorIndex + 1) % bricks.Length;
            }
        }
    }
}
