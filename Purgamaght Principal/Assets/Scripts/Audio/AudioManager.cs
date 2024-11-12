using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    
    [Header("Lista de efeito sonoros")]
    [SerializeField] private List<AudioClip> _listAudioFx;

    [Header("Lista de musicas")]
    [SerializeField] private List<AudioClip> _listAudioMusic;

    [SerializeField]public movPlayer player;

    public void Update()
    {
        Passos();        
    }   

    void Start()
    {
        PlayBackgroundMusic();
        
    }

    public void PlayBackgroundMusic()
    {
        musicSource.clip = _listAudioMusic[0];
        musicSource.loop = true;
        musicSource.Play();
    }

    public void Passos()
    {
        bool isGrounded = player.VerificarPiso();

        if (isGrounded && player.rb.velocity.magnitude > 0)
        {
            // Reproduzir o som de passos se a velocidade do jogador for maior que zero
            if (!SFXSource.isPlaying && _listAudioFx.Count > 0)
            {
                SFXSource.clip = _listAudioFx[0];
                SFXSource.Play();
            }
        }
        else
        {
            SFXSource.Stop();
        }
    }
}
