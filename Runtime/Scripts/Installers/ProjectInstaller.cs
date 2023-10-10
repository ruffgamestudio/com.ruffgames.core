using System.Linq;
using _game.Storage;
using com.ruffgames.core.Core.Runtime.Scripts;
using com.ruffgames.core.Runtime.Scripts.Managers;
using com.ruffgames.core.Runtime.Scripts.Storage;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ProjectReferenceData _projectReferenceData;
        public override void InstallBindings()
        {
            Container.Bind<EventManager>().AsSingle().NonLazy();
            Container.Bind<SettingsManager>().AsSingle().NonLazy();
            Container.Bind(new[] { typeof(IStorage) }.Concat(typeof(IStorage).GetInterfaces())).To<JsonStorage>()
                .AsSingle().NonLazy();
            Container.Bind<FeelVibrationManager>().AsSingle().NonLazy();
            Container.Bind<ProjectReferenceData>().FromNewScriptableObject(_projectReferenceData).AsSingle();
        }
    }
}
