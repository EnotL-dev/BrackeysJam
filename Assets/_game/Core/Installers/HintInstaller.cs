using Assets._game.Bar.Controller;
using Assets._game.Hint.Controller;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers
{
    public class HintInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HintService>().AsSingle().NonLazy();
        }
    }
}