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

            var wealth = wealthTypes[UnityEngine.Random.Range(0, wealthTypes.Length)];

            List<NPCProperty> properties = GenerateProperties(barService.GetVibe().vibe);

            //foreach ( var property in nPCProperties ) {
            //    npcProperties.Add( property );
            //}

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
                properties,
                farDrink
            );
        }

        private List<NPCProperty> GenerateProperties( float currentVibe ) {
            var selectedProperties = new HashSet<NPCProperty>();

            // Roll for how many traits this NPC gets (e.g., 1 to 2 traits)
            int traitCount = UnityEngine.Random.Range(1, 3);

            // TODO: Replace this loop with your vibe-based probability weights later
            while ( selectedProperties.Count < traitCount && selectedProperties.Count < npcProperties.Length ) {
                NPCProperty randomProperty = npcProperties[UnityEngine.Random.Range(0, npcProperties.Length)];
                selectedProperties.Add(randomProperty);
            }

            return selectedProperties.ToList();
        }
    }
}