using UnityEngine;
using UnityEngine.SceneManagement;
public class StartDemo : MonoBehaviour
{
    void Update(){
        ExitGame();
        if(Input.GetButtonDown("Submit"))
        SceneManager.LoadScene(1);
    }
    void ExitGame()
    {
        if (Input.GetButtonDown("Cancel"))
            Application.Quit();
    }
}