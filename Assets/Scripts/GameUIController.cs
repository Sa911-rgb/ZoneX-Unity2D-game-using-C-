using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public void restartButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void homeButton() {
        SceneManager.LoadScene("MainMenu");
    }
}
