using System.Collections;
using UnityEngine;

namespace Assets._game.Npc {
    public class NPCInfo {

        public string name { get; private set; }
        public int age { get; private set; }
        public string sex { get; private set; }
        public float height { get; private set; }
        public float weight { get; private set; }



        //this is for testing, will deletee after
        public NPCInfo() {
            name = "test";
            age = 50;
            sex = "Male";
            height = 1.8f;
            weight = 75.7f;
        }

        public NPCInfo( string name, int age, string sex, float height, float weight ) {
            this.name = name;
            this.age = age;
            this.sex = sex;
            this.height = height;
            this.weight = weight;
        }


    }
}