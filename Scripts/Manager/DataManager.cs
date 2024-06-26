using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public SkillDataSO PlayerSkillData;
    public SkillDataSO EnemySkillData;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DataTransfer(PlayerSO data, HealthSystem health, CharacterStatHandler statHandler) // GameManager Event OnBattleStart
    {
        PlayerSkillData.CurHealth = health.CurHealth; // CurHealth로 변경
        PlayerSkillData.BaseDamage = statHandler.CurrentStat.BaseDamage;
        for (int i = 0; i < data.SkillData.Count; i++)
        {
            if (PlayerSkillData.SkillData.Contains(data.SkillData[i])) continue;
            PlayerSkillData.SkillData.Add(data.SkillData[i]);
        }
    }
    public void DataTransfer(EnemySO data, HealthSystem health, CharacterStatHandler statHandler) // GameManager Event OnBattleStart
    {
        EnemySkillData.CurHealth = health.CurHealth; // CurHealth로 변경
        EnemySkillData.BaseDamage = statHandler.CurrentStat.BaseDamage;
        for (int i = 0; i < data.SkillData.Count; i++)
        {
            if (EnemySkillData.SkillData.Contains(data.SkillData[i])) continue;
            EnemySkillData.SkillData.Add(data.SkillData[i]);
        }
    }

    //public void ResetData() // GamaManager Event OnBattleEnd
    //{
    //    PlayerSkillData.SkillData.Clear();
    //    EnemySkillData.SkillData.Clear();
    //}

    private void OnApplicationQuit()
    {
        PlayerSkillData.CurHealth = 0f;
        EnemySkillData.CurHealth = 0f;
        PlayerSkillData.BaseDamage = 0f;
        EnemySkillData.BaseDamage = 0f;
        PlayerSkillData.SkillData.Clear();
        EnemySkillData.SkillData.Clear();
    }
}
