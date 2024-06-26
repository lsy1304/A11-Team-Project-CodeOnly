using UnityEngine;

public interface IInteractable
{
    void OpenUI(PlayerStateMachine stateMachine);
    void CloseUI();
}
public class Store : MonoBehaviour, IInteractable
{
    public GameObject StoreUI;

    PlayerStateMachine stateMachine;

    public void OpenUI(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        stateMachine.Player.VirtualCamera.enabled = false;
        stateMachine.IsInteracted = true;
        Cursor.lockState = CursorLockMode.None;
        StoreUI.SetActive(true);
    }

    public void CloseUI()
    {
        stateMachine.IsInteracted = false;
        stateMachine.Player.VirtualCamera.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        StoreUI.SetActive(false);
    }
}
