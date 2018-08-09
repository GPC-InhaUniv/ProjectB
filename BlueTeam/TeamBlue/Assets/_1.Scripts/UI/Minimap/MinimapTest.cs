using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTest : MonoBehaviour {

    public GameObject Player;
    public List<GameObject> Enemy;
    // Use this for initialization

    //포지션 저장할 위치
    Vector2 playerPos;
    Vector2 enemyPos;

    public List<RectTransform> Enemypoint;

    public float horizontalAngle = 0;

    //  RectTransform enemypoints;


    private void Start()
    {

        // enemypoints = Enemypoint.GetComponent<RectTransform>();
        //  Enemypoint[0].localPosition = new Vector3(0, 0, 0);
        StartCoroutine(StartSona());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Enemy.Contains(other.gameObject))
        {
            Enemy.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Enemy.Contains(other.gameObject))
        {

            Enemy.Remove(other.gameObject);

        }
    }

    IEnumerator StartSona()
    {
        ShotSona();

        yield return new WaitForSeconds(1f);
        StartCoroutine(StartSona());
    }

    void ShotSona()
    {
        /*
        float angle = Vector2.Dot(playerPos, new Vector2(0, 1));
        Vector3 playerFacing3D = new Vector3(playerPos.x, playerPos.y, 0);

        Vector3 crossResult = Vector3.Cross(playerFacing3D, new Vector3(0, 1, 0));
        if (crossResult.z < 0)
            angle *= -1;
        */



        playerPos = new Vector2(Player.transform.position.x, Player.transform.position.z);

        for (int i = 0; i < Enemy.Count; i++)
        {
            enemyPos = new Vector2(Enemy[i].transform.position.x, Enemy[i].transform.position.z);
            Vector2 playerToEnemy = enemyPos - playerPos;
            playerToEnemy = Quaternion.Euler(0, 0, horizontalAngle) * playerToEnemy;
            Enemypoint[i].localPosition = playerToEnemy * 4.5f;
        }

        if (Enemy.Count < Enemypoint.Count)
        {
            for (int i = Enemy.Count; i < Enemypoint.Count; i++)
            {
                Enemypoint[i].localPosition = new Vector3(55f, 0, 0);
            }
        }



    }
}
