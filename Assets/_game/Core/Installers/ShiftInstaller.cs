using Assets._game.Core.StateMachine;
using Assets._game.Player.View;
using Assets._game.Shift.Controller;
using Assets._game.Shift.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers
{
    public class ShiftInstaller : MonoInstaller
    {
        [SerializeField] private ShiftManagerView shiftManagerView;
        public override void InstallBindings()
        {
            Container.Bind<ShiftManagerView>()
                 .FromComponentOn(shiftManagerView.gameObject)
                 .AsSingle();

            Container.Bind<IShiftService>()
                 .To<ShiftService>()
                 .AsSingle();

            Debug.Log("<color=green>Shifts initialized</color>");
        }
    }
}