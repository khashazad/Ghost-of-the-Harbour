using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Assign the Spawn Point in the Inspector")]
    public Transform spawnPoint;
    public int lives = 2; // Set initial lives

    private Vector3 startPos;
    private Quaternion startRot;

    public delegate void OnLivesChanged();
    public static event OnLivesChanged LivesChangedCallback;

    private void Awake(){
        this.startPos = spawnPoint.position;
        this.startRot = spawnPoint.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has the tag "Water"
        if (other.CompareTag("water"))
        {
            Debug.Log(startPos);
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Respawn at the designated spawn position and rotation
            spawnPoint.position = startPos;
            spawnPoint.rotation = startRot;

            // If you're using continuous movement or a character controller,
            // you might want to reset controller states here as well
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameEnd");
    }

    // Public method to decrement lives from other scripts
    public void TakeDamage()
    {
        lives--;

        // Trigger LivesChangedCallback event to update UI
        LivesChangedCallback?.Invoke();

        if (lives <= 0)
        {
            GameOver();
        }
    }
}