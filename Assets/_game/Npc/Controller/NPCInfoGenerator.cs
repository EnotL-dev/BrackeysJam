using Assets._game.Bar.Controller;
using Assets._game.Bar.Model.Alcohol;
using Assets._game.Npc.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets._game.Npc.Controller {
    public class NPCInfoGenerator {

        string[] names = { "John", "Alex", "Mike", "David", "James" };
        string[] sexes = { "Male", "Female" };
        NPCProperty[] npcProperties;
        NPCWealthType[] wealthTypes;
        AlcoholType[] alcoholTypes;

        IBarService barService;

        [Inject]
        void Construct( IBarService barService ) {
            this.barService = barService;
        }

        public NPCInfo Generate() {
            string name = names[UnityEngine.Random.Range(0, names.Length)];
            int age = UnityEngine.Random.Range(10, 80);
            string sex = sexes[UnityEngine.Random.Range(0, sexes.Length)];

            wealthTypes ??= (NPCWealthType[])System.Enum.GetValues(typeof(NPCWealthType));
            npcProperties ??= (NPCProperty[])System.Enum.GetValues(typeof(NPCProperty));
            alcoholTypes ??= (AlcoholType[])System.Enum.GetValues(typeof(AlcoholType));

            float vibe = barService.GetVibe().vibe;

            var wealth = GenerateWealth(vibe);

            var property = GenerateProperty(vibe);

            var farDrink = alcoholTypes[UnityEngine.Random.Range(0, alcoholTypes.Length)];


            float height = sex == "Male"
            ? UnityEngine.Random.Range(1.65f, 1.90f)
            : UnityEngine.Random.Range(1.55f, 1.80f);

            float weight = UnityEngine.Random.Range(50f, 130f);

            return new NPCInfo(
                name,
                age,
                sex,
                height,
                weight,
                wealth,
                property,
                farDrink
            );
        }

        private NPCWealthType GenerateWealth( float vibe ) {

            float badWeight;
            float normalWeight;
            float goodWeight;

            if ( vibe < 30f ) {
                badWeight = 70f;
                normalWeight = 20f;
                goodWeight = 10f;
            }
            else if ( vibe < 70f ) {
                badWeight = 50f;
                normalWeight = 30f;
                goodWeight = 20f;
            }
            else {
                badWeight = 20f;
                normalWeight = 50f;
                goodWeight = 30f;
            }

            float roll = UnityEngine.Random.Range(0f, 100f);

            if ( roll < badWeight )
                return NPCWealthType.poor;

            if ( roll < badWeight + normalWeight )
                return NPCWealthType.normal;

            return NPCWealthType.rich;
        }

        private NPCProperty GenerateProperty( float vibe ) {

            float roll = UnityEngine.Random.Range(0f, 100f);

            if ( vibe < 30f ) {

                if ( roll < 40f )
                    return NPCProperty.Drunkard;

                if ( roll < 60f )
                    return NPCProperty.HotTemper;

                return NPCProperty.Rogue;
            }
            else if ( vibe < 70f ) {

                if ( roll < 35f )
                    return NPCProperty.Drunkard;

                if ( roll < 65f )
                    return NPCProperty.HotTemper;

                return NPCProperty.Rogue;
            }
            else {
                if ( roll < 35f )
                    return NPCProperty.Drunkard;

                if ( roll < 70f )
                    return NPCProperty.HotTemper;
            }

            return NPCProperty.Rogue;
        }
    }
}
