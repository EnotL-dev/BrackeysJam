using Assets._game.Bar.Controller;
using Assets._game.Npc;
using UnityEngine;
using Zenject;

public class Seat : MonoBehaviour {

    OrderFactory orderFactory;


    private Renderer seatRenderer;

    [SerializeField]
    private Material normalMaterial;

    [SerializeField]
    private Material brokenMaterial;

    public bool IsOccupied { get; private set; } = false;
    public bool IsBroken { get; private set; } = false;


    [Inject]
    void Construct( OrderFactory orderFactory ) {
        this.orderFactory = orderFactory;
    }


    public void Start() {
        seatRenderer = GetComponent<MeshRenderer>();

        SetBrokenVisual(false);
    }

    //public bool TryReserve() {
    //    if ( IsOccupied ) return false;

    //    IsOccupied = true;
    //    return true;
    //}


    public void Break() {
        if ( IsBroken ) return;

        IsBroken = true;

        SetBrokenVisual(IsBroken);

        Debug.Log($"{name} has broken!");
    }

    public void Release() {
        IsOccupied = false;
    }

    public void Repair() {
        if ( !IsBroken )
            return;

        IsBroken = false;

        SetBrokenVisual(false);

        Debug.Log($"{name} has been repaired!");
    }

    private void SetBrokenVisual( bool broken ) {
        if ( seatRenderer == null ) return;

        if ( broken ) {
            seatRenderer.material = brokenMaterial;
        }
        else {
            seatRenderer.material = normalMaterial;
        }
    }



    //TODO: refactor to find the best seat base on distance,
    //and hold a list current seat or npc that already recognise 
    //to prevent trigger when passing by
    private void OnTriggerEnter( Collider other ) {
        //make sit down animation in here
        if ( other.CompareTag("NPC") ) {

            var script = other.GetComponent<NPCScript>();

            //read ncp preference (skip for now)

            //call order
            //var order = orderFactory.CreateRandomOrder();

            //script.PlaceOrder(order);

            script.SitDown();



            //}
        }
    }

    private void OnTriggerExit( Collider other ) {
        if ( other.CompareTag("NPC") ) {
            Release();

        }
    }

    public bool TryReserve() {
        if ( IsOccupied || IsBroken )
            return false;

        IsOccupied = true;
        return true;
    }
}
