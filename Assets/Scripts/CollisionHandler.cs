using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Components
    Movement movement;
    AudioSource audioSource;

    // State
    bool isControllable = true;
    bool isCollidable = true;

    // Parameters
    [SerializeField] float delay = 1f;

    // Audio
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    // Particle Systems
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    private void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!isControllable || !isCollidable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartFinishSequence()
    {
        movement.enabled = false;
        isControllable = false;
        Debug.Log("Level Completed!");
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        Invoke("NextLevel", delay);
    }

    private void StartCrashSequence()
    {
        movement.enabled = false;
        isControllable = false;
        Debug.Log("You Crashed!");
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        Invoke("ReloadLevel", delay);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void NextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            NextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
            if (isCollidable)
            {
                Debug.Log("Collisions Enabled");
            }
            else
            {
                Debug.Log("Collisions Disabled");
            }
        }
    }
}
