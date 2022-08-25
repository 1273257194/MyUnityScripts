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
    unsafe class Cinemachine_CinemachineDollyCart_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(Cinemachine.CinemachineDollyCart);

            field = type.GetField("m_Path", flag);
            app.RegisterCLRFieldGetter(field, get_m_Path_0);
            app.RegisterCLRFieldSetter(field, set_m_Path_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_Path_0, AssignFromStack_m_Path_0);
            field = type.GetField("m_Position", flag);
            app.RegisterCLRFieldGetter(field, get_m_Position_1);
            app.RegisterCLRFieldSetter(field, set_m_Position_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_m_Position_1, AssignFromStack_m_Position_1);


        }



        static object get_m_Path_0(ref object o)
        {
            return ((Cinemachine.CinemachineDollyCart)o).m_Path;
        }

        static StackObject* CopyToStack_m_Path_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineDollyCart)o).m_Path;
            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_m_Path_0(ref object o, object v)
        {
            ((Cinemachine.CinemachineDollyCart)o).m_Path = (Cinemachine.CinemachinePathBase)v;
        }

        static StackObject* AssignFromStack_m_Path_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            Cinemachine.CinemachinePathBase @m_Path = (Cinemachine.CinemachinePathBase)typeof(Cinemachine.CinemachinePathBase).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((Cinemachine.CinemachineDollyCart)o).m_Path = @m_Path;
            return ptr_of_this_method;
        }

        static object get_m_Position_1(ref object o)
        {
            return ((Cinemachine.CinemachineDollyCart)o).m_Position;
        }

        static StackObject* CopyToStack_m_Position_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Cinemachine.CinemachineDollyCart)o).m_Position;
            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_m_Position_1(ref object o, object v)
        {
            ((Cinemachine.CinemachineDollyCart)o).m_Position = (System.Single)v;
        }

        static StackObject* AssignFromStack_m_Position_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Single @m_Position = *(float*)&ptr_of_this_method->Value;
            ((Cinemachine.CinemachineDollyCart)o).m_Position = @m_Position;
            return ptr_of_this_method;
        }



    }
}
