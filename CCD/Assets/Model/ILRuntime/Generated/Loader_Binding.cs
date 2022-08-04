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
    unsafe class Loader_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::Loader);

            field = type.GetField("asset", flag);
            app.RegisterCLRFieldGetter(field, get_asset_0);
            app.RegisterCLRFieldSetter(field, set_asset_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_asset_0, AssignFromStack_asset_0);
            field = type.GetField("TestFunctionDelegate", flag);
            app.RegisterCLRFieldGetter(field, get_TestFunctionDelegate_1);
            app.RegisterCLRFieldSetter(field, set_TestFunctionDelegate_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_TestFunctionDelegate_1, AssignFromStack_TestFunctionDelegate_1);


        }



        static object get_asset_0(ref object o)
        {
            return ((global::Loader)o).asset;
        }

        static StackObject* CopyToStack_asset_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::Loader)o).asset;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_asset_0(ref object o, object v)
        {
            ((global::Loader)o).asset = (UnityEngine.AddressableAssets.AssetReference)v;
        }

        static StackObject* AssignFromStack_asset_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.AddressableAssets.AssetReference @asset = (UnityEngine.AddressableAssets.AssetReference)typeof(UnityEngine.AddressableAssets.AssetReference).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::Loader)o).asset = @asset;
            return ptr_of_this_method;
        }

        static object get_TestFunctionDelegate_1(ref object o)
        {
            return ((global::Loader)o).TestFunctionDelegate;
        }

        static StackObject* CopyToStack_TestFunctionDelegate_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::Loader)o).TestFunctionDelegate;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_TestFunctionDelegate_1(ref object o, object v)
        {
            ((global::Loader)o).TestFunctionDelegate = (global::Loader.TestDelegateFunction)v;
        }

        static StackObject* AssignFromStack_TestFunctionDelegate_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::Loader.TestDelegateFunction @TestFunctionDelegate = (global::Loader.TestDelegateFunction)typeof(global::Loader.TestDelegateFunction).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            ((global::Loader)o).TestFunctionDelegate = @TestFunctionDelegate;
            return ptr_of_this_method;
        }



    }
}
