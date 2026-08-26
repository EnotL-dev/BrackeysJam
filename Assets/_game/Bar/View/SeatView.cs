using Assets._game.Bar.Controller;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class SeatView : MonoBehaviour {

    SeatService seatService;

    

    [Inject]
    void Construct( SeatService seatService ) {
        this.seatService = seatService;
    }

    void Start() {
        seatService.InitializeListSeat(GetChildrenWithTag());
    }

    public List<Seat> GetChildrenWithTag() {
        List<Seat> matchedObjects = new List<Seat>();
        Transform[] allChildren = this.gameObject.GetComponentsInChildren<Transform>(includeInactive: true);

        for ( int i = 0; i < allChildren.Length; i++ ) {
            if ( allChildren[i].CompareTag("Seat") ) {
                matchedObjects.Add(allChildren[i].gameObject.GetComponent<Seat>());
            }
        }

        Debug.Log($"find {matchedObjects.Count} seat");

        return matchedObjects;
    }


}
