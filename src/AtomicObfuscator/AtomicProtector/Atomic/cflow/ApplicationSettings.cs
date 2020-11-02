using System;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x02000017 RID: 23
	internal class ApplicationSettings
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00007D6C File Offset: 0x00005F6C
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00007D73 File Offset: 0x00005F73
		public static bool Status { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00007D7B File Offset: 0x00005F7B
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00007D82 File Offset: 0x00005F82
		public static bool DeveloperMode { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00007D8A File Offset: 0x00005F8A
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00007D91 File Offset: 0x00005F91
		public static string Hash { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00007D99 File Offset: 0x00005F99
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public static string Version { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00007DA8 File Offset: 0x00005FA8
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00007DAF File Offset: 0x00005FAF
		public static string Update_Link { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00007DB7 File Offset: 0x00005FB7
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00007DBE File Offset: 0x00005FBE
		public static bool Freemode { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00007DC6 File Offset: 0x00005FC6
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00007DCD File Offset: 0x00005FCD
		public static bool Login { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00007DD5 File Offset: 0x00005FD5
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00007DDC File Offset: 0x00005FDC
		public static string Name { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00007DE4 File Offset: 0x00005FE4
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00007DEB File Offset: 0x00005FEB
		public static bool Register { get; set; }
	}
}
