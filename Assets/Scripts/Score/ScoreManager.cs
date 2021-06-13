using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoreManager : MonoBehaviour {

    [SerializeField] private List<TextMeshProUGUI> _scoreFields;
    [SerializeField] private AudioClip _pointsIncreasedAudio;

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
    }
    
    public void GoalReached() {
        _points += 50;
        UpdateText();
        PlayClip(_pointsIncreasedAudio);
    }

    private void UpdateText() {
        foreach (TextMeshProUGUI scoreField in _scoreFields)
        {
            scoreField.text = $"Score: {_points}";
        }
    }

    private void PlayClip(AudioClip clip) {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
