using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace UnityHotFix.Game.PaperAirplane
{
    public class ParticleSystemsController
    {
        private GameObject particleObj;

        private List<ParticleSystem> particleSystems = new List<ParticleSystem>();
        private List<GameObject> tempObjs = new List<GameObject>();
        public static GameObject tempObj;

        public async void Init()
        {
            particleObj = await Addressables.LoadAssetAsync<GameObject>("HoleObj").ToUniTask();
        }

        public GameObject GetObj()
        {
            tempObj = Object.Instantiate(particleObj) as GameObject;
            // tempObjs.Add(tempObj); 
            return tempObj;
        }


        public async void SetEffect()
        {
            particleSystems = tempObj.GetComponentsInChildren<ParticleSystem>().ToList();
            for (int i = 0; i < particleSystems.Count; i++)
            {
                var current = particleSystems[i];
                var emission = current.emission;
                var main = current.main;
                var shape = current.shape;
                var noise = current.noise;
                var trails = current.trails;
                trails.enabled = false;
                noise.enabled = true;
                noise.strength = 1.2f;
                main.loop = false;
                main.startSpeed = 3;
                emission.enabled = true;
                emission.rateOverTimeMultiplier = 0;
                emission.SetBursts(new ParticleSystem.Burst[] {new ParticleSystem.Burst(0, 200)});
                shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            Object.Destroy(tempObj.gameObject);
        }

        public async void Wait()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            if (tempObjs.Count <= 1)
            {
                return;
            }

            Object.Destroy(tempObjs[tempObjs.Count - 1]);
            tempObjs.RemoveAt(tempObjs.Count - 1);
        }
    }
}