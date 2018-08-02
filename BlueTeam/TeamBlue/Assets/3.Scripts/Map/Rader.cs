using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaderObject
{
    public Image Icon;// { get; set; }
    public GameObject Owner;// { get; set; }
}

public class Rader : MonoBehaviour
{
    public Transform playerPos;
    float mapScale = 10.0f;

    public List<RaderObject> raderObjects = new List<RaderObject>();

    public void RegisterRaderObject(GameObject gameObject, Image i)
    {
        Image image = Instantiate(i);
        raderObjects.Add(new RaderObject() { Owner = gameObject, Icon = image });
    }

    public void RemoveRaderObject(GameObject o)
    {
        List<RaderObject> newList = new List<RaderObject>();
        for (int i = 0; i < raderObjects.Count; i++)
        {
            if (raderObjects[i].Owner == o) 
            {
                Destroy(raderObjects[i].Icon);
                continue;
            }
            else
                newList.Add(raderObjects[i]);
        }
        raderObjects.RemoveRange(0, raderObjects.Count);
        raderObjects.AddRange(newList);
    }

    void DrawRaderDots()
    {
        foreach(RaderObject raderObject in raderObjects)
        {
            Vector3 raderPos = (raderObject.Owner.transform.position - playerPos.position);
            float distToObject = Vector3.Distance(playerPos.position, raderObject.Owner.transform.position) * mapScale;
            float deltay = Mathf.Atan2(raderPos.x, raderPos.z) * Mathf.Rad2Deg -270 - playerPos.eulerAngles.y;
            raderPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1 ;
            raderPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

            raderObject.Icon.transform.SetParent(this.transform);
            raderObject.Icon.transform.position = new Vector3(raderPos.x, raderPos.z, 0) + this.transform.position;
        }
    }

    private void Update()
    {
        DrawRaderDots();
    }
}




