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
    [SerializeField] private TextMeshProUGUI bonusDescriptionTextView;

    [Header("Our Game")]
    [SerializeField] private TextMeshProUGUI ruleTextView;
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
    [SerializeField] private TextMeshProUGUI headerNameTextView;


    [Header("Dayli Quest")]
    [SerializeField] private TextMeshProUGUI daily�onditionText;
    [SerializeField] private TextMeshProUGUI dailyHeaderText;
    [SerializeField] private TextMeshProUGUI rewardText;

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

            chooseModeOpenMainMenuTextView.text = "����";


            telegramTextView.text = "��� �����";
            claimTextView.text = "�������";
            telegramOpenMainMenuTextView.text = "����";
            promocodeSuccessTextView.text = "�������� ������� �����������";
            promocodeAlreadyUseTextView.text = "�������� ��� �����������";
            promocodeUndefinedTextView.text = "������ ��������� ���";
            bonusDescriptionTextView.text = "��������� �� ��� ����� � ����� �������� ������� ������ ���";

            ruleTextView.text = "������ ��� �������������� �������������";
            ourGameOpenMainMenuTextView.text = "����";
            geomertyDashNameTextView.text = "Geometry Dash 3D";
            dressGameNameTextView.text = "������ �������� - ����� �������";
            slapBattlesTextView.text = "����� ������� | ����� ����";
            twoPlayerTextView.text = "���� �� �����: �����";

            survivalTextView.text = "��������� ����";
            savedTextView.text = "���������";
            donatTextView.text = "������";
            lidersOpenMainMenuTextView.text = "����";


            simpleShopOpenMainMenuTextView.text = "����";
            lightChapterTextView.text = "����";
            itemTextView.text = "��������";
            boosterTextView.text = "�������";
            accessoryTextView.text = "���������";
            buyTextView.text = "������";
            closeTextView.text = "�������";


            inAppShopOpenMainMenuTextView.text = "����";
            headerNameTextView.text = "������� �������";


            daily�onditionText.text = "��������� ��� ������� ����� �������� �������";
            dailyHeaderText.text = "���������� �������";
            rewardText.text = "�������:";
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

            chooseModeOpenMainMenuTextView.text = "Menu";


            telegramTextView.text = "Our channel";
            claimTextView.text = "Pick up";
            telegramOpenMainMenuTextView.text = "Menu";
            promocodeSuccessTextView.text = "Promo code successfully used";
            promocodeAlreadyUseTextView.text = "Promo code has already been used";
            promocodeUndefinedTextView.text = "There is no such promotional code";
            bonusDescriptionTextView.text = "Subscribe to our channel and enter the promotional code you find there";

            ruleTextView.text = "Only for authorized users";
            ourGameOpenMainMenuTextView.text = "Menu";
            geomertyDashNameTextView.text = "Geometry Dash 3D";
            dressGameNameTextView.text = "Create a Queen - Wednesday Salon";
            slapBattlesTextView.text = "Slap Battles | Robbi Obbi";
            twoPlayerTextView.text = "Games For Two: Duel";

            survivalTextView.text = "Survived Games";
            savedTextView.text = "Saved";
            donatTextView.text = "Donations";
            lidersOpenMainMenuTextView.text = "Menu";

            simpleShopOpenMainMenuTextView.text = "Menu";
            lightChapterTextView.text = "Light";
            itemTextView.text = "Items";
            boosterTextView.text = "Boosters";
            accessoryTextView.text = "Accessories";
            buyTextView.text = "Buy";
            closeTextView.text = "Close";


            inAppShopOpenMainMenuTextView.text = "Menu";
            headerNameTextView.text = "Premium Shop";

            daily�onditionText.text = "Meet all conditions to receive the reward";
            dailyHeaderText.text = "Daily Tasks";
            rewardText.text = "Reward:";
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

            chooseModeOpenMainMenuTextView.text = "Menu";


            telegramTextView.text = "Kanalimiz";
            claimTextView.text = "Al";
            telegramOpenMainMenuTextView.text = "Menu";
            promocodeSuccessTextView.text = "Promosyon kodu basariyla kullanildi";
            promocodeAlreadyUseTextView.text = "Promosyon kodu zaten kullanildi";
            promocodeUndefinedTextView.text = "Boyle bir promosyon kodu yoktur";
            bonusDescriptionTextView.text = "Kanalimiza abone olun ve orada buldugunuz promosyon kodunu girin";

            ruleTextView.text = "Yalnizca yetkili kullanicilar icin";
            ourGameOpenMainMenuTextView.text = "Menu";
            geomertyDashNameTextView.text = "Geometry Dash 3D";
            dressGameNameTextView.text = "Kralicesini Yaratin - Carsamba Salonu";
            slapBattlesTextView.text = "Savas Tokadi | Robbi Obbi";
            twoPlayerTextView.text = "Iki Kisilik Oyunlar: Duello";

            survivalTextView.text = "Hayatta Kalan Oyunlar";
            savedTextView.text = "Kaydedildi";
            donatTextView.text = "Bagislar";
            lidersOpenMainMenuTextView.text = "Menu";

            simpleShopOpenMainMenuTextView.text = "Menu";
            lightChapterTextView.text = "Isik";
            itemTextView.text = "Ogeler";
            boosterTextView.text = "Arttiricilar";
            accessoryTextView.text = "Aksesuarlar";
            buyTextView.text = "Satin Al";
            closeTextView.text = "Kapat";


            inAppShopOpenMainMenuTextView.text = "Menu";
            headerNameTextView.text = "Premium Magaza";

            daily�onditionText.text = "Odulu almak icin tum kosullari karsilayin";
            dailyHeaderText.text = "Gunluk Gorevler";
            rewardText.text = "Odul:";
        }
    }
}