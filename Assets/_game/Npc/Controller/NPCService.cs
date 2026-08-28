using Assets._game.Bar.Controller;
using Assets._game.Npc.Enum;
using Assets._game.Npc.View;
using Assets._game.Player.Controller;
using Assets._game.TestingScript;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc.Controller {
    public class NPCService {

        private ISeatService seatService;
        private WaitingLineService comeInWaitingLineService; //THIS DONT USE FOR NOW
        private WaitingLineService barWaitingLineService;
        private OrderService orderService; //THIS DONT USE FOR NOW
        private IBarService barService;
        IPlayerInteractionService playerInteractionService;

        [Inject]
        void Construct( ISeatService seatService,
            [Inject(Id = "ComeIn")] WaitingLineService comeInWaitingLineService,
            [Inject(Id = "Bar")] WaitingLineService barWait,
            OrderService orderService,
            IPlayerInteractionService playerInteractionService,
            IBarService barService ) {
            this.seatService = seatService;
            this.comeInWaitingLineService = comeInWaitingLineService;
            this.barWaitingLineService = barWait;
            this.orderService = orderService;
            this.playerInteractionService = playerInteractionService;
            this.barService = barService;
        }


        public void AcceptNpc( NPCScript npc ) {
            Seat seat = seatService.FindBestSeat(npc.transform.position);

            if ( seat == null ) {
                Debug.Log("there is no seat");
                RejectNpc(npc);
                return;
            }

            if ( barWaitingLineService.TryReserve(out Vector3 targetPos) ) {
                npc.MoveToBar(targetPos, seat);
            }
            else {
                Debug.Log("Bar waiting line is full or unavailable.");
                RejectNpc(npc);
            }

            var info = npc.npcInfo;
            if ( info.npcProperties == NPCProperty.HotTemper ) {
                barService.AddChaos(0.1f);
            }

            EndInteraction(npc);
        }

        public void RejectNpc( NPCScript npc ) {

            npc.Leave();

            EndInteraction(npc);
        }

        public void EndInteraction( NPCScript npc ) {

            var script = npc.GetComponent<NPCInteractionScript>();
            script.ModifyCanInteract();

            playerInteractionService.EndInteraction();
        }



    }
}