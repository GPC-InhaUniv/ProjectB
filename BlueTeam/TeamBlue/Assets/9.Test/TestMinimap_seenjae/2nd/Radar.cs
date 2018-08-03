using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image Icon;
    public GameObject Owner; 
}


public class Radar : MonoBehaviour ,IPositionInteractionable
{
    public Vector3 PlayerPosition;
    
    public List<RadarObject> radarObjects = new List<RadarObject>();

    private void Start()
    {
        
    }

    private void Update()
    {
        DrawIcon();
    }

    public void RegistEnemyIcon(GameObject Character, Image enemyDot)
    {
        Image image = Instantiate(enemyDot);
       
        radarObjects.Add(new RadarObject() { Icon = image, Owner = Character });
        
    }

    public void DrawIcon()
    {
        foreach(RadarObject radarObject in radarObjects )
        {
            Vector3 radarObjectPosition = radarObject.Owner.transform.position - PlayerPosition;
            float distanceToObject = Vector3.Distance(radarObjectPosition, PlayerPosition);
            
            radarObject.Icon.transform.SetParent(this.transform);
            radarObject.Icon.transform.position = new Vector3(radarObjectPosition.x, radarObjectPosition.z, 0) + this.transform.position;
        }
    }

    public void SendPosition()
    { }

    public void ReceivePosition(Vector3 position)
    {
        PlayerPosition = position;
    }
}
