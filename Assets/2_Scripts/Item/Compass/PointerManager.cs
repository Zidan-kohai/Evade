using GeekplaySchool;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour {

    [SerializeField] private PointerIcon _enemyPointerPrefab;
    [SerializeField] private PointerIcon _playerFallPointerPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _pointerParent;

    private Dictionary<IEnemy, PointerIcon> _dictionaryEnemy = new Dictionary<IEnemy, PointerIcon>();
    private Dictionary<IPlayer, PointerIcon> _dictionaryFallAIPlayer = new Dictionary<IPlayer, PointerIcon>();

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


        if (_dictionaryEnemy.Count > 0 && !_dictionaryEnemy.ContainsKey(enemyPointer))
        {
            List<PointerIcon> icons = new List<PointerIcon>();

            foreach (var item in _dictionaryEnemy)
            {
                icons.Add(item.Value);
            }

            while(icons.Count > 0)
            {
                PointerIcon icon = icons[0];
                icons.RemoveAt(0);
                Destroy(icon.gameObject);
            }

            _dictionaryEnemy.Clear();
        }
        else if(_dictionaryEnemy.ContainsKey(enemyPointer))
        {
            return;
        }

        PointerIcon newPointer = Instantiate(_enemyPointerPrefab, _pointerParent);
        _dictionaryEnemy.Add(enemyPointer, newPointer);
    }
    public void AddToList(IPlayer playerPointer)
    {
        if (_dictionaryFallAIPlayer.Count > 0 && !_dictionaryFallAIPlayer.ContainsKey(playerPointer))
        {
            List<PointerIcon> icons = new List<PointerIcon>();

            foreach (var item in _dictionaryFallAIPlayer)
            {
                icons.Add(item.Value);
            }

            while (icons.Count > 0)
            {
                PointerIcon icon = icons[0];
                icons.RemoveAt(0);
                Destroy(icon.gameObject);
            }

            _dictionaryFallAIPlayer.Clear();
        }
        else if (_dictionaryFallAIPlayer.ContainsKey(playerPointer))
        {
            return;
        }

        PointerIcon newPointer = Instantiate(_playerFallPointerPrefab, _pointerParent);
        _dictionaryFallAIPlayer.Add(playerPointer, newPointer);
    }

    public void RemoveFromList(IEnemy enemyPointer) {
        Destroy(_dictionaryEnemy[enemyPointer].gameObject);
        _dictionaryEnemy.Remove(enemyPointer);
    }

    public void RemoveFromList(IPlayer playerPointer)
    {
        if (!_dictionaryFallAIPlayer.ContainsKey(playerPointer)) return;

        Destroy(_dictionaryFallAIPlayer[playerPointer].gameObject);
        _dictionaryFallAIPlayer.Remove(playerPointer);
    }

    void LateUpdate()
    {
        foreach (KeyValuePair<IPlayer, PointerIcon> kvp in _dictionaryFallAIPlayer)
        {
            IPlayer enemyPointer = kvp.Key;
            PointerIcon pointerIcon = kvp.Value;

            Vector3 position = _camera.WorldToScreenPoint(enemyPointer.GetTransform().position);
            position.x = Mathf.Clamp(position.x, 50, Screen.width - 50);
            position.y = Mathf.Clamp(position.y, 50, Screen.height - 50);

            pointerIcon.Show();

            pointerIcon.SetIconPosition(position);
        }

        if (!isActive) return;

        foreach (KeyValuePair<IEnemy, PointerIcon> kvp in _dictionaryEnemy)
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
