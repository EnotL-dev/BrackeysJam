using Assets._game.Bar.Controller;
using Assets._game.Player.Controller;
using Assets._game.TestingScript;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc.Controller {
    public class NPCService {

        private SeatService seatService;
        private WaitingLineService waitingLineService;
        private OrderService orderService;
        IPlayerInteractionService playerInteractionService;

        [Inject]
        void Construct(SeatService seatService, 
            WaitingLineService waitingLineService,
            OrderService orderService,
            IPlayerInteractionService playerInteractionService) {
            this.seatService = seatService;
            this.waitingLineService = waitingLineService;
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


            waitingLineService.Exit(npc);
            npc.MoveToDest(seat.transform.position);

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