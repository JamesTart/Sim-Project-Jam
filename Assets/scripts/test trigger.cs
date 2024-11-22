using UnityEngine;

public class SimpleAudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;    // AudioSource component for playback
    public AudioClip[] audioClips;     // Array to hold multiple audio clips
    private int currentClipIndex = 0;  // Tracks the current audio clip index

    private bool isPlayerInTrigger = false; // Tracks if player is in the trigger zone

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers it
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        // Check if "E" is pressed and the player is in the trigger zone
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            PlayNextAudio();
        }
    }

    private void PlayNextAudio()
    {
        if (audioClips == null || audioClips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned.");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource assigned to the script.");
            return;
        }

        // Play the current audio clip
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();

        // Move to the next clip (loop back to the first if at the end)
        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
    }
}
