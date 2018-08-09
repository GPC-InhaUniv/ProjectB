using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    //[SerializeField]
    //Image Icon;
    [SerializeField]
    List<GameObject> Enemy;

    [SerializeField]
    List<RectTransform> EnemyIconPosition;

    private void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(!Enemy.Contains(other.gameObject)) 
        {
            Enemy.Add(other.gameObject); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(Enemy.Contains(other.gameObject))
        {
            Enemy.Remove(other.gameObject);
        }
    }

    void CreatIcons()
    {

    }
    void DrawIcon()
    {
        for(int i = 0; i<Enemy.Count; i++)
        {
            
        }
    }
}
