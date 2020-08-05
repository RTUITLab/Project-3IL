using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceEnemy : MonoBehaviour {
    [SerializeField] AudioClip[] _voiceClips;
    AudioSource _audioSource = null;
    void Start () {
        _audioSource = this.gameObject.GetComponent<AudioSource> ();
        PlayNextSong ();
    }
    //Play a list of voices in random order
    void PlayNextSong () {
        _audioSource.clip = _voiceClips[Random.Range (0, _voiceClips.Length)];
        _audioSource.Play ();
        Invoke ("PlayNextSong", _audioSource.clip.length + Random.Range (0, 20));
    }
    public void PlayVoice () {
        _audioSource.clip = _voiceClips[Random.Range (0, _voiceClips.Length)];
        _audioSource.Play ();
    }
}