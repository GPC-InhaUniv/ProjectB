using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image Icon;
    public GameObject Owner; //해당 유닛
}


public class Radar : MonoBehaviour
{
    public Transform PlayerPosition;

    public List<RadarObject> radarObjects = new List<RadarObject>();

    private void Update()
    {
        DrawIcon();
    }

    public void RegistEnemyIcon(GameObject Character, Image enemyDot)
    {
        Image image = Instantiate(enemyDot);
        Debug.Log(image.name);
        radarObjects.Add(new RadarObject() { Icon = image, Owner = Character });
        
    }

    public void DrawIcon()
    {
        foreach(RadarObject radarObject in radarObjects )
        {
            Vector3 radarObjectPosition = radarObject.Owner.transform.position - PlayerPosition.transform.position;

            radarObject.Icon.transform.SetParent(this.transform);
            radarObject.Icon.transform.position = new Vector3(radarObjectPosition.x, radarObjectPosition.z, 0) + this.transform.position;
        }
    }

    
}
