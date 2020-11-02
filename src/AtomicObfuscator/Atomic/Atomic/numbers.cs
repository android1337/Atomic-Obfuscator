using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic.Atomic
{
	// Token: 0x0200000E RID: 14
	internal class numbers
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00006984 File Offset: 0x00004B84
		public static void InjectClass(ModuleDef module)
		{
			ModuleDefMD moduleDefMD = ModuleDefMD.Load(typeof(runtime).Module);
			TypeDef typeDef = moduleDefMD.ResolveTypeDef(MDToken.ToRID(typeof(runtime).MetadataToken));
			IEnumerable<IDnlibDef> source = InjectHelper1.InjectHelper.Inject(typeDef, module.GlobalType, module);
			numbers.init = (MethodDef)source.Single((IDnlibDef method) => method.Name == "Encrypt");
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

		// Token: 0x0600004E RID: 78 RVA: 0x00006A6C File Offset: 0x00004C6C
		public static void InjectClass1(ModuleDef module)
		{
			ModuleDefMD moduleDefMD = ModuleDefMD.Load(typeof(runtime).Module);
			TypeDef typeDef = moduleDefMD.ResolveTypeDef(MDToken.ToRID(typeof(runtime).MetadataToken));
			IEnumerable<IDnlibDef> source = InjectHelper1.InjectHelper.Inject(typeDef, module.GlobalType, module);
			numbers.init1 = (MethodDef)source.Single((IDnlibDef method) => method.Name == "Season");
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

		// Token: 0x0600004F RID: 79 RVA: 0x00006B54 File Offset: 0x00004D54
		public static string EncryptDecrypt(string szPlainText, int szEncryptionKey)
		{
			StringBuilder stringBuilder = new StringBuilder(szPlainText);
			StringBuilder stringBuilder2 = new StringBuilder(szPlainText.Length);
			for (int i = 0; i < szPlainText.Length; i++)
			{
				char c = stringBuilder[i];
				c = (char)((int)c ^ szEncryptionKey);
				stringBuilder2.Append(c);
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public static string Random(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("1234567890", length)
			select s[numbers.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00006BFC File Offset: 0x00004DFC
		public static void String(ModuleDef module)
		{
			numbers.InjectClass(module);
			foreach (TypeDef typeDef in module.GetTypes())
			{
				bool isGlobalModuleType = typeDef.IsGlobalModuleType;
				if (!isGlobalModuleType)
				{
					foreach (MethodDef methodDef in typeDef.Methods)
					{
						bool flag = !methodDef.HasBody;
						if (!flag)
						{
							IList<Instruction> instructions = methodDef.Body.Instructions;
							for (int i = 0; i < instructions.Count; i++)
							{
								bool flag2 = instructions[i].OpCode == OpCodes.Ldstr;
								if (flag2)
								{
									string s = numbers.Random(5);
									string szPlainText = (string)instructions[i].Operand;
									string text = numbers.EncryptDecrypt(szPlainText, int.Parse(s));
									text += "                                                                                                                                       ";
									instructions[i].Operand = text;
									instructions.Insert(i + 1, Instruction.Create(OpCodes.Ldc_I4, int.Parse(s)));
									instructions.Insert(i + 2, Instruction.Create(OpCodes.Call, numbers.init1));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public static double RandomDouble(double min, double max)
		{
			return numbers.rnd.NextDouble() * (max - min) + min;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00006DCC File Offset: 0x00004FCC
		public static void InlineInteger(MethodDef method, int i)
		{
			bool isGlobalModuleType = method.DeclaringType.IsGlobalModuleType;
			if (!isGlobalModuleType)
			{
				IList<Instruction> instructions = method.Body.Instructions;
				try
				{
					bool flag = instructions[i - 1].OpCode == OpCodes.Callvirt;
					if (flag)
					{
						bool flag2 = instructions[i + 1].OpCode == OpCodes.Call;
						if (flag2)
						{
							return;
						}
					}
					bool flag3 = instructions[i + 4].IsBr();
					if (!flag3)
					{
						bool flag4 = true;
						int num = numbers.random.Next(0, 2);
						int num2 = num;
						if (num2 != 0)
						{
							if (num2 == 1)
							{
								flag4 = false;
							}
						}
						else
						{
							flag4 = true;
						}
						Local local = new Local(method.Module.CorLibTypes.String);
						method.Body.Variables.Add(local);
						Local local2 = new Local(method.Module.CorLibTypes.Int32);
						method.Body.Variables.Add(local2);
						int ldcI4Value = instructions[i].GetLdcI4Value();
						string text = Renamer.Generator.GenerateString();
						instructions.Insert(i, Instruction.Create(OpCodes.Ldloc_S, local2));
						instructions.Insert(i, Instruction.Create(OpCodes.Stloc_S, local2));
						bool flag5 = flag4;
						if (flag5)
						{
							instructions.Insert(i, Instruction.Create(OpCodes.Ldc_I4, ldcI4Value));
							instructions.Insert(i, Instruction.Create(OpCodes.Ldc_I4, ldcI4Value + 1));
						}
						else
						{
							instructions.Insert(i, Instruction.Create(OpCodes.Ldc_I4, ldcI4Value + 1));
							instructions.Insert(i, Instruction.Create(OpCodes.Ldc_I4, ldcI4Value));
						}
						instructions.Insert(i, Instruction.Create(OpCodes.Call, method.Module.Import(typeof(string).GetMethod("op_Equality", new Type[]
						{
							typeof(string),
							typeof(string)
						}))));
						instructions.Insert(i, Instruction.Create(OpCodes.Ldstr, text));
						instructions.Insert(i, Instruction.Create(OpCodes.Ldloc_S, local));
						instructions.Insert(i, Instruction.Create(OpCodes.Stloc_S, local));
						bool flag6 = flag4;
						if (flag6)
						{
							instructions.Insert(i, Instruction.Create(OpCodes.Ldstr, text));
						}
						else
						{
							instructions.Insert(i, Instruction.Create(OpCodes.Ldstr, Renamer.Generator.GenerateString()));
						}
						instructions.Insert(i + 5, Instruction.Create(OpCodes.Brtrue_S, instructions[i + 6]));
						instructions.Insert(i + 7, Instruction.Create(OpCodes.Br_S, instructions[i + 8]));
						instructions.RemoveAt(i + 10);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00007098 File Offset: 0x00005298
		public static void encrypt(ModuleDef md)
		{
			foreach (TypeDef typeDef in md.Types.ToArray<TypeDef>())
			{
				foreach (MethodDef methodDef in typeDef.Methods.ToArray<MethodDef>())
				{
					bool flag = !methodDef.HasBody;
					if (!flag)
					{
						bool flag2 = !methodDef.Body.HasInstructions;
						if (!flag2)
						{
							bool flag3 = methodDef.DeclaringType == md.GlobalType;
							if (!flag3)
							{
								for (int k = 0; k < methodDef.Body.Instructions.Count; k++)
								{
									bool flag4 = methodDef.Body.Instructions[k].OpCode == OpCodes.Ldc_I4;
									if (flag4)
									{
										int ldcI4Value = methodDef.Body.Instructions[k].GetLdcI4Value();
										double value = numbers.RandomDouble(1.0, 1000.0);
										string text = Convert.ToString(value);
										double num = double.Parse(text);
										int num2 = ldcI4Value - (int)num;
										methodDef.Body.Instructions[k].Operand = num2;
										methodDef.Body.Instructions[k].OpCode = OpCodes.Ldc_I4;
										methodDef.Body.Instructions.Insert(k + 1, Instruction.Create(OpCodes.Ldstr, text));
										methodDef.Body.Instructions.Insert(k + 2, Instruction.Create(OpCodes.Ldc_I4, numbers.rnd.Next(1, 10000)));
										methodDef.Body.Instructions.Insert(k + 3, Instruction.Create(OpCodes.Call, numbers.init));
										methodDef.Body.Instructions.Insert(k + 4, OpCodes.Conv_I4.ToInstruction());
										methodDef.Body.Instructions.Insert(k + 5, Instruction.Create(OpCodes.Add));
										k += 5;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0400003A RID: 58
		public static MethodDef init;

		// Token: 0x0400003B RID: 59
		public static MethodDef init1;

		// Token: 0x0400003C RID: 60
		public static Random rnd = new Random();

		// Token: 0x0400003D RID: 61
		public static Random random = new Random();
	}
}
