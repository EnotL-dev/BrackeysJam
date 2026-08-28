using Assets._game.Bar.Model.Alcohol;
using Assets._game.Npc.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._game.Npc {

    [CreateAssetMenu(fileName = "NPCIconDatabase", menuName = "Game/NPC Icon Database")]
    public class NPCIconDatabase : ScriptableObject {

        [Serializable]
        public class WealthMapping {
            public NPCWealthType type;
            public Sprite icon;
        }

        [Serializable]
        public class PropertyMapping {
            public NPCProperty type;
            public Sprite icon;
        }

        [Serializable]
        public class DrinkMapping {
            public AlcoholType type;
            public Sprite icon;
        }

        [Header("Wealth Icons")]
        [SerializeField] private List<WealthMapping> wealthIcons = new();

        [Header("Property / Characteristic Icons")]
        [SerializeField] private List<PropertyMapping> propertyIcons = new();

        [Header("Drink Icons")]
        [SerializeField] private List<DrinkMapping> drinkIcons = new();

        // Fast runtime lookup caches
        private Dictionary<NPCWealthType, Sprite> _wealthDict;
        private Dictionary<NPCProperty, Sprite> _propertyDict;
        private Dictionary<AlcoholType, Sprite> _drinkDict;

        private void OnEnable() {
            _wealthDict = null;
            _propertyDict = null;
            _drinkDict = null;
        }

        public Sprite GetWealthIcon( NPCWealthType type ) {
            if ( _wealthDict == null ) {
                _wealthDict = new Dictionary<NPCWealthType, Sprite>();
                foreach ( var item in wealthIcons ) {
                    if ( item.icon != null && !_wealthDict.ContainsKey(item.type) ) {
                        _wealthDict.Add(item.type, item.icon);
                    }
                }
            }
            return _wealthDict.GetValueOrDefault(type);
        }

        public Sprite GetPropertyIcon( NPCProperty type ) {
            if ( _propertyDict == null ) {
                _propertyDict = new Dictionary<NPCProperty, Sprite>();
                foreach ( var item in propertyIcons ) {
                    if ( item.icon != null && !_propertyDict.ContainsKey(item.type) ) {
                        _propertyDict.Add(item.type, item.icon);
                    }
                }
            }
            return _propertyDict.GetValueOrDefault(type);
        }

        public Sprite GetDrinkIcon( AlcoholType type ) {
            if ( _drinkDict == null ) {
                _drinkDict = new Dictionary<AlcoholType, Sprite>();
                foreach ( var item in drinkIcons ) {
                    if ( item.icon != null && !_drinkDict.ContainsKey(item.type) ) {
                        _drinkDict.Add(item.type, item.icon);
                    }
                }
            }
            return _drinkDict.GetValueOrDefault(type);
        }
    }
}