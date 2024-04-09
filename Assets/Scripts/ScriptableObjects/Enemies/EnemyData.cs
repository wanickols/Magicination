using MGCNTN.Battle;
using UnityEngine;
///TODO Fix namespace

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "New Enemy Data")]
public class EnemyData : MemberBattleInfo
{
    [SerializeField] private EnemyStats stats;
    public EnemyStats Stats => stats;
}
