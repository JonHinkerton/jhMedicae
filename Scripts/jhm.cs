using Kingmaker.Blueprints.JsonSystem.Helpers;
using Kingmaker.EntitySystem.Properties.BaseGetter;
using System;
using System.Collections;
using System.Collections.Generic;
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
        return "Ability Weapon is Needle";
    }
}
class ClassesWithGuid
{
    public static List<(Type, string)> Classes = new List<(Type, string)>()
    {
        (typeof(CheckAbilityWeaponIsNeedle), "8f5f1c0e21a08e24db71bc053ae54aa9")
    };
}
