using Assets._game.Bar.Controller;
using Assets._game.Player.Controller;
using Assets._game.TestingScript;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc.Controller {
    public class NPCService {

        private SeatService seatService;
        private WaitingLineService comeInWaitingLineService;
        private WaitingLineService barWaitingLineService;
        private OrderService orderService;
        IPlayerInteractionService playerInteractionService;

        [Inject]
        void Construct( SeatService seatService,
            [Inject(Id = "ComeIn")] WaitingLineService comeInWaitingLineService,
            [Inject(Id = "Bar")] WaitingLineService barWait,
            OrderService orderService,
            IPlayerInteractionService playerInteractionService ) {
            this.seatService = seatService;
            this.comeInWaitingLineService = comeInWaitingLineService;
            this.barWaitingLineService = barWait;
            this.orderService = orderService;
            this.playerInteractionService = playerInteractionService;
        }


        public void AcceptNpc( NPCScript npc ) {


            Seat seat = seatService.FindBestSeat();

            Debug.Log("done find best seat");

            if ( seat == null ) {

                Debug.Log("there is no seat");
                RejectNpc(npc);
                return;
            }


            comeInWaitingLineService.Exit(npc);

            var pos = barWaitingLineService.GetNextAvailablePosition();
            npc.MoveToBar(pos);

            Debug.Log("npc moving now");

            EndInteraction();
        }

        public void RejectNpc( NPCScript npc ) {

            //npc.Leave();
            EndInteraction();
        }

        public void EndInteraction() {
            playerInteractionService.EndInteraction();
        }



    }
}