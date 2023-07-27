namespace Battle
{
    /// <summary>
    /// Where math and logic is done for combat 
    /// </summary>
    public static class BattleCalculations
    {

        public static int Attack(Actor attacker, Actor defender)
        {

            Stats dStats = defender.Stats;
            int damage = attacker.Stats.ATK - dStats.DEF;

            if (damage < 0)
                damage = 0;

            dStats.HP -= damage;

            defender.updateHealth?.Invoke(dStats.HP, dStats.MAXHP);

            return damage;
        }
    }
}