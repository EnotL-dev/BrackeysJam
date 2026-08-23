using UnityEngine;

public enum NPCState {
   
    Spawn,
    MoveToLine,
    Waiting, //wait in line

    WaitingProduct, //wait for food, drink (fodd production slow (upgardeable))
    Eat,

    Fight, 
    Destroy,
    Piss,

    Left,
}
