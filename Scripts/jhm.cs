using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem.Helpers;
using Kingmaker.Controllers.Units;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Properties.BaseGetter;
using Kingmaker.Mechanics.Entities;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.View;
using Kingmaker.View.Mechanics.Entities;
using Kingmaker.Visual.Critters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[TypeId("8f5f1c0e21a08e24db71bc053ae54aa9")]
public class CheckAbilityWeaponIsNeedle : PropertyGetter, PropertyContextAccessor.IOptionalAbilityWeapon, PropertyContextAccessor.IOptional, PropertyContextAccessor.IBase
{
    protected override int GetBaseValue()
    {
        if (this.GetAbilityWeapon() == null)
        {
            return 0;
        }

        var weapon = this.GetAbilityWeapon().Blueprint.name;

        if (weapon.IndexOf("needle", StringComparison.OrdinalIgnoreCase) == -1)
        {
            return 0;
        }
        return 1;
    }

    protected override string GetInnerCaption(bool useLineBreaks)
    {
        return "jhm Check if Needle Weapon";
    }
}

[Serializable]
[TypeId("2ebf85d8c73645194a2e4fe6bb27a3bb")]
public class MoveServoSkullToAbilityTarget : ContextAction
{
    protected override void RunAction()
    {
        var log = PFLog.Mods;
        try
        {
            var target = base.Target.Entity;
            var caster = base.Context.MaybeCaster;

            UnitPartFollowedByUnits required = caster.GetRequired<UnitPartFollowedByUnits>();
            UnitPartFamiliarLeader leader = base.Context.MaybeCaster.GetOrCreate<UnitPartFamiliarLeader>();
            var servo = leader.FirstFamiliar;

            var basePosition = ObstacleAnalyzer.FindClosestPointToStandOn(target.Position, 0.3f);
            Vector3 servoPosition = new Vector3(basePosition.x + 0.5f, basePosition.y + 0.5f, basePosition.z);

            FollowersFormationController.CreateFollowerAction(servo, required, servoPosition, FollowerActionType.Move);
        }
        catch (Exception e) { log.Log("jhm exception: " + e,ToString()); }
        
    }

    public override string GetCaption()
    {
        return $"jhm Move Servoskull to target";
    }
}

[Serializable]
[TypeId("531885d8c73645194a8765e6bb27a3bb")]
public class CheckCasterIsTarget : PropertyGetter, PropertyContextAccessor.IOptionalAbilityWeapon, PropertyContextAccessor.IOptional, PropertyContextAccessor.IBase
{
    protected override int GetBaseValue()
    {
        var log = PFLog.Mods;
        var target = this.PropertyContext.ContextMainTarget;
        var caster = this.PropertyContext.ContextCaster;
        log.Log("jhm checkcaster " + target + " " + caster);
        if (target == caster)
        {
            return 1;
        }

        return 0;
    }

    protected override string GetInnerCaption(bool useLineBreaks)
    {
        return $"jhm is caster target";
    }
}
class ClassesWithGuid
{
    public static List<(Type, string)> Classes = new List<(Type, string)>()
    {
        (typeof(CheckAbilityWeaponIsNeedle), "8f5f1c0e21a08e24db71bc053ae54aa9"),
        (typeof(MoveServoSkullToAbilityTarget), "2ebf85d8c73645194a2e4fe6bb27a3bb"),
        (typeof(CheckCasterIsTarget), "531885d8c73645194a8765e6bb27a3bb")
    };
}
