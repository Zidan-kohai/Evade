using GeekplaySchool;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour {

    [SerializeField] private PointerIcon _pointerPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _pointerParent;

    private Dictionary<IEnemy, PointerIcon> _dictionary = new Dictionary<IEnemy, PointerIcon>();

    public static PointerManager Instance;

    public bool isActive = false;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            if (Geekplay.Instance.PlayerData.CurrentEquipedItemID == 1)
            {
                isActive = true;
            }
        } else {
            Destroy(this);
        }
    }

    public void AddToList(IEnemy enemyPointer) {
        if (!isActive) return;


        if (_dictionary.Count > 0 && !_dictionary.ContainsKey(enemyPointer))
        {
            List<PointerIcon> icons = new List<PointerIcon>();

            foreach (var item in _dictionary)
            {
                icons.Add(item.Value);
            }

            while(icons.Count > 0)
            {
                PointerIcon icon = icons[0];
                icons.RemoveAt(0);
                Destroy(icon.gameObject);
            }

            _dictionary.Clear();
        }
        else if(_dictionary.ContainsKey(enemyPointer))
        {
            return;
        }

        PointerIcon newPointer = Instantiate(_pointerPrefab, _pointerParent);
        _dictionary.Add(enemyPointer, newPointer);
    }

    public void RemoveFromList(IEnemy enemyPointer) {
        Destroy(_dictionary[enemyPointer].gameObject);
        _dictionary.Remove(enemyPointer);
    }

    void LateUpdate()
    {
        if (!isActive) return;

        foreach (KeyValuePair<IEnemy, PointerIcon> kvp in _dictionary)
        {
            IEnemy enemyPointer = kvp.Key;
            PointerIcon pointerIcon = kvp.Value;

            Vector3 position = _camera.WorldToScreenPoint(enemyPointer.GetTransform().position);
            position.x = Mathf.Clamp(position.x, 50, Screen.width - 50);
            position.y = Mathf.Clamp(position.y, 50, Screen.height - 50);

            pointerIcon.Show();

            pointerIcon.SetIconPosition(position);
        }

    }
}
