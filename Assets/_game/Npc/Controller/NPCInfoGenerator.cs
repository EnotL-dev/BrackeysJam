using Assets._game.Bar.Model.Alcohol;
using Assets._game.Npc.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._game.Npc.Controller {
    public class NPCInfoGenerator {

        string[] names = { "John", "Alex", "Mike", "David", "James" };
        string[] sexes = { "Male", "Female" };
        NPCProperty[] nPCProperties;
        NPCWealthType[] wealthTypes;
        AlcoholType[] alcoholTypes;

        public NPCInfo Generate() {
            string name = names[UnityEngine.Random.Range(0, names.Length)];
            int age = UnityEngine.Random.Range(10, 80);
            string sex = sexes[UnityEngine.Random.Range(0, sexes.Length)];

            if ( wealthTypes == null ) wealthTypes = (NPCWealthType[])System.Enum.GetValues(typeof(NPCWealthType));
            if ( nPCProperties == null ) nPCProperties = (NPCProperty[])System.Enum.GetValues(typeof(NPCProperty));
            if ( alcoholTypes == null ) alcoholTypes = (AlcoholType[])System.Enum.GetValues(typeof(AlcoholType));

            var wealth = wealthTypes[UnityEngine.Random.Range(0, wealthTypes.Length)];

            //This random should be influence by vibe status
            NPCProperty nPCProperty = nPCProperties[UnityEngine.Random.Range(0, nPCProperties.Length)]; //TODO: this should be stack
            List<NPCProperty> properties = new ();
            properties.Add(nPCProperty);

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
    }
}