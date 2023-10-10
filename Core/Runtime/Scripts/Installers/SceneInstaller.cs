using com.ruffgames.core.Runtime.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace com.ruffgames.core.Core.Runtime.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
             Container.BindFactory<Object,Level, LevelFactory>().FromFactory<PrefabFactory<Level>>();
        }
    }
}
