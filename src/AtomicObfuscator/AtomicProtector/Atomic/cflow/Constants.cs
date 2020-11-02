using System;
using System.Linq;
using System.Security.Principal;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x02000015 RID: 21
	internal class Constants
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00007BD7 File Offset: 0x00005DD7
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00007BDE File Offset: 0x00005DDE
		public static string Token { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00007BE6 File Offset: 0x00005DE6
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00007BED File Offset: 0x00005DED
		public static string Date { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00007BF5 File Offset: 0x00005DF5
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00007BFC File Offset: 0x00005DFC
		public static string APIENCRYPTKEY { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00007C04 File Offset: 0x00005E04
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00007C0B File Offset: 0x00005E0B
		public static string APIENCRYPTSALT { get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x00007C14 File Offset: 0x00005E14
		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
			select s[Constants.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00007C60 File Offset: 0x00005E60
		public static string HWID()
		{
			return WindowsIdentity.GetCurrent().User.Value;
		}

		// Token: 0x04000045 RID: 69
		public static bool Breached = false;

		// Token: 0x04000046 RID: 70
		public static bool Started = false;

		// Token: 0x04000047 RID: 71
		public static string IV = null;

		// Token: 0x04000048 RID: 72
		public static string Key = null;

		// Token: 0x04000049 RID: 73
		public static string ApiUrl = "https://api.auth.gg/csharp/";

		// Token: 0x0400004A RID: 74
		public static bool Initialized = false;

		// Token: 0x0400004B RID: 75
		public static Random random = new Random();
	}
}
