using MGCNTN;
using MGCNTN.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : ICommand
{
    private Actor user;
    private List<Actor> targets;
    private Skill skill;
    private bool success = true;

    public UseSkill(Actor user, List<Actor> targets, Skill skill)
    {
        this.targets = targets;
        this.skill = skill;
        this.user = user;
    }

    public bool isFinished { get; private set; } = false;
    public IEnumerator Co_Execute()
    {
        foreach (Actor target in targets)
        {
            try
            {
                skill.finished += finish;
                skill.Spawn(user.transform, target.transform.position);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                success = false;
                finish();
            }
        }
        yield return null;
    }

    private void finish()
    {
        if (success)
        {
            foreach (Actor target in targets)
                BattleCalculations.UseSkill(user, target, skill.Data);
        }
        isFinished = true;
    }
}
