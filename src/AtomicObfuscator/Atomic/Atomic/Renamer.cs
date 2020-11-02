using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic.Atomic
{
	// Token: 0x0200000F RID: 15
	internal class Renamer
	{
		// Token: 0x0200002A RID: 42
		public interface IRenaming
		{
			// Token: 0x060000F0 RID: 240
			ModuleDefMD Rename(ModuleDefMD module);
		}

		// Token: 0x0200002B RID: 43
		public static class Generator
		{
			// Token: 0x060000F1 RID: 241 RVA: 0x0000B878 File Offset: 0x00009A78
			public static string GenerateString()
			{
				int num = 2;
				byte[] array = new byte[num];
				new RNGCryptoServiceProvider().GetBytes(array);
				string str = null;
				return str + Renamer.Generator.EncodeString(array, Renamer.Generator.unicodeCharset);
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x0000B8B4 File Offset: 0x00009AB4
			private static string EncodeString(byte[] buff, char[] charset)
			{
				int i = (int)buff[0];
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 1; j < buff.Length; j++)
				{
					for (i = (i << 8) + (int)buff[j]; i >= charset.Length; i /= charset.Length)
					{
						stringBuilder.Append(charset[i % charset.Length]);
					}
				}
				bool flag = i != 0;
				if (flag)
				{
					stringBuilder.Append(charset[i % charset.Length]);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x0000B934 File Offset: 0x00009B34
			public static byte[] GetBytes(int lenght)
			{
				byte[] array = new byte[lenght];
				new RNGCryptoServiceProvider().GetBytes(array);
				return array;
			}

			// Token: 0x04000080 RID: 128
			private static readonly char[] unicodeCharset = new char[0].Concat(from ord in Enumerable.Range(8203, 5)
			select (char)ord).Concat(from ord in Enumerable.Range(8233, 6)
			select (char)ord).Concat(from ord in Enumerable.Range(8298, 6)
			select (char)ord).Except(new char[]
			{
				'搷'
			}).ToArray<char>();
		}

		// Token: 0x0200002C RID: 44
		public static class Renamer3
		{
			// Token: 0x060000F5 RID: 245 RVA: 0x0000B9FC File Offset: 0x00009BFC
			public static ModuleDef Rename(ModuleDef mod)
			{
				ModuleDefMD module = (ModuleDefMD)mod;
				Renamer.IRenaming renaming = new Renamer.NamespacesRenaming();
				module = renaming.Rename(module);
				renaming = new Renamer.ClassesRenaming();
				module = renaming.Rename(module);
				renaming = new Renamer.MethodsRenaming();
				module = renaming.Rename(module);
				renaming = new Renamer.PropertiesRenaming();
				module = renaming.Rename(module);
				renaming = new Renamer.FieldsRenaming();
				return renaming.Rename(module);
			}
		}

		// Token: 0x0200002D RID: 45
		public class FieldsRenaming : Renamer.IRenaming
		{
			// Token: 0x060000F6 RID: 246 RVA: 0x0000BA5C File Offset: 0x00009C5C
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						foreach (FieldDef fieldDef in typeDef.Fields)
						{
							string text;
							bool flag = Renamer.FieldsRenaming._names.TryGetValue(fieldDef.Name, out text);
							if (flag)
							{
								fieldDef.Name = text;
							}
							else
							{
								string text2 = Renamer.Generator.GenerateString();
								Renamer.FieldsRenaming._names.Add(fieldDef.Name, text2);
								fieldDef.Name = text2;
							}
						}
					}
				}
				return Renamer.FieldsRenaming.ApplyChangesToResources(module);
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x0000BB6C File Offset: 0x00009D6C
			private static ModuleDefMD ApplyChangesToResources(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						foreach (MethodDef methodDef in typeDef.Methods)
						{
							bool flag = methodDef.Name != "InitializeComponent";
							if (!flag)
							{
								IList<Instruction> instructions = methodDef.Body.Instructions;
								for (int i = 0; i < instructions.Count - 3; i++)
								{
									bool flag2 = instructions[i].OpCode == OpCodes.Ldstr;
									if (flag2)
									{
										foreach (KeyValuePair<string, string> keyValuePair in Renamer.FieldsRenaming._names)
										{
											bool flag3 = keyValuePair.Key == instructions[i].Operand.ToString();
											if (flag3)
											{
												instructions[i].Operand = keyValuePair.Value;
											}
										}
									}
								}
							}
						}
					}
				}
				return module;
			}

			// Token: 0x04000081 RID: 129
			private static Dictionary<string, string> _names = new Dictionary<string, string>();
		}

		// Token: 0x0200002E RID: 46
		public class ClassesRenaming : Renamer.IRenaming
		{
			// Token: 0x060000FA RID: 250 RVA: 0x0000BD40 File Offset: 0x00009F40
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						bool flag = typeDef.Name == "GeneratedInternalTypeHelper" || typeDef.Name == "Resources" || typeDef.Name == "Settings";
						if (!flag)
						{
							string text;
							bool flag2 = Renamer.ClassesRenaming._names.TryGetValue(typeDef.Name, out text);
							if (flag2)
							{
								typeDef.Name = text;
							}
							else
							{
								string text2 = Renamer.Generator.GenerateString();
								Renamer.ClassesRenaming._names.Add(typeDef.Name, text2);
								typeDef.Name = text2;
							}
						}
					}
				}
				return Renamer.ClassesRenaming.ApplyChangesToResources(module);
			}

			// Token: 0x060000FB RID: 251 RVA: 0x0000BE4C File Offset: 0x0000A04C
			private static ModuleDefMD ApplyChangesToResources(ModuleDefMD module)
			{
				foreach (Resource resource in module.Resources)
				{
					foreach (KeyValuePair<string, string> keyValuePair in Renamer.ClassesRenaming._names)
					{
						bool flag = resource.Name.Contains(keyValuePair.Key);
						if (flag)
						{
							resource.Name = resource.Name.Replace(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				foreach (TypeDef typeDef in module.GetTypes())
				{
					foreach (PropertyDef propertyDef in typeDef.Properties)
					{
						bool flag2 = propertyDef.Name != "ResourceManager";
						if (!flag2)
						{
							IList<Instruction> instructions = propertyDef.GetMethod.Body.Instructions;
							for (int i = 0; i < instructions.Count; i++)
							{
								bool flag3 = instructions[i].OpCode == OpCodes.Ldstr;
								if (flag3)
								{
									foreach (KeyValuePair<string, string> keyValuePair2 in Renamer.ClassesRenaming._names)
									{
										bool flag4 = instructions[i].Operand.ToString().Contains(keyValuePair2.Key);
										if (flag4)
										{
											instructions[i].Operand = instructions[i].Operand.ToString().Replace(keyValuePair2.Key, keyValuePair2.Value);
										}
									}
								}
							}
						}
					}
				}
				return module;
			}

			// Token: 0x04000082 RID: 130
			private static Dictionary<string, string> _names = new Dictionary<string, string>();
		}

		// Token: 0x0200002F RID: 47
		public class MethodsRenaming : Renamer.IRenaming
		{
			// Token: 0x060000FE RID: 254 RVA: 0x0000C104 File Offset: 0x0000A304
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						bool flag = typeDef.Name == "GeneratedInternalTypeHelper";
						if (!flag)
						{
							foreach (MethodDef methodDef in typeDef.Methods)
							{
								bool flag2 = !methodDef.HasBody;
								if (!flag2)
								{
									bool flag3 = methodDef.Name == ".ctor" || methodDef.Name == ".cctor";
									if (!flag3)
									{
										methodDef.Name = Renamer.Generator.GenerateString();
									}
								}
							}
						}
					}
				}
				return module;
			}
		}

		// Token: 0x02000030 RID: 48
		public class NamespacesRenaming : Renamer.IRenaming
		{
			// Token: 0x06000100 RID: 256 RVA: 0x0000C228 File Offset: 0x0000A428
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						bool flag = typeDef.Namespace == "";
						if (!flag)
						{
							string text;
							bool flag2 = Renamer.NamespacesRenaming._names.TryGetValue(typeDef.Namespace, out text);
							if (flag2)
							{
								typeDef.Namespace = text;
							}
							else
							{
								string text2 = Renamer.Generator.GenerateString();
								Renamer.NamespacesRenaming._names.Add(typeDef.Namespace, text2);
								typeDef.Namespace = text2;
							}
						}
					}
				}
				return Renamer.NamespacesRenaming.ApplyChangesToResources(module);
			}

			// Token: 0x06000101 RID: 257 RVA: 0x0000C308 File Offset: 0x0000A508
			private static ModuleDefMD ApplyChangesToResources(ModuleDefMD module)
			{
				foreach (Resource resource in module.Resources)
				{
					foreach (KeyValuePair<string, string> keyValuePair in Renamer.NamespacesRenaming._names)
					{
						bool flag = resource.Name.Contains(keyValuePair.Key);
						if (flag)
						{
							resource.Name = resource.Name.Replace(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				foreach (TypeDef typeDef in module.GetTypes())
				{
					foreach (PropertyDef propertyDef in typeDef.Properties)
					{
						bool flag2 = propertyDef.Name != "ResourceManager";
						if (!flag2)
						{
							IList<Instruction> instructions = propertyDef.GetMethod.Body.Instructions;
							for (int i = 0; i < instructions.Count; i++)
							{
								bool flag3 = instructions[i].OpCode == OpCodes.Ldstr;
								if (flag3)
								{
									foreach (KeyValuePair<string, string> keyValuePair2 in Renamer.NamespacesRenaming._names)
									{
										bool flag4 = instructions[i].ToString().Contains(keyValuePair2.Key);
										if (flag4)
										{
											instructions[i].Operand = instructions[i].Operand.ToString().Replace(keyValuePair2.Key, keyValuePair2.Value);
										}
									}
								}
							}
						}
					}
				}
				return module;
			}

			// Token: 0x04000083 RID: 131
			private static Dictionary<string, string> _names = new Dictionary<string, string>();
		}

		// Token: 0x02000031 RID: 49
		public class PropertiesRenaming : Renamer.IRenaming
		{
			// Token: 0x06000104 RID: 260 RVA: 0x0000C5BC File Offset: 0x0000A7BC
			public ModuleDefMD Rename(ModuleDefMD module)
			{
				foreach (TypeDef typeDef in module.GetTypes())
				{
					bool isGlobalModuleType = typeDef.IsGlobalModuleType;
					if (!isGlobalModuleType)
					{
						foreach (PropertyDef propertyDef in typeDef.Properties)
						{
							propertyDef.Name = Renamer.Generator.GenerateString();
						}
					}
				}
				return module;
			}
		}
	}
}
