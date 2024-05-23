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

    private void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            if (Geekplay.Instance.mobile)
            {
                switchCameraTextView.text = "����������� ������";
            }
            else
            {
                switchCameraTextView.text = "����������� ������(TAB)";
            }

            carryTextView.text = "�����";
            putTextView.text = "���������";
            raisingTextView.text = "�������";
            exerciseTextView.text = "���� ������ ������ � ������� 3 ������";
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
        }
    }
}
