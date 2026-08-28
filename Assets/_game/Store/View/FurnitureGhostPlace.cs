using Assets._game.Player.View;
using Assets._game.Store.Model;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets._game.Store.View
{
    public class FurnitureGhostPlace : MonoBehaviour
    {
        [Inject] FurnitureManagerView furnitureManagerView;

        private void OnTriggerEnter(Collider col)
        {
            if (col != null)
            {
                IFurniture furniture = col.gameObject.GetComponent<IFurniture>();
                if(furniture != null)
                {
                    furnitureManagerView.SetAtPlace(gameObject, furniture.ThisFurnitureSO().GetFurnitureType());
                    Destroy(col.transform.parent.gameObject);
                }
            }
        }
    }
}