using Assets._game.Bar.Model.Alcohol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets._game.Bar.Model.SOScript.DrinkSO.Alcohol {
    public class AlcoholCatalog {

        private readonly Dictionary<AlcoholType, AlcoholSO> lookup = new();

        public AlcoholCatalog() {
            AlcoholSO[] alcohols =
            Resources.LoadAll<AlcoholSO>("Bar/Alchohol/DrinkSO/Alcohol");

            Array.Sort(alcohols, ( a, b ) => a.BuyCost.CompareTo(b.BuyCost));

            foreach ( AlcoholSO alcohol in alcohols ) {
                if ( alcohol == null ) continue;

                if ( !lookup.TryAdd(alcohol.AlcoholType, alcohol) ) {
                    Debug.LogWarning(
                        $"[AlcoholCatalog] Duplicate AlcoholType: " +
                        $"{alcohol.AlcoholType}"
                    );
                }
            }

            Debug.Log(
                $"[AlcoholCatalog] Loaded {lookup.Count} alcohols"
            );
        }

        public AlcoholSO Get( AlcoholType type ) {
            if ( lookup.TryGetValue(type, out AlcoholSO alcohol) )
                return alcohol;

            Debug.LogError(
                $"[AlcoholCatalog] No AlcoholSO mapped for type: {type}"
            );

            return null;
        }

        public bool TryGet(
            AlcoholType type,
            out AlcoholSO alcohol ) {
            return lookup.TryGetValue(type, out alcohol);
        }

        public IEnumerable<AlcoholSO> GetAll() {
            return lookup.Values;
        }
    }
}