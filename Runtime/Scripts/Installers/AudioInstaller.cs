using com.ruffgames.core.Runtime.Scripts.Audio;
using com.ruffgames.core.Runtime.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace com.ruffgames.core.Runtime.Scripts.Installers
{
    [CreateAssetMenu(menuName = "Audio/Installer")]
    public class AudioInstaller : ScriptableObjectInstaller<AudioInstaller>
    {
        [SerializeField] private AudioClips _audioClips;

        public override void InstallBindings()
        {
            Container.Bind<AudioClips>().FromScriptableObject(_audioClips).AsSingle();
            Container.Bind<AudioManager>().AsSingle().NonLazy();
        }
    }
}
