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
    unsafe class Slotpart_Training_Limb_Scripts_GameSceneObjController_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(Slotpart.Training.Limb.Scripts.GameSceneObjController);

            field = type.GetField("sceneObjs", flag);
            app.RegisterCLRFieldGetter(field, get_sceneObjs_0);
            app.RegisterCLRFieldSetter(field, set_sceneObjs_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_sceneObjs_0, AssignFromStack_sceneObjs_0);


        }



        static object get_sceneObjs_0(ref object o)
        {
            return ((Slotpart.Training.Limb.Scripts.GameSceneObjController)o).sceneObjs;
        }

        static StackObject* CopyToStack_sceneObjs_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Slotpart.Training.Limb.Scripts.GameSceneObjController)o).sceneObjs;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_sceneObjs_0(ref object o, object v)
        {
            ((Slotpart.Training.Limb.Scripts.GameSceneObjController)o).sceneObjs = (System.Collections.Generic.Dictionary<System.String, UnityEngine.GameObject>)v;
        }

        static StackObject* AssignFromStack_sceneObjs_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.Dictionary<System.String, UnityEngine.GameObject> @sceneObjs = (System.Collections.Generic.Dictionary<System.String, UnityEngine.GameObject>)typeof(System.Collections.Generic.Dictionary<System.String, UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((Slotpart.Training.Limb.Scripts.GameSceneObjController)o).sceneObjs = @sceneObjs;
            return ptr_of_this_method;
        }



    }
}
