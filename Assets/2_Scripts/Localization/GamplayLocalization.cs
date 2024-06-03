using GeekplaySchool;
using TMPro;
using UnityEngine;

public class GamplayLocalization : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI switchCameraTextView;
    [SerializeField] private TextMeshProUGUI carryTextView;
    [SerializeField] private TextMeshProUGUI putTextView;
    [SerializeField] private TextMeshProUGUI raisingTextView;
    [SerializeField] private TextMeshProUGUI exerciseTextView;
    [SerializeField] private TextMeshProUGUI remainingTextView;
    [SerializeField] private TextMeshProUGUI mainMenuButtonTextView;


    [Header("Lose Panel")]
    [SerializeField] private TextMeshProUGUI LoseMenuHeaderTextView;
    [SerializeField] private TextMeshProUGUI LoseMenuMainMenuTextView;
    [SerializeField] private TextMeshProUGUI LoseMenuLookTextView;
    [SerializeField] private TextMeshProUGUI LoseMenuYoutTimeTextView;
    [SerializeField] private TextMeshProUGUI LoseMenuYoutEarnTextView;

    [Header("End Panel")]
    [SerializeField] private TextMeshProUGUI EndPanelWindowNameTextView;
    [SerializeField] private TextMeshProUGUI EndPanelWindowHelpTextView;
    [SerializeField] private TextMeshProUGUI EndPanelWindowTimeTextView;
    [SerializeField] private TextMeshProUGUI EndPanelWindowMoneyTextView;
    [SerializeField] private TextMeshProUGUI EndPanelWindowExperienceTextView;

    [Header("Look Panel")]
    [SerializeField] private TextMeshProUGUI ExitTextView;

    [Header("Choose Map")]
    [SerializeField] private TextMeshProUGUI chooseMapVoiseTextView;

    [Header("Mobile Panel")]
    [SerializeField] private TextMeshProUGUI mobileCarryTextView;
    [SerializeField] private TextMeshProUGUI mobilePutTextView;
    [SerializeField] private TextMeshProUGUI mobileRaisingTextView;
    [SerializeField] private TextMeshProUGUI mobileMainMenuTextView;

    [SerializeField] private TextMeshProUGUI booster1TextView;
    [SerializeField] private TextMeshProUGUI booster2TextView;
    [SerializeField] private TextMeshProUGUI booster3TextView;
    [SerializeField] private TextMeshProUGUI booster4TextView;
    [SerializeField] private TextMeshProUGUI booster5TextView;
    [SerializeField] private TextMeshProUGUI booster6TextView;
     

    private void Start()
    {
        mainMenuButtonTextView.text = "����-Tab";

        if (Geekplay.Instance.mobile)
        {
            booster1TextView.gameObject.SetActive(false);
            booster2TextView.gameObject.SetActive(false);
            booster3TextView.gameObject.SetActive(false);
            booster4TextView.gameObject.SetActive(false);
            booster5TextView.gameObject.SetActive(false);
            booster6TextView.gameObject.SetActive(false);
        }

        if (Geekplay.Instance.language == "ru")
        {
            booster6TextView.text = "�";

            if (Geekplay.Instance.mobile)
            {
                switchCameraTextView.text = "����������� ������";
            }
            else
            {
                switchCameraTextView.text = "����������� ������(Q)";
            }

            carryTextView.text = "�����";
            putTextView.text = "���������";
            raisingTextView.text = "�������";
            exerciseTextView.text = "���� ������ ������ � ������� 1 ������ � 30 ������";
            remainingTextView.text = "���������� �����";


            LoseMenuHeaderTextView.text = "�� ������";
            LoseMenuMainMenuTextView.text = "������� ����";
            LoseMenuLookTextView.text = "���������";
            LoseMenuYoutTimeTextView.text = "���� �����";
            LoseMenuYoutEarnTextView.text = "�� ����������";


            EndPanelWindowNameTextView.text = "���";
            EndPanelWindowHelpTextView.text = "�����";
            EndPanelWindowTimeTextView.text = "�����";
            EndPanelWindowMoneyTextView.text = "������";
            EndPanelWindowExperienceTextView.text = "����";

            ExitTextView.text = "�����";

            chooseMapVoiseTextView.text = "������������ �� �����";


            mobileCarryTextView.text = "�����";
            mobilePutTextView.text = "���������";
            mobileRaisingTextView.text = "�������";
            mobileMainMenuTextView.text = "�����";

        }
        else if (Geekplay.Instance.language == "en")
        {
            mainMenuButtonTextView.text = "Menu-Tab";
            booster6TextView.text = "G";

            if (Geekplay.Instance.mobile)
            {
                switchCameraTextView.text = "Switch camera";
            }
            else
            {
                switchCameraTextView.text = "Switch camera(Q)";
            }

            carryTextView.text = "Carry";
            putTextView.text = "Release";
            raisingTextView.text = "Raise";
            exerciseTextView.text = "Your task is to survive for 1 minute and 30 seconds";
            remainingTextView.text = "Remaining time";

            LoseMenuHeaderTextView.text = "You are dead";
            LoseMenuMainMenuTextView.text = "Main Menu";
            LoseMenuLookTextView.text = "Watch";
            LoseMenuYoutTimeTextView.text = "Your time";
            LoseMenuYoutEarnTextView.text = "You earned";


            EndPanelWindowNameTextView.text = "Name";
            EndPanelWindowHelpTextView.text = "Helped";
            EndPanelWindowTimeTextView.text = "Time";
            EndPanelWindowMoneyTextView.text = "Money";
            EndPanelWindowExperienceTextView.text = "Experience";

            ExitTextView.text = "Exit";

            chooseMapVoiseTextView.text = "Vote for a map";

            mobileCarryTextView.text = "Carry";
            mobilePutTextView.text = "Release";
            mobileRaisingTextView.text = "Raise";
            mobileMainMenuTextView.text = "Exit";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            mainMenuButtonTextView.text = "Menu-Tab";
            booster6TextView.text = "G";

            if (Geekplay.Instance.mobile)
            {
                switchCameraTextView.text = "Kamera kamerasini oynat";
            }
            else 
            { 
                switchCameraTextView.text = "Kamerayi degistir(Q)";
            }

            carryTextView.text = "Oyle";
            putTextView.text = "Iptal";
            raisingTextView.text = "Artirmak";
            exerciseTextView.text = "Goreviniz 1 dakika 30 saniye boyunca hayatta kalmak";
            remainingTextView.text = "Kalan sure";

            LoseMenuHeaderTextView.text = "Oldunuz";
            LoseMenuMainMenuTextView.text = "Ana Menu";
            LoseMenuLookTextView.text = "Izle";
            LoseMenuYoutTimeTextView.text = "Zamaniniz";
            LoseMenuYoutEarnTextView.text = "Kazandiniz";


            EndPanelWindowNameTextView.text = "Ad";
            EndPanelWindowHelpTextView.text = "Yardim edildi";
            EndPanelWindowTimeTextView.text = "Zaman";
            EndPanelWindowMoneyTextView.text = "Para";
            EndPanelWindowExperienceTextView.text = "Deneyim";

            ExitTextView.text = "Cikmak";

            chooseMapVoiseTextView.text = "Haritaya oy verin";

            mobileCarryTextView.text = "Oyle";
            mobilePutTextView.text = "Iptal";
            mobileRaisingTextView.text = "Artirmak";
            mobileMainMenuTextView.text = "Cikmak";
        }
    }
}
