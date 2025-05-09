using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    // Duration for how long the splash screen will be shown
    [SerializeField]public float splashDuration = 3f;

    private void Start()
    {
        // Start the countdown to load the main scene
        Invoke("LoadMainMenu", splashDuration);
    }

    // Method to load the next scene (main menu or game scene)
    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuL1"); // Change "MainMenu" to your actual first scene name
    }
}
