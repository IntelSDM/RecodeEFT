using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Hag.Helpers
{
	internal static class Access
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static T GetPrivateField<T>(this object obj, string name)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
			Type type = obj.GetType();
			FieldInfo field = type.GetField(name, bindingAttr);
			return (T)((object)field.GetValue(obj));
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
		internal static void SetPrivateField(this object obj, string name, object value)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
			Type type = obj.GetType();
			FieldInfo field = type.GetField(name, bindingAttr);
			field.SetValue(obj, value);
		}
		internal static void SetPublicField(this object obj, string name, object value)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			Type type = obj.GetType();
			FieldInfo field = type.GetField(name, bindingAttr);
			field.SetValue(obj, value);
		}
		// Token: 0x06000003 RID: 3 RVA: 0x000020B0 File Offset: 0x000002B0
		internal static void CallPrivateMethod(this object obj, string name, params object[] param)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
			Type type = obj.GetType();
			MethodInfo method = type.GetMethod(name, bindingAttr);
			method.Invoke(obj, param);
		}
	}
}
