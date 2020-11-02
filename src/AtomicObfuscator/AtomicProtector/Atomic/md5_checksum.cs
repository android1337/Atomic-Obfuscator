using System;
using System.Collections.Generic;
using System.Linq;
using Atomic;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace AtomicProtector.Atomic
{
	// Token: 0x02000012 RID: 18
	internal class md5_checksum
	{
		// Token: 0x06000062 RID: 98 RVA: 0x0000798C File Offset: 0x00005B8C
		public static void InjectClass(ModuleDef module)
		{
			ModuleDefMD moduleDefMD = ModuleDefMD.Load(typeof(md5_runtime).Module);
			TypeDef typeDef = moduleDefMD.ResolveTypeDef(MDToken.ToRID(typeof(md5_runtime).MetadataToken));
			IEnumerable<IDnlibDef> source = InjectHelper1.InjectHelper.Inject(typeDef, module.GlobalType, module);
			md5_checksum.init = (MethodDef)source.Single((IDnlibDef method) => method.Name == "AtomicOnGod");
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

		// Token: 0x06000063 RID: 99 RVA: 0x00007A74 File Offset: 0x00005C74
		public static void Execute(ModuleDef module)
		{
			md5_checksum.InjectClass(module);
			MethodDef methodDef = module.GlobalType.FindOrCreateStaticConstructor();
			methodDef.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, md5_checksum.init));
		}

		// Token: 0x0400003E RID: 62
		public static MethodDef init;
	}
}
