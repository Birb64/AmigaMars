using UnityEngine;
using UnityEngine.SceneManagement;
public class StartDemo : MonoBehaviour
{
    void Update(){
        if(Input.GetButtonDown("Submit"))
        SceneManager.LoadScene(1);
    }
}