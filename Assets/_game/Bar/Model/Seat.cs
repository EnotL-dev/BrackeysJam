using Assets._game.Bar.Controller;
using Assets._game.Npc;
using UnityEngine;
using Zenject;

public class Seat : MonoBehaviour{
    Transform transform;

    BarService barService;

    [Inject]
    void Construct(BarService barService ) {
        this.barService = barService;
    }

    public bool IsOccupied { get; private set; }

    public void Start() {
        transform = GetComponent<Transform>();
    }

    public bool TryReserve() {
        if ( IsOccupied ) return false;

        IsOccupied = true;
        return true;
    }

    public void Release() {
        IsOccupied = false;
    }


    //TODO: refactor to find the best seat base on distance,
    //and hold a list current seat or npc that already recognise 
    //to prevent trigger when passing by
    private void OnTriggerEnter( Collider other ) {
        if ( other.CompareTag("NPC") ) {

            var script = other.GetComponent<NPCScript>();

            

            script.PlaceOrder();



        }
    }
}
