using TMPro;
using UnityEngine;
public class VillagerInterection : MonoBehaviour
{
    [SerializeField] DoorRay doorRay;
    [SerializeField] TextMeshProUGUI interectionMessage;
    [SerializeField] Villager myEnum;
    void Start()
    {
        doorRay = GetComponent<DoorRay>();
    }
    bool nowInterect = false;
    private void Update()
    {
        Vector3 rayStartPosition = transform.position + doorRay.rayOffset;
        Collider[] hit = Physics.OverlapBox(rayStartPosition, doorRay.laySize * 0.5f, transform.rotation, doorRay.mask);

        if (hit.Length > 0)
        {
            nowInterect = true;
            interectionMessage.enabled = true;
            VillagerManager.Instance.player = hit[0].gameObject.GetComponent<Player>();
            interectionMessage.text = VillagerManager.Instance.VillagerTxt(myEnum);

            VillagerManager.Instance.playerState = hit[0].gameObject.GetComponent<Player>().GetPlayerStateMachine();
        }
        else if (nowInterect)
        {
            interectionMessage.text = string.Empty;
            nowInterect = false;
        }
    }

    public void OpenStoreUI()
    {
        VillagerManager.Instance.playerState.Player.VirtualCamera.enabled = false;
        VillagerManager.Instance.playerState.IsInteracted = true;
        VillagerManager.Instance.VillagerOption(myEnum);
    }

    public void CloseStoreUI()
    {
        VillagerManager.Instance.playerState.Player.VirtualCamera.enabled = true;
        VillagerManager.Instance.playerState.IsInteracted = false;
        interectionMessage.enabled = false;
        nowInterect = false;
        VillagerManager.Instance.playerState = null;
        VillagerManager.Instance.CloseShop();
    }
}
