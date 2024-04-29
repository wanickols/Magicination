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

        public static void Use(Actor attacker, Actor defender, ObjectData data)
        {


            data.use(defender.memberBattleInfo, attacker.Stats);

            defender.statusApplied();
            defender.UpdateHealth();
            defender.UpdateEnergy();
        }

        public static void UseSkill(Actor attacker, Actor defender, SkillData skillData)
        {

            //Check Damage
            if (skillData.Power > 0)
                Attack(attacker, defender);
            else if (skillData.Power < 0)
                defender.takeDamage(skillData.Power);

            Use(attacker, defender, skillData);
        }

        private static int handleElementalDamage() { return 0; }

    }
}