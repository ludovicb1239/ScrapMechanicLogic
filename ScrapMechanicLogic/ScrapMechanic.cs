using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Xml;
using System.Collections;
using System.Windows.Forms;
using System.Numerics;
using System.Formats.Asn1;
using System.Data.SqlTypes;
using static System.Windows.Forms.Design.AxImporter;
using System.Drawing;
using System.Globalization;

namespace ScrapMechanicLogic
{
    internal class ScrapMechanic
    {
        static public DefaultObjectStruct LogicDataPreset  = new DefaultObjectStruct
        {
            color = "df7f01",
            controller = new(),
            pos = new(),
            shapeId = "9f0f56e8-2c31-4d83-996c-d00a9b296c3f",
            xaxis = 1,
            zaxis = -2
        };
        static public DefaultObjectStruct LightDataPreset  = new DefaultObjectStruct
        {
            color = "df7f01",
            controller = new(),
            pos = new(),
            shapeId = "ed27f5e2-cac5-4a32-a5d9-49f116acc6af",
            xaxis = 1,
            zaxis = -2
        };
        static public DefaultObjectStruct SwitchDataPreset = new DefaultObjectStruct
        {
            color = "df7f01",
            controller = new(),
            pos = new(),
            shapeId = "7cf717d7-d167-4f2d-a6e7-6b2c70aa3986",
            xaxis = -3,
            zaxis = 2
        };
        static public DefaultObjectStruct ButtonDataPreset = new DefaultObjectStruct
        {
            color = "df7f01",
            controller = new(),
            pos = new(),
            shapeId = "1e8d93a4-506b-470d-9ada-9c0a321e2db5",
            xaxis = -1,
            zaxis = 3
        };
        static public DefaultObjectStruct BlockDataPreset  = new DefaultObjectStruct
        {
            color = "8D8F89",
            bounds = new Bound { x = 1, y = 1, z = 1 },
            pos = new(),
            shapeId = "a6c6ce30-dd47-4587-b475-085d55c6a3b4",
            xaxis = 1, //1
            zaxis = 3   //3
        };
        static private JsonSerializerOptions JSONoptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
        public static void ParseObject(List<DefaultObjectStruct> objects, DescriptionStruct description)
        {
            try
            {
                // Get the roaming path for the current user
                string roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fullPath = Path.Combine(roamingPath, @"Axolot Games\Scrap Mechanic\User\");

                // Get all subfolders in the specified path
                string[] subfolders = Directory.GetDirectories(fullPath);

                if (subfolders.Length == 0)
                {
                    //Error no users found
                    MessageBox.Show("No users folder found in " + fullPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Write the file to every users
                foreach (string subfolder in subfolders)
                {
                    string folderPath = Path.Combine(subfolder, @"Blueprints\");
                    folderPath = Path.Combine(folderPath, description.localId);
                    string blueprintPath = Path.Combine(folderPath, @"blueprint.json");
                    string descriptionPath = Path.Combine(folderPath, @"description.json");

                    Directory.CreateDirectory(folderPath);

                    using (StreamWriter sw = new StreamWriter(blueprintPath))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("{\"bodies\":[{\"childs\":[");

                        for (int i = 0; i < objects.Count; i++)
                        {
                            string str = JsonSerializer.Serialize(objects[i], JSONoptions);
                            sb.Append(str);
                            sb.Append(",");
                        }

                        string finalJson = sb.ToString().TrimEnd(',') + "]}],\"version\":3}";
                        sw.Write(finalJson);

                    }
                    using (StreamWriter sw = new StreamWriter(descriptionPath))
                    {
                        sw.Write(JsonSerializer.Serialize(description, new JsonSerializerOptions
                        {
                            WriteIndented = true
                        }
                        ));
                    }

                    //string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icon.png");
                    string destinationFilePath = Path.Combine(folderPath, "icon.png");
                    File.Copy("icon.png", destinationFilePath, true);

                    Console.WriteLine("\nJSON data has been written to the file: \n" + blueprintPath + "\nand : \n" + descriptionPath);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public static void getBlock(BlockType blockType)
        {
            BlockDataPreset.color = ScrapMechanicEnums.blockDataDictionary[blockType].Color;
            BlockDataPreset.shapeId = ScrapMechanicEnums.blockDataDictionary[blockType].ShapeId;
        }
        public static void CreateFromBlocks(List<Position> positions, BlockType blockType, List<DefaultObjectStruct> list, string[] palette, List<byte> paletteIndex, List<Bound> boundings, int scale = 1)
        {
            getBlock(blockType);
            if (palette == null || paletteIndex == null)
            {
                foreach (Position pos in positions)
                {
                    BlockDataPreset.pos = pos;
                    list.Add(BlockDataPreset);
                }
            }
            else
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    BlockDataPreset.color = palette[paletteIndex[i]];
                    if (scale != 1)
                    {
                        Position pos = positions[i];
                        pos.x *= scale;
                        pos.y *= scale;
                        pos.z *= scale;
                        BlockDataPreset.pos = pos;

                        Bound bound = boundings[i];
                        bound.x *= scale;
                        bound.y *= scale;
                        bound.z *= scale;
                        BlockDataPreset.bounds = bound;
                    }
                    else
                    {
                        BlockDataPreset.pos = positions[i];
                        BlockDataPreset.bounds = boundings[i];
                    }
                    list.Add(BlockDataPreset);
                }
            }
        }
        public static void CreateFromBlocks(List<Position> positions, List<BlockType> blockTypes, List<DefaultObjectStruct> list, string[] palette, List<byte> paletteIndex, List<Bound> boundings, int scale = 1)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                getBlock(blockTypes[i]);
                BlockDataPreset.color = palette[paletteIndex[i]];
                if (scale != 1)
                {
                    Position pos = positions[i];
                    pos.x *= scale;
                    pos.y *= scale;
                    pos.z *= scale;
                    BlockDataPreset.pos = pos;

                    Bound bound = boundings[i];
                    bound.x *= scale;
                    bound.y *= scale;
                    bound.z *= scale;
                    BlockDataPreset.bounds = bound;
                }
                else
                {
                    BlockDataPreset.pos = positions[i];
                    BlockDataPreset.bounds = boundings[i];
                }
                list.Add(BlockDataPreset);
            }
        }
        public static void CreateFromBlocks(List<Position> positions, BlockType blockType, List<DefaultObjectStruct> list, List<string> colors, int scale = 1)
        {
            getBlock(blockType);
            for (int i = 0; i < positions.Count; i++)
            {
                BlockDataPreset.color = colors[i];
                if (scale != 1)
                {
                    Position pos = positions[i];
                    pos.x *= scale;
                    pos.y *= scale;
                    pos.z *= scale;
                    BlockDataPreset.pos = pos;

                    BlockDataPreset.bounds = new Bound() { x = scale, y = scale, z = scale };
                }
                else
                {
                    BlockDataPreset.pos = positions[i];
                    BlockDataPreset.bounds = new Bound() { x = 1, y = 1, z = 1 };
                }
                list.Add(BlockDataPreset);
            }
        }
        public static void CreateFromBlocks(List<Position> positions, BlockType blockType, List<DefaultObjectStruct> list, List<string> colors, List<Bound> boundings, int scale = 1)
        {
            getBlock(blockType);
            for (int i = 0; i < positions.Count; i++)
            {
                BlockDataPreset.color = colors[i];
                if (scale != 1)
                {
                    Position pos = positions[i];
                    pos.x *= scale;
                    pos.y *= scale;
                    pos.z *= scale;
                    BlockDataPreset.pos = pos;

                    Bound bound = boundings[i];
                    bound.x *= scale;
                    bound.y *= scale;
                    bound.z *= scale;
                    BlockDataPreset.bounds = bound;
                }
                else
                {
                    BlockDataPreset.pos = positions[i];
                    BlockDataPreset.bounds = boundings[i];
                }
                list.Add(BlockDataPreset);
            }
        }
    }

    internal class ScrapMechanicColor
    {
        public static string RGBToHex(int red, int green, int blue)
        {
            // Convert RGB to hex format
            string hexColor = $"{red:X2}{green:X2}{blue:X2}";
            return hexColor;
        }
        public static string RGBToHex(Color color)
        {
            if (color.IsEmpty)
                return RGBToHex(0,0,0);
            else
                return RGBToHex(color.R, color.G, color.B);
        }
        public static string RGBToHex(uint color)
        {
            // Extract individual color components (assuming RGBA order, where A is the most significant byte)
            byte red = (byte)((color >> 16) & 0xFF);
            byte green = (byte)((color >> 8) & 0xFF);
            byte blue = (byte)(color & 0xFF);

            // Convert RGB to hex format
            string hexColor = $"{red:X2}{green:X2}{blue:X2}";
            return hexColor;
        }
        public static Color HexToColor(string color)
        {
            Color c = Color.Empty;

            // empty color
            if ((color == null) || (color.Length == 0))
                return c;

            c = Color.FromArgb(Convert.ToInt32(color.Substring(0, 2), 16),
                               Convert.ToInt32(color.Substring(2, 2), 16),
                               Convert.ToInt32(color.Substring(4, 2), 16));

            return c;
        }
        public static string CompressColor(string inputColor)
        {
            Color originalColor = HexToColor(inputColor);
            double minDistance = double.MaxValue;
            string closestColor = "";

            for(int i = 0; i < ScrapMechanicEnums.predefinedColors.Count; i++)
            {
                Color predefined = Color.FromArgb((int)ScrapMechanicEnums.predefinedColorsArgb[i]);
                double distance = CalculateDistance(originalColor, predefined);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestColor = ScrapMechanicEnums.predefinedColors[i];
                }

            }
            return closestColor;
        }
        public static Color CompressColor(Color inputColor)
        {
            double minDistance = double.MaxValue;
            Color closestColor = Color.Empty;

            for (int i = 0; i < ScrapMechanicEnums.predefinedColorsArgb.Count; i++)
            {
                Color predefined = Color.FromArgb((int)ScrapMechanicEnums.predefinedColorsArgb[i]);
                double distance = CalculateDistance(inputColor, predefined);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestColor = Color.FromArgb((int)ScrapMechanicEnums.predefinedColorsArgb[i]);
                }
            }
            return closestColor;
        }

        private static double CalculateDistance(Color color1, Color color2)
        {
            int rDiff = color1.R - color2.R;
            int gDiff = color1.G - color2.G;
            int bDiff = color1.B - color2.B;

            return (rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
        }
    }
}