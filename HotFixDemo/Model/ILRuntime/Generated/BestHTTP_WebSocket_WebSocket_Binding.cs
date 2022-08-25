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
    unsafe class BestHTTP_WebSocket_WebSocket_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(BestHTTP.WebSocket.WebSocket);
            args = new Type[]{};
            method = type.GetMethod("Open", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Open_0);
            args = new Type[]{};
            method = type.GetMethod("Close", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Close_1);
            args = new Type[]{};
            method = type.GetMethod("get_InternalRequest", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_InternalRequest_2);

            field = type.GetField("OnOpen", flag);
            app.RegisterCLRFieldGetter(field, get_OnOpen_0);
            app.RegisterCLRFieldSetter(field, set_OnOpen_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_OnOpen_0, AssignFromStack_OnOpen_0);
            field = type.GetField("OnMessage", flag);
            app.RegisterCLRFieldGetter(field, get_OnMessage_1);
            app.RegisterCLRFieldSetter(field, set_OnMessage_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_OnMessage_1, AssignFromStack_OnMessage_1);
            field = type.GetField("OnError", flag);
            app.RegisterCLRFieldGetter(field, get_OnError_2);
            app.RegisterCLRFieldSetter(field, set_OnError_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_OnError_2, AssignFromStack_OnError_2);
            field = type.GetField("OnClosed", flag);
            app.RegisterCLRFieldGetter(field, get_OnClosed_3);
            app.RegisterCLRFieldSetter(field, set_OnClosed_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_OnClosed_3, AssignFromStack_OnClosed_3);

            args = new Type[]{typeof(System.Uri)};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* Open_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            BestHTTP.WebSocket.WebSocket instance_of_this_method = (BestHTTP.WebSocket.WebSocket)typeof(BestHTTP.WebSocket.WebSocket).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Open();

            return __ret;
        }

        static StackObject* Close_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            BestHTTP.WebSocket.WebSocket instance_of_this_method = (BestHTTP.WebSocket.WebSocket)typeof(BestHTTP.WebSocket.WebSocket).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Close();

            return __ret;
        }

        static StackObject* get_InternalRequest_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            BestHTTP.WebSocket.WebSocket instance_of_this_method = (BestHTTP.WebSocket.WebSocket)typeof(BestHTTP.WebSocket.WebSocket).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.InternalRequest;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object get_OnOpen_0(ref object o)
        {
            return ((BestHTTP.WebSocket.WebSocket)o).OnOpen;
        }

        static StackObject* CopyToStack_OnOpen_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((BestHTTP.WebSocket.WebSocket)o).OnOpen;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_OnOpen_0(ref object o, object v)
        {
            ((BestHTTP.WebSocket.WebSocket)o).OnOpen = (BestHTTP.WebSocket.OnWebSocketOpenDelegate)v;
        }

        static StackObject* AssignFromStack_OnOpen_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            BestHTTP.WebSocket.OnWebSocketOpenDelegate @OnOpen = (BestHTTP.WebSocket.OnWebSocketOpenDelegate)typeof(BestHTTP.WebSocket.OnWebSocketOpenDelegate).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((BestHTTP.WebSocket.WebSocket)o).OnOpen = @OnOpen;
            return ptr_of_this_method;
        }

        static object get_OnMessage_1(ref object o)
        {
            return ((BestHTTP.WebSocket.WebSocket)o).OnMessage;
        }

        static StackObject* CopyToStack_OnMessage_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((BestHTTP.WebSocket.WebSocket)o).OnMessage;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_OnMessage_1(ref object o, object v)
        {
            ((BestHTTP.WebSocket.WebSocket)o).OnMessage = (BestHTTP.WebSocket.OnWebSocketMessageDelegate)v;
        }

        static StackObject* AssignFromStack_OnMessage_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            BestHTTP.WebSocket.OnWebSocketMessageDelegate @OnMessage = (BestHTTP.WebSocket.OnWebSocketMessageDelegate)typeof(BestHTTP.WebSocket.OnWebSocketMessageDelegate).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((BestHTTP.WebSocket.WebSocket)o).OnMessage = @OnMessage;
            return ptr_of_this_method;
        }

        static object get_OnError_2(ref object o)
        {
            return ((BestHTTP.WebSocket.WebSocket)o).OnError;
        }

        static StackObject* CopyToStack_OnError_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((BestHTTP.WebSocket.WebSocket)o).OnError;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_OnError_2(ref object o, object v)
        {
            ((BestHTTP.WebSocket.WebSocket)o).OnError = (BestHTTP.WebSocket.OnWebSocketErrorDelegate)v;
        }

        static StackObject* AssignFromStack_OnError_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            BestHTTP.WebSocket.OnWebSocketErrorDelegate @OnError = (BestHTTP.WebSocket.OnWebSocketErrorDelegate)typeof(BestHTTP.WebSocket.OnWebSocketErrorDelegate).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((BestHTTP.WebSocket.WebSocket)o).OnError = @OnError;
            return ptr_of_this_method;
        }

        static object get_OnClosed_3(ref object o)
        {
            return ((BestHTTP.WebSocket.WebSocket)o).OnClosed;
        }

        static StackObject* CopyToStack_OnClosed_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((BestHTTP.WebSocket.WebSocket)o).OnClosed;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_OnClosed_3(ref object o, object v)
        {
            ((BestHTTP.WebSocket.WebSocket)o).OnClosed = (BestHTTP.WebSocket.OnWebSocketClosedDelegate)v;
        }

        static StackObject* AssignFromStack_OnClosed_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            BestHTTP.WebSocket.OnWebSocketClosedDelegate @OnClosed = (BestHTTP.WebSocket.OnWebSocketClosedDelegate)typeof(BestHTTP.WebSocket.OnWebSocketClosedDelegate).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((BestHTTP.WebSocket.WebSocket)o).OnClosed = @OnClosed;
            return ptr_of_this_method;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Uri @uri = (System.Uri)typeof(System.Uri).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = new BestHTTP.WebSocket.WebSocket(@uri);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
