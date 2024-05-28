using GeekplaySchool;
using TMPro;
using UnityEngine;

public class MainMenuLocalization : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private TextMeshProUGUI chooseModeTextView;
    [SerializeField] private TextMeshProUGUI simpleShopTextView;
    [SerializeField] private TextMeshProUGUI inAppShopTextView;
    [SerializeField] private TextMeshProUGUI dailyExerciseTextView;
    [SerializeField] private TextMeshProUGUI lidersTextView;
    [SerializeField] private TextMeshProUGUI telergamBonusTextView;
    [SerializeField] private TextMeshProUGUI ourGameTextView;

    [Header("Choose Mode")]
    [SerializeField] private TextMeshProUGUI chooseModeOpenMainMenuTextView;

    [Header("Telegram")]
    [SerializeField] private TextMeshProUGUI telegramTextView;
    [SerializeField] private TextMeshProUGUI claimTextView;
    [SerializeField] private TextMeshProUGUI promocodeSuccessTextView;
    [SerializeField] private TextMeshProUGUI promocodeAlreadyUseTextView;
    [SerializeField] private TextMeshProUGUI promocodeUndefinedTextView;
    [SerializeField] private TextMeshProUGUI telegramOpenMainMenuTextView;

    [Header("Our Game")]
    [SerializeField] private TextMeshProUGUI ourGameOpenMainMenuTextView;
    [SerializeField] private TextMeshProUGUI geomertyDashNameTextView;
    [SerializeField] private TextMeshProUGUI dressGameNameTextView;
    [SerializeField] private TextMeshProUGUI slapBattlesTextView;
    [SerializeField] private TextMeshProUGUI twoPlayerTextView;

    [Header("Leaders")]
    [SerializeField] private TextMeshProUGUI survivalTextView;
    [SerializeField] private TextMeshProUGUI savedTextView;
    [SerializeField] private TextMeshProUGUI donatTextView;
    [SerializeField] private TextMeshProUGUI lidersOpenMainMenuTextView;

    [Header("Simple Shop")]
    [SerializeField] private TextMeshProUGUI simpleShopOpenMainMenuTextView;
    [SerializeField] private TextMeshProUGUI lightChapterTextView;
    [SerializeField] private TextMeshProUGUI itemTextView;
    [SerializeField] private TextMeshProUGUI boosterTextView;
    [SerializeField] private TextMeshProUGUI accessoryTextView;
    [SerializeField] private TextMeshProUGUI buyTextView;
    [SerializeField] private TextMeshProUGUI closeTextView;

    [Header("InApp Shop")]
    [SerializeField] private TextMeshProUGUI inAppShopOpenMainMenuTextView;


    private void Start()
    {
        if(Geekplay.Instance.language == "ru")
        {
            chooseModeTextView.text = "������� �����";
            simpleShopTextView.text = "�������";
            inAppShopTextView.text = "������� �������";
            dailyExerciseTextView.text = "���������� �������";
            lidersTextView.text = "������";
            telergamBonusTextView.text = "������";
            ourGameTextView.text = "���� ����";

            chooseModeOpenMainMenuTextView.text = "������� ����";


            telegramTextView.text = "��� �����";
            claimTextView.text = "�������";
            telegramOpenMainMenuTextView.text = "������� ����";
            promocodeSuccessTextView.text = "�������� ������� �����������";
            promocodeAlreadyUseTextView.text = "�������� ��� �����������";
            promocodeUndefinedTextView.text = "������ ��������� ���";


            ourGameOpenMainMenuTextView.text = "������� ����";
            geomertyDashNameTextView.text = "��������� ���";
            dressGameNameTextView.text = "��������";
            slapBattlesTextView.text = "����� �������";
            twoPlayerTextView.text = "��� ������";

            survivalTextView.text = "��������� ����";
            savedTextView.text = "���������";
            donatTextView.text = "������";
            lidersOpenMainMenuTextView.text = "������� ����";


            simpleShopOpenMainMenuTextView.text = "������� ����";
            lightChapterTextView.text = "����";
            itemTextView.text = "��������";
            boosterTextView.text = "�������";
            accessoryTextView.text = "���������";
            buyTextView.text = "������";
            closeTextView.text = "�������";


            inAppShopOpenMainMenuTextView.text = "������� ����";
        }
        else if(Geekplay.Instance.language == "en")
        {
            chooseModeTextView.text = "Select mode";
            simpleShopTextView.text = "Store";
            inAppShopTextView.text = "Premium Store";
            dailyExerciseTextView.text = "Daily Task";
            lidersTextView.text = "Leaders";
            telergamBonusTextView.text = "Bonuses";
            ourGameTextView.text = "Our game";

            chooseModeOpenMainMenuTextView.text = "Main Menu";


            telegramTextView.text = "Our channel";
            claimTextView.text = "Pick up";
            telegramOpenMainMenuTextView.text = "Main Menu";
            promocodeSuccessTextView.text = "Promo code successfully used";
            promocodeAlreadyUseTextView.text = "Promo code has already been used";
            promocodeUndefinedTextView.text = "There is no such promotional code";

            ourGameOpenMainMenuTextView.text = "Main Menu";
            geomertyDashNameTextView.text = "Geomerty Dash";
            dressGameNameTextView.text = "Dress Up";
            slapBattlesTextView.text = "Slap Battle";
            twoPlayerTextView.text = "Two Players";

            survivalTextView.text = "Survived Games";
            savedTextView.text = "Saved";
            donatTextView.text = "Donations";
            lidersOpenMainMenuTextView.text = "Main Menu";

            simpleShopOpenMainMenuTextView.text = "Main Menu";
            lightChapterTextView.text = "Light";
            itemTextView.text = "Items";
            boosterTextView.text = "Boosters";
            accessoryTextView.text = "Accessories";
            buyTextView.text = "Buy";
            closeTextView.text = "Close";


            inAppShopOpenMainMenuTextView.text = "Main Menu";
        }
        else if(Geekplay.Instance.language == "tr")
        {
            chooseModeTextView.text = "Mod secin";
            simpleShopTextView.text = "Magaza";
            inAppShopTextView.text = "Premium Magaza";
            dailyExerciseTextView.text = "Gunluk Gorev";
            lidersTextView.text = "Liderler";
            telergamBonusTextView.text = "Bonuslar";
            ourGameTextView.text = "Oyunlarimiz";

            chooseModeOpenMainMenuTextView.text = "Ana Menu";


            telegramTextView.text = "Kanalimiz";
            claimTextView.text = "Al";
            telegramOpenMainMenuTextView.text = "Ana Menu";
            promocodeSuccessTextView.text = "Promosyon kodu basariyla kullanildi";
            promocodeAlreadyUseTextView.text = "Promosyon kodu zaten kullanildi";
            promocodeUndefinedTextView.text = "Boyle bir promosyon kodu yoktur";

            ourGameOpenMainMenuTextView.text = "Ana Menu";
            geomertyDashNameTextView.text = "Geomerty Dash";
            dressGameNameTextView.text = "Giydirme";
            slapBattlesTextView.text = "Tokat Savasi";
            twoPlayerTextView.text = "Iki Oyuncu";

            survivalTextView.text = "Hayatta Kalan Oyunlar";
            savedTextView.text = "Kaydedildi";
            donatTextView.text = "Bagislar";
            lidersOpenMainMenuTextView.text = "Ana Menu";

            simpleShopOpenMainMenuTextView.text = "Ana Menu";
            lightChapterTextView.text = "Isik";
            itemTextView.text = "Ogeler";
            boosterTextView.text = "Arttiricilar";
            accessoryTextView.text = "Aksesuarlar";
            buyTextView.text = "Satin Al";
            closeTextView.text = "Kapat";


            inAppShopOpenMainMenuTextView.text = "Ana Menu";
        }
    }
}