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
            Slotpart_Tools_MonoSingleton_1_ResMgr_Binding.Register(app);
            ResMgr_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
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
            UnityEngine_GameObject_Binding.Register(app);
            UnityEngine_AddressableAssets_Addressables_Binding.Register(app);
            UnityEngine_ResourceManagement_AsyncOperations_AsyncOperationHandle_1_GameObject_Binding.Register(app);
            System_Activator_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            UnityEngine_UI_Image_Binding.Register(app);
            UnityEngine_UI_Text_Binding.Register(app);
            UnityEngine_UI_Button_Binding.Register(app);
            UnityEngine_Events_UnityEvent_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            System_Int32_Binding.Register(app);
            UpLoadGameInfo_Binding.Register(app);
            GameStartInfo_Binding.Register(app);
            Slotpart_Tools_MonoSingleton_1_SendMsg_Binding.Register(app);
            SendMsg_Binding.Register(app);
            UnityEngine_Color_Binding.Register(app);
            UnityEngine_UI_Graphic_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncVoidMethodBuilder_Binding.Register(app);
            Slotpart_Tools_MonoSingleton_1_GameSceneObjController_Binding.Register(app);
            DotweenTool_Binding.Register(app);
            System_TimeSpan_Binding.Register(app);
            Cysharp_Threading_Tasks_UniTask_Binding.Register(app);
            Cysharp_Threading_Tasks_UniTask_Binding_Awaiter_Binding.Register(app);
            Cysharp_Threading_Tasks_DOTweenAsyncExtensions_Binding.Register(app);
            Cysharp_Threading_Tasks_DOTweenAsyncExtensions_Binding_TweenAwaiter_Binding.Register(app);
            System_Uri_Binding.Register(app);
            BestHTTP_WebSocket_WebSocket_Binding.Register(app);
            System_Console_Binding.Register(app);
            System_Action_1_String_Binding.Register(app);
            BestHTTP_HTTPRequest_Binding.Register(app);
            BestHTTP_HTTPResponse_Binding.Register(app);
            Slotpart_Training_Limb_Scripts_GameSceneObjController_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_GameObject_Binding.Register(app);
            Slotpart_Tools_MonoSingleton_1_DotweenTool_Binding.Register(app);
            UnityEngine_Mathf_Binding.Register(app);
            UnityEngine_Application_Binding.Register(app);
            Slotpart_Tools_BaseSingleton_1_UpLoadGameInfo_Binding.Register(app);
            UnityEngine_Random_Binding.Register(app);
            UnityEngine_Camera_Binding.Register(app);
            Cinemachine_CinemachineDollyCart_Binding.Register(app);
            System_DateTime_Binding.Register(app);
            LitJson_JsonMapper_Binding.Register(app);
            // Cysharp_Threading_Tasks_CompilerServices_AsyncUniTaskMethodBuilder_Binding.Register(app);
            Cinemachine_CinemachinePathBase_Binding.Register(app);
            Slotpart_Training_PaperAirplane_Scripts_AudioManager_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_AudioClip_Binding.Register(app);
            UnityEngine_Behaviour_Binding.Register(app);
            UnityEngine_Time_Binding.Register(app);
            System_Single_Binding.Register(app);
            DeviceInitInfo_Binding.Register(app);
            Slotpart_Tools_BaseSingleton_1_DataPostTool_Binding.Register(app);
            DataPostTool_Binding.Register(app);
            UnityEngine_Animator_Binding.Register(app);
            DG_Tweening_ShortcutExtensions_Binding.Register(app);
            System_Collections_Generic_List_1_ParticleSystem_Binding.Register(app);
            System_Collections_Generic_List_1_GameObject_Binding.Register(app);
            Cysharp_Threading_Tasks_AddressablesAsyncExtensions_Binding.Register(app);
            Cysharp_Threading_Tasks_UniTask_1_GameObject_Binding.Register(app);
            Cysharp_Threading_Tasks_UniTask_1_GameObject_Binding_Awaiter_Binding.Register(app);
            System_Linq_Enumerable_Binding.Register(app);
            UnityEngine_ParticleSystem_Binding.Register(app);
            UnityEngine_ParticleSystem_Binding_TrailModule_Binding.Register(app);
            UnityEngine_ParticleSystem_Binding_NoiseModule_Binding.Register(app);
            UnityEngine_ParticleSystem_Binding_MinMaxCurve_Binding.Register(app);
            UnityEngine_ParticleSystem_Binding_MainModule_Binding.Register(app);
            UnityEngine_ParticleSystem_Binding_EmissionModule_Binding.Register(app);
            UnityEngine_ParticleSystem_Binding_Burst_Binding.Register(app);
            UnityEngine_ParticleSystem_Binding_ShapeModule_Binding.Register(app);
            System_Action_1_Int32_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_List_1_Transform_Binding.Register(app);
            UnityEngine_Renderer_Binding.Register(app);
            UnityEngine_Material_Binding.Register(app);
            UnityEngine_Shader_Binding.Register(app);
            System_Collections_Generic_List_1_Transform_Binding.Register(app);
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
