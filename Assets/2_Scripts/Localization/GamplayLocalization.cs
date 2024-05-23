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
    [SerializeField] private TextMeshProUGUI LookPanelPreviosTextView;
    [SerializeField] private TextMeshProUGUI LookPanelNextTextView;

    [Header("Choose Map")]
    [SerializeField] private TextMeshProUGUI chooseMapVoiseTextView;

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
                switchCameraTextView.text = "Переключить камеру(TAB)";
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

            LookPanelPreviosTextView.text = "Предыдущий";
            LookPanelNextTextView.text = "Следующий";


            chooseMapVoiseTextView.text = "Проголосуйте за карту";

        }
        else if (Geekplay.Instance.language == "en")
        {
            if (Geekplay.Instance.mobile)
            {
                switchCameraTextView.text = "Switch camera";
            }
            else
            {
                switchCameraTextView.text = "Switch camera(TAB)";
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

            LookPanelPreviosTextView.text = "Previous";
            LookPanelNextTextView.text = "Next";


            chooseMapVoiseTextView.text = "Vote for a map";
        }
        else if (Geekplay.Instance.language == "tr")
        {
            if (Geekplay.Instance.mobile)
            {
                switchCameraTextView.text = "Kamera kamerasini oynat";
            }
            else 
            { 
                switchCameraTextView.text = "Kamerayi degistir(TAB)";
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

            LookPanelPreviosTextView.text = "Onceki";
            LookPanelNextTextView.text = "Sonraki";


            chooseMapVoiseTextView.text = "Haritaya oy verin";
        }
    }
}
