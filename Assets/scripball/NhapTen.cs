using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NhapTen : MonoBehaviour
{
    public Button xacNhanButton;
    public TMP_InputField inputField;

    public void NhanXacNhan()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            gameObject.SetActive(false);
            PlayfabManager.Instance.nameUser = inputField.text;
            PlayfabManager.Instance.SubmitUserNameName(inputField.text);
        }
    }
}
