	using AtomicObfuscator.Atomic;
using System;
using System.Windows.Forms;

namespace Atomic
{
	// Token: 0x02000006 RID: 6
	internal static class Program
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00004D17 File Offset: 0x00002F17
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
