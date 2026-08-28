using Assets._game.Player.View;
using Assets._game.Store.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Assets._game.Store.View
{
    public class FurnitureManagerView : MonoBehaviour
    {
        [SerializeField] private List<FurniturePlaces> furniturePlaces;
        [Inject] PlayerInteractionView playerInteractionView;

        public void ShowFreePlaces(FurnitureType furnitureType)
        {
            foreach (FurniturePlaces place in furniturePlaces)
            {
                if (place.furnitureType == furnitureType)
                {
                    if (!place.Busy)
                        place.placeGhost.gameObject.SetActive(true);
                }
            }
        }

        public void HidePlaces(FurnitureType furnitureType)
        {
            foreach(FurniturePlaces place in furniturePlaces)
            {
                if (place.furnitureType == furnitureType)
                {
                    place.placeGhost.gameObject.SetActive(false);
                }
            }
        }

        public void SetAtPlace(GameObject placeObject, FurnitureType furnitureType)
        {
            foreach (FurniturePlaces place in furniturePlaces)
            {
                if(place.placeGhost.gameObject == placeObject)
                {
                    place.Busy = true;
                    place.realObject.SetActive(true);

                    break;
                }
            }

            playerInteractionView.ForcedInteractionRelease();
            HidePlaces(furnitureType);
        }

        // Seven, Use REAL object not GHOST -> if someone gonna broke it just use this method for HIDE the real object
        public void RemovePlace(GameObject placeObject) 
        {
            foreach (FurniturePlaces place in furniturePlaces)
            {
                if(place.realObject == placeObject)
                {
                    place.realObject.SetActive(false);
                    place.Busy = false;
                }
            }
        }
    }
}