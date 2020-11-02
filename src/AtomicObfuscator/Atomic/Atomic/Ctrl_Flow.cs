using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic.Atomic
{
	// Token: 0x0200000B RID: 11
	internal class Ctrl_Flow
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00005B8C File Offset: 0x00003D8C
		public static void Brs(ModuleDef md)
		{
			foreach (TypeDef typeDef in md.Types.ToArray<TypeDef>())
			{
				foreach (MethodDef methodDef in typeDef.Methods.ToArray<MethodDef>())
				{
					bool flag = methodDef.HasBody && methodDef.Body.Instructions.Count != 0;
					if (flag)
					{
						IList<Instruction> instructions = methodDef.Body.Instructions;
						int num = 0;
						if (num < instructions.Count)
						{
							Instruction instruction = Instruction.Create(OpCodes.Nop);
							Instruction instruction2 = OpCodes.Call.ToInstruction(md.Import(typeof(string).GetMethod("IsNullOrEmpty", new Type[]
							{
								typeof(string)
							})));
						}
					}
				}
			}
		}

		// Token: 0x04000034 RID: 52
		public static List<Instruction> instr = new List<Instruction>();

		// Token: 0x04000035 RID: 53
		private static List<int> Integers = new List<int>();

		// Token: 0x04000036 RID: 54
		private static List<int> Integers_Position = new List<int>();

		// Token: 0x04000037 RID: 55
		private static int Index = 0;

		// Token: 0x04000038 RID: 56
		private static Random rnd = new Random();
	}
}
