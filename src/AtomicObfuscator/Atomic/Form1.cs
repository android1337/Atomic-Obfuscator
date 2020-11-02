using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using AtomicProtector.Atomic.cflow;
using DiscordRPC;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using MetroSuite;

namespace Atomic
{
	// Token: 0x02000004 RID: 4
	public partial class Form1 : Form
	{
		// Token: 0x06000008 RID: 8
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		// Token: 0x06000009 RID: 9
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x0600000A RID: 10 RVA: 0x000020FC File Offset: 0x000002FC
		public Form1()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002150 File Offset: 0x00000350
		private void button1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002154 File Offset: 0x00000354
		private void Initialize()
		{
			this.client = new DiscordRpcClient("697457882014154802");
			this.client.Initialize();
			this.client.SetPresence(new RichPresence
			{
				Details = "#1 C# Obfuscator",
				State = "https://discord.gg/Bwk5c8R",
				Assets = new Assets
				{
					LargeImageKey = "atomic",
					LargeImageText = "AtomicObfuscator",
					SmallImageKey = ""
				}
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D8 File Offset: 0x000003D8
		private void Form1_Load(object sender, EventArgs e)
		{
			//Thread thread = new Thread(delegate()
			//{
			//	Form1.Title();
			//});
			//thread.Start();
			//this.label1.Text = Environment.UserName;
			//OnProgramStart.Initialize("AtomicGayProtector", "806940", "7cWjKpnjebanvoVajeAQwxsV8A11oYh1a0w", "1.0");
			//API.Log(Environment.UserName, "load");
			bool flag = File.Exists("C:\\ProgramData\\atomicobfuscatorkeysave.txt");

			
			if (flag)
			{
				string aio = File.ReadAllText("C:\\ProgramData\\atomicobfuscatorkeysave.txt");
				bool flag2 = API.AIO(aio);
				
				if (flag2)
				{
					this.textBox2.Visible = false;
					this.button10.Visible = false;
					this.label7.Visible = true;
					this.button1.Visible = true;
					this.button2.Visible = true;
					this.button3.Visible = true;
					this.button4.Visible = true;
					this.button5.Visible = true;
					this.Initialize();
				}
				else
				{
					File.Delete("C:\\ProgramData\\atomicobfuscatorkeysave.txt");
				}
				
			}


			string text = new WebClient
			{
				Proxy = null
			}.DownloadString("https://pastebin.com/raw/yc422EAZ");
			this.label7.Text = text;
			bool flag3 = File.Exists("C:\\ProgramData\\atomicconfigg.txt");
			if (flag3)
			{
				string text2 = File.ReadAllText("C:\\ProgramData\\atomicconfigg.txt");
				bool flag4 = text2.Contains("numberprotect");
				if (flag4)
				{
					this.checkBox2.Checked = true;
				}
				bool flag5 = text2.Contains("stringenc");
				if (flag5)
				{
					this.checkBox6.Checked = true;
				}
				bool flag6 = text2.Contains("cflow");
				if (flag6)
				{
					this.checkBox5.Checked = true;
				}
				bool flag7 = text2.Contains("renamer");
				if (flag7)
				{
					this.checkBox4.Checked = true;
				}
				bool flag8 = text2.Contains("mutation");
				if (flag8)
				{
					this.checkBox3.Checked = true;
				}
				bool flag9 = text2.Contains("packer");
				if (flag9)
				{
					this.checkBox1.Checked = true;
				}
				bool flag10 = text2.Contains("antidump");
				if (flag10)
				{
					this.checkBox7.Checked = true;
				}
				FileInfo fileInfo = new FileInfo("C:\\ProgramData\\atomicconfigg.txt");
				DateTime lastWriteTime = fileInfo.LastWriteTime;
				this.label2.Text = "Status: Last updated\n" + lastWriteTime.ToString();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000245C File Offset: 0x0000065C
		public static void Title()
		{
			for (;;)
			{
				foreach (Process process in Process.GetProcessesByName("HTTPDebuggerUI"))
				{
					process.Kill();
				}
				foreach (Process process2 in Process.GetProcessesByName("MegaDumper"))
				{
					process2.Kill();
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024C8 File Offset: 0x000006C8
		public static bool Simplify(MethodDef methodDef)
		{
			bool flag = methodDef.Parameters == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				methodDef.Body.SimplifyMacros(methodDef.Parameters);
				result = true;
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002500 File Offset: 0x00000700
		public static bool Optimize(MethodDef methodDef)
		{
			bool flag = methodDef.Body == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				methodDef.Body.OptimizeMacros();
				methodDef.Body.OptimizeBranches();
				result = true;
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000253C File Offset: 0x0000073C
		private void metroButton1_Click(object sender, EventArgs e)
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
							//array.Array(methodDef);
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
							//mutatio.Mutate1(methodDef3);
						}
					}
				}
			}
			bool numbers4 = this.Numbers;
			if (numbers4)
			{
				//numbers.encrypt(moduleDef);
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

		// Token: 0x06000012 RID: 18 RVA: 0x000028E0 File Offset: 0x00000AE0
		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			this.Numbers = !this.Numbers;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028F2 File Offset: 0x00000AF2
		private void checkBox6_CheckedChanged(object sender, EventArgs e)
		{
			this.Strings = !this.Strings;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002904 File Offset: 0x00000B04
		private void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			this.Cflow = !this.Cflow;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002916 File Offset: 0x00000B16
		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			this.Renamer = !this.Renamer;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002928 File Offset: 0x00000B28
		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			this.Mutation = !this.Mutation;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000293A File Offset: 0x00000B3A
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.Packer = !this.Packer;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000294C File Offset: 0x00000B4C
		private void checkBox7_CheckedChanged(object sender, EventArgs e)
		{
			this.AntiDumper = !this.AntiDumper;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002960 File Offset: 0x00000B60
		private void textBox1_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				Array array = (Array)e.Data.GetData(DataFormats.FileDrop);
				bool flag = array != null;
				if (flag)
				{
					string text = array.GetValue(0).ToString();
					int num = text.LastIndexOf(".");
					bool flag2 = num != -1;
					if (flag2)
					{
						string a = text.Substring(num).ToLower();
						bool flag3 = a == ".exe" || a == ".dll";
						if (flag3)
						{
							base.Activate();
							this.textBox1.Text = text;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A18 File Offset: 0x00000C18
		private void textBox1_DragEnter(object sender, DragEventArgs e)
		{
			bool dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
			if (dataPresent)
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A4C File Offset: 0x00000C4C
		private void button2_Click(object sender, EventArgs e)
		{
			this.label7.Visible = false;
			this.label4.Visible = false;
			this.label3.Visible = true;
			this.textBox1.Visible = true;
			this.button9.Visible = true;
			this.checkBox2.Visible = false;
			this.checkBox6.Visible = false;
			this.checkBox5.Visible = false;
			this.checkBox4.Visible = false;
			this.checkBox3.Visible = false;
			this.checkBox1.Visible = false;
			this.checkBox7.Visible = false;
			this.button6.Visible = false;
			this.label2.Visible = false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B10 File Offset: 0x00000D10
		private void button1_Click_1(object sender, EventArgs e)
		{
			this.label7.Visible = true;
			this.label4.Visible = false;
			this.label3.Visible = false;
			this.textBox1.Visible = false;
			this.button9.Visible = false;
			this.checkBox2.Visible = false;
			this.checkBox6.Visible = false;
			this.checkBox5.Visible = false;
			this.checkBox4.Visible = false;
			this.checkBox3.Visible = false;
			this.checkBox1.Visible = false;
			this.checkBox7.Visible = false;
			this.button6.Visible = false;
			this.label2.Visible = false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002BD4 File Offset: 0x00000DD4
		private void button3_Click(object sender, EventArgs e)
		{
			this.label7.Visible = false;
			this.label4.Visible = false;
			this.label3.Visible = true;
			this.textBox1.Visible = false;
			this.button9.Visible = false;
			this.checkBox2.Visible = true;
			this.checkBox6.Visible = true;
			this.checkBox5.Visible = true;
			this.checkBox4.Visible = true;
			this.checkBox3.Visible = true;
			this.checkBox1.Visible = true;
			this.checkBox7.Visible = true;
			this.button6.Visible = false;
			this.label2.Visible = false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C98 File Offset: 0x00000E98
		private void button4_Click(object sender, EventArgs e)
		{
			this.label7.Visible = false;
			this.label4.Visible = false;
			this.label3.Visible = false;
			this.textBox1.Visible = false;
			this.button9.Visible = false;
			this.checkBox2.Visible = false;
			this.checkBox6.Visible = false;
			this.checkBox5.Visible = false;
			this.checkBox4.Visible = false;
			this.checkBox3.Visible = false;
			this.checkBox1.Visible = false;
			this.checkBox7.Visible = false;
			this.button6.Visible = true;
			this.label2.Visible = true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002D5C File Offset: 0x00000F5C
		private void button5_Click(object sender, EventArgs e)
		{
			this.label7.Visible = false;
			this.label4.Visible = true;
			this.label3.Visible = false;
			this.textBox1.Visible = false;
			this.button9.Visible = false;
			this.checkBox2.Visible = false;
			this.checkBox6.Visible = false;
			this.checkBox5.Visible = false;
			this.checkBox4.Visible = false;
			this.checkBox3.Visible = false;
			this.checkBox1.Visible = false;
			this.checkBox7.Visible = false;
			this.button6.Visible = false;
			this.label2.Visible = false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002E20 File Offset: 0x00001020
		private void button7_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E2A File Offset: 0x0000102A
		private void label6_Click(object sender, EventArgs e)
		{
			Process.Start("https://discord.gg/Bwk5c8R");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E38 File Offset: 0x00001038
		private void button6_Click(object sender, EventArgs e)
		{
			this.label2.Text = "Status: Updating..";
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			bool @checked = this.checkBox2.Checked;
			if (@checked)
			{
				text = "numberprotect";
			}
			bool checked2 = this.checkBox6.Checked;
			if (checked2)
			{
				text2 = "stringenc";
			}
			bool checked3 = this.checkBox5.Checked;
			if (checked3)
			{
				text3 = "cflow";
			}
			bool checked4 = this.checkBox4.Checked;
			if (checked4)
			{
				text4 = "renamer";
			}
			bool checked5 = this.checkBox3.Checked;
			if (checked5)
			{
				text5 = "mutation";
			}
			bool checked6 = this.checkBox1.Checked;
			if (checked6)
			{
				text6 = "packer";
			}
			bool checked7 = this.checkBox7.Checked;
			if (checked7)
			{
				text7 = "antidump";
			}
			string[] contents = new string[]
			{
				text,
				text2,
				text3,
				text4,
				text5,
				text6,
				text7
			};
			File.WriteAllLines("C:\\ProgramData\\atomicconfigg.txt", contents);
			FileInfo fileInfo = new FileInfo("C:\\ProgramData\\atomicconfigg.txt");
			DateTime lastWriteTime = fileInfo.LastWriteTime;
			this.label2.Text = "Status: Last updated\n" + lastWriteTime.ToString();
			MessageBox.Show("Config Updated!", "AtomicProtector", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002FB0 File Offset: 0x000011B0
		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002FEC File Offset: 0x000011EC
		private void label5_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003028 File Offset: 0x00001228
		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003064 File Offset: 0x00001264
		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000030A0 File Offset: 0x000012A0
		private void label1_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000030DC File Offset: 0x000012DC
		private void label3_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003118 File Offset: 0x00001318
		private void label7_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003154 File Offset: 0x00001354
		private void label4_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000318E File Offset: 0x0000138E
		private void button8_Click(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Minimized;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000319C File Offset: 0x0000139C
		private void metroButton2_Click(object sender, EventArgs e)
		{
			string text = this.textBox2.Text.Replace(" ", "");
			
			bool flag = API.AIO(text);
			if (flag)
			{
				this.textBox2.Visible = false;
				this.button10.Visible = false;
				this.label7.Visible = true;
				this.button1.Visible = true;
				this.button2.Visible = true;
				this.button3.Visible = true;
				this.button4.Visible = true;
				this.button5.Visible = true;
				File.WriteAllText("C:\\ProgramData\\atomicobfuscatorkeysave.txt", text);
				this.Initialize();
			}
			
		}

		// Token: 0x04000004 RID: 4
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

		// Token: 0x0400000D RID: 13
		public DiscordRpcClient client;

		private void label8_Click(object sender, EventArgs e)
		{

		}

		private void button9_Click(object sender, EventArgs e)
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
							//array.Array(methodDef);
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
							//mutatio.Mutate1(methodDef3);
						}
					}
				}
			}
			bool numbers4 = this.Numbers;
			if (numbers4)
			{
				//numbers.encrypt(moduleDef);
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

		private void button10_Click(object sender, EventArgs e)
		{
			
				this.textBox2.Visible = false;
				this.button10.Visible = false;
				this.label7.Visible = true;
				this.button1.Visible = true;
				this.button2.Visible = true;
				this.button3.Visible = true;
				this.button4.Visible = true;
				this.button5.Visible = true;
				//File.WriteAllText("C:\\ProgramData\\atomicobfuscatorkeysave.txt", text);
				this.Initialize();
		
		}
	}
}
