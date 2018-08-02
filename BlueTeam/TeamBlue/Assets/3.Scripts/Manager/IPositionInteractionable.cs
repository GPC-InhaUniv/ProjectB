using UnityEngine;

interface IPositionInteractionable {

    void SendPosition();
    void ReceivePosition(Vector3 position);

}
