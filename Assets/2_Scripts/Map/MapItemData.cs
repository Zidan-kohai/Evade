using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Map/Item Data")]
public class MapItemData : ScriptableObject
{
    public string Name;
    public string Hardnest;
    public string Description;
    public int SceneIndex;
}
