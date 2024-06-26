using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] DoorRay doorRay;
    public Animator animator;
    PlayerStateMachine player;

    private void Start()
    {
        doorRay = GetComponent<DoorRay>();
    }

    bool isInterect = false;
    private void Update()
    {
        Vector3 rayStartPosition = transform.position + doorRay.rayOffset;
        Collider[] hit = Physics.OverlapBox(rayStartPosition, doorRay.laySize * 0.5f, transform.rotation, doorRay.mask);

        if (hit.Length > 0)
        {
            isInterect = true;
            animator.SetBool("isIn", true);
            if (DungeonManager.Instance.player == null)
            {
                DungeonManager.Instance.player = hit[0].transform;
                player = hit[0].gameObject.GetComponent<Player>().GetPlayerStateMachine();
            }
            player.Player.VirtualCamera.enabled = false;
        }
        else if (isInterect)
        {
            animator.SetBool("isIn", false);
            DungeonManager.Instance.OffDungeonCanvas();
            //DungeonManager.Instance.player = null;
            isInterect = false;
            player.Player.VirtualCamera.enabled = true;
        }
    }
}
