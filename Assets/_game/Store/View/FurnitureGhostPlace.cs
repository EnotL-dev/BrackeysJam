using Assets._game.Player.View;
using Assets._game.Sound.EnumInterface;
using Assets._game.Store.Model;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Store.View
{
    public class FurnitureGhostPlace : MonoBehaviour
    {
        FurnitureManagerView furnitureManagerView;
        ISFXService sFXService;

        [Inject]
        void Construct(FurnitureManagerView furnitureManagerView,
            ISFXService sFXService) {
            this.furnitureManagerView = furnitureManagerView;
            this.sFXService = sFXService;
        }



        private void OnTriggerEnter(Collider col)
        {
            if (col != null)
            {
                IFurniture furniture = col.gameObject.GetComponent<IFurniture>();
                if(furniture != null)
                {
                    var type  =furniture.ThisFurnitureSO().GetFurnitureType();

                    if(type == FurnitureType.chair ) {
                        sFXService.Play(SFXType.PlaceChair);
                    }
                    else if(type == FurnitureType.plant ) {
                        sFXService.Play(SFXType.PlacePlant);
                    }

                    furnitureManagerView.SetAtPlace(gameObject, type);
                    Destroy(col.transform.parent.gameObject);
                }
            }
        }
    }
}