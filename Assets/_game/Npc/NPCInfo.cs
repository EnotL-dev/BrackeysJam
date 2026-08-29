using Assets._game.Bar.Model.Alcohol;
using Assets._game.Npc.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCInfo {

        public string name { get; private set; }
        public int age { get; private set; }
        public string sex { get; private set; }
        public float height { get; private set; }
        public float weight { get; private set; }
        public NPCWealthType wealth { get; private set; }
        public NPCProperty npcProperties { get; private set; }

        public AlcoholType farDrink { get; private set; }


        //this is for testing, will deletee after
        public NPCInfo() {
            name = "test";
            age = 50;
            sex = "Male";
            height = 1.8f;
            weight = 75.7f;
            wealth = NPCWealthType.Normal;
            npcProperties = NPCProperty.Drunkard;
            farDrink = AlcoholType.Beer;
        }

        public NPCInfo( string name,
            int age,
            string sex,
            float height,
            float weight,
            NPCWealthType walth,
            NPCProperty nPCProperties,
            AlcoholType farDrink ) {
            this.name = name;
            this.age = age;
            this.sex = sex;
            this.height = height;
            this.weight = weight;
            this.wealth = walth;
            this.npcProperties = nPCProperties;
            this.farDrink = farDrink;
        }


    }
}