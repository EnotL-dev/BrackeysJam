using Assets._game.Interaction.View;
using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCInteractionScript : MonoBehaviour, IInteractable {

        [SerializeField] string dialoge; //TODO use a sperate script for this



        public string GetTip() => "E to talk";


        public void OnInteract() {

            Debug.Log("This should open the pannel for talkin and info of npc");

        }


        public bool FreezePlayer() {
            //Stop the npc from moving\

            return true; //for now
        }

        public void OnContinuousInteraction() {
            throw new System.NotImplementedException();
        }

        public void OnEndInteraction() {
            throw new System.NotImplementedException();
        }

        public void OnStartInteraction() {
            throw new System.NotImplementedException();
        }
    }
}