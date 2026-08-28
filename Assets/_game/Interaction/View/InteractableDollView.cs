using Assets._game.Store.Model;
using Assets._game.Store.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class InteractableDollView : MonoBehaviour, IInteractable, IFurniture
    {
        [Inject] FurnitureManagerView furnitureManagerView;

        public bool CanBuy() => true;

        [SerializeField] private FurnitureSO furnitureSO;
        public FurnitureSO ThisFurnitureSO() => furnitureSO;
        public string GetTip() => $"E - grab {furnitureSO.Name()} $";

        public bool FreezePlayer() => false;
        public bool IsDraggableObject() => true;
        public void OnContinuousInteraction()
        {
            //nothing
        }

        public void OnEndInteraction()
        {
            furnitureManagerView.HidePlaces(ThisFurnitureSO().GetFurnitureType());
        }

        public void OnInteract()
        {
            

            furnitureManagerView.ShowFreePlaces(ThisFurnitureSO().GetFurnitureType());
        }

        public void OnStartInteraction()
        {
            //nothing
        }
    }
}