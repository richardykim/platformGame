using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameHasEnded = false;
    public float restartDelay = 2f;
    public GameObject completeLevelUI;
    [SerializeField] private AudioSource levelCompleteSound;
    public void EndGame()
    {
        if (gameHasEnded == false){
            gameHasEnded = true;
            print("Game Over!");

            // Restart Game
            Invoke("Restart", restartDelay);
        }
    }

    public void CompleteLevel()
    {
        print("LEVEL COMPLETE");
        levelCompleteSound.Play();
        completeLevelUI.SetActive(true);
    }

    void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
