using GeekplaySchool;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class Codes
{
    public string code;
    public int codeNumber;
    public UnityEvent rewardEvent;
}

public class Promocode : MonoBehaviour
{
    [SerializeField] private InputField inputText; //���� ������ ��������
    [SerializeField] private Codes[] codes; //������ ����� � ������ (��� ������� � ������)
    [SerializeField] private GameObject promocodeDontExistTextVeiwHandler;
    [SerializeField] private GameObject promocodeUsedTextVeiwHandler;
    [SerializeField] private GameObject promocodeUsedNowTextVeiwHandler;

    //������� ��� ������ "�����"

    public void Start()
    {
        inputText.onValueChanged.AddListener(OnInputValueChange);
    }

    private void OnInputValueChange(string str)
    {
        promocodeUsedTextVeiwHandler.SetActive(false);
        promocodeUsedNowTextVeiwHandler.SetActive(false);
        promocodeDontExistTextVeiwHandler.SetActive(false);
    }

    public void ClaimBtn()
    {
        for (int i = 0; i < codes.Length; i++)
        {
            if (inputText.text == codes[i].code && !Geekplay.Instance.PlayerData.Codes.Contains(codes[i].codeNumber))
            {
                codes[i].rewardEvent.Invoke();
                Geekplay.Instance.PlayerData.Codes.Add(codes[i].codeNumber);
                promocodeUsedNowTextVeiwHandler.SetActive(true);
                promocodeUsedTextVeiwHandler.SetActive(false);
                promocodeDontExistTextVeiwHandler.SetActive(false);

                DailyExerciseController.Instance.SetProgress(Days.Day2, 4);

                Geekplay.Instance.Save();
                return;
            }
            else if(inputText.text == codes[i].code && Geekplay.Instance.PlayerData.Codes.Contains(codes[i].codeNumber))
            {
                promocodeUsedTextVeiwHandler.SetActive(true);
                promocodeUsedNowTextVeiwHandler.SetActive(false);
                promocodeDontExistTextVeiwHandler.SetActive(false);
                return;
            }
        }

        promocodeDontExistTextVeiwHandler.SetActive(true);
        promocodeUsedTextVeiwHandler.SetActive(false);
        promocodeUsedNowTextVeiwHandler.SetActive(false);
    }

    public void OpenTelegram()
    {
        Application.OpenURL("https://t.me/+uQFcFVwGmwM3ZDNi");
    }

    //�������, ������� ����� ��������� � ��������

    public void Promocode1()
    {
    }

    public void Promocode2()
    {
    }

    public void Promocode3()
    {
    }
}
