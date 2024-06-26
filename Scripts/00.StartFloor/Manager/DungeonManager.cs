using System.Collections;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [SerializeField] DungeonBG dungeon;
    [SerializeField] FadeEffect fadeEffect;
    [HideInInspector] public RandomManager randomManager;
    [HideInInspector] public NavMeshBakerBuild navMeshBakerBuild;
    [SerializeField] Transform dungeonEntrancePosition;
    

    [SerializeField] Transform[] dungeonDropPosition;
    public Transform player;
    protected override void Awake()
    {
        base.Awake();
        randomManager = GetComponent<RandomManager>();
        navMeshBakerBuild = GetComponent<NavMeshBakerBuild>();
    }
    public void OnDungeonCanvas()
    {
        dungeon.OnDungeonCanvas();
    }
    public void OffDungeonCanvas()
    {
        dungeon.OffDungeonCanvas();
    }
    public void DropPlayer(int num)
    {
        OffDungeonCanvas();
        StartCoroutine(Fade(num));
    }

    private IEnumerator Fade(int num)
    {
        Player nowPlayer = player.GetComponent<Player>();
        nowPlayer.enabled = false;
        yield return fadeEffect.UseFadeEffect(FadeState.FadeOut);
        player.position = dungeonDropPosition[num].position;
        yield return fadeEffect.UseFadeEffect(FadeState.FadeIn);
        nowPlayer.enabled = true;
        fadeEffect.OffFadeObject();
    }

    public IEnumerator ReturnStartPalce(Player player)
    {
        yield return new WaitForSeconds(1f);
        player.enabled = false;
        yield return fadeEffect.UseFadeEffect(FadeState.FadeOut);
        player.transform.position = dungeonEntrancePosition.position;
        yield return fadeEffect.UseFadeEffect(FadeState.FadeIn);
        player.enabled = true;
        fadeEffect.OffFadeObject();
    }
}
