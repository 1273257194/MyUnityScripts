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
    unsafe class GameStartInfo_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::GameStartInfo);

            field = type.GetField("gameNameAbb", flag);
            app.RegisterCLRFieldGetter(field, get_gameNameAbb_0);
            app.RegisterCLRFieldSetter(field, set_gameNameAbb_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_gameNameAbb_0, AssignFromStack_gameNameAbb_0);
            field = type.GetField("deviceInitInfo", flag);
            app.RegisterCLRFieldGetter(field, get_deviceInitInfo_1);
            app.RegisterCLRFieldSetter(field, set_deviceInitInfo_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_deviceInitInfo_1, AssignFromStack_deviceInitInfo_1);
            field = type.GetField("userId", flag);
            app.RegisterCLRFieldGetter(field, get_userId_2);
            app.RegisterCLRFieldSetter(field, set_userId_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_userId_2, AssignFromStack_userId_2);
            field = type.GetField("gameTime", flag);
            app.RegisterCLRFieldGetter(field, get_gameTime_3);
            app.RegisterCLRFieldSetter(field, set_gameTime_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_gameTime_3, AssignFromStack_gameTime_3);


        }



        static object get_gameNameAbb_0(ref object o)
        {
            return ((global::GameStartInfo)o).gameNameAbb;
        }

        static StackObject* CopyToStack_gameNameAbb_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::GameStartInfo)o).gameNameAbb;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_gameNameAbb_0(ref object o, object v)
        {
            ((global::GameStartInfo)o).gameNameAbb = (System.String)v;
        }

        static StackObject* AssignFromStack_gameNameAbb_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @gameNameAbb = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::GameStartInfo)o).gameNameAbb = @gameNameAbb;
            return ptr_of_this_method;
        }

        static object get_deviceInitInfo_1(ref object o)
        {
            return ((global::GameStartInfo)o).deviceInitInfo;
        }

        static StackObject* CopyToStack_deviceInitInfo_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::GameStartInfo)o).deviceInitInfo;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_deviceInitInfo_1(ref object o, object v)
        {
            ((global::GameStartInfo)o).deviceInitInfo = (global::DeviceInitInfo)v;
        }

        static StackObject* AssignFromStack_deviceInitInfo_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::DeviceInitInfo @deviceInitInfo = (global::DeviceInitInfo)typeof(global::DeviceInitInfo).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::GameStartInfo)o).deviceInitInfo = @deviceInitInfo;
            return ptr_of_this_method;
        }

        static object get_userId_2(ref object o)
        {
            return ((global::GameStartInfo)o).userId;
        }

        static StackObject* CopyToStack_userId_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::GameStartInfo)o).userId;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_userId_2(ref object o, object v)
        {
            ((global::GameStartInfo)o).userId = (System.String)v;
        }

        static StackObject* AssignFromStack_userId_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @userId = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::GameStartInfo)o).userId = @userId;
            return ptr_of_this_method;
        }

        static object get_gameTime_3(ref object o)
        {
            return ((global::GameStartInfo)o).gameTime;
        }

        static StackObject* CopyToStack_gameTime_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::GameStartInfo)o).gameTime;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_gameTime_3(ref object o, object v)
        {
            ((global::GameStartInfo)o).gameTime = (System.Int32)v;
        }

        static StackObject* AssignFromStack_gameTime_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @gameTime = ptr_of_this_method->Value;
            ((global::GameStartInfo)o).gameTime = @gameTime;
            return ptr_of_this_method;
        }



    }
}
