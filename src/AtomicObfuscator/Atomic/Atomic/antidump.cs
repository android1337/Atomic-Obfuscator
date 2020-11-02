using System;
using System.Collections.Generic;
using System.Linq;
using AtomicProtector.Atomic;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic.Atomic
{
	// Token: 0x02000008 RID: 8
	internal class antidump
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00005284 File Offset: 0x00003484
		public static void InjectClass(ModuleDef module)
		{
			ModuleDefMD moduleDefMD = ModuleDefMD.Load(typeof(anti_dump_runtime).Module);
			TypeDef typeDef = moduleDefMD.ResolveTypeDef(MDToken.ToRID(typeof(anti_dump_runtime).MetadataToken));
			IEnumerable<IDnlibDef> source = InjectHelper1.InjectHelper.Inject(typeDef, module.GlobalType, module);
			antidump.init = (MethodDef)source.Single((IDnlibDef method) => method.Name == "Initialize");
			foreach (MethodDef methodDef in module.GlobalType.Methods)
			{
				bool flag = methodDef.Name == ".ctor";
				if (flag)
				{
					module.GlobalType.Remove(methodDef);
					break;
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000536C File Offset: 0x0000356C
		public static void Execute(ModuleDef module)
		{
			antidump.InjectClass(module);
			MethodDef methodDef = module.GlobalType.FindOrCreateStaticConstructor();
			methodDef.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, antidump.init));
		}

		// Token: 0x0400002E RID: 46
		public static MethodDef init;
	}
}
