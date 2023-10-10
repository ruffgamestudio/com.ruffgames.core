using System.Collections.Generic;
using System.Linq;
using com.ruffgames.core.Core.Runtime.Scripts;
using com.ruffgames.core.Runtime.Scripts.Audio;
using Sirenix.Utilities;
using UnityEngine;

namespace com.ruffgames.core.Runtime.Scripts.Managers
{
    public class AudioManager
    {
        private GameObject _sourceContainer;

        private readonly EventManager _eventManager;
        private readonly SettingsManager _settingsManager;
        private readonly AudioClips _audioClips;
        private readonly List<AudioSource> _audioSources;

        public AudioManager(EventManager eventManager, SettingsManager settingsManager, AudioClips audioClips)
        {
            _eventManager = eventManager;
            _eventManager.OnSoundStateChanged += SetMute;
            _settingsManager = settingsManager;
            _audioClips = audioClips;
            _audioSources = new List<AudioSource>();
        }

        ~AudioManager()
        {
            _eventManager.OnSoundStateChanged -= SetMute;
        }

        private void SetMute(bool state)
        {
            _audioSources.Where(x => x).ForEach(x => x.mute = state);
        }

        public void Play(AudioClipType type)
        {
            if (!_settingsManager.IsSoundEnabled) return;

            var clip = _audioClips.GetClip(type);
            PlayClip(clip);
        }

        private void PlayClip(AudioClipHolder clipHolder,bool dontDuplicate = true)
        {
            _audioSources.RemoveAll(x => !x);

            foreach (var source in _audioSources)
            {
                if (source.clip == clipHolder.Clip)
                {
                    if (dontDuplicate)
                    {
                        source.Stop();
                        source.loop = clipHolder.Loop;
                        source.mute = !_settingsManager.IsSoundEnabled;
                        source.volume = clipHolder.Volume;
                        source.Play();
                        return;
                    }
                }

                if (!source.isPlaying)
                {
                    source.clip = clipHolder.Clip;
                    source.loop = clipHolder.Loop;
                    source.mute = !_settingsManager.IsSoundEnabled;
                    source.volume = clipHolder.Volume;
                    source.Play();
                    return;
                }
             
            }

            if (_sourceContainer is null)
            {
                _sourceContainer = new GameObject("AudioSourceContainer");
            }

            var newSource = _sourceContainer.AddComponent<AudioSource>();
            newSource.playOnAwake = false;
            newSource.loop = clipHolder.Loop;
            newSource.clip = clipHolder.Clip;
            newSource.mute = !_settingsManager.IsSoundEnabled;
            newSource.volume = clipHolder.Volume;
            newSource.Play();
            
            _audioSources.Add(newSource);
        }
    }
}
