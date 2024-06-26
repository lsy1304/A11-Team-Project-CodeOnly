using UnityEngine;

public class DungeonBG : MonoBehaviour
{
    [SerializeField] private GameObject dungeonBG;

    public void OnDungeonCanvas()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dungeonBG.SetActive(true);
    }
    public void OffDungeonCanvas()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dungeonBG.SetActive(false);
    }

    public void OnClickDungeon(int num)
    {
        DungeonManager.Instance.randomManager.InstantiateRandomPrefabs(num);
        DungeonManager.Instance.DropPlayer(num);
    }
}