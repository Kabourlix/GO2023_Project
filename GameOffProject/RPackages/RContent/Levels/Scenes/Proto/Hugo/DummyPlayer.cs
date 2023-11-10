// Created by Kabourlix Cendrée on 11/11/2023

#nullable enable

using System;
using SDKabu.KCharacter;
using UnityEngine;

namespace Rezoskour.Content.Levels.Scenes.Proto.Hugo
{
    public class DummyPlayer : MonoBehaviour, IKActor
    {
        public Guid ID { get; } = Guid.NewGuid();
        public KHealthComp HealthComp { get; private set; } = null!;

        private void Awake()
        {
            HealthComp = new KHealthComp(2000, this);
        }
    }
}