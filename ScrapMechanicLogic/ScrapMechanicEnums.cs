using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapMechanicLogic
{
    public enum BlockType
    {
        ConcreteBlock1,
        WoodBlock1,
        MetalBlock1,
        BarrierBlock,
        TileBlock,
        BrickBlock,
        GlassBlock,
        GlassTileBlock,
        PathLightBlock,
        SpaceshipBlock,
        CardboardBlock,
        ScrapWoodBlock,
        WoodBlock2,
        WoodBlock3,
        ScrapMetalBlock,
        MetalBlock2,
        MetalBlock3,
        ScrapStoneBlock,
        ConcreteBlock2,
        ConcreteBlock3,
        CrackedConcreteBlock,
        ConcreteSlabBlock,
        RustedMetalBlock,
        ExtrudedMetalBlock,
        BubblePlasticBlock,
        PlasticBlock,
        InsulationBlock,
        PlasterBlock,
        CarpetBlock,
        PaintedWallBlock,
        NetBlock,
        SolidNetBlock,
        PunchedSteelBlock,
        StripedNetBlock,
        SquareMeshBlock,
        RestroomBlock,
        DiamondPlateBlock,
        AluminumBlock,
        WornMetalBlock,
        SpaceshipFloorBlock,
        SandBlock,
        ArmoredGlassBlock
    }
    public enum LogicOperand
    {
        AND,
        OR,
        XOR,
        NOT,
        NOR,
    }

    [Serializable]
    public struct DescriptionStruct
    {
        public string description { get; set; }
        public string localId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int version { get; set; }

        public DescriptionStruct(string name = "Converted", string type = "Blueprint")
        {
            this.description = "#{STEAM_WORKSHOP_NO_DESCRIPTION}";
            this.localId = "8f349f78-07ca-4c88-9356-b154aa821602";
            this.name = name;
            this.type = type;
            this.version = 0;
        }
    }
    [Serializable]
    public struct DefaultObjectStruct
    {
        public Bound? bounds { get; set; }
        public string color { get; set; }
        public ControllerInfo? controller { get; set; }
        public Position pos { get; set; }
        public string shapeId { get; set; }
        public int xaxis { get; set; }
        public int zaxis { get; set; }
    }

    [Serializable]
    public struct ControllerInfo
    {
        public bool active { get; set; }
        public List<Controller>? controllers { get; set; }
        public int id { get; set; }
        public object joints { get; set; }
        public int? mode { get; set; }
    }

    [Serializable]
    public struct Controller
    {
        public int id { get; set; }
    }
    [Serializable]
    public struct Bound
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
    }

    [Serializable]
    public struct Position
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
    }

    public class DefaultBlockData
    {
        public string Color { get; set; }
        public string ShapeId { get; set; }
    }
    
    internal class ScrapMechanicEnums
    {
        public static Dictionary<BlockType, DefaultBlockData> blockDataDictionary = new Dictionary<BlockType, DefaultBlockData>
        {
            { BlockType.ConcreteBlock1,       new DefaultBlockData { Color = "8d8f89", ShapeId = "a6c6ce30-dd47-4587-b475-085d55c6a3b4" } },
            { BlockType.WoodBlock1,           new DefaultBlockData { Color = "9b683a", ShapeId = "df953d9c-234f-4ac2-af5e-f0490b223e71" } },
            { BlockType.MetalBlock1,          new DefaultBlockData { Color = "675f51", ShapeId = "8aedf6c2-94e1-4506-89d4-a0227c552f1e" } },
            { BlockType.BarrierBlock,         new DefaultBlockData { Color = "ce9e0c", ShapeId = "09ca2713-28ee-4119-9622-e85490034758" } },
            { BlockType.TileBlock,            new DefaultBlockData { Color = "bfdfed", ShapeId = "8ca49bff-eeef-4b43-abd0-b527a567f1b7" } },
            { BlockType.BrickBlock,           new DefaultBlockData { Color = "af967b", ShapeId = "0603b36e-0bdb-4828-b90c-ff19abcdfe34" } },
            { BlockType.GlassBlock,           new DefaultBlockData { Color = "e4f8ff", ShapeId = "5f41af56-df4c-4837-9b3c-10781335757f" } },
            { BlockType.GlassTileBlock,       new DefaultBlockData { Color = "c2f9ff", ShapeId = "749f69e0-56c9-488c-adf6-66c58531818f" } },
            { BlockType.PathLightBlock,       new DefaultBlockData { Color = "727272", ShapeId = "073f92af-f37e-4aff-96b3-d66284d5081c" } },
            { BlockType.SpaceshipBlock,       new DefaultBlockData { Color = "820a0a", ShapeId = "027bd4ec-b16d-47d2-8756-e18dc2af3eb6" } },
            { BlockType.CardboardBlock,       new DefaultBlockData { Color = "a48052", ShapeId = "f0cba95b-2dc4-4492-8fd9-36546a4cb5aa" } },
            { BlockType.ScrapWoodBlock,       new DefaultBlockData { Color = "cd9d71", ShapeId = "1fc74a28-addb-451a-878d-c3c605d63811" } },
            { BlockType.WoodBlock2,           new DefaultBlockData { Color = "dc9153", ShapeId = "1897ee42-0291-43e4-9645-8c5a5d310398" } },
            { BlockType.WoodBlock3,           new DefaultBlockData { Color = "f2ad74", ShapeId = "061b5d4b-0a6a-4212-b0ae-9e9681f1cbfb" } },
            { BlockType.ScrapMetalBlock,      new DefaultBlockData { Color = "df6226", ShapeId = "1f7ac0bb-ad45-4246-9817-59bdf7f7ab39" } },
            { BlockType.MetalBlock2,          new DefaultBlockData { Color = "869499", ShapeId = "1016cafc-9f6b-40c9-8713-9019d399783f" } },
            { BlockType.MetalBlock3,          new DefaultBlockData { Color = "88a5ac", ShapeId = "c0dfdea5-a39d-433a-b94a-299345a5df46" } },
            { BlockType.ScrapStoneBlock,      new DefaultBlockData { Color = "848484", ShapeId = "30a2288b-e88e-4a92-a916-1edbfc2b2dac" } },
            { BlockType.ConcreteBlock2,       new DefaultBlockData { Color = "8d8f89", ShapeId = "ff234e42-5da4-43cc-8893-940547c97882" } },
            { BlockType.ConcreteBlock3,       new DefaultBlockData { Color = "c9d7dc", ShapeId = "e281599c-2343-4c86-886e-b2c1444e8810" } },
            { BlockType.CrackedConcreteBlock, new DefaultBlockData { Color = "8d8f89", ShapeId = "f5ceb7e3-5576-41d2-82d2-29860cf6e20e" } },
            { BlockType.ConcreteSlabBlock,    new DefaultBlockData { Color = "af967b", ShapeId = "cd0eff89-b693-40ee-bd4c-3500b23df44e" } },
            { BlockType.RustedMetalBlock,     new DefaultBlockData { Color = "738192", ShapeId = "220b201e-aa40-4995-96c8-e6007af160de" } },
            { BlockType.ExtrudedMetalBlock,   new DefaultBlockData { Color = "858795", ShapeId = "25a5ffe7-11b1-4d3e-8d7a-48129cbaf05e" } },
            { BlockType.BubblePlasticBlock,   new DefaultBlockData { Color = "9acfd2", ShapeId = "f406bf6e-9fd5-4aa0-97c1-0b3c2118198e" } },
            { BlockType.PlasticBlock,         new DefaultBlockData { Color = "0b9ade", ShapeId = "628b2d61-5ceb-43e9-8334-a4135566df7a" } },
            { BlockType.InsulationBlock,      new DefaultBlockData { Color = "fff063", ShapeId = "9be6047c-3d44-44db-b4b9-9bcf8a9aab20" } },
            { BlockType.PlasterBlock,         new DefaultBlockData { Color = "979797", ShapeId = "b145d9ae-4966-4af6-9497-8fca33f9aee3" } },
            { BlockType.CarpetBlock,          new DefaultBlockData { Color = "368085", ShapeId = "febce8a6-6c05-4e5d-803b-dfa930286944" } },
            { BlockType.PaintedWallBlock,     new DefaultBlockData { Color = "eeeeee", ShapeId = "e981c337-1c8a-449c-8602-1dd990cbba3a" } },
            { BlockType.NetBlock,             new DefaultBlockData { Color = "435359", ShapeId = "4aa2a6f0-65a4-42e3-bf96-7dec62570e0b" } },
            { BlockType.SolidNetBlock,        new DefaultBlockData { Color = "888888", ShapeId = "3d0b7a6e-5b40-474c-bbaf-efaa54890e6a" } },
            { BlockType.PunchedSteelBlock,    new DefaultBlockData { Color = "888888", ShapeId = "ea6864db-bb4f-4a89-b9ec-977849b6713a" } },
            { BlockType.StripedNetBlock,      new DefaultBlockData { Color = "888888", ShapeId = "a479066d-4b03-46b5-8437-e99fec3f43ee" } },
            { BlockType.SquareMeshBlock,      new DefaultBlockData { Color = "c36512", ShapeId = "b4fa180c-2111-4339-b6fd-aed900b57093" } },
            { BlockType.RestroomBlock,        new DefaultBlockData { Color = "607b79", ShapeId = "920b40c8-6dfc-42e7-84e1-d7e7e73128f6" } },
            { BlockType.DiamondPlateBlock,    new DefaultBlockData { Color = "43494d", ShapeId = "f7d4bfed-1093-49b9-be32-394c872a1ef4" } },
            { BlockType.AluminumBlock,        new DefaultBlockData { Color = "727272", ShapeId = "3e3242e4-1791-4f70-8d1d-0ae9ba3ee94c" } },
            { BlockType.WornMetalBlock,       new DefaultBlockData { Color = "66837c", ShapeId = "d740a27d-cc0f-4866-9e07-6a5c516ad719" } },
            { BlockType.SpaceshipFloorBlock,  new DefaultBlockData { Color = "dadada", ShapeId = "4ad97d49-c8a5-47f3-ace3-d56ba3affe50" } },
            { BlockType.SandBlock,            new DefaultBlockData { Color = "c69146", ShapeId = "c56700d9-bbe5-4b17-95ed-cef05bd8be1b" } },
            { BlockType.ArmoredGlassBlock,    new DefaultBlockData { Color = "3abfb1", ShapeId = "b5ee5539-75a2-4fef-873b-ef7c9398b3f5" } }
        };

        public static readonly List<string> predefinedColors = new List<string>
        {
            // List of 40 predefined colors
            "eeeeee", "7f7f7f", "4a4a4a", "222222", "f5f071", "e2db13", "817c00", "323000",
            "cbf66f", "a0ea00", "577d07", "375000", "68ff88", "19e753", "0e8031", "064023",
            "7eeded", "2ce6e6", "118787", "0a4444", "4c6fe3", "0a3ee2", "0f2e91", "0a1d5a",
            "ae79f0", "7514ed", "500aa6", "35086c", "ee7bf0", "cf11d2", "720a74", "520653",
            "f06767", "d02525", "7c0000", "560202", "eeaf5c", "df7f00", "673b00", "472800"
        };
        public static readonly List<uint> predefinedColorsArgb = new List<uint>
        {
            0xFFEEEEEE,0xFF7F7F7F,0xFF4A4A4A,0xFF222222,0xFFF5F071,0xFFE2DB13,0xFF817C00,0xFF323000,
            0xFFCBF66F,0xFFA0EA00,0xFF577D07,0xFF375000,0xFF68FF88,0xFF19E753,0xFF0E8031,0xFF064023,
            0xFF7EEDED,0xFF2CE6E6,0xFF118787,0xFF0A4444,0xFF4C6FE3,0xFF0A3EE2,0xFF0F2E91,0xFF0A1D5A,
            0xFFAE79F0,0xFF7514ED,0xFF500AA6,0xFF35086C,0xFFEE7BF0,0xFFCF11D2,0xFF720A74,0xFF520653,
            0xFFF06767,0xFFD02525,0xFF7C0000,0xFF560202,0xFFEEAF5C,0xFFDF7F00,0xFF673B00,0xFF472800
        };
    }
}
