using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    private List<Collider> alreadyCollider = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollider.Clear();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (alreadyCollider.Contains(other)) return;

        alreadyCollider.Add(other);

        if (other.TryGetComponent(out Enemy enemy)) // first attack
        {
            GetComponentInParent<Player>().SendData();
            enemy.SendData();
            BattleManager.Instance.StartBattle(GetComponentInParent<Player>(), enemy,false);
        }
        else if (other.TryGetComponent(out Player player)) // last attack
        {
            player.SendData();
            GetComponentInParent<Enemy>().SendData();
            BattleManager.Instance.StartBattle(player, GetComponentInParent<Enemy>(), true);
        }
    }
}
