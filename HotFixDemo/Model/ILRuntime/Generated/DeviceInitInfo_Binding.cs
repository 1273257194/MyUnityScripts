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
    unsafe class DeviceInitInfo_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::DeviceInitInfo);

            field = type.GetField("devAdd", flag);
            app.RegisterCLRFieldGetter(field, get_devAdd_0);
            app.RegisterCLRFieldSetter(field, set_devAdd_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_devAdd_0, AssignFromStack_devAdd_0);
            field = type.GetField("devCode", flag);
            app.RegisterCLRFieldGetter(field, get_devCode_1);
            app.RegisterCLRFieldSetter(field, set_devCode_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_devCode_1, AssignFromStack_devCode_1);


        }



        static object get_devAdd_0(ref object o)
        {
            return ((global::DeviceInitInfo)o).devAdd;
        }

        static StackObject* CopyToStack_devAdd_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::DeviceInitInfo)o).devAdd;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_devAdd_0(ref object o, object v)
        {
            ((global::DeviceInitInfo)o).devAdd = (System.String)v;
        }

        static StackObject* AssignFromStack_devAdd_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @devAdd = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::DeviceInitInfo)o).devAdd = @devAdd;
            return ptr_of_this_method;
        }

        static object get_devCode_1(ref object o)
        {
            return ((global::DeviceInitInfo)o).devCode;
        }

        static StackObject* CopyToStack_devCode_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::DeviceInitInfo)o).devCode;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_devCode_1(ref object o, object v)
        {
            ((global::DeviceInitInfo)o).devCode = (System.String)v;
        }

        static StackObject* AssignFromStack_devCode_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @devCode = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::DeviceInitInfo)o).devCode = @devCode;
            return ptr_of_this_method;
        }



    }
}
