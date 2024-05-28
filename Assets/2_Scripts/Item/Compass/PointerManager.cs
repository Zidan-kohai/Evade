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

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        foreach (var kvp in _dictionary)
        {

            IEnemy enemyPointer = kvp.Key;
            PointerIcon pointerIcon = kvp.Value;

            Vector3 toEnemy = enemyPointer.GetTransform().position - _playerTransform.position;
            Ray ray = new Ray(_playerTransform.position, toEnemy);
            Debug.DrawRay(_playerTransform.position, toEnemy);


            float rayMinDistance = Mathf.Infinity;
            int index = 0;

            for (int p = 0; p < 4; p++)
            {
                if (planes[p].Raycast(ray, out float distance))
                {
                    if (distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                        index = p;
                    }
                }
            }

            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
            Vector3 worldPosition = ray.GetPoint(rayMinDistance);
            Vector3 position = _camera.WorldToScreenPoint(worldPosition);
            Quaternion rotation = GetIconRotation(index);

            pointerIcon.Show();

            pointerIcon.SetIconPosition(position, rotation);
        }

    }
    private Quaternion GetIconRotation(int planeIndex) {
        if (planeIndex == 0) {
            return Quaternion.Euler(0f, 0f, 90f);
        } else if (planeIndex == 1) {
            return Quaternion.Euler(0f, 0f, -90f);
        } else if (planeIndex == 2) {
            return Quaternion.Euler(0f, 0f, 180);
        } else if (planeIndex == 3) {
            return Quaternion.Euler(0f, 0f, 0f);
        }
        return Quaternion.identity;
    }

}
