using MGCNTN;
using MGCNTN.Battle;
using UnityEngine;
///TODO Fix namespace

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "New Enemy Data")]
public class EnemyData : MemberBattleInfo
{

    public int BattleWeight => stats.BattleWeight;
    public EnemyRarity enemyRarity => stats.enemyRarity;

    [SerializeField] private EnemyStats stats;
    public override Stats baseStats => stats;
}
