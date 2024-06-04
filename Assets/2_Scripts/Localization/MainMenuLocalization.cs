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
    [SerializeField] private TextMeshProUGUI dailyСonditionText;
    [SerializeField] private TextMeshProUGUI dailyHeaderText;
    [SerializeField] private TextMeshProUGUI rewardText;

    private void Start()
    {
        if(Geekplay.Instance.language == "ru")
        {
            chooseModeTextView.text = "Выбрать режим";
            simpleShopTextView.text = "Магазин";
            inAppShopTextView.text = "Премиум магазин";
            dailyExerciseTextView.text = "Ежедневные задание";
            lidersTextView.text = "Лидеры";
            telergamBonusTextView.text = "Бонусы";
            ourGameTextView.text = "Наши игры";

            chooseModeOpenMainMenuTextView.text = "Меню";


            telegramTextView.text = "Наш канал";
            claimTextView.text = "Забрать";
            telegramOpenMainMenuTextView.text = "Меню";
            promocodeSuccessTextView.text = "Промокод успешно использован";
            promocodeAlreadyUseTextView.text = "Промокод уже использован";
            promocodeUndefinedTextView.text = "Такого промокода нет";
            bonusDescriptionTextView.text = "Подпишись на наш канал и введи промокод который найдёшь там";

            ruleTextView.text = "Только для авторизованных пользователей";
            ourGameOpenMainMenuTextView.text = "Меню";
            geomertyDashNameTextView.text = "Geometry Dash 3D";
            dressGameNameTextView.text = "Создай Королеву - Салон Уэнсдей";
            slapBattlesTextView.text = "Битва Пощечин | Робби Обби";
            twoPlayerTextView.text = "Игры На Двоих: Дуэль";

            survivalTextView.text = "Пережитые игры";
            savedTextView.text = "Спасенные";
            donatTextView.text = "Донаты";
            lidersOpenMainMenuTextView.text = "Меню";


            simpleShopOpenMainMenuTextView.text = "Меню";
            lightChapterTextView.text = "Свет";
            itemTextView.text = "Предметы";
            boosterTextView.text = "Бустеры";
            accessoryTextView.text = "Аксесуары";
            buyTextView.text = "Купить";
            closeTextView.text = "Закрыто";


            inAppShopOpenMainMenuTextView.text = "Меню";
            headerNameTextView.text = "Премиум Магазин";


            dailyСonditionText.text = "Выполните все условия чтобы получить награду";
            dailyHeaderText.text = "Ежедневные задания";
            rewardText.text = "Награда:";
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

            dailyСonditionText.text = "Meet all conditions to receive the reward";
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

            dailyСonditionText.text = "Odulu almak icin tum kosullari karsilayin";
            dailyHeaderText.text = "Gunluk Gorevler";
            rewardText.text = "Odul:";
        }
    }
}