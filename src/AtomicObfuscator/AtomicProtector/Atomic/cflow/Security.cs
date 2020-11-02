using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x0200001A RID: 26
	internal class Security
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00009A50 File Offset: 0x00007C50
		public static string Signature(string value)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(value);
				byte[] value2 = md.ComputeHash(bytes);
				result = BitConverter.ToString(value2).Replace("-", "");
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00009AB0 File Offset: 0x00007CB0
		private static string Session(int length)
		{
			Random random = new Random();
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz", length)
			select s[random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00009AFC File Offset: 0x00007CFC
		public static string Obfuscate(int length)
		{
			Random random = new Random();
			return new string((from s in Enumerable.Repeat<string>("gd8JQ57nxXzLLMPrLylVhxoGnWGCFjO4knKTfRE6mVvdjug2NF/4aptAsZcdIGbAPmcx0O+ftU/KvMIjcfUnH3j+IMdhAW5OpoX3MrjQdf5AAP97tTB5g1wdDSAqKpq9gw06t3VaqMWZHKtPSuAXy0kkZRsc+DicpcY8E9+vWMHXa3jMdbPx4YES0p66GzhqLd/heA2zMvX8iWv4wK7S3QKIW/a9dD4ALZJpmcr9OOE=", length)
			select s[random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00009B48 File Offset: 0x00007D48
		public static void Start()
		{
			bool started = Constants.Started;
			if (started)
			{
				MessageBox.Show("A session has already been started, please end the previous one!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				Process.GetCurrentProcess().Kill();
			}
			else
			{
				using (StreamReader streamReader = new StreamReader("C:\\Windows\\System32\\drivers\\etc\\hosts"))
				{
					string text = streamReader.ReadToEnd();
					bool flag = text.Contains("api.auth.gg");
					if (flag)
					{
						Constants.Breached = true;
						MessageBox.Show("DNS redirecting has been detected!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Hand);
						Process.GetCurrentProcess().Kill();
					}
				}
				InfoManager infoManager = new InfoManager();
				infoManager.StartListener();
				Constants.Token = Guid.NewGuid().ToString();
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(Security.PinPublicKey));
				Constants.APIENCRYPTKEY = Convert.ToBase64String(Encoding.Default.GetBytes(Security.Session(32)));
				Constants.APIENCRYPTSALT = Convert.ToBase64String(Encoding.Default.GetBytes(Security.Session(16)));
				Constants.IV = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(16)));
				Constants.Key = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(32)));
				Constants.Started = true;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00009CAC File Offset: 0x00007EAC
		public static void End()
		{
			bool flag = !Constants.Started;
			if (flag)
			{
				MessageBox.Show("No session has been started, closing for security reasons!", "AtomicObfuscator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				Process.GetCurrentProcess().Kill();
			}
			else
			{
				Constants.Token = null;
				//ServicePointManager.ServerCertificateValidationCallback = ((object < p0 >, X509Certificate<p1>, X509Chain<p2>, SslPolicyErrors<p3>) => true);
				Constants.APIENCRYPTKEY = null;
				Constants.APIENCRYPTSALT = null;
				Constants.IV = null;
				Constants.Key = null;
				Constants.Started = false;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00009D38 File Offset: 0x00007F38
		private static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return certificate != null && certificate.GetPublicKeyString() == "041B6566A801CC518B5AC35FE91FCF4BDD9E94F3AE8156DF327083B7DA8A62D5CE2A4C25245F1567A0AC51EF12C549E8D62CC27DB1398DE17F62015ADF00207D5D";
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00009D60 File Offset: 0x00007F60
		public static string Integrity(string filename)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(filename))
				{
					byte[] value = md.ComputeHash(fileStream);
					result = BitConverter.ToString(value).Replace("-", "").ToLowerInvariant();
				}
			}
			return result;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00009DE0 File Offset: 0x00007FE0
		public static bool MaliciousCheck(string date)
		{
			DateTime d = DateTime.Parse(date);
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = d - now;
			bool flag = Convert.ToInt32(timeSpan.Seconds.ToString().Replace("-", "")) >= 5 || Convert.ToInt32(timeSpan.Minutes.ToString().Replace("-", "")) >= 1;
			bool result;
			if (flag)
			{
				Constants.Breached = true;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000065 RID: 101
		private const string _key = "041B6566A801CC518B5AC35FE91FCF4BDD9E94F3AE8156DF327083B7DA8A62D5CE2A4C25245F1567A0AC51EF12C549E8D62CC27DB1398DE17F62015ADF00207D5D";
	}
}
