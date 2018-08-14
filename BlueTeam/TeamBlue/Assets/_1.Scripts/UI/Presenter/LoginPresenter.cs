using UnityEngine;
using UnityEngine.UI;
using ProjectB.GameManager;

namespace ProjectB.UI.Presenter
{
    public class LoginPresenter : MonoBehaviour
    {


        [SerializeField]
        InputField idInputField;

        [SerializeField]
        InputField pwInputField;

        [SerializeField]
        GameObject loginPanel;

        [SerializeField]
        GameObject registerPanel;

        [SerializeField]
        InputField idRegisterInputField;

        [SerializeField]
        InputField pwRegisterInputField;


        void Start()
        {
            //개발용으로 로그인 버튼 누를시 자동 로그인 됩니다.
            AccountInfo.Instance.Id = "123123";
            AccountInfo.Instance.Password = "123123";
        }

        public void UpdateUserInfo()
        {
            AccountInfo.Instance.Id = idInputField.text;
            AccountInfo.Instance.Password = pwInputField.text;
        }

        public void UpdateRegisterInfo()
        {
            AccountInfo.Instance.Id = idRegisterInputField.text;
            AccountInfo.Instance.Password = pwRegisterInputField.text;
        }

        public void OnClickLoginButton()
        {
            if (AccountInfo.Instance.Id != "" && AccountInfo.Instance.Password != "")
            {
                AccountInfo.Login(AccountInfo.Instance.Id, AccountInfo.Instance.Password);
                GameControllManager.Instance.MoveNextScene(LoadType.VillageCheckDownLoad, 0);

            }
            else
            {
                Debug.Log("아이디와 비밀번호 입력 바랍니다.");
            }
        }

        public void OnClickRegisterButton()
        {
            loginPanel.SetActive(false);
            registerPanel.SetActive(true);
        }

        public void OnClickRegisterButtonAtRegister()
        {
            AccountInfo.Register(AccountInfo.Instance.Id, AccountInfo.Instance.Password);
        }

        public void OnClickExitButton()
        {
            Application.Quit();
        }

        public void OnClickExitButtonAtRegister()
        {
            loginPanel.SetActive(true);
            registerPanel.SetActive(false);
        }

    }
}