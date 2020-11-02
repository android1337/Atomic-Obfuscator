// Decompiled with JetBrains decompiler
// Type: AtomicProtector.Atomic.cflow.cfhelper
// Assembly: AtomicObfuscator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2B1F80D-5C6A-4DF3-B343-FE98A4C6DD2A
// Assembly location: C:\Users\liam_\Desktop\AtomicGay\Original\AtomicObfuscator.exe

using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtomicProtector.Atomic.cflow
{
    internal class cfhelper
    {
        private static Random geneartor = new Random();

        public static bool HasUnsafeInstructions(MethodDef methodDef)
        {
            return methodDef.HasBody && methodDef.Body.HasVariables && methodDef.Body.Variables.Any<Local>((Func<Local, bool>)(x => x.Type.IsPointer));
        }

        public static Blocks GetBlocks(MethodDef method)
        {
            Blocks blocks = new Blocks();
            Block block1 = new Block();
            int num1 = 0;
            int num2 = 0;
            block1.ID = num1;
            int num3 = num1 + 1;
            block1.nextBlock = block1.ID + 1;
            block1.instructions.Add(Instruction.Create(OpCodes.Nop));
            blocks.blocks.Add(block1);
            Block block2 = new Block();
            foreach (Instruction instruction in (IEnumerable<Instruction>)method.Body.Instructions)
            {
                int pops = 0;
                int pushes;
                instruction.CalculateStackUsage(out pushes, out pops);
                block2.instructions.Add(instruction);
                num2 += pushes - pops;
                if (pushes == 0 && instruction.OpCode != OpCodes.Nop && (num2 == 0 || instruction.OpCode == OpCodes.Ret))
                {
                    block2.ID = num3;
                    ++num3;
                    block2.nextBlock = block2.ID + 1;
                    blocks.blocks.Add(block2);
                    block2 = new Block();
                }
            }
            return blocks;
        }

        public static List<Instruction> Calc(int value)
        {
            List<Instruction> instructionList = new List<Instruction>();
            int num1 = cfhelper.geneartor.Next(0, 100000);
            bool boolean = Convert.ToBoolean(cfhelper.geneartor.Next(0, 2));
            int num2 = cfhelper.geneartor.Next(0, 100000);
            instructionList.Add(Instruction.Create(OpCodes.Ldc_I4, value - num1 + (boolean ? -num2 : num2)));
            instructionList.Add(Instruction.Create(OpCodes.Ldc_I4, num1));
            instructionList.Add(Instruction.Create(OpCodes.Add));
            instructionList.Add(Instruction.Create(OpCodes.Ldc_I4, num2));
            instructionList.Add(Instruction.Create(boolean ? OpCodes.Add : OpCodes.Sub));
            return instructionList;
        }
    }
}
