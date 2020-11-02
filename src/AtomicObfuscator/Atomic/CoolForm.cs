using Atomic;
using Atomic.Atomic;
using AtomicProtector.Atomic.cflow;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtomicObfuscator.Atomic
{
    public partial class CoolForm : Form
    {
        public CoolForm()
        {
            InitializeComponent();
        }

		public const int WM_NCLBUTTONDOWN = 161;

		// Token: 0x04000005 RID: 5
		public const int HT_CAPTION = 2;

		// Token: 0x04000006 RID: 6
		private bool Strings = false;

		// Token: 0x04000007 RID: 7
		private bool Numbers = false;

		// Token: 0x04000008 RID: 8
		private bool Cflow = false;

		// Token: 0x04000009 RID: 9
		private bool Renamer = false;

		// Token: 0x0400000A RID: 10
		private bool Mutation = false;

		// Token: 0x0400000B RID: 11
		private bool Packer = false;

		// Token: 0x0400000C RID: 12
		private bool AntiDumper = false;

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.Numbers = !this.Numbers;
        }

		private void checkBox6_CheckedChanged(object sender, EventArgs e)
		{
			this.Strings = !this.Strings;
		}

		private void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			this.Cflow = !this.Cflow;
		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			this.Renamer = !this.Renamer;
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			this.Mutation = !this.Mutation;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.Packer = !this.Packer;
		}

		public string DirectoryName = "";

		private void TextBox1_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				var array = (Array)e.Data.GetData(DataFormats.FileDrop);
				if (array == null)
					return;
				var text = array.GetValue(0).ToString();
				var num = text.LastIndexOf(".", StringComparison.Ordinal);
				if (num == -1)
					return;
				var text2 = text.Substring(num);
				text2 = text2.ToLower();
				if (text2 != ".exe" && text2 != ".dll")
					return;
				Activate();
				textBox1.Text = text;
				var num2 = text.LastIndexOf("\\", StringComparison.Ordinal);
				if (num2 != -1)
				{
					DirectoryName = text.Remove(num2, text.Length - num2);
				}

				if (DirectoryName.Length == 2)
				{
					DirectoryName += "\\";
				}
			}
			catch
			{
				/* ignored */
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ModuleDef moduleDef = ModuleDefMD.Load(this.textBox1.Text);
			bool cflow = this.Cflow;

			foreach (TypeDef typeDef in moduleDef.Types.ToArray<TypeDef>())
			{
				foreach (MethodDef methodDef in typeDef.Methods.ToArray<MethodDef>())
				{
					bool flag = methodDef.HasBody && methodDef.Body.Instructions.Count != 0;
					if (flag)
					{
						bool numbers2 = this.Numbers;
						if (numbers2)
						{
							array.Array(methodDef);
						}
					}
				}
			}
			foreach (TypeDef typeDef2 in moduleDef.Types.ToArray<TypeDef>())
			{
				foreach (MethodDef methodDef2 in typeDef2.Methods.ToArray<MethodDef>())
				{
					bool flag2 = methodDef2.HasBody && methodDef2.Body.Instructions.Count > 0 && !methodDef2.IsConstructor;
					if (flag2)
					{
						bool cflow2 = this.Cflow;
						if (cflow2)
						{
							bool flag3 = !cfhelper.HasUnsafeInstructions(methodDef2);
							if (flag3)
							{
								bool flag4 = Form1.Simplify(methodDef2);
								if (flag4)
								{
									Blocks blocks = cfhelper.GetBlocks(methodDef2);
									bool flag5 = blocks.blocks.Count != 1;
									if (flag5)
									{
										control_flow.toDoBody(methodDef2, blocks);
										break;
									}
								}
								Form1.Optimize(methodDef2);
							}
						}
					}
				}
			}
			bool cflow3 = this.Cflow;

			bool mutation2 = this.Mutation;
			if (mutation2)
			{
				foreach (TypeDef typeDef3 in moduleDef.Types.ToArray<TypeDef>())
				{
					foreach (MethodDef methodDef3 in typeDef3.Methods.ToArray<MethodDef>())
					{
						bool flag6 = methodDef3.HasBody && methodDef3.Body.Instructions.Count != 0;
						if (flag6)
						{
							mutatio.Mutate1(methodDef3);
						}
					}
				}
			}
			bool numbers4 = this.Numbers;
			if (numbers4)
			{
				numbers.encrypt(moduleDef);
			}
			Directory.CreateDirectory(".\\AtomicProtected\\");
			moduleDef.Write(".\\AtomicProtected\\" + Path.GetFileName(this.textBox1.Text), new ModuleWriterOptions(moduleDef)
			{
				Logger = DummyLogger.NoThrowInstance
			});
			bool packer = this.Packer;
			if (packer)
			{

			}
			Process.Start(".\\AtomicProtected\\");
		}


		private void TextBox1_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}
	}
	}
