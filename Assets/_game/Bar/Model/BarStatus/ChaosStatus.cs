using Assets._game.Bar.Controller;
using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._game.Bar.Model.BarStatus {
    public class ChaosStatus {

        public float chaosScale { get; private set; } = 0;

        public void AddChaos( float amt ) {
            chaosScale += amt;

            Debug.Log($"Chaos Status {chaosScale}");
        }

        public void ReduceChaos( float amt ) {
            chaosScale = Math.Max(chaosScale - amt, 0);
            Debug.Log($"Chaos Status {chaosScale}");
        }


    }
}