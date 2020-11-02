using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x02000018 RID: 24
	internal class OnProgramStart
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00007DFC File Offset: 0x00005FFC
		public static void Initialize(string name, string aid, string secret, string version)
		{
			bool flag = string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(aid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version);
			if (flag)
			{
				System.Windows.MessageBox.Show("tell hugzho to fix application settings", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			OnProgramStart.AID = aid;
			OnProgramStart.Secret = secret;
			OnProgramStart.Version = version;
			OnProgramStart.Name = name;
			string[] array = new string[0];
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.Proxy = null;
					Security.Start();
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString());
					nameValueCollection["aid"] = Encryption.APIService(OnProgramStart.AID);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("start");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					bool flag2 = Security.MaliciousCheck(array[1]);
					if (flag2)
					{
						System.Windows.MessageBox.Show("Malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						API.Log(Environment.UserName, "debugger");
						Process.GetCurrentProcess().Kill();
					}
					bool breached = Constants.Breached;
					if (breached)
					{
						System.Windows.MessageBox.Show("Malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						//API.Log(Environment.UserName, "debugger");
						Process.GetCurrentProcess().Kill();
					}
					bool flag3 = array[0] != Constants.Token;
					if (flag3)
					{
						System.Windows.MessageBox.Show("Security check failed!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
						API.Log(Environment.UserName, "debugger");
						Process.GetCurrentProcess().Kill();
					}
					string text = array[2];
					string text2 = text;
					if (text2 != null)
					{
						if (!(text2 == "success"))
						{
							if (text2 == "binderror")
							{
								System.Windows.MessageBox.Show(Encryption.Decode("RmFpbGVkIHRvIGJpbmQgdG8gc2VydmVyLCBjaGVjayB5b3VyIEFJRCAmIFNlY3JldCBpbiB5b3VyIGNvZGUh"), "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
								Process.GetCurrentProcess().Kill();
								return;
							}
							if (text2 == "banned")
							{
								System.Windows.MessageBox.Show("outbuilt is gay and banned our application, join discord for new version", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
								Process.Start("https://discord.gg/Bwk5c8R");
								Process.GetCurrentProcess().Kill();
								return;
							}
						}
						else
						{
							Constants.Initialized = true;
							bool flag4 = array[3] == "Enabled";
							if (flag4)
							{
								ApplicationSettings.Status = true;
							}
							bool flag5 = array[4] == "Enabled";
							if (flag5)
							{
								ApplicationSettings.DeveloperMode = true;
							}
							ApplicationSettings.Hash = array[5];
							ApplicationSettings.Version = array[6];
							ApplicationSettings.Update_Link = array[7];
							bool flag6 = array[8] == "Enabled";
							if (flag6)
							{
								ApplicationSettings.Freemode = true;
							}
							bool flag7 = array[9] == "Enabled";
							if (flag7)
							{
								ApplicationSettings.Login = true;
							}
							ApplicationSettings.Name = array[10];
							bool flag8 = array[11] == "Enabled";
							if (flag8)
							{
								ApplicationSettings.Register = true;
							}
							bool developerMode = ApplicationSettings.DeveloperMode;
							if (developerMode)
							{
								string text3 = Security.Integrity(Process.GetCurrentProcess().MainModule.FileName);
								Clipboard.SetText(text3);
							}
							else
							{
								bool flag9 = ApplicationSettings.Version != OnProgramStart.Version;
								if (flag9)
								{
									System.Windows.MessageBox.Show("Update " + ApplicationSettings.Version + " available, click ok to update!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
									Process.Start(ApplicationSettings.Update_Link);
									Process.GetCurrentProcess().Kill();
								}
								bool flag10 = array[12] == "Enabled";
								if (flag10)
								{
									bool flag11 = ApplicationSettings.Hash != Security.Integrity(Process.GetCurrentProcess().MainModule.FileName);
									if (flag11)
									{
										System.Windows.MessageBox.Show("Hash check failed!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
										Process.GetCurrentProcess().Kill();
									}
								}
							}
							bool flag12 = !ApplicationSettings.Status;
							if (flag12)
							{
								System.Windows.MessageBox.Show("Application disabled", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
								Process.GetCurrentProcess().Kill();
							}
						}
					}
					Security.End();
				}
				catch
				{
					System.Windows.MessageBox.Show("Failed to connect! Try again.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x04000060 RID: 96
		public static string AID = null;

		// Token: 0x04000061 RID: 97
		public static string Secret = null;

		// Token: 0x04000062 RID: 98
		public static string Version = null;

		// Token: 0x04000063 RID: 99
		public static string Name = null;

		// Token: 0x04000064 RID: 100
		public static string Salt = null;
	}
}
