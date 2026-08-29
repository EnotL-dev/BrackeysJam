using Assets._game.Store.Model;
using Assets._game.Store.View;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Interaction.View
{
    public class InteractableItemView : MonoBehaviour, IInteractable, IFurniture
    {
        [Inject] FurnitureManagerView furnitureManagerView;
        [Inject] StoreView storeView;
        private bool _wasRemoved = false;
        public bool WasRemoved { get => _wasRemoved; }

        public bool CanBuy() => storeView.CanBuy(ThisFurnitureSO().Cost());

        [SerializeField] private FurnitureSO furnitureSO;
        public FurnitureSO ThisFurnitureSO() => furnitureSO;
        public string GetTip()
        {
            return !WasRemoved ? $"[E] - buy {furnitureSO.Cost()} $" : $"[E] - {furnitureSO.Name()}";
        }
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
            if (!_wasRemoved)
            {
                storeView.BuyFurnitureFromStore(gameObject);
                _wasRemoved = true;
            }

            furnitureManagerView.ShowFreePlaces(ThisFurnitureSO().GetFurnitureType());
        }

        public void OnStartInteraction()
        {
            //nothing
        }
    }
}