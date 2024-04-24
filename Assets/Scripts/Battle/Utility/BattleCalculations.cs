namespace MGCNTN.Battle
{
    /// <summary>
    /// Where math and logic is done for combat 
    /// </summary>
    /// Called by Commands
    public static class BattleCalculations
    {

        public static void Attack(Actor attacker, Actor defender)
        {
            int damage = attacker.Stats.ATK - defender.Stats.DEF;

            if (damage < 1)
                damage = 1;


            defender.takeDamage(damage);
        }
        public static void useItem(Actor attacker, Actor defender, ObjectData data)
        {
            Stats dStats = defender.Stats;


            data.use(defender.memberBattleInfo, attacker.Stats);

            defender.checkDeath(false);

            defender.updateHealth?.Invoke(dStats.HP, dStats.MAXHP);
            defender.updateMP?.Invoke(dStats.ENG, dStats.MAXENG);
        }


    }
}