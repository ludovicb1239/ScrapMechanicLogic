using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ScrapMechanicLogic
{
    public partial class LogicForm : Form
    {
        private int i = 0;
        private List<DefaultObjectStruct> objects = new();
        public LogicForm()
        {
            InitializeComponent();


            /*
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter to allow only Verilog files
            openFileDialog.Filter = "Verilog Files (*.v)|*.v|All Files (*.*)|*.*";

            // Show the dialog and get the result
            DialogResult result = openFileDialog.ShowDialog();

            // Process the selected file
            if (result == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                openVerilog(selectedFilePath);
                Console.WriteLine("Done reading file");
            }
            */

            List<DefaultObjectStruct> logicGates = createGameOfLife(15);
            DescriptionStruct description = new DescriptionStruct("Converted Logic");
            ScrapMechanic.ParseObject(logicGates, description);
        }
        //AJOUTER UNE GATE RESET POUR TOUT LE MONDE ET UN BOUTON SET SUR COTE

        public void addLogicObject(ControllerInfo controller, Position pos)
        {
            if (controller.controllers.Count == 0) { controller.controllers = null; }
            DefaultObjectStruct newObject = ScrapMechanic.LogicDataPreset;
            newObject.controller = controller;
            newObject.pos = pos;
            objects.Add(newObject);
            i++;
        }
        public void addLightObject(ControllerInfo controller, Position pos)
        {
            if (controller.controllers != null && controller.controllers.Count == 0) { controller.controllers = null; }
            DefaultObjectStruct newObject = ScrapMechanic.LightDataPreset;
            newObject.controller = controller;
            newObject.pos = pos;
            objects.Add(newObject);
            i++;
        }
        public void addButtonObject(ControllerInfo controller, Position pos)
        {
            if (controller.controllers != null && controller.controllers.Count == 0) { controller.controllers = null; }
            DefaultObjectStruct newObject = ScrapMechanic.ButtonDataPreset;
            newObject.controller = controller;
            newObject.pos = pos;
            objects.Add(newObject);
            i++;
        }
        public List<DefaultObjectStruct> createGameOfLife(short oneSideLenght)
        {
            Dictionary<int, ControllerInfo> dict = new();

            //0 - CLK
            //1 - RES
            dict.Add(0, new ControllerInfo
            {
                active = false,
                controllers = new() { },
                id = 0,
                joints = null,
                mode = (int)LogicOperand.AND
            });
            dict.Add(1, new ControllerInfo
            {
                active = false,
                controllers = new() { },
                id = 1,
                joints = null,
                mode = (int)LogicOperand.AND
            });

            int[] logicNum = new int[oneSideLenght*oneSideLenght];
            int[] inputNums = new int[oneSideLenght * oneSideLenght];
            int[] outputNums = new int[oneSideLenght * oneSideLenght];
            // ---x
            // |
            // y

            // A B C
            // D x E
            // F G H

            i = 0;

            //int[] = A,B,C,D,E,F,G,H
            List<int[]> ABCindexes = new List<int[]>();
            for (int y = 0; y < oneSideLenght; y++)
            {
                for (int x = 0; x < oneSideLenght; x++)
                {
                    int[] newArray = new int[8];
                    int xMinus1 = (x - 1) < 0 ? oneSideLenght - 1 : (x - 1);
                    int xPlus1  = (x + 1) < oneSideLenght ? (x + 1) : 0;
                    int yMinus1 = (y - 1) < 0 ? oneSideLenght - 1 : (y - 1);
                    int yPlus1  = (y + 1) < oneSideLenght ? (y + 1) : 0;
                    newArray[0] = xMinus1 + oneSideLenght * yMinus1; //A
                    newArray[1] = x       + oneSideLenght * yMinus1; //B
                    newArray[2] = xPlus1  + oneSideLenght * yMinus1; //C
                    newArray[3] = xMinus1 + oneSideLenght * y     ;  //D
                    newArray[4] = xPlus1  + oneSideLenght * y  ;     //E
                    newArray[5] = xMinus1 + oneSideLenght * yPlus1;  //F
                    newArray[6] = x       + oneSideLenght * yPlus1;  //G
                    newArray[7] = xPlus1  + oneSideLenght * yPlus1;  //H

                    ABCindexes.Add(newArray);
                }
            }
            for (int y = 0; y < oneSideLenght; y++)
            {
                for (int x = 0; x < oneSideLenght; x++)
                {
                    logicNum[i] = dict.Count;

                    //Light = 0
                    dict.Add(logicNum[i], new ControllerInfo 
                    {
                        active = false,
                        controllers = null,
                        id = logicNum[i],
                    });
                    int RSFlipFlopNum = dict.Count;
                    outputNums[i] = RSFlipFlopNum + 1;
                    createRSFlipFlop(dict);
                    //set
                    dict.Add(logicNum[i] + 4, new ControllerInfo
                    {
                        active = false,
                        controllers = new()
                        { new Controller { id = RSFlipFlopNum + 2 } },
                        id = logicNum[i] + 4,
                        joints = null,
                        mode = (int)LogicOperand.AND
                    });
                    //reset
                    dict.Add(logicNum[i] + 5, new ControllerInfo
                    {
                        active = false,
                        controllers = new()
                        { new Controller { id = RSFlipFlopNum } },
                        id = logicNum[i] + 5,
                        joints = null,
                        mode = (int)LogicOperand.AND
                    });
                    dict.Add(logicNum[i] + 6, new ControllerInfo
                    {
                        active = false,
                        controllers = new()
                        { new Controller { id = logicNum[i] + 5 } },
                        id = logicNum[i] + 6,
                        joints = null,
                        mode = (int)LogicOperand.NOT
                    });
                    dict.Add(logicNum[i] + 7, new ControllerInfo
                    {
                        active = false,
                        controllers = new()
                        { new Controller { id = logicNum[i] + 4 }, new Controller { id = logicNum[i] + 6 } },
                        id = logicNum[i] + 7,
                        joints = null,
                        mode = (int)LogicOperand.OR
                    });
                    dict.Add(logicNum[i] + 8, new ControllerInfo
                    {
                        active = false,
                        controllers = new()
                        { new Controller { id = logicNum[i] + 7 } },
                        id = logicNum[i] + 8,
                        joints = null,
                        mode = (int)LogicOperand.AND
                    });
                    dict.Add(logicNum[i] + 9, new ControllerInfo
                    {
                        active = false,
                        controllers = new()
                        { new Controller { id = logicNum[i] + 7 } },
                        id = logicNum[i] + 9,
                        joints = null,
                        mode = (int)LogicOperand.AND
                    });
                    dict[RSFlipFlopNum + 1].controllers.Add(new Controller { id = logicNum[i] }); //RS - Q Add light
                    dict[RSFlipFlopNum + 1].controllers.Add(new Controller { id = logicNum[i] + 9 }); //RS - Q Add loopback
                    dict[0].controllers.Add(new Controller { id = logicNum[i] + 4 });
                    dict[0].controllers.Add(new Controller { id = logicNum[i] + 5 });
                    dict[1].controllers.Add(new Controller { id = RSFlipFlopNum });

                    //inputs
                    inputNums[i] = dict.Count;
                    for (int n = 0; n < 8; n++)
                    {
                        dict.Add(inputNums[i] + n, new ControllerInfo
                        {
                            active = false,
                            controllers = new() { },
                            id = inputNums[i] + n,
                            joints = null,
                            mode = (int)LogicOperand.AND
                        });
                    }

                    int isOddNum = dict.Count;
                    AddSingleConverger(dict, inputNums[i]);
                    int is2367Num = dict.Count;
                    AddDoubleConverger(dict, inputNums[i]);
                    int is012Num = dict.Count;
                    AddTripleConverger(dict, inputNums[i]);

                    dict[isOddNum].controllers.Add(new Controller { id = logicNum[i] + 8});
                    dict[is2367Num].controllers.Add(new Controller { id = logicNum[i] + 8 });
                    dict[is2367Num].controllers.Add(new Controller { id = logicNum[i] + 9 });
                    dict[is012Num].controllers.Add(new Controller { id = logicNum[i] + 9 });

                    int buttonNum = dict.Count;
                    dict.Add(buttonNum, new ControllerInfo
                    {
                        controllers = new()
                        { new Controller { id = RSFlipFlopNum + 2 } },
                        id = buttonNum,
                        joints = null,
                    });


                    i++;
                    Console.WriteLine(dict.Count);
                }
            }
            i = 0;
            // Connect all of the gates inputs togeter
            for (int y = 0; y < oneSideLenght; y++)
            {
                for (int x = 0; x < oneSideLenght; x++)
                {
                    for (int l = 0; l < 8; l++)
                    {
                        //get the index of each letters
                        int n = ABCindexes[i][l];
                        dict[outputNums[n]].controllers.Add(new Controller { id = inputNums[i] + l });
                    }
                    i++;
                }
            }

            return setObjectsGameOfLife(dict, oneSideLenght);

        }
        static void AddSingleConverger(Dictionary<int, ControllerInfo> dict, int inputNum)
        {
            int logicNum = dict.Count;
            dict.Add(logicNum, new ControllerInfo
            {
                active = false,
                controllers = new() { },
                id = logicNum,
                joints = null,
                mode = (int)LogicOperand.XOR
            });
            for (int n = 0; n < 8; n++)
            {
                dict[inputNum + n].controllers.Add(new Controller { id = logicNum });
            }
        }
        static void AddDoubleConverger(Dictionary<int, ControllerInfo> dict, int inputNum)
        {
            int logicNum = dict.Count;
            dict.Add(logicNum, new ControllerInfo
            {
                active = false,
                controllers = new() { },
                id = logicNum,
                joints = null,
                mode = (int)LogicOperand.XOR
            });
            int i = 1;
            for (int a = 0; a < 7; a++)
            {
                for (int b = 1 + a; b < 8; b++)
                {
                    dict.Add(logicNum + i, new ControllerInfo
                    {
                        active = false,
                        controllers = new() { new Controller { id = logicNum } },
                        id = logicNum + i,
                        joints = null,
                        mode = (int)LogicOperand.AND
                    });
                    dict[inputNum + a].controllers.Add(new Controller { id = logicNum + i });
                    dict[inputNum + b].controllers.Add(new Controller { id = logicNum + i });
                    i++;
                }
            }
        }
        static void AddTripleConverger(Dictionary<int, ControllerInfo> dict, int inputNum)
        {
            int logicNum = dict.Count;
            dict.Add(logicNum, new ControllerInfo
            {
                active = false,
                controllers = new() { },
                id = logicNum,
                joints = null,
                mode = (int)LogicOperand.NOR
            });
            int i = 1;
            for (int a = 0; a < 6; a++)
            {
                for (int b = 1 + a; b < 7; b++)
                {
                    for (int c = 1 + b; c < 8; c++)
                    {
                        dict.Add(logicNum + i, new ControllerInfo
                        {
                            active = false,
                            controllers = new() { new Controller { id = logicNum } },
                            id = logicNum + i,
                            joints = null,
                            mode = (int)LogicOperand.AND
                        });
                        dict[inputNum + a].controllers.Add(new Controller { id = logicNum + i });
                        dict[inputNum + b].controllers.Add(new Controller { id = logicNum + i });
                        dict[inputNum + c].controllers.Add(new Controller { id = logicNum + i });
                        i++;
                    }
                }
            }
        }
        static string getFromInt(int val)
        {
            switch (val)
            {
                case 0: return "A";
                case 1: return "B";
                case 2: return "C";
                case 3: return "D";
                case 4: return "E";
                case 5: return "F";
                case 6: return "G";
                case 7: return "H";
            }
            return "X";
        }
        List<DefaultObjectStruct> setObjectsGameOfLife(Dictionary<int, ControllerInfo> infos, short oneSideLenght)
        {
            
            i = 0;
            objects = new List<DefaultObjectStruct>();
            addLogicObject(infos[0], new Position { x = -1, y = 0, z = 0 });
            addLogicObject(infos[1], new Position { x = -2, y = 0, z = 0 });

            for (int y = 0; y < oneSideLenght; y++)
            {
                for (int x = 0; x < oneSideLenght; x++)
                {
                    if (infos.TryGetValue(i, out var controlleri))
                        addLightObject(controlleri, new Position { x = x, y = 0, z = y });
                    for (int z = 1; z < 105; z++)
                    {
                        if (infos.TryGetValue(i, out var controllerj))
                            addLogicObject(controllerj, new Position { x = x, y = z, z = y });
                    }
                    if (infos.TryGetValue(i, out var controllerk))
                        addButtonObject(controllerk, new Position { x = x + oneSideLenght + 1, y = 0, z = y });
                }
            }
            return objects;
        }

        List<DefaultObjectStruct> setObjectsRAM(Dictionary<int, ControllerInfo> infos, short bus)
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
        List<DefaultObjectStruct> create8bRam(short bus)
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

            return setObjectsRAM(dict, bus);
        }
        static void createRSFlipFlop(Dictionary<int, ControllerInfo> dict)
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
        static void createBC(Dictionary<int, ControllerInfo> dict)
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
        static void createEqualCompare(Dictionary<int, ControllerInfo> dict, short bus, int compareValue)
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
}
