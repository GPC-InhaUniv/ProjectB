using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{

    //const int numberOfIcons = 30;
    const float minimapScale = 5.0f;

    [SerializeField]
    GameObject player;
    
    [SerializeField]
    List<GameObject> enemys;

    [SerializeField]
    List<RectTransform> enemyIconPositions;

    Vector2 playerPosition;
    Vector2 enemyPosition;

    //[SerializeField]
    //Image enemyIcon; //적 아이콘이 들어갈 자리
    //List<Image> enemyIcons = new List<Image>();


    //[SerializeField]
    //Image Icon;
    

    

    //[SerializeField]
    //RectTransform parentIconPosition;
    
    void Start()
    {
       // CreatIcons();
    }

    void Update()
    {
        DrawIcons();
    }

    void OnTriggerEnter(Collider other)
    { 
        //Radar collider에 적이 들어오면 적 리스트에 들어온 적을 추가한다.
        if(!enemys.Contains(other.gameObject)) 
        {
            enemys.Add(other.gameObject); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Radar collider에서 사라지면 적 리스트에 들어온 적을 제거한다.
        if(enemys.Contains(other.gameObject))
        {
            enemys.Remove(other.gameObject);
        }
    }

    //void CreatIcons()
    //{

    //    for(int i = 0; i<numberOfIcons; i++)
    //    {
    //        enemyIcons.Add(enemyIcon);
    //        enemyIcons[i].enabled = false;//비활성화
    //        enemyIcons[i].transform.position = parentIconPosition.transform.position;//저장할 좌표를 구해주기

    //    }
    //    Debug.Log(enemyIcons.Count);
    //}
    private IEnumerator StartDrawIcon()
    {
        DrawIcons();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(StartDrawIcon());
    }

    void DrawIcons()
    {
        playerPosition = new Vector2(player.transform.position.x, player.transform.position.z);

        for(int i = 0; i<enemys.Count; i++)
        {
            enemyPosition = new Vector2(enemys[i].transform.position.x, enemys[i].transform.position.z);
            Vector2 playerToEnemy = enemyPosition - playerPosition;
            enemyIconPositions[i].localPosition = playerToEnemy * minimapScale;
        }

        if(enemys.Count<enemyIconPositions.Count)
        {
            for (int i = enemys.Count; i < enemyIconPositions.Count; i++) 
            {
                enemyIconPositions[i].localPosition = new Vector3(100f, 0, 0);
                
            }
        }
    }
}
