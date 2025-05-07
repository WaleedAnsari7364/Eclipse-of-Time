using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public float moveSpeed = 2f;    // Speed of the up and down movement
    public float offsetAmount = 1f; // Offset distance from the initial position

    private Vector3 initialPosition;
    private bool movingUp = true;

    void Start()
    {
        // Save the initial position of the GameObject
        initialPosition = transform.position;
    }

    void Update()
    {
        // Move the GameObject up and down
        MoveUpDown();

        // Update the position with an offset
        ApplyOffset();
    }

    void MoveUpDown()
    {
        // Calculate the new Y position based on the movement speed
        float newY = movingUp ? initialPosition.y + moveSpeed * Time.deltaTime : initialPosition.y - moveSpeed * Time.deltaTime;

        // Check if the GameObject should change direction
        if (newY > initialPosition.y + offsetAmount || newY < initialPosition.y - offsetAmount)
        {
            movingUp = !movingUp;
        }

        // Set the new position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void ApplyOffset()
    {
        // Apply the offset to the X position
        transform.position = new Vector3(initialPosition.x + Mathf.Sin(Time.time) * offsetAmount, transform.position.y, transform.position.z);
    }
}
