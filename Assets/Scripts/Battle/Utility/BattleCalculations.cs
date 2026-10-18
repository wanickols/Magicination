namespace MGCNTN.Battle
{
    /// <summary>
    /// Where math and logic is done for combat 
    /// </summary>
    /// Called by Commands
    public static class BattleCalculations
    {

        public static void Attack(Actor attacker, Actor defender, int power = 0)
        {
            int damage = attacker.Stats.ATK - defender.Stats.DEF + power;

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

            if(skillData.isHeal)
                defender.takeDamage(-skillData.Power);
            else
                Attack(attacker, defender, skillData.Power);
    
            Use(attacker, defender, skillData);
        }

        private static int handleElementalDamage() { return 0; }

    }
}