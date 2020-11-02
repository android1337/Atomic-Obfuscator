using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x02000019 RID: 25
	internal class API
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00008314 File Offset: 0x00006514
		public static void Log(string username, string action)
		{
			bool flag = !Constants.Initialized;
			if (flag)
			{
				MessageBox.Show("failed to initialize!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			bool flag2 = string.IsNullOrWhiteSpace(action);
			if (flag2)
			{
				MessageBox.Show("Missing log information!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["aid"] = Encryption.APIService(OnProgramStart.AID);
					nameValueCollection["username"] = Encryption.APIService(username);
					nameValueCollection["pcuser"] = Encryption.APIService(Environment.UserName);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["data"] = Encryption.APIService(action);
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("log");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					Security.End();
				}
				catch
				{
					MessageBox.Show("Failed to connect! Please try again", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00008508 File Offset: 0x00006708
		public static bool AIO(string AIO)
		{
			bool flag = API.AIOLogin(AIO);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = API.AIORegister(AIO);
				if (flag2)
				{
					result = true;
				}
				else
				{
					MessageBox.Show("Invalid key. Join discord for help", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008550 File Offset: 0x00006750
		public static bool AIOLogin(string AIO)
		{
			bool flag = !Constants.Initialized;
			if (flag)
			{
				MessageBox.Show("failed to initialize!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			bool flag2 = string.IsNullOrWhiteSpace(AIO);
			if (flag2)
			{
				MessageBox.Show("Missing user login information!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["username"] = Encryption.APIService(AIO);
					nameValueCollection["password"] = Encryption.APIService(AIO);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("login");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					bool flag3 = array[0] != Constants.Token;
					if (flag3)
					{
						MessageBox.Show("Security error!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
						Process.GetCurrentProcess().Kill();
					}
					bool flag4 = Security.MaliciousCheck(array[1]);
					if (flag4)
					{
						MessageBox.Show("malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					bool breached = Constants.Breached;
					if (breached)
					{
						MessageBox.Show("malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					string text = array[2];
					string text2 = text;
					if (text2 != null)
					{
						if (text2 == "success")
						{
							Security.End();
							User.ID = array[3];
							User.Username = array[4];
							User.Password = array[5];
							User.Email = array[6];
							User.HWID = array[7];
							User.UserVariable = array[8];
							User.Rank = array[9];
							User.IP = array[10];
							User.Expiry = array[11];
							User.LastLogin = array[12];
							User.RegisterDate = array[13];
							string text3 = array[14];
							foreach (string text4 in text3.Split(new char[]
							{
								'~'
							}))
							{
								string[] array3 = text4.Split(new char[]
								{
									'^'
								});
								try
								{
									App.Variables.Add(array3[0], array3[1]);
								}
								catch
								{
								}
							}
							return true;
						}
						if (text2 == "invalid_details")
						{
							Security.End();
							return false;
						}
						if (text2 == "time_expired")
						{
							MessageBox.Show("Your subscription has expired!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
						if (text2 == "hwid_updated")
						{
							Security.End();
							User.ID = array[3];
							User.Username = array[4];
							User.Password = array[5];
							User.Email = array[6];
							User.HWID = array[7];
							User.UserVariable = array[8];
							User.Rank = array[9];
							User.IP = array[10];
							User.Expiry = array[11];
							User.LastLogin = array[12];
							User.RegisterDate = array[13];
							string text5 = array[14];
							foreach (string text6 in text5.Split(new char[]
							{
								'~'
							}))
							{
								string[] array5 = text6.Split(new char[]
								{
									'^'
								});
								try
								{
									App.Variables.Add(array5[0], array5[1]);
								}
								catch
								{
								}
							}
							return true;
						}
						if (text2 == "invalid_hwid")
						{
							MessageBox.Show("Invalid HWID! Contact hugzho if you changed PC/reset pc.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
					}
				}
				catch
				{
					MessageBox.Show("Failed to connect! Please try again.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Security.End();
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008AA4 File Offset: 0x00006CA4
		public static bool AIORegister(string AIO)
		{
			bool flag = !Constants.Initialized;
			if (flag)
			{
				MessageBox.Show("failed to initialize!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Security.End();
				Process.GetCurrentProcess().Kill();
			}
			bool flag2 = string.IsNullOrWhiteSpace(AIO);
			if (flag2)
			{
				MessageBox.Show("Invalid key!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["type"] = Encryption.APIService("register");
					nameValueCollection["username"] = Encryption.APIService(AIO);
					nameValueCollection["password"] = Encryption.APIService(AIO);
					nameValueCollection["email"] = Encryption.APIService(AIO);
					nameValueCollection["license"] = Encryption.APIService(AIO);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					bool flag3 = array[0] != Constants.Token;
					if (flag3)
					{
						MessageBox.Show("Security error!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
						Security.End();
						Process.GetCurrentProcess().Kill();
					}
					bool flag4 = Security.MaliciousCheck(array[1]);
					if (flag4)
					{
						MessageBox.Show("malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					bool breached = Constants.Breached;
					if (breached)
					{
						MessageBox.Show("malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					Security.End();
					string text = array[2];
					string text2 = text;
					if (text2 != null)
					{
						if (text2 == "success")
						{
							return true;
						}
						if (text2 == "error")
						{
							return false;
						}
					}
				}
				catch
				{
					MessageBox.Show("Failed to connect! Please try again.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008DAC File Offset: 0x00006FAC
		public static bool Login(string username, string password)
		{
			bool flag = !Constants.Initialized;
			if (flag)
			{
				MessageBox.Show("failed to initialize!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			bool flag2 = string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password);
			if (flag2)
			{
				MessageBox.Show("Missing user login information!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["username"] = Encryption.APIService(username);
					nameValueCollection["password"] = Encryption.APIService(password);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.APIService(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.APIService("login");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					bool flag3 = array[0] != Constants.Token;
					if (flag3)
					{
						MessageBox.Show("Security error!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
						Process.GetCurrentProcess().Kill();
					}
					bool flag4 = Security.MaliciousCheck(array[1]);
					if (flag4)
					{
						MessageBox.Show("malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					bool breached = Constants.Breached;
					if (breached)
					{
						MessageBox.Show("malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					string text = array[2];
					string text2 = text;
					if (text2 != null)
					{
						if (text2 == "success")
						{
							User.ID = array[3];
							User.Username = array[4];
							User.Password = array[5];
							User.Email = array[6];
							User.HWID = array[7];
							User.UserVariable = array[8];
							User.Rank = array[9];
							User.IP = array[10];
							User.Expiry = array[11];
							User.LastLogin = array[12];
							User.RegisterDate = array[13];
							string text3 = array[14];
							foreach (string text4 in text3.Split(new char[]
							{
								'~'
							}))
							{
								string[] array3 = text4.Split(new char[]
								{
									'^'
								});
								try
								{
									App.Variables.Add(array3[0], array3[1]);
								}
								catch
								{
								}
							}
							Security.End();
							return true;
						}
						if (text2 == "invalid_details")
						{
							MessageBox.Show("Sorry, your username/password does not match!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
						if (text2 == "time_expired")
						{
							MessageBox.Show("Your subscription has expired!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
						if (text2 == "hwid_updated")
						{
							User.ID = array[3];
							User.Username = array[4];
							User.Password = array[5];
							User.Email = array[6];
							User.HWID = array[7];
							User.UserVariable = array[8];
							User.Rank = array[9];
							User.IP = array[10];
							User.Expiry = array[11];
							User.LastLogin = array[12];
							User.RegisterDate = array[13];
							string text5 = array[14];
							foreach (string text6 in text5.Split(new char[]
							{
								'~'
							}))
							{
								string[] array5 = text6.Split(new char[]
								{
									'^'
								});
								try
								{
									App.Variables.Add(array5[0], array5[1]);
								}
								catch
								{
								}
							}
							Security.End();
							return true;
						}
						if (text2 == "invalid_hwid")
						{
							MessageBox.Show("Invalid HWID! Contact hugzho if you changed PC/Reset pc.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
					}
				}
				catch
				{
					MessageBox.Show("Failed to connect! please try again.", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Security.End();
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000932C File Offset: 0x0000752C
		public static bool Register(string username, string password, string email, string license)
		{
			bool flag = !Constants.Initialized;
			if (flag)
			{
				MessageBox.Show("Please initialize your application first!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Security.End();
				Process.GetCurrentProcess().Kill();
			}
			bool flag2 = string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(license);
			if (flag2)
			{
				MessageBox.Show("Invalid registrar information!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["type"] = Encryption.APIService("register");
					nameValueCollection["username"] = Encryption.APIService(username);
					nameValueCollection["password"] = Encryption.APIService(password);
					nameValueCollection["email"] = Encryption.APIService(email);
					nameValueCollection["license"] = Encryption.APIService(license);
					nameValueCollection["hwid"] = Encryption.APIService(Constants.HWID());
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					bool flag3 = array[0] != Constants.Token;
					if (flag3)
					{
						MessageBox.Show("Security error has been triggered!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
						Security.End();
						Process.GetCurrentProcess().Kill();
					}
					bool flag4 = Security.MaliciousCheck(array[1]);
					if (flag4)
					{
						MessageBox.Show("Possible malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					bool breached = Constants.Breached;
					if (breached)
					{
						MessageBox.Show("Possible malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					string text = array[2];
					string text2 = text;
					if (text2 != null)
					{
						if (text2 == "success")
						{
							Security.End();
							return true;
						}
						if (text2 == "invalid_license")
						{
							MessageBox.Show("License does not exist!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
						if (text2 == "email_used")
						{
							MessageBox.Show("Email has already been used!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
						if (text2 == "invalid_username")
						{
							MessageBox.Show("You entered an invalid/used username!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
					}
				}
				catch
				{
					MessageBox.Show("Failed to establish a secure SSL tunnel with the server!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000096F0 File Offset: 0x000078F0
		public static bool ExtendSubscription(string username, string password, string license)
		{
			bool flag = !Constants.Initialized;
			if (flag)
			{
				MessageBox.Show("Please initialize your application first!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Security.End();
				Process.GetCurrentProcess().Kill();
			}
			bool flag2 = string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(license);
			if (flag2)
			{
				MessageBox.Show("Invalid registrar information!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
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
					nameValueCollection["type"] = Encryption.APIService("extend");
					nameValueCollection["username"] = Encryption.APIService(username);
					nameValueCollection["password"] = Encryption.APIService(password);
					nameValueCollection["license"] = Encryption.APIService(license);
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					bool flag3 = array[0] != Constants.Token;
					if (flag3)
					{
						MessageBox.Show("Security error has been triggered!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
						Security.End();
						Process.GetCurrentProcess().Kill();
					}
					bool flag4 = Security.MaliciousCheck(array[1]);
					if (flag4)
					{
						MessageBox.Show("Possible malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					bool breached = Constants.Breached;
					if (breached)
					{
						MessageBox.Show("Possible malicious activity detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						Process.GetCurrentProcess().Kill();
					}
					string text = array[2];
					string text2 = text;
					if (text2 != null)
					{
						if (text2 == "success")
						{
							Security.End();
							return true;
						}
						if (text2 == "invalid_token")
						{
							MessageBox.Show("Token does not exist!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
						if (text2 == "invalid_details")
						{
							MessageBox.Show("Your user details are invalid!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
							Security.End();
							Process.GetCurrentProcess().Kill();
							return false;
						}
					}
				}
				catch
				{
					MessageBox.Show("Failed to establish a secure SSL tunnel with the server!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}
	}
}
