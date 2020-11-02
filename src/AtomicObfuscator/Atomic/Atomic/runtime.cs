// Decompiled with JetBrains decompiler
// Type: Atomic.Atomic.runtime
// Assembly: AtomicObfuscator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B2B1F80D-5C6A-4DF3-B343-FE98A4C6DD2A
// Assembly location: C:\Users\liam_\Desktop\AtomicGay\Original\AtomicObfuscator.exe

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Atomic.Atomic
{
    internal class runtime
    {
        public static double Encrypt(string value, uint num)
        {
            if (!(Assembly.GetCallingAssembly() == Assembly.GetExecutingAssembly()))
                return 69.0;
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            if (currentMethod.Name != nameof(Encrypt))
                Environment.Exit(0);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            int num1 = 0;
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            return num1 == 0 ? double.Parse(value) : 69.0;
        }

        public static string Season(string value, uint num)
        {
            if (!(Assembly.GetCallingAssembly() == Assembly.GetExecutingAssembly()))
                return "";
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            MethodBase currentMethod = MethodBase.GetCurrentMethod();
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            if (currentMethod.Name != nameof(Season))
                Environment.Exit(0);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            int num1 = 0;
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            return num1 == 0 ? runtime.d4v7654745(value, (int)num) : "";
        }

        public static string d4v7654745(string szPlainText, int szEncryptionKey)
        {
            szPlainText = szPlainText.ToString().Replace("                                                                                                                                       ", "");
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            StringBuilder stringBuilder1 = new StringBuilder(szPlainText);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            StringBuilder stringBuilder2 = new StringBuilder(szPlainText.Length);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            for (int index = 0; index < szPlainText.Length; ++index)
            {
                if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                    Environment.Exit(0);
                char ch1 = stringBuilder1[index];
                if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                    Environment.Exit(0);
                char ch2 = (char)((uint)ch1 ^ (uint)szEncryptionKey);
                if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                    Environment.Exit(0);
                stringBuilder2.Append(ch2);
                if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                    Environment.Exit(0);
            }
            return stringBuilder2.ToString();
        }

        public static IntPtr ResolveToken(int token)
        {
            return typeof(runtime).Module.ResolveMethod(token).MethodHandle.GetFunctionPointer();
        }

        [DllImport("kernel32.dll")]
        private static extern unsafe bool VirtualProtect(
          byte* lpAddress,
          int dwSize,
          uint flNewProtect,
          out uint lpflOldProtect);

        private static unsafe void Initialize()
        {
            if (Assembly.GetCallingAssembly() != Assembly.GetExecutingAssembly())
                Environment.Exit(0);
            byte* hinstance = (byte*)(void*)Marshal.GetHINSTANCE(typeof(runtime).Module);
            byte* numPtr1 = hinstance + 60;
            byte* numPtr2;
            byte* numPtr3 = (numPtr2 = hinstance + *(uint*)numPtr1) + 6;
            ushort num1 = *(ushort*)numPtr3;
            byte* numPtr4 = numPtr3 + 14;
            ushort num2 = *(ushort*)numPtr4;
            byte* lpAddress1 = numPtr2 = numPtr4 + 4 + (int)num2;
            byte* numPtr5 = stackalloc byte[11];
            uint lpflOldProtect;
            runtime.VirtualProtect(lpAddress1 - 16, 8, 64U, out lpflOldProtect);
            *(int*)(lpAddress1 - 12) = 0;
            byte* lpAddress2 = hinstance + *(uint*)(lpAddress1 - 16);
            *(int*)(lpAddress1 - 16) = 0;
            runtime.VirtualProtect(lpAddress2, 72, 64U, out lpflOldProtect);
            byte* lpAddress3 = hinstance + *(uint*)(lpAddress2 + 8);
            *(int*)lpAddress2 = 0;
            *(int*)(lpAddress2 + 4) = 0;
          //  *(int*)(lpAddress2 + (new IntPtr(2) * 4).ToInt64()) = 0;
          //  *(int*)(lpAddress2 + (new IntPtr(3) * 4).ToInt64()) = 0;
            runtime.VirtualProtect(lpAddress3, 4, 64U, out lpflOldProtect);
            *(int*)lpAddress3 = 0;
            for (int index = 0; index < (int)num1; ++index)
            {
                runtime.VirtualProtect(lpAddress1, 8, 64U, out lpflOldProtect);
                Marshal.Copy(new byte[8], 0, (IntPtr)(void*)lpAddress1, 8);
                lpAddress1 += 40;
            }
        }
    }
}
