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

        private SeatService seatService;
        private WaitingLineService comeInWaitingLineService; //THIS DONT USE FOR NOW
        private WaitingLineService barWaitingLineService;
        private OrderService orderService; //THIS DONT USE FOR NOW
        private IBarService barService;
        IPlayerInteractionService playerInteractionService;

        [Inject]
        void Construct( SeatService seatService,
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

            var pos = barWaitingLineService.GetNextAvailablePosition();

            if ( pos == null ) {
                Debug.Log("there is no bar wait line, or full");
                RejectNpc(npc);
                return;
            }

            var info = npc.npcInfo;
            if ( info.npcProperties == NPCProperty.HotTemper ) {
                barService.AddChaos(0.1f);
            }


            npc.MoveToBar(pos, seat);

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