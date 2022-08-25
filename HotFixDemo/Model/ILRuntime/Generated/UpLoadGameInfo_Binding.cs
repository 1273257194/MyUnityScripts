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
    unsafe class UpLoadGameInfo_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::UpLoadGameInfo);

            field = type.GetField("gameStartInfo", flag);
            app.RegisterCLRFieldGetter(field, get_gameStartInfo_0);
            app.RegisterCLRFieldSetter(field, set_gameStartInfo_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_gameStartInfo_0, AssignFromStack_gameStartInfo_0);
            field = type.GetField("DeviceUrl", flag);
            app.RegisterCLRFieldGetter(field, get_DeviceUrl_1);
            app.RegisterCLRFieldSetter(field, set_DeviceUrl_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_DeviceUrl_1, AssignFromStack_DeviceUrl_1);
            field = type.GetField("ip", flag);
            app.RegisterCLRFieldGetter(field, get_ip_2);
            app.RegisterCLRFieldSetter(field, set_ip_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_ip_2, AssignFromStack_ip_2);
            field = type.GetField("TrainOverDataPostUrl", flag);
            app.RegisterCLRFieldGetter(field, get_TrainOverDataPostUrl_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_TrainOverDataPostUrl_3, null);


        }



        static object get_gameStartInfo_0(ref object o)
        {
            return global::UpLoadGameInfo.gameStartInfo;
        }

        static StackObject* CopyToStack_gameStartInfo_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::UpLoadGameInfo.gameStartInfo;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_gameStartInfo_0(ref object o, object v)
        {
            global::UpLoadGameInfo.gameStartInfo = (global::GameStartInfo)v;
        }

        static StackObject* AssignFromStack_gameStartInfo_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::GameStartInfo @gameStartInfo = (global::GameStartInfo)typeof(global::GameStartInfo).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            global::UpLoadGameInfo.gameStartInfo = @gameStartInfo;
            return ptr_of_this_method;
        }

        static object get_DeviceUrl_1(ref object o)
        {
            return ((global::UpLoadGameInfo)o).DeviceUrl;
        }

        static StackObject* CopyToStack_DeviceUrl_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::UpLoadGameInfo)o).DeviceUrl;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_DeviceUrl_1(ref object o, object v)
        {
            ((global::UpLoadGameInfo)o).DeviceUrl = (System.String)v;
        }

        static StackObject* AssignFromStack_DeviceUrl_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @DeviceUrl = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::UpLoadGameInfo)o).DeviceUrl = @DeviceUrl;
            return ptr_of_this_method;
        }

        static object get_ip_2(ref object o)
        {
            return ((global::UpLoadGameInfo)o).ip;
        }

        static StackObject* CopyToStack_ip_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::UpLoadGameInfo)o).ip;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_ip_2(ref object o, object v)
        {
            ((global::UpLoadGameInfo)o).ip = (System.String)v;
        }

        static StackObject* AssignFromStack_ip_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @ip = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::UpLoadGameInfo)o).ip = @ip;
            return ptr_of_this_method;
        }

        static object get_TrainOverDataPostUrl_3(ref object o)
        {
            return ((global::UpLoadGameInfo)o).TrainOverDataPostUrl;
        }

        static StackObject* CopyToStack_TrainOverDataPostUrl_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::UpLoadGameInfo)o).TrainOverDataPostUrl;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
