using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image Icon;
    public GameObject Owner; 
}


public class Radar : MonoBehaviour//,IPositionInteractionable
{
    public Transform CharacterPosition; 
    
    private List<RadarObject> radarObjects = new List<RadarObject>();
    
    private void Update()
    {
        DrawIcon();
    }

    public void RegistIcon(GameObject Character, Image Dot)
    {
        //추후 풀에서 불러오도록 변경
        Image image = Instantiate(Dot);
       
        radarObjects.Add(new RadarObject() { Icon = image, Owner = Character });
        
    }

    public void RemoveIcon(GameObject gameObject)
    {
        //for(int i = 0; i < radarObjects.Count; i++)
        //{
        //    if(radarObjects[i].Owner == gameObject)
        //    {
        //        radarObjects[i].Icon.enabled = false;
        //    }
        //}
    }

    public void DrawIcon()
    {
        foreach(RadarObject radarObject in radarObjects )
        {
            Vector3 radarObjectPosition = radarObject.Owner.transform.position - CharacterPosition.transform.position;
            //float distanceToObject = Vector3.Distance(radarObjectPosition, PlayerPosition);
            
            radarObject.Icon.transform.SetParent(this.transform);
            radarObject.Icon.transform.position = new Vector3(radarObjectPosition.x, radarObjectPosition.z, 0) + this.transform.position;
        }
    }

    public void SendPosition() { }

   
    //public void ReceivePosition(Vector3 position)
    //{
    //    PlayerPosition = position;
    //}
}
