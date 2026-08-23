using Assets._game.Npc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.TestingScript {
    public class WaitingLineScript : MonoBehaviour {

        [SerializeField] int maxCap; //TODO: refactor this to scalable (it should support upgrade)

        [Tooltip("use for position for waiting")]
        [SerializeField] Transform[] transforms;

        List<NPCScript> scripts = new();



        public void OnTriggerEnter( Collider other ) {
            if ( other.CompareTag("NPC") ) {
                if ( scripts.Count > maxCap ) return;
                Debug.Log("triggered npc and waiting line");

                var script = other.GetComponent<NPCScript>();
                scripts.Add(script);
                //EnterLine(script);

            }
        }


        //public void EnterLine( NPCScript script ) {
        //    script.MoveToWaitingLine(GetMostPosition());
        //}

        public Transform GetMostPosition() {
            return transforms[scripts.Count];
        }

        public void Exit() {
        }


        public bool HasAvailableSlot() {
            return scripts.Count < maxCap;
        }


    }
}