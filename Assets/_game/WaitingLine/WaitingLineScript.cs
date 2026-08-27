using Assets._game.Npc.View;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.TestingScript {
    public class WaitingLineScript : MonoBehaviour {

        [SerializeField] int maxCap; //TODO: refactor this to scalable (it should support upgrade)

        [SerializeField] private Transform queueStartPoint;

        [SerializeField] private Vector3 queueDirection = Vector3.back;

        [SerializeField] private float spacing = 1.5f;

        public int CurrentOccupiedCount { get; set; }
        public int MaxCap => maxCap;

        public event Action<NPCScript> OnNpcTriggerEnter;
        public event Action<NPCScript> OnNpcTriggerExit;



        public void OnTriggerEnter( Collider other ) {
            if ( other.CompareTag("NPC") ) {
                Debug.Log("triggered npc and waiting line");
                CurrentOccupiedCount++;
                var script = other.GetComponent<NPCScript>();
                OnNpcTriggerEnter?.Invoke(script);
                //EnterLine(script);

            }
        }

        public void OnTriggerExit( Collider other ) {
            if ( other.CompareTag("NPC") ) {
                CurrentOccupiedCount--;
                var script = other.GetComponent<NPCScript>();
                OnNpcTriggerExit?.Invoke(script);
            }
        }


        //public void EnterLine( NPCScript script ) {
        //  int index = scripts.IndexOf(script);
        //  script.MoveToWaitingLine(GetPosition(index));
        //}

        public Vector3 GetPosition( int index ) {
            Transform origin = queueStartPoint != null ? queueStartPoint : transform;

            // Uses local space so rotation of the queue object works automatically
            Vector3 worldDirection = origin.TransformDirection(queueDirection.normalized);
            return origin.position + (worldDirection * (index * spacing));
        }


        private void OnDrawGizmosSelected() {
            for ( int i = 0; i < maxCap; i++ ) {
                Gizmos.color = (i < CurrentOccupiedCount) ? Color.red : Color.green;
                Gizmos.DrawWireSphere(GetPosition(i), 0.3f);
            }
        }
        //public Transform GetPosition( int index ) {
        //    return transforms[index];
        //}

    }
}