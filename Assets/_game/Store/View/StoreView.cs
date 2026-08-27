using Assets._game.Bar.Controller;
using Assets._game.Interaction.View;
using Assets._game.Store.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

namespace Assets._game.Store.View
{
    public class StoreView : MonoBehaviour
    {
        [Inject] DiContainer container;
        [Inject] IEconomyService economyService;

        [SerializeField] private Transform playerTransform;
        [Space(5)]
        [SerializeField] private List<FurnitureSpawnProreties> spawnProreties;
        private GameObject[] spawnedFurniture;

        private void Start()
        {
            spawnedFurniture = new GameObject[spawnProreties.Count];

            SpawnNewObjects();
        }

        private void LateUpdate()
        {
            if(Vector3.Distance(transform.position, playerTransform.position) > 6f)
            {
                SpawnNewObjects();
            }
        }

        private void SpawnNewObjects()
        {
            for(int i = 0; i< spawnProreties.Count; i++)
            {
                if (spawnedFurniture[i] == null)
                {
                    GameObject newObject = container.InstantiatePrefab(spawnProreties[i].spawnPrefab);
                    newObject.transform.position = spawnProreties[i].spawnPoint.position;
                    newObject.transform.rotation = spawnProreties[i].spawnPoint.rotation;
                    spawnedFurniture[i] = newObject;
                }    
            }
        }

        public void BuyFurnitureFromStore(GameObject furnitureItem)
        {
            int index = spawnedFurniture.ToList().FindIndex(obj => 
                obj.transform.GetComponentInChildren<InteractableItemView>().gameObject == furnitureItem);

            spawnedFurniture[index] = null;

            furnitureItem.GetComponent<Rigidbody>().isKinematic = false;
            furnitureItem.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

            int cost = furnitureItem.GetComponent<InteractableItemView>().ThisFurnitureSO().Cost();

            economyService.BuyFurniture(cost);
        }

        public bool CanBuy(int cost)
        {
            return economyService.Money >= cost;
        }
    }
}
