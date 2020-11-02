using System;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic.Atomic
{
	// Token: 0x0200000A RID: 10
	internal class cflow
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00005A34 File Offset: 0x00003C34
		public static void Execute(ModuleDef module)
		{
			foreach (TypeDef typeDef in module.Types)
			{
				foreach (MethodDef methodDef in typeDef.Methods.ToArray<MethodDef>())
				{
					bool flag = methodDef.HasBody && methodDef.Body.HasInstructions && !methodDef.Body.HasExceptionHandlers;
					if (flag)
					{
						for (int j = 0; j < methodDef.Body.Instructions.Count - 2; j++)
						{
							Instruction instruction = methodDef.Body.Instructions[j + 1];
							methodDef.Body.Instructions.Insert(j + 1, Instruction.Create(OpCodes.Ldstr, "Atomic"));
							methodDef.Body.Instructions.Insert(j + 1, Instruction.Create(OpCodes.Br_S, instruction));
							j += 2;
						}
					}
				}
			}
		}
	}
}
