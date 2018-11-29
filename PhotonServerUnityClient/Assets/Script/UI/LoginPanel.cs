using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public GameObject registerPanel;
    public InputField usernameIF;
    public InputField passwordIF;
    public Text hintMessage;
    private LoginRequest loginRequest;

    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
    }

    public void OnLoginButton()
    {
        hintMessage.text = "";
        loginRequest.Username = usernameIF.text;
        loginRequest.Password = passwordIF.text;
        loginRequest.DefaultRequest();//登录请求
    }
    public void OnRegisterButton()
    {
        this.gameObject.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        switch (returnCode)
        {
            case ReturnCode.Success:
                //跳转下一场景
                SceneManager.LoadScene("Game");
                break;
            case ReturnCode.Failed:
                //提示错误
                hintMessage.text = "用户名或密码错误";
                break;
            default:
                break;
        }
    }
}
