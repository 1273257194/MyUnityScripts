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
    unsafe class Slotpart_Training_PaperAirplane_Scripts_AudioManager_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(Slotpart.Training.PaperAirplane.Scripts.AudioManager);
            args = new Type[]{};
            method = type.GetMethod("get_Instance", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Instance_0);
            args = new Type[]{typeof(UnityEngine.AudioClip)};
            method = type.GetMethod("PlayOne", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, PlayOne_1);

            field = type.GetField("AudioClips", flag);
            app.RegisterCLRFieldGetter(field, get_AudioClips_0);
            app.RegisterCLRFieldSetter(field, set_AudioClips_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_AudioClips_0, AssignFromStack_AudioClips_0);


        }


        static StackObject* get_Instance_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = Slotpart.Training.PaperAirplane.Scripts.AudioManager.Instance;

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* PlayOne_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.AudioClip @audioClip = (UnityEngine.AudioClip)typeof(UnityEngine.AudioClip).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Slotpart.Training.PaperAirplane.Scripts.AudioManager instance_of_this_method = (Slotpart.Training.PaperAirplane.Scripts.AudioManager)typeof(Slotpart.Training.PaperAirplane.Scripts.AudioManager).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.PlayOne(@audioClip);

            return __ret;
        }


        static object get_AudioClips_0(ref object o)
        {
            return ((Slotpart.Training.PaperAirplane.Scripts.AudioManager)o).AudioClips;
        }

        static StackObject* CopyToStack_AudioClips_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Slotpart.Training.PaperAirplane.Scripts.AudioManager)o).AudioClips;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_AudioClips_0(ref object o, object v)
        {
            ((Slotpart.Training.PaperAirplane.Scripts.AudioManager)o).AudioClips = (System.Collections.Generic.Dictionary<System.String, UnityEngine.AudioClip>)v;
        }

        static StackObject* AssignFromStack_AudioClips_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.Dictionary<System.String, UnityEngine.AudioClip> @AudioClips = (System.Collections.Generic.Dictionary<System.String, UnityEngine.AudioClip>)typeof(System.Collections.Generic.Dictionary<System.String, UnityEngine.AudioClip>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((Slotpart.Training.PaperAirplane.Scripts.AudioManager)o).AudioClips = @AudioClips;
            return ptr_of_this_method;
        }



    }
}
