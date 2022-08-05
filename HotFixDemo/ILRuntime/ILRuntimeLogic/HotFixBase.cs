using UnityEngine;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace ILRuntime.ILRuntimeLogic
{
    public abstract class HotFixBase : MonoBehaviour
    {
        public string bindClass;
        private AppDomain appDomain;
        private ILRuntimeWrapper ilRuntimeWrapper;

        private IType classType;
        private ILTypeInstance instance;

        public virtual void Init()
        {
            ilRuntimeWrapper = ILRuntimeWrapper.Instance;
            appDomain = ilRuntimeWrapper.appDomain;
        }

        public virtual void InitHotFixMethod()
        {
            if (string.IsNullOrEmpty(bindClass))
            {
                bindClass = "UnityHotFix.Properties.UnityLogic";
            }

            Debug.Log(bindClass);
            if (ILRuntimeWrapper.Instance.IsGameStart)
            {
                classType = appDomain.LoadedTypes[bindClass];
                instance = (classType as ILType)?.Instantiate();

                ilRuntimeWrapper.awakeMethod = classType.GetMethod("Awake", 0);
                ilRuntimeWrapper.startMethod = classType.GetMethod("Start", 0);
                ilRuntimeWrapper.updateMethod = classType.GetMethod("Update", 0);
                ilRuntimeWrapper.onDestroyMethod = classType.GetMethod("OnDestroy", 0);
                ilRuntimeWrapper.fixedUpdateMethod = classType.GetMethod("FixedUpdate", 0);
                ilRuntimeWrapper.lateUpdateMethod = classType.GetMethod("LateUpdate", 0);

                if (ilRuntimeWrapper.awakeMethod != null)
                {
                    appDomain.Invoke(ilRuntimeWrapper.awakeMethod, instance);
                }
            }

            //开始调用热更工程的start
            appDomain.Invoke(ilRuntimeWrapper.startMethod, instance);
        }
    }
}
