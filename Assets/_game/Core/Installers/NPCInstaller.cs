using Assets._game.Bar.Controller;
using Assets._game.Npc.Controller;
using Assets._game.Npc.View;
using Assets._game.TestingScript;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Core.Installers {
    public class NPCInstaller : MonoInstaller {


        [SerializeField] WaitingLineScript waitingLineScript;
        [SerializeField] NPCInfoView NPCInfoViewInstance;

        public override void InstallBindings() {
            BindView();

            Debug.Log("<color=green>NPC info view was initialized</color>");
        }

        private void BindView() {
            Container.Bind<NPCService>().AsSingle();
            Container.Bind<SeatService>().AsSingle();
            Container.Bind<WaitingLineService>().AsSingle();
            Container.Bind<BarService>().AsSingle();

            Container.Bind<NPCInfoView>().
                FromInstance(NPCInfoViewInstance).
                AsSingle();

            Container.Bind<WaitingLineScript>().
                FromInstance(waitingLineScript).
                AsSingle();


        }
    }
}