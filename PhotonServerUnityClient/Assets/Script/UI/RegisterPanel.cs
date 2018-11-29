using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RegisterPanel : MonoBehaviour
{
    public GameObject loginPanel;
    public InputField usernameIF;
    public InputField passwordIF;
    public Text hintMessage;

    private RegisterRequest registerRequest;

    private void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();
    }

    public void OnRegisterButton()
    {
        hintMessage.text = "";
        registerRequest.Username = usernameIF.text;
        registerRequest.Password = passwordIF.text;
        registerRequest.DefaultRequest();
    }
    public void OnBackButton()
    {
        gameObject.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {
        switch (returnCode)
        {
            case ReturnCode.Success:
                hintMessage.text = "注册成功";
                break;
            case ReturnCode.Failed:
                hintMessage.text = "注册失败，账号重复";
                break;
            default:
                break;
        }
    }
}
