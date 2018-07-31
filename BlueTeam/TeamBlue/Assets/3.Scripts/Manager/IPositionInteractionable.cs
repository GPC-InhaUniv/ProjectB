using UnityEngine;

interface IPositionInteractionable {

    void SendPosition(Vector3 position);
    void ReceivePosition(Vector3 position);

}
