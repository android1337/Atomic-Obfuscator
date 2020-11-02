using System;
using System.Collections.Generic;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x02000014 RID: 20
	internal class App
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00007B50 File Offset: 0x00005D50
		public static string GrabVariable(string name)
		{
			string result;
			try
			{
				bool flag = User.ID != null || User.HWID != null || User.IP != null || !Constants.Breached;
				if (flag)
				{
					result = App.Variables[name];
				}
				else
				{
					Constants.Breached = true;
					result = "User is not logged in, failed to obfuscate.";
				}
			}
			catch
			{
				result = "N/A";
			}
			return result;
		}

		// Token: 0x0400003F RID: 63
		public static string Error = null;

		// Token: 0x04000040 RID: 64
		public static Dictionary<string, string> Variables = new Dictionary<string, string>();
	}
}
