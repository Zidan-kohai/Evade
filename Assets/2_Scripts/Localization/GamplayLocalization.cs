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
     

    private void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            if (Geekplay.Instance.mobile)
            {
                switchCameraTextView.text = "Переключить камеру";
            }
            else
            {
                switchCameraTextView.text = "Переключить камеру(Q)";
            }

            carryTextView.text = "Нести";
            putTextView.text = "Отпустить";
            raisingTextView.text = "Поднять";
            exerciseTextView.text = "Ваша задача выжить в течении 3 минуты";

            LoseMenuHeaderTextView.text = "Вы мертвы";
            LoseMenuMainMenuTextView.text = "Главное меню";
            LoseMenuLookTextView.text = "Наблюдать";
            LoseMenuYoutTimeTextView.text = "Ваше время";
            LoseMenuYoutEarnTextView.text = "Вы заработали";


            EndPanelWindowNameTextView.text = "Имя";
            EndPanelWindowHelpTextView.text = "Помог";
            EndPanelWindowTimeTextView.text = "Время";
            EndPanelWindowMoneyTextView.text = "Деньги";
            EndPanelWindowExperienceTextView.text = "Опыт";

            ExitTextView.text = "Выйти";

            chooseMapVoiseTextView.text = "Проголосуйте за карту";


            mobileCarryTextView.text = "Нести";
            mobilePutTextView.text = "Отпустить";
            mobileRaisingTextView.text = "Поднять";
            mobileMainMenuTextView.text = "Выйти (Таб)";

        }
        else if (Geekplay.Instance.language == "en")
        {
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
            exerciseTextView.text = "Your task is to survive for 3 minutes";

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
            mobileMainMenuTextView.text = "Exit (Tab)";
        }
        else if (Geekplay.Instance.language == "tr")
        {
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
            exerciseTextView.text = "3 dakika boyunca silinecek";

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
            mobileMainMenuTextView.text = "Cikmak (Tab)";
        }
    }
}
