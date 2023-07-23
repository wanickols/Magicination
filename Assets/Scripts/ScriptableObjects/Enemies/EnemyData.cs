using Battle;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "New Enemy Data")]
public class EnemyData : BattleData
{
    [SerializeField] private EnemyStats stats;
    public EnemyStats Stats => stats;
}
