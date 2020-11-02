using System;
using System.Collections.Generic;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x02000020 RID: 32
	internal class control_flow
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000A794 File Offset: 0x00008994
		public static void toDoBody(MethodDef method, Blocks blocks)
		{
			List<Instruction> list = new List<Instruction>();
			method.Body.Instructions.Clear();
			Local local = new Local(method.Module.CorLibTypes.Int32);
			method.Body.Variables.Add(local);
			foreach (Instruction item in cfhelper.Calc(0))
			{
				method.Body.Instructions.Add(item);
			}
			method.Body.Instructions.Add(Instruction.Create(OpCodes.Stloc, local));
			Instruction instruction = Instruction.Create(OpCodes.Nop);
			method.Body.Instructions.Add(instruction);
			method.Body.Instructions.Add(Instruction.Create(OpCodes.Ldloc, local));
			Instruction instruction2 = Instruction.Create(OpCodes.Switch, list);
			method.Body.Instructions.Add(instruction2);
			method.Body.Instructions.Add(Instruction.Create(OpCodes.Br, blocks.getBlock(blocks.blocks.Count - 1).instructions[blocks.getBlock(blocks.blocks.Count - 1).instructions.Count - 1]));
			foreach (Block block in blocks.blocks)
			{
				bool flag = block != blocks.getBlock(blocks.blocks.Count - 1);
				if (flag)
				{
					Instruction item2 = Instruction.Create(OpCodes.Stloc, local);
					list.Add(block.instructions[0]);
					Instruction item3 = Instruction.Create(OpCodes.Nop);
					foreach (Instruction item4 in block.instructions)
					{
						method.Body.Instructions.Add(item4);
					}
					foreach (Instruction item5 in cfhelper.Calc(block.nextBlock))
					{
						method.Body.Instructions.Add(item5);
					}
					method.Body.Instructions.Add(item2);
					method.Body.Instructions.Add(item3);
					method.Body.Instructions.Add(new Instruction(OpCodes.Br, instruction));
					method.Body.Instructions.Add(item3);
				}
			}
			method.Body.Instructions.Add(Instruction.Create(OpCodes.Br, blocks.getBlock(blocks.blocks.Count - 1).instructions[0]));
			method.Body.Instructions.Add(new Instruction(OpCodes.Br, instruction));
			foreach (Instruction item6 in blocks.getBlock(blocks.blocks.Count - 1).instructions)
			{
				method.Body.Instructions.Add(item6);
			}
			list.Add(blocks.getBlock(blocks.blocks.Count - 1).instructions[0]);
			instruction2.Operand = list;
		}
	}
}
