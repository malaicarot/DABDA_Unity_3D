using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;
using System.Linq;


/*Tạo lớp trừu tượng chung cho các chức năng của itemitem*/
public abstract class ItemAbility
{
    public abstract string itemName { get; }
    public abstract void Proccess();
}

/*Tạo lớp "con" kế thừa "ItemAbility"*/
public class ChrysanthemumAbility : ItemAbility
{
    public override string itemName => "chrysanthemum"; // Ghi đè PT itemName
    public override void Proccess() // Ghi đè PT Proccess
    {
        Debug.Log("Flower is use!");
    }
}

/*Tạo lớp Factory để quản lý và tạo các đối tượng kế thừa "ItemAbility"*/
public class AbilityFactory
{
    Dictionary<string, Type> abilitiesByName; // Lưu các tên và loại của các chức năngnăng

    public AbilityFactory() // PT tìm các lớp con hiện tại, khởi tạo và lưu vào Dictionary
    {
        var abilitiesType = Assembly.GetAssembly(typeof(ItemAbility)).GetTypes(). // Tìm các assembly hiện tại của "ItemAbility"
        Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ItemAbility))); // Nếu như nó là lớp và không phải lớp trừu tượng và lớp con của "ItemAbility"

        abilitiesByName = new Dictionary<string, Type>();

        foreach (var type in abilitiesType) //Duyệt qua abilitiesType
        {
            var tempType = Activator.CreateInstance(type) as ItemAbility; // Khởi tạo tạm thời một biến như "ItemAbility"
            abilitiesByName.Add(tempType.itemName, type); // Thêm vào Dictionary
        }
    }

    public ItemAbility GetItemAbility(string abilityType) //PT trả về loại chức năng (năng lực)
    {
        if (abilitiesByName.ContainsKey(abilityType))
        {
            Type type = abilitiesByName[abilityType];
            var ability = Activator.CreateInstance(type) as ItemAbility;
            return ability;
        }
        return null;
    }

    internal IEnumerable<string> GetItemAbilityName() //PT trả về danh sách tên chức năng của các itemitem
    {
        return abilitiesByName.Keys;
    }
}


