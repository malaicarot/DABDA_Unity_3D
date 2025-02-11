using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;
using System.Linq;
using UnityEngine.SceneManagement;


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
    public override string description => "Liệu sau tất cả, màu sắc của nó là gì?";

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

public class LampAbility : ItemAbility
{
    public override string itemName => "Lamp"; // Ghi đè PT itemName
    public override string description => "Một chút ánh sáng giữa lúc tối tăm nhất cũng có thể là con đường dẫn đến lối thoát!";

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
    public override string description => "Là Gương nhưng lại không thể soi! Là sự thật nhưng không thể chấp nhận!";

    public override bool isSupport => false;
    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject waterGem = GameObject.Find("WaterGem");
        GameObject flower = GameObject.Find("Chrysanthemum");
        GameObject player = GameObject.Find("Player");
        SaveManager.SingletonSaveData.UpdateCheckpointData(SceneManager.GetActiveScene().buildIndex, player.transform.position.x, player.transform.position.y, player.transform.position.z);


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

public class WaterGemAbility : ItemAbility
{
    public override string itemName => "WaterGem"; // Ghi đè PT itemName
    public override string description => "Như dòng nước siết, chảy mạnh mẽ, cuốn trôi đi những cảm xúc tiêu cực. Đó là cách tốt nhất hay chỉ là sự tránh né?";

    public override bool isSupport => true;
    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject mirror = GameObject.Find("Mirror");
        Camera camera1 = mirror.GetComponentInChildren<Camera>();
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        GameObject timeLine = GameObject.Find("MasterTimeline");
        TimelineController timelineController = timeLine.GetComponent<TimelineController>();

        if (camera1.enabled == true)
        {
            playerMovement.Floating();
            timelineController.PlayTimeline();
        }
    }
}

public class LavaGemAbility : ItemAbility
{
    public override string itemName => "LavaGem"; // Ghi đè PT itemName
    public override string description => "Đốt cháy mọi thứ! Đúng vậy bằng chính sự giận dữ!";

    public override bool isSupport => true;
    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject stone = GameObject.Find("Stone");
        StoneMarker[] lavaStone = stone.GetComponentsInChildren<StoneMarker>();
        GameObject torch = GameObject.Find("Torch");
        ParticleSystem torchParticleSystem = torch.GetComponentInChildren<ParticleSystem>();
        GameObject timeLine = GameObject.Find("MasterTimeLine");
        Debug.Log(timeLine);

        TimelineController timelineController = timeLine.GetComponent<TimelineController>();
        Debug.Log(timelineController);
        foreach (StoneMarker item in lavaStone)
        {
            ParticleSystem particleSystem = item.gameObject.GetComponentInChildren<ParticleSystem>();
            if (particleSystem.isPlaying && torchParticleSystem.isPlaying)
            {
                timelineController.PlayTimeline();
                Debug.Log("Done");
            }
        }
    }
}

public class HammerAbility : ItemAbility
{
    public override string itemName => "Hammer"; // Ghi đè PT itemName
    public override string description => "Trút giận liệu có giúp ta khá hơn? ";

    public override bool isSupport => false;
    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.StartCrushing();
    }
}

public class LavaStoneAbility : ItemAbility
{
    public override string itemName => "LavaStone"; // Ghi đè PT itemName
    public override string description => "Bạn không thể tưởng tượng được sự bùng nổ của cơn giận dữ đâu!";

    public override bool isSupport => false;
    public override void Proccess() // Ghi đè PT Proccess
    {
        AbilityItems abilityItems = new AbilityItems();
        GameObject stone = GameObject.Find("Stone");
        StoneMarker[] lavaStone = stone.GetComponentsInChildren<StoneMarker>();
        GameObject player = GameObject.Find("Player");
        GameObject hammer = GameObject.Find("Hammer");
        GameObject torch = GameObject.Find("Torch");
        ParticleSystem torchParticleSystem = torch.GetComponentInChildren<ParticleSystem>();


        foreach (StoneMarker item in lavaStone)
        {
            if (hammer != null)
            {
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.StartCrushing();
                ParticleSystem stoneParticleSystem = item.gameObject.GetComponentInChildren<ParticleSystem>();
                stoneParticleSystem.Play();
            }
        }

        if (torchParticleSystem.isPlaying)
        {
            abilityItems.VocalnoErupts();
        }
    }
}

public class TorchAbility : ItemAbility
{
    public override string itemName => "Torch"; // Ghi đè PT itemName
    public override string description => "Ngọc đuốc dùng để làm gì nhỉ?";

    public override bool isSupport => false;
    public override void Proccess() // Ghi đè PT Proccess
    {
        AbilityItems abilityItems = new AbilityItems();
        GameObject lavaGem = GameObject.Find("LavaGem");
        GameObject torch = GameObject.Find("Torch");
        GameObject stone = GameObject.Find("Stone");
        StoneMarker[] lavaStone = stone.GetComponentsInChildren<StoneMarker>();
        ParticleSystem particleSystem = torch.GetComponentInChildren<ParticleSystem>();
        bool eruptsAble = true;

        if (lavaGem != null)
        {
            particleSystem.Play();
        }

        foreach (StoneMarker item in lavaStone)
        {
            ParticleSystem stoneParticleSystem = item.gameObject.GetComponentInChildren<ParticleSystem>();
            if (!stoneParticleSystem.isPlaying)
            {
                eruptsAble = false;
                break;
            }
        }

        if (eruptsAble)
        {
            abilityItems.VocalnoErupts();
        }
    }
}

public class AngleStatueAbility : ItemAbility
{
    public override string itemName => "AngelStatue"; // Ghi đè PT itemName
    public override string description => "Cầu nguyện chỉ là cách tránh né thực tại. Hãy tự mình đối mặt và giải quyết vấn đề của ngươi!";
    public override bool isSupport => false;
    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject timeline = GameObject.Find("GemTimeLine");
        TimelineController timelineController = timeline.GetComponent<TimelineController>();
        timelineController.PlayTimeline();
    }
}

public class ShieldKnightAbility : ItemAbility
{
    public override string itemName => "ShieldKnight"; // Ghi đè PT itemName
    public override string description => "Đôi khi, chấp nhận sự thật đau lòng là bước đầu tiên để giải phóng trái tim và tìm lại bình yên!";

    public override bool isSupport => false;
    public override void Proccess() // Ghi đè PT Proccess
    {

        GameObject swordKnight = GameObject.Find("SwordKnight");
        // BoxCollider swordKnightboxCollider = swordKnight.GetComponent<BoxCollider>();
        // swordKnightboxCollider.enabled = false;

        GameObject depression = GameObject.Find("AcceptanceCircle");
        GameObject loadScene = GameObject.Find("TriggerLoadScene_5");
        ParticleSystem particleSystem = depression.GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
        BoxCollider boxCollider = loadScene.GetComponent<BoxCollider>();
        boxCollider.enabled = true;

    }
}

public class SwordKnightAbility : ItemAbility
{
    public override string itemName => "SwordKnight"; // Ghi đè PT itemName
    public override string description => "Đôi khi, chấp nhận sự thật đau lòng là bước đầu tiên để giải phóng trái tim và tìm lại bình yên!";

    public override bool isSupport => false;
    public override void Proccess() // Ghi đè PT Proccess
    {
        GameObject shieldKnight = GameObject.Find("ShieldKnight");
        // BoxCollider shieldKnightboxCollider = shieldKnight.GetComponentIndex[00<BoxCollider>();
        // shieldKnightboxCollider.enabled = false;


        GameObject depression = GameObject.Find("DepressionCircle");
        GameObject loadScene = GameObject.Find("TriggerLoadScene_4");
        ParticleSystem particleSystem = depression.GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
        BoxCollider boxCollider = loadScene.GetComponent<BoxCollider>();
        boxCollider.enabled = true;


    }
}

public class LightGemAbility : ItemAbility
{
    public override string itemName => "LightGem"; // Ghi đè PT itemName
    public override string description => "";

    public override bool isSupport => true;
    public override void Proccess() // Ghi đè PT Proccess
    {
        Debug.Log("Light Gem!");
    }
}

public class AbilityItems
{
    public void VocalnoErupts()
    {
        GameObject lavaEruption = GameObject.Find("LavaEruption");
        ParticleSystem[] lavaScourceParticle = lavaEruption.GetComponentsInChildren<ParticleSystem>();
        if (lavaEruption != null && lavaScourceParticle.Length >= 0)
        {
            foreach (ParticleSystem particleSystem in lavaScourceParticle)
            {
                particleSystem.Play();
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


