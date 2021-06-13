using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoreManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private AudioClip pointsIncreasedAudio;

    private int _points = 0;
    private AudioSource _audioSource = null;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        UpdateText();
    }

    public void AddPoint() {
        _points++;
        UpdateText();
        PlayClip(pointsIncreasedAudio);
    }

    private void UpdateText() {
        textField.text = $"Score: {_points}";
    }

    private void PlayClip(AudioClip clip) {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
