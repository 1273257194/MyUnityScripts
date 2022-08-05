using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

//will auto register in unity
#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static private void RegisterBindingAction()
        {
            ILRuntime.Runtime.CLRBinding.CLRBindingUtils.RegisterBindingAction(Initialize);
        }


        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            UnityEngine_Debug_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            System_String_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            UnityEngine_UI_Image_Binding.Register(app);
            UnityEngine_UI_Text_Binding.Register(app);
            UnityEngine_UI_Button_Binding.Register(app);
            UnityEngine_Events_UnityEvent_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            Slotpart_Tools_MonoSingleton_1_ResMgr_Binding.Register(app);
            ResMgr_Binding.Register(app);
            System_Collections_Generic_List_1_String_Binding.Register(app);
            System_Type_Binding.Register(app);
            System_Action_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_String_ILTypeInstance_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding_ValueCollection_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding_ValueCollection_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_List_1_String_Binding_Enumerator_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            System_Activator_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
