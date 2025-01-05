using Plugins.Audio.Core;
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SourceAudio))]
public class Checkpoint : MonoBehaviour
{
    public static Action<Transform> OnCheckpointReached;

    [SerializeField] private string _musicName;
    private SourceAudio _sourceAudio;
    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _sourceAudio = GetComponent<SourceAudio>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _sourceAudio.Play(_musicName);
        OnCheckpointReached?.Invoke(transform);
        _collider.enabled = false;
    }
}