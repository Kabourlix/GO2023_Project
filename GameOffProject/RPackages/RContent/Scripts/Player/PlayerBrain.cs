// Copyrighted by team Rézoskour
// Created by alexandre buzon on 14

using System;
using SDKabu.KCharacter;
using UnityEngine;

namespace Rezoskour.Content
{
    public class PlayerBrain : MonoBehaviour, IKActor
    {
        public Guid ID { get; } = Guid.NewGuid();
        public KHealthComp HealthComp { get; private set; } = null!;

        private void Start()
        {
            HealthComp = new KHealthComp(4, this);
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            HealthComp.Dispose();
        }
    }
}