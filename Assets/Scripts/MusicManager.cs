using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private AudioSource managerSource = null;
    [SerializeField] private AudioClip[] musicClips = null;
    [SerializeField] private AudioClip btnSFX = null;
    public bool menuSound = true;
    private bool lobbySound = false;
    private bool nivel1Sound = false;
    
    // Propiedad DON'T DESTROY ON LOAD
    public static MusicManager musicMngStatic; //comparte esta variable con todas las escenas

    private void Awake()
    {
        if (musicMngStatic != null && musicMngStatic != this)
        {
            Destroy(gameObject);
            return;
        }
        musicMngStatic = this;
        DontDestroyOnLoad(this);

        managerSource = gameObject.GetComponent<AudioSource>();
    }
    //---------------------

    private void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                if (menuSound == false)
                {
                    menuSound = true;
                    managerSource.volume = 0.25f;
                    managerSource.Stop();
                    managerSource.clip = musicClips[0];
                    managerSource.Play();
                }
                break;

            case "Lobby":
                if (lobbySound == false)
                {
                    lobbySound = true; 
                    managerSource.volume = 0.15f;
                    managerSource.Stop();
                    managerSource.clip = musicClips[1];
                    managerSource.Play();
                }
                break;

            case "Nivel 1":
                if (nivel1Sound == false)
                {
                    nivel1Sound = true;
                    managerSource.volume = 0.40f;
                    managerSource.Stop();
                    managerSource.clip = musicClips[2];
                    managerSource.Play();
                }
                break;
        }
    }

    public void ButtonSFX()
    {
        managerSource.PlayOneShot(btnSFX);
    }
}
