using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;
using System.Linq;
using UnityEditor.Timeline.Actions;
using System.Collections;

/*Tạo lớp trừu tượng chung cho các chức năng của itemitem*/
public abstract class ItemAbility
{
    public abstract string itemName { get; }
    public abstract string description { get; }
    public abstract void Proccess();
    public abstract bool isSupport { get; }
}

/*Tạo lớp "con" kế thừa "ItemAbility"*/
public class ChrysanthemumAbility : ItemAbility
{
    public override string itemName => "Chrysanthemum"; // Ghi đè PT itemName
    public override string description => "This is a flower!";

    public override bool isSupport => false;



    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject waterGem = GameObject.Find("WaterGem");
        GameObject flower = GameObject.Find("Chrysanthemum");
        if (waterGem != null && flower != null)
        {
            MeshRenderer[] meshRenderer = flower.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer pental in meshRenderer)
            {
                if (pental.name == "pental")
                {
                    pental.material.color = Color.white;
                }
            }
        }
    }
}

public class WaterGemAbility : ItemAbility
{
    public override string itemName => "WaterGem"; // Ghi đè PT itemName
    public override string description => "This is a water gem!";

    public override bool isSupport => true;
    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject mirror = GameObject.Find("Mirror");
        Camera camera1 = mirror.GetComponentInChildren<Camera>();
        GameObject scene = GameObject.Find("Scene");

        LoadScene loadScene = scene.GetComponent<LoadScene>();

        if (camera1.enabled == true)
        {
            loadScene.loadScene();

        }
    }
}

public class LampAbility : ItemAbility
{
    public override string itemName => "Lamp"; // Ghi đè PT itemName
    public override string description => "This is a lamp!";

    public override bool isSupport => true;

    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject lamp = GameObject.Find("Lamp");
        Light light = lamp.GetComponentInChildren<Light>();
        MeshRenderer meshRenderer = lamp.GetComponentInChildren<MeshRenderer>();
        Material material = meshRenderer.materials[1];
        if (material.IsKeywordEnabled("_EMISSION"))
        {
            meshRenderer.materials[1].DisableKeyword("_EMISSION");
            material.color = Color.black;
            light.enabled = false;

        }
        else
        {
            meshRenderer.materials[1].EnableKeyword("_EMISSION");
            light.enabled = true;
            material.color = Color.white;
        }
    }
}

public class MirrorAbility : ItemAbility
{
    public override string itemName => "Mirror"; // Ghi đè PT itemName
    public override string description => "This is a mirror!";

    public override bool isSupport => false;
    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject waterGem = GameObject.Find("WaterGem");
        GameObject flower = GameObject.Find("Chrysanthemum");
        GameObject timeline = GameObject.Find("MasterTimeline");
        // TimelineController timelineController = timeline.GetComponent<TimelineController>();
        if (flower != null && waterGem != null)
        {
            MeshRenderer[] meshRenderer = flower.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer pental in meshRenderer)
            {
                if (pental.name == "pental" && pental.material.color == Color.white)
                {
                    GameObject mirror = GameObject.Find(itemName);
                    Camera camera1 = mirror.GetComponentInChildren<Camera>();
                    camera1.enabled = true;
                }
            }
        }
    }
}

/*Tạo lớp Factory để quản lý và tạo các đối tượng kế thừa "ItemAbility"*/
public static class AbilityFactory
{
    static Dictionary<string, Type> abilitiesByName; // Lưu các tên và loại của các chức năng
    static bool isInitialized => abilitiesByName != null;

    static void InitializeFactory() // PT tìm các lớp con hiện tại, khởi tạo và lưu vào Dictionary
    {
        if (isInitialized)
        {
            return;
        }
        var abilitiesType = Assembly.GetAssembly(typeof(ItemAbility)).GetTypes(). // Tìm các assembly hiện tại của "ItemAbility"
        Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ItemAbility))); // Nếu như nó là lớp và không phải lớp trừu tượng và lớp con của "ItemAbility"

        abilitiesByName = new Dictionary<string, Type>();

        foreach (var type in abilitiesType) //Duyệt qua abilitiesType
        {
            var tempType = Activator.CreateInstance(type) as ItemAbility; // Khởi tạo tạm thời một biến như "ItemAbility"
            abilitiesByName.Add(tempType.itemName, type); // Thêm vào Dictionary
        }
    }

    public static ItemAbility GetItemAbility(string abilityType) //PT trả về loại chức năng (năng lực)
    {
        InitializeFactory();
        if (abilitiesByName.ContainsKey(abilityType))
        {
            Type type = abilitiesByName[abilityType];
            var ability = Activator.CreateInstance(type) as ItemAbility;
            return ability;
        }
        return null;
    }

    internal static IEnumerable<string> GetItemAbilityName() //PT trả về danh sách tên chức năng của các itemitem
    {
        InitializeFactory();
        return abilitiesByName.Keys;
    }
}


