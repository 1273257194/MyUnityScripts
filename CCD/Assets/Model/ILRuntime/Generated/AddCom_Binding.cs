using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class AddCom_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::AddCom);

            field = type.GetField("btn", flag);
            app.RegisterCLRFieldGetter(field, get_btn_0);
            app.RegisterCLRFieldSetter(field, set_btn_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_btn_0, AssignFromStack_btn_0);


        }



        static object get_btn_0(ref object o)
        {
            return ((global::AddCom)o).btn;
        }

        static StackObject* CopyToStack_btn_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::AddCom)o).btn;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_btn_0(ref object o, object v)
        {
            ((global::AddCom)o).btn = (UnityEngine.UI.Button)v;
        }

        static StackObject* AssignFromStack_btn_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.UI.Button @btn = (UnityEngine.UI.Button)typeof(UnityEngine.UI.Button).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::AddCom)o).btn = @btn;
            return ptr_of_this_method;
        }



    }
}
