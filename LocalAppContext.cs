using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
    internal static class LocalAppContext
    {
        private const bool V1 = false;
        private const bool V = V1;
        private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

#pragma warning disable IDE0044 // Add readonly modifier
        private static bool s_canForwardCalls;
#pragma warning restore IDE0044 // Add readonly modifier

        private static Dictionary<string, bool> s_switchMap;

        private readonly static object s_syncLock;

        private static bool DisableCaching
        {
            get;
            set;
        }

        public static bool V11 => V1;

        private static TryGetSwitchDelegate TryGetSwitchFromCentralAppContext1 { get => TryGetSwitchFromCentralAppContext; set => TryGetSwitchFromCentralAppContext = value; }

        static LocalAppContext()
        {
            s_switchMap = new Dictionary<string, bool>();
            LocalAppContext.s_syncLock = new object();
            LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
            AppContextDefaultValues.PopulateDefaultValues();
            LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
        }

        internal static void DefineSwitchDefault(string switchName, bool initialValue)
        {
            LocalAppContext.s_switchMap[switchName] = initialValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
        {
            if (switchValue < 0)
            {
                return false;
            }
            if (switchValue > 0)
            {
                return true;
            }
            return LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue);
        }

        private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
        {
            if (LocalAppContext.DisableCaching)
            {
                return LocalAppContext.IsSwitchEnabled(switchName);
            }
            bool flag = LocalAppContext.IsSwitchEnabled(switchName);
            switchValue = (flag ? 1 : -1);
            return flag;
        }

        public static bool IsSwitchEnabled(string switchName)
        {
            bool flag = V;
            return s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext1(switchName, out flag)
                ? flag
                : LocalAppContext.IsSwitchEnabledLocal(switchName);
        }

        private static bool IsSwitchEnabledLocal(string switchName)
        {
            bool flag;
            bool flag1;
            lock (LocalAppContext.s_switchMap)
            {
                flag1 = LocalAppContext.s_switchMap.TryGetValue(switchName, out flag);
            }
            if (flag1)
            {
                return flag;
            }
            return false;
        }

        private static bool SetupDelegate()
        {
            Type type = typeof(object).Assembly.GetType("System.AppContext");
            if (type == null)
            {
                return false;
            }
            MethodInfo method = type.GetMethod("TryGetSwitch", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string), typeof(bool).MakeByRefType() }, null);
            if (method == null)
            {
                return false;
            }
            LocalAppContext.TryGetSwitchFromCentralAppContext1 = (LocalAppContext.TryGetSwitchDelegate)Delegate.CreateDelegate(typeof(LocalAppContext.TryGetSwitchDelegate), method);
            return true;
        }

        private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
    }
}