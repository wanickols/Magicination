namespace Battle
{
    /// <summary>
    /// Where math and logic is done for combat 
    /// </summary>
    /// Called by Commands
    public static class BattleCalculations
    {

        public static int Attack(Actor attacker, Actor defender)
        {

            Stats dStats = defender.Stats;
            int damage = attacker.Stats.ATK - dStats.DEF;

            if (damage < 1)
                damage = 1;


            dStats.HP -= damage;

            checkDeath(defender);

            defender.updateHealth?.Invoke(dStats.HP, dStats.MAXHP);

            return damage;
        }


        private static void checkDeath(Actor actor)
        {
            if (actor.Stats.HP <= 0)
            {
                actor.Stats.HP = 0;
                actor.Die();
            }
        }
    }
}