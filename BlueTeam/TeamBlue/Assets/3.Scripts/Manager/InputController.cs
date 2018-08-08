using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour {

    [SerializeField]
    AccountInfo userAccountInfo;

    [SerializeField]
    InputField idInputField;

    [SerializeField]
    InputField pwInputField;


    void Start()
    {
        //개발용으로 로그인 버튼 누를시 자동 로그인 됩니다.
        userAccountInfo.Id = "12341234";
        userAccountInfo.Password = "123123";
    }

    public void UpdateUserInfo()
    {
        userAccountInfo.Id = idInputField.text;
        userAccountInfo.Password = pwInputField.text;
    }

    public void OnClickLoginButton()
    {
        if(userAccountInfo.Id!="" && userAccountInfo.Password!= " ")
        {
            AccountInfo.Login(userAccountInfo.Id, userAccountInfo.Password);
            LoadingSceneManager.LoadScene(LoadType.VillageCheckDownLoad, 0);

        }
        else
        {
            Debug.Log("아이디와 비밀번호 입력 바랍니다.");
        }
    }
	
}
