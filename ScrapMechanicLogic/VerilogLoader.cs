using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ScrapMechanicLogic.VoxelForm;

namespace ScrapMechanicLogic
{
    internal class VerilogLoader
    {
        public class Component
        {
            public string name { get; set; }
            public List<string> inputs { get; set; }
            public List<string> outputs { get; set; }
            public List<string> interns { get; set; }
            public List<GateComponent> gateComponents { get; set; }
        }
        public class GateComponent
        {
            public int gateID;
            public LogicOperand operand { get; set; }
            public int[] internalOutputs { get; set; }
            public int[] internalInputs { get; set; }
            public string[] externalOutputs { get; set; }
            public string[] externalInputs { get; set; }
        }
        public void openVerilog(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    List<Component> components = new List<Component>();

                    string content = File.ReadAllText(filePath);
                    int gateNum = 0;

                    // Replace any character before specific symbols with predefined placeholders
                    content = ReplaceSpecialSymbols(content);
                    Console.WriteLine(content);
                    // Regular expression to match Verilog module definition with special characters in module name
                    Regex moduleRegex = new Regex(@"\bmodule\s+([\\a-zA-Z_]\w*)\s*\(.*?\);", RegexOptions.Singleline);
                    Regex portRegex = new Regex(@"(\binput\b|\boutput\b)\s+([\\a-zA-Z_]\w*)(?:\s*,|\s*;)?\s*//?.*?(?=\n|$)", RegexOptions.Singleline);
                    Regex wireRegex = new Regex(@"\bwire\s+(\\?[a-zA-Z_]\w*)(?:\s*,|\s*;)?", RegexOptions.Singleline);
                    Regex assignRegex = new Regex(@"\bassign\s+(\\?[a-zA-Z_]\w*)\s*=\s*(.*?);", RegexOptions.Singleline);

                    Match moduleMatch = moduleRegex.Match(content);
                    if (moduleMatch.Success)
                    {
                        Component comp = new();

                        comp.outputs = new List<string>();
                        comp.inputs = new List<string>();
                        comp.interns = new List<string>();
                        comp.gateComponents = new List<GateComponent>();

                        string moduleName = moduleMatch.Groups[1].Value;
                        string portsContent = moduleMatch.Value;
                        comp.name = moduleName;
                        //Console.WriteLine($"Module Name: {moduleName}");

                        // Extract input and output ports
                        MatchCollection portMatches = portRegex.Matches(portsContent);
                        foreach (Match portMatch in portMatches)
                        {
                            string portType = portMatch.Groups[1].Value;
                            string portName = portMatch.Groups[2].Value;
                            switch (portType)
                            {
                                case "input":
                                    comp.inputs.Add(portName);
                                    break;
                                case "output":
                                    comp.outputs.Add(portName);
                                    break;
                                default:
                                    Console.WriteLine("Wrong portType");
                                    break;
                            }
                            //Console.WriteLine($"  {portType} Port: {portName}");
                        }

                        // Extract wires
                        MatchCollection wireMatches = wireRegex.Matches(content);
                        foreach (Match wireMatch in wireMatches)
                        {
                            string wireName = wireMatch.Groups[1].Value;
                            comp.interns.Add(wireName);
                            //Console.WriteLine($"  Wire: {wireName}");
                        }

                        // Extract assign statements
                        MatchCollection assignMatches = assignRegex.Matches(content);
                        foreach (Match assignMatch in assignMatches)
                        {
                            string assignedPort = assignMatch.Groups[1].Value;
                            string logic = assignMatch.Groups[2].Value;
                            //Console.WriteLine($"  Assign: {assignedPort} = {logic}");
                            List<GateComponent> newComps = new();
                            gateNum = DecomposeLogicExpression(logic, gateNum, assignedPort, out newComps);
                            comp.gateComponents.AddRange(newComps);
                        }
                        components.Add(comp);
                    }
                    else
                    {
                        Console.WriteLine("No module definition found.");
                    }
                }
                else
                {
                    Console.WriteLine("File not found: " + filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        static string ReplaceSpecialSymbols(string content)
        {
            // Replace any character before specific symbols with predefined placeholders
            content = Regex.Replace(content, @"\\([a-zA-Z_]\w*)<", @"$1_LESSTHAN_");
            content = Regex.Replace(content, @"\\([a-zA-Z_]\w*)=", @"$1_EQUALS_");
            content = Regex.Replace(content, @"\\([a-zA-Z_]\w*)>", @"$1_GREATHERTHAN_");
            return content;
        }
        public static int DecomposeLogicExpression(string expression, int gateNum, string baseOutput, out List<GateComponent> newComps)
        {
            newComps = new List<GateComponent>();

            GateComponent baseComp = new GateComponent();
            baseComp.gateID = gateNum;
            baseComp.externalOutputs = new string[] { baseOutput };
            gateNum += 1;

            // Remove all spaces from the expression
            expression = expression.Replace(" ", "");

            // Regular expression pattern to match parentheses and logic operators
            string pattern = @"(\(|\)|[&|^~])|([A-Za-z]\w*)";

            // Extract tokens using regular expression
            List<string> tokens = new List<string>();
            MatchCollection matches = Regex.Matches(expression, pattern);
            foreach (Match match in matches)
            {
                // Use the first capturing group if available, else use the second one (for operators and variables respectively)
                string token = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value;
                tokens.Add(token);
            }

            // Stack to keep track of operators and parentheses
            Stack<string> stack = new Stack<string>();

            foreach (string token in tokens)
            {
                if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        Console.WriteLine(stack.Pop());
                        GetLogicOperand(stack.Pop());

                        //gates.Add();
                    }
                    stack.Pop(); // Pop the opening parenthesis
                }
                else if (IsOperator(token))
                {
                    while (stack.Count > 0 && Precedence(token) <= Precedence(stack.Peek()))
                    {

                        Console.WriteLine(stack.Pop());
                        GetLogicOperand(stack.Pop());
                        //gates.Add();
                    }
                    stack.Push(token);
                }
                else
                {
                    newComps.Add(new GateComponent { });
                }
            }

            // Pop any remaining operators from the stack
            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());
                //gates.Add(GetLogicOperand(stack.Pop()));
            }

            return gateNum;
        }
        public static bool IsOperator(string token)
        {
            return token == "&" || token == "|" || token == "^" || token == "~";
        }
        public static int Precedence(string op)
        {
            if (op == "~") return 3;
            if (op == "&") return 2;
            if (op == "|") return 1;
            if (op == "^") return 1;
            return 0;
        }
        public static LogicOperand GetLogicOperand(string op)
        {
            LogicOperand gate;
            switch (op)
            {
                case "&":
                    gate = LogicOperand.AND;
                    break;
                case "|":
                    gate = LogicOperand.OR;
                    break;
                case "^":
                    gate = LogicOperand.XOR;
                    break;
                case "~":
                    gate = LogicOperand.NOT;
                    break;
                default:
                    gate = LogicOperand.AND;
                    break;
            }
            return gate;
        }
    }
}
