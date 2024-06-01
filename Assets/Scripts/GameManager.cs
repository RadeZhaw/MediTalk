using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public AudioClip[] STD;
    public AudioClip[] Skin;
    private bool isSTD;
    private int charIndex;
    public GameObject player;
    public Button nextButton;
    public Button previousButton;

    private AudioSource audioSource;
    private AudioClip[] currentClips;
    private int currentIndex = 0;
    public GameObject[] medals;
    public Animator animator;

    void Start()
    {
        animator = player.GetComponent<Animator>();
        audioSource = player.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on player GameObject.");
        }

        if (animator == null)
        {
            Debug.LogError("Animator component not assigned in the Inspector.");
        }

        isSTD = MenuManager.singleTon.isSTD;
        charIndex = MenuManager.singleTon.characterIndex;

        if (charIndex != 0)
        {
            if (charIndex - 1 < medals.Length)
            {
                medals[charIndex - 1].SetActive(true);
            }
            else
            {
                Debug.LogError("charIndex is out of bounds of the medals array.");
            }
        }

        UpdateCurrentClips();
        PlayCurrentClip();
        UpdateButtonStates();
    }

    void Update()
    {
        if (!audioSource.isPlaying && !audioSource.loop)
        {
            OnAudioSourceStopped();
        }
    }

    void UpdateCurrentClips()
    {
        currentClips = isSTD ? STD : Skin;
    }

    void PlayCurrentClip()
    {
        if (currentClips.Length > 0 && currentIndex >= 0 && currentIndex < currentClips.Length)
        {
            audioSource.clip = currentClips[currentIndex];
            audioSource.Play();
        }
    }

    public void NextClip()
    {
        Debug.LogError("Next");
        if (animator != null)
        {
            animator.SetInteger("val", 0);
        }
        if (currentIndex < currentClips.Length - 1)
        {
            currentIndex++;
            PlayCurrentClip();
            UpdateButtonStates();
        }
    }

    public void PreviousClip()
    {
        Debug.LogError("Previous");
        if (animator != null)
        {
            animator.SetInteger("val", 0);
        }
        if (currentIndex > 0)
        {
            currentIndex--;
            PlayCurrentClip();
            UpdateButtonStates();
        }
    }

    void UpdateButtonStates()
    {
        previousButton.gameObject.SetActive(currentIndex > 0);
        nextButton.gameObject.SetActive(currentIndex < currentClips.Length - 1);
    }

    void OnAudioSourceStopped()
    {
        if (animator != null)
        {
            animator.SetInteger("val", 1);
        }
        Debug.Log("Audio source stopped playing");
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}