using UnityEngine;

public class Seat : MonoBehaviour{
    Transform transform;

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
}
