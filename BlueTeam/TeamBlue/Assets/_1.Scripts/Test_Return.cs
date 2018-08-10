
using UnityEngine.UI;
using UnityEngine;
using ProjectB.GameManager;

public class Test_Return : MonoBehaviour {

    [SerializeField]
    GameObject panel;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickWorldMapBtn()
    {
        if (!panel.activeInHierarchy)
            panel.SetActive(true);
        else panel.SetActive(false);
       
    }

    public void OnClickDungeonBtn(int index)
    {
        Debug.Log("던전 입장");
        GameControllManager.Instance.MoveNextScene(LoadType.BrickDungeon, index);
    }
}
