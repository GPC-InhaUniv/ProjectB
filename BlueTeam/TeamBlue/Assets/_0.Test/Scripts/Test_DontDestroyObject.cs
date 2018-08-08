using UnityEngine;

public class Test_DontDestroyObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
