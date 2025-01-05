using Plugins.Audio.Core;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SourceAudio))]
public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private string _musicName;
    public SourceAudio _sourceAudio;

    private void Awake()
    {
        _sourceAudio = GetComponent<SourceAudio>();
        _sourceAudio.Play(_musicName);
    }
}
