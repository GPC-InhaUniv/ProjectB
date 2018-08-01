using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minimap
{
    public class RaderObject
    {
        public Image Icon { get; set; }
        public GameObject Owner { get; set; }
    }

    public class Rader : MonoBehaviour
    {
        public Transform playerPos;
        float mapScale = 2.0f;

        public static List<RaderObject> raderObjects = new List<RaderObject>();

        private void Update()
        {
            DrawRaderDots();
        }
        public static void RegisterRaderobject(GameObject o, Image image)
        {
            Image raderImage = Instantiate(image);
            raderObjects.Add(new RaderObject() { Owner = o, Icon = raderImage });
        }

        public static void RemoveRaderObject(GameObject OwnerObject)
        {
            List<RaderObject> newList = new List<RaderObject>();
            for (int i = 0; i < raderObjects.Count; i++) 
            {
                if(raderObjects[i].Owner == false)
                {
                    Destroy(raderObjects[i].Icon);
                    continue;
                }
                else
                {
                    newList.Add(raderObjects[i]);
                }
                raderObjects.RemoveRange(0, raderObjects.Count);
                raderObjects.AddRange(newList);
            }
        }

        void DrawRaderDots()
        {
            foreach(RaderObject raderObject in raderObjects)
            {
                Vector3 raderPos = (raderObject.Owner.transform.position - playerPos.position);
                float distanceToObject = Vector3.Distance(playerPos.position, raderObject.Owner.transform.position) * mapScale;
                float deltay = Mathf.Atan2(raderPos.x, raderPos.z) * Mathf.Deg2Rad-270-playerPos.eulerAngles.y;
                raderPos.x = distanceToObject * Mathf.Cos(deltay * Mathf.Deg2Rad);
                raderPos.y = distanceToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

                raderObject.Icon.transform.SetParent(this.transform);
                raderObject.Owner.transform.position = new Vector3(raderPos.x, raderPos.z, 0) + this.transform.position;
            }
        }
    }


}
