
using UnityEngine;
using UnityEngine.UI;

public class TimerScreen : MonoBehaviour
{
    [SerializeField] private float totalTime = 180.0f; // Total countdown time in seconds
    private float currentTime; // Current remaining time

    public GameObject PortalMessage; // Reference to the GameObject to activate

    public GameObject Portal;

    public Text timerText; // Reference to the UI Text element


    public GameObject InitialMessage;

    void Start()
    {
        Portal.SetActive(false);
        InitialMessage.SetActive(true);

        Invoke("DestroyInitialMessage", 3f);
        // Get reference to the UI Text component

        // Set initial time
        currentTime = totalTime;
    }

    void Update()
    {
        // Update current time
        currentTime -= Time.deltaTime;

        // Clamp current time to avoid negative values
        if (currentTime < 0)
        {
            currentTime = 0;

            // Activate the GameObject when the timer reaches zero
            if (PortalMessage != null)
            {
                PortalMessage.SetActive(true);
                Portal.SetActive(true);
                Invoke("DestroyPortalMessage", 3f); 
            }
        }

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Update UI text with formatted time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    void DestroyInitialMessage()
    {
        InitialMessage.SetActive(false);
    }

    void DestroyPortalMessage()
    {
        PortalMessage.SetActive(false);
    }
}
