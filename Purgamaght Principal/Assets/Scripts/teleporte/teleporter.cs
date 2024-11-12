using UnityEngine;
using UnityEngine.SceneManagement;

public class teleporter : MonoBehaviour
{
    public string Cena = "TargetScene";  // Name of the target scene
    public float Alcance = 5f;                // Range within which teleport can happen

    private GameObject player;
    private GameObject chegada;

    void Start()
    {
        // Find the player object in the scene (could also be assigned manually)
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object with tag 'Player' not found in the scene.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Check distance between this object and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // If player is in range and "E" key is pressed
            if (distanceToPlayer <= Alcance && Input.GetKeyDown(KeyCode.E))
            {
                // Load the target scene
                SceneManager.LoadScene(Cena);

                // Start the coroutine to teleport the player once the new scene loads
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Cena)
        {
            // Find the object with tag "chegada" in the new scene
            chegada = GameObject.FindGameObjectWithTag("chegada");
            if (chegada != null)
            {
                // Move the player to the "chegada" position
                player.transform.position = chegada.transform.position;
            }
            else
            {
                Debug.LogError("Object with tag 'chegada' not found in the target scene.");
            }

            // Unsubscribe from the event to prevent multiple calls
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
