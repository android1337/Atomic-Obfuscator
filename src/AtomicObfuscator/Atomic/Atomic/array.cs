using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic.Atomic
{
	// Token: 0x02000009 RID: 9
	internal class array
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000053B8 File Offset: 0x000035B8
		public static bool IsSafe(List<Instruction> instructions, int i)
		{
			bool flag = new int[]
			{
				-2,
				-1,
				0,
				1,
				2
			}.Contains(instructions[i].GetLdcI4Value());
			return !flag;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000053F8 File Offset: 0x000035F8
		public static void Array(MethodDef method)
		{
			array.Index = 0;
			array.Integers_Position = new List<int>();
			array.Integers = new List<int>();
			array.instr = new List<Instruction>();
			array.instr.Add(Instruction.Create(OpCodes.Ldc_I4, array.Index));
			array.instr.Add(Instruction.Create(OpCodes.Newarr, method.Module.Import(typeof(int))));
			array.instr.Add(OpCodes.Dup.ToInstruction());
			Local local = new Local(method.Module.CorLibTypes.Int32);
			method.Body.Variables.Add(local);
			int i = 0;
			while (i < method.Body.Instructions.Count)
			{
				bool flag = method.Body.Instructions[i].OpCode == OpCodes.Ldc_I4;
				if (flag)
				{
					bool flag2 = !array.IsSafe(method.Body.Instructions.ToList<Instruction>(), i);
					if (!flag2)
					{
						int num = array.rnd.Next(50, 500);
						array.Integers.Add(method.Body.Instructions[i].GetLdcI4Value() ^ num);
						array.Integers.Add(num);
						array.Integers_Position.Add(i);
						method.Body.Instructions[i] = new Instruction(OpCodes.Ldstr, "66636");
						method.Body.Instructions.Insert(i + 1, Instruction.Create(OpCodes.Call, method.Module.Import(typeof(int).GetMethod("Parse", new Type[]
						{
							typeof(string)
						}))));
						array.instr.Add(OpCodes.Ldc_I4.ToInstruction(array.Index));
						array.instr.Add(new Instruction(OpCodes.Ldc_I4, array.Integers[array.Index]));
						array.instr.Add(OpCodes.Stelem_I4.ToInstruction());
						array.instr.Add(OpCodes.Dup.ToInstruction());
						array.instr.Add(OpCodes.Ldc_I4.ToInstruction(array.Index + 1));
						array.instr.Add(new Instruction(OpCodes.Ldc_I4, array.Integers[array.Index + 1]));
						array.instr.Add(OpCodes.Stelem_I4.ToInstruction());
						array.instr.Add(OpCodes.Dup.ToInstruction());
						array.Index += 2;
					}
				}
				IL_2A6:
				i++;
				continue;
				goto IL_2A6;
			}
			bool flag3 = array.instr[array.instr.Count - 1].OpCode == OpCodes.Dup && array.instr.Count > 4;
			if (flag3)
			{
				array.instr[array.instr.Count - 1] = new Instruction(OpCodes.Nop);
			}
			bool flag4 = array.instr.Count > 4;
			if (flag4)
			{
				array.instr[0].Operand = array.Index;
			}
			array.instr.Add(Instruction.Create(OpCodes.Stloc_S, local));
			int num2 = 0;
			int num3 = 0;
			for (int j = 0; j < method.Body.Instructions.Count; j++)
			{
				IMethod method2 = method.Module.Import(typeof(int).GetMethod("Parse", new Type[]
				{
					typeof(string)
				}));
				string b = "66636";
				bool flag5 = method.Body.Instructions[j].Operand == null;
				if (!flag5)
				{
					bool flag6 = method.Body.Instructions[j].Operand.ToString() == method2.ToString();
					if (flag6)
					{
						try
						{
							bool flag7 = method.Body.Instructions[j - 1].Operand.ToString() == b;
							if (flag7)
							{
								bool flag8 = num2 == 0;
								if (flag8)
								{
									int num4 = 0;
									foreach (Instruction item in array.instr)
									{
										method.Body.Instructions.Insert(num4, item);
										num4++;
										j++;
										num2++;
									}
								}
								method.Body.Instructions[j] = new Instruction(OpCodes.Ldloc_S, local);
								method.Body.Instructions.Insert(j + 1, OpCodes.Ldc_I4.ToInstruction(num3));
								method.Body.Instructions.Insert(j + 2, OpCodes.Ldelem_I4.ToInstruction());
								method.Body.Instructions.Insert(j + 3, Instruction.Create(OpCodes.Ldloc_S, local));
								method.Body.Instructions.Insert(j + 4, OpCodes.Ldc_I4.ToInstruction(num3 + 1));
								method.Body.Instructions.Insert(j + 5, OpCodes.Ldelem_I4.ToInstruction());
								method.Body.Instructions.Insert(j + 6, OpCodes.Xor.ToInstruction());
								num3 += 2;
								method.Body.Instructions.RemoveAt(j - 1);
							}
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x0400002F RID: 47
		public static List<Instruction> instr = new List<Instruction>();

		// Token: 0x04000030 RID: 48
		private static List<int> Integers = new List<int>();

		// Token: 0x04000031 RID: 49
		private static List<int> Integers_Position = new List<int>();

		// Token: 0x04000032 RID: 50
		private static int Index = 0;

		// Token: 0x04000033 RID: 51
		private static Random rnd = new Random();
	}
}
