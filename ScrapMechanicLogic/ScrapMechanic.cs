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
        static int i = 0;
        static List<DefaultObjectStruct> objects = new List<DefaultObjectStruct>();
        static DefaultObjectStruct LogicDataPreset  = new DefaultObjectStruct
        {
            color = "df7f01",
            controller = new(),
            pos = new(),
            shapeId = "9f0f56e8-2c31-4d83-996c-d00a9b296c3f",
            xaxis = 1,
            zaxis = -2
        };
        static DefaultObjectStruct LightDataPreset  = new DefaultObjectStruct
        {
            color = "df7f01",
            pos = new(),
            shapeId = "ed27f5e2-cac5-4a32-a5d9-49f116acc6af",
            xaxis = -1,
            zaxis = 2
        };
        static DefaultObjectStruct SwitchDataPreset = new DefaultObjectStruct
        {
            color = "df7f01",
            controller = new(),
            pos = new(),
            shapeId = "7cf717d7-d167-4f2d-a6e7-6b2c70aa3986",
            xaxis = -3,
            zaxis = 2
        };
        static DefaultObjectStruct BlockDataPreset  = new DefaultObjectStruct
        {
            color = "8D8F89",
            bounds = new Bound { x = 1, y = 1, z = 1 },
            pos = new(),
            shapeId = "a6c6ce30-dd47-4587-b475-085d55c6a3b4",
            xaxis = 1, //1
            zaxis = 3   //3
        };
        static JsonSerializerOptions JSONoptions = new JsonSerializerOptions
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
        public void addLogicObject(ControllerInfo controller, Position pos)
        {
            if (controller.controllers.Count == 0) { controller.controllers = null; }
            LogicDataPreset.controller = controller;
            LogicDataPreset.pos = pos;
            objects.Add(LogicDataPreset);
            i++;
        }
        public void addLightObject(ControllerInfo controller, Position pos)
        {
            if (controller.controllers.Count == 0) { controller.controllers = null; }
            LightDataPreset.controller = controller;
            LightDataPreset.pos = pos;
            objects.Add(LightDataPreset);
            i++;
        }
        public List<DefaultObjectStruct> setObjects(Dictionary<int, ControllerInfo> infos, short bus)
        {
            i = 0;
            objects = new List<DefaultObjectStruct>();

            addLogicObject(infos[0], new Position { x = 0, y = 0, z = 0 });

            for (int b = 0; b < 8; b++)
                addLogicObject(infos[1 + b], new Position { x = 1, y = b, z = 0 });
            for (int b = 0; b < 8; b++)
                addLogicObject(infos[9 + b], new Position { x = 2, y = b, z = 0 });
            for (short b = 0; b < bus; b++)
                addLogicObject(infos[17 + b], new Position { x = 3, y = b, z = 0 });

            for (int z = 0; z < (int)Math.Pow(2, bus); z++)
            {
                for (int y = 0; y < 1 + bus; y++)
                {
                    if (infos.TryGetValue(i, out var controlleri))
                        addLogicObject(controlleri, new Position { x = 4, y = y, z = z });
                }
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (infos.TryGetValue(i, out var controlleri))
                            addLogicObject(controlleri, new Position { x = 5 + x, y = y, z = z });
                    }
                }
            }
            return objects;
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
        public List<DefaultObjectStruct> create8bRam(short bus)
        {
            Dictionary<int, ControllerInfo> dict = new Dictionary<int, ControllerInfo>();

            //0 - R/W
            //[8] - INPUT
            //[8] - OUTPUT
            //[bus] - SEL

            dict.Add(0, new ControllerInfo
            {
                active = false,
                controllers = new()
                { },
                id = 0,
                joints = null,
                mode = (int)LogicOperand.AND
            });
            for (int b = 0; b < 16; b++)
            {
                dict.Add(1 + b, new ControllerInfo
                {
                    active = false,
                    controllers = new()
                    { },
                    id = 1 + b,
                    joints = null,
                    mode = (int)LogicOperand.OR
                });
            }
            for (short b = 0; b < bus; b++)
            {
                dict.Add(17 + b, new ControllerInfo
                {
                    active = false,
                    controllers = new()
                    { },
                    id = 17 + b,
                    joints = null,
                    mode = (int)LogicOperand.AND
                });
            }


            int[,] logicNumBC = new int[(int)Math.Pow(2, bus), 8];
            int[] logicNumCompare = new int[(int)Math.Pow(2, bus)];
            for (int i = 0; i < (int)Math.Pow(2, bus); i++)
            {
                logicNumCompare[i] = dict.Count;
                createEqualCompare(dict, bus, i);
                for (int b = 0; b < 8; b++)
                {
                    int logicNumFlipFlop = dict.Count;
                    createRSFlipFlop(dict);
                    logicNumBC[i, b] = dict.Count;
                    createBC(dict);
                    dict[logicNumBC[i, b] + 1].controllers.Add(new Controller { id = logicNumFlipFlop });
                    dict[logicNumBC[i, b] + 2].controllers.Add(new Controller { id = logicNumFlipFlop + 2 });
                    dict[logicNumFlipFlop + 1].controllers.Add(new Controller { id = logicNumBC[i, b] + 4 });
                    dict[logicNumCompare[i]].controllers.Add(new Controller { id = logicNumBC[i, b] + 1 });
                    dict[logicNumCompare[i]].controllers.Add(new Controller { id = logicNumBC[i, b] + 2 });
                    dict[logicNumCompare[i]].controllers.Add(new Controller { id = logicNumBC[i, b] + 4 });
                    dict[0].controllers.Add(new Controller { id = logicNumBC[i, b] + 1 });
                    dict[0].controllers.Add(new Controller { id = logicNumBC[i, b] + 2 });
                    dict[0].controllers.Add(new Controller { id = logicNumBC[i, b] + 3 });
                    dict[b + 1].controllers.Add(new Controller { id = logicNumBC[i, b] + 0 });
                    dict[b + 1].controllers.Add(new Controller { id = logicNumBC[i, b] + 2 });
                    dict[b + 17].controllers.Add(new Controller { id = logicNumCompare[i] + 1 + b });
                    dict[logicNumBC[i, b] + 4].controllers.Add(new Controller { id = 9 + b });
                }
            }

            return setObjects(dict, bus);
        }
        public static void createRSFlipFlop(Dictionary<int, ControllerInfo> dict)
        {
            //IN 0 - R
            //IN 2 - S
            //OUT 1 - Q


            int logicNum = dict.Count;
            dict.Add(logicNum, new ControllerInfo
            {
                active = false,
                controllers = new()
                {
                    new Controller { id = logicNum+1 }
                },
                id = logicNum,
                joints = null,
                mode = (int)LogicOperand.OR
            });
            dict.Add(logicNum + 1, new ControllerInfo
            {
                active = false,
                controllers = new()
                {
                    new Controller { id = logicNum+2 }
                },
                id = logicNum + 1,
                joints = null,
                mode = (int)LogicOperand.NOT
            });
            dict.Add(logicNum + 2, new ControllerInfo
            {
                active = false,
                controllers = new()
                {
                    new Controller { id = logicNum }
                },
                id = logicNum + 2,
                joints = null,
                mode = (int)LogicOperand.NOR
            });
        }
        public static void createBC(Dictionary<int, ControllerInfo> dict)
        {
            //IN 0 - Input
            //IN 1&2&4 - Select
            //IN 1&2&3 - R/W
            //IN 4 - Q
            //OUT 4- OUTPUT
            //OUT 1 - R
            //OUT 2 - S


            int logicNum = dict.Count;
            dict.Add(logicNum, new ControllerInfo
            {
                active = false,
                controllers = new()
                {
                    new Controller { id = logicNum+1 }
                },
                id = logicNum,
                joints = null,
                mode = (int)LogicOperand.NOT
            });
            dict.Add(logicNum + 1, new ControllerInfo
            {
                active = false,
                controllers = new()
                { },
                id = logicNum + 1,
                joints = null,
                mode = (int)LogicOperand.AND
            });
            dict.Add(logicNum + 2, new ControllerInfo
            {
                active = false,
                controllers = new()
                { },
                id = logicNum + 2,
                joints = null,
                mode = (int)LogicOperand.AND
            });
            dict.Add(logicNum + 3, new ControllerInfo
            {
                active = false,
                controllers = new()
                {
                    new Controller { id = logicNum + 4 }
                },
                id = logicNum + 3,
                joints = null,
                mode = (int)LogicOperand.NOT
            });
            dict.Add(logicNum + 4, new ControllerInfo
            {
                active = false,
                controllers = new()
                { },
                id = logicNum + 4,
                joints = null,
                mode = (int)LogicOperand.AND
            });
        }
        public static void createEqualCompare(Dictionary<int, ControllerInfo> dict, short bus, int compareValue)
        {
            //OUT 0 - OutputCompare
            //IN +1 - bit 0
            //IN +2 - bit 1

            int logicNum = dict.Count;
            dict.Add(logicNum, new ControllerInfo
            {
                active = false,
                controllers = new()
                { },
                id = logicNum,
                joints = null,
                mode = (int)LogicOperand.AND
            });
            for (int i = 0; i < bus; i++)
            {
                dict.Add(logicNum + i + 1, new ControllerInfo
                {
                    active = false,
                    controllers = new()
                    {
                        new Controller { id = logicNum }
                    },
                    id = logicNum + i + 1,
                    joints = null,
                    mode = (int)(((compareValue >> i) & 1) == 1 ? LogicOperand.AND : LogicOperand.NOT)
                });
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