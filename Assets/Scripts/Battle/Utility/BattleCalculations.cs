namespace MGCNTN.Battle
{
    /// <summary>
    /// Where math and logic is done for combat 
    /// </summary>
    /// Called by Commands
    public static class BattleCalculations
    {

        public static int Attack(Actor attacker, Actor defender)
        {

            Stats dStats = defender.data.Stats;
            int damage = attacker.data.Stats.ATK - dStats.DEF;

            if (damage < 1)
                damage = 1;


            dStats.HP -= damage;

            checkDeath(defender, false);

            defender.updateHealth?.Invoke(dStats.HP, dStats.MAXHP);

            return damage;
        }
        public static void useItem(Actor defender, IConsumable data)
        {
            Stats dStats = defender.data.Stats;
            data.Consume(dStats);

            checkDeath(defender, false);

            defender.updateHealth?.Invoke(dStats.HP, dStats.MAXHP);
            defender.updateMP?.Invoke(dStats.MP, dStats.MAXMP);
        }


        public static void checkDeath(Actor actor, bool instant)
        {
            if (actor.data.Stats.HP <= 0)
            {
                actor.data.Stats.HP = 0;

                if (instant)
                    actor.IsDead = true;

                actor.Die();
            }
        }
    }
}