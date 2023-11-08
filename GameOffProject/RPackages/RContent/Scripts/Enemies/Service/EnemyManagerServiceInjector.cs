// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable
using System;
using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content
{
    internal class EnemyManagerServiceInjector : MonoBehaviour
    {
        private void Awake()
        {
            IEnemyManager manager = new EnemyManager();
            KServiceInjection.Add<IEnemyManager>(manager);
        }

        private void OnDestroy()
        {
            KServiceInjection.Remove<IEnemyManager>();
        }
    }
}