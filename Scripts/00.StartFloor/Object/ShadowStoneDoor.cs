using UnityEngine;
public class ShadowStoneDoor : MonoBehaviour
{
    [SerializeField] DoorRay doorRay;
    public Animator animator;

    private void Start()
    {
        doorRay = GetComponent<DoorRay>();
    }

    private void Update()
    {
        Vector3 rayStartPosition = transform.position + doorRay.rayOffset;
        Collider[] hit = Physics.OverlapBox(rayStartPosition, doorRay.laySize * 0.5f, transform.rotation, doorRay.mask);

        if (hit.Length > 0)
        {
            animator.SetBool("isOpen", true);
        }
    }
}
