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
    unsafe class ResMgr_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::ResMgr);

            field = type.GetField("parents", flag);
            app.RegisterCLRFieldGetter(field, get_parents_0);
            app.RegisterCLRFieldSetter(field, set_parents_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_parents_0, AssignFromStack_parents_0);
            field = type.GetField("loadPool", flag);
            app.RegisterCLRFieldGetter(field, get_loadPool_1);
            app.RegisterCLRFieldSetter(field, set_loadPool_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_loadPool_1, AssignFromStack_loadPool_1);


        }



        static object get_parents_0(ref object o)
        {
            return ((global::ResMgr)o).parents;
        }

        static StackObject* CopyToStack_parents_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::ResMgr)o).parents;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_parents_0(ref object o, object v)
        {
            ((global::ResMgr)o).parents = (UnityEngine.Transform)v;
        }

        static StackObject* AssignFromStack_parents_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Transform @parents = (UnityEngine.Transform)typeof(UnityEngine.Transform).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ResMgr)o).parents = @parents;
            return ptr_of_this_method;
        }

        static object get_loadPool_1(ref object o)
        {
            return ((global::ResMgr)o).loadPool;
        }

        static StackObject* CopyToStack_loadPool_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::ResMgr)o).loadPool;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_loadPool_1(ref object o, object v)
        {
            ((global::ResMgr)o).loadPool = (System.Collections.Generic.List<System.String>)v;
        }

        static StackObject* AssignFromStack_loadPool_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<System.String> @loadPool = (System.Collections.Generic.List<System.String>)typeof(System.Collections.Generic.List<System.String>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ResMgr)o).loadPool = @loadPool;
            return ptr_of_this_method;
        }



    }
}
