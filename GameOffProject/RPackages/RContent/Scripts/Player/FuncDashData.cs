// Created by Hugo Da Maïa on 24/11/2023

#nullable enable

using UnityEngine;

namespace Rezoskour.Content
{
    [CreateAssetMenu(fileName = "FuncDash", menuName = "Rezoskour/Func Dash Data", order = 0)]
    internal class FuncDashData : DashData
    {
        [SerializeField] private TrajFunction typeOfTraj = TrajFunction.Sin;

        public TrajFunction TypeOfTraj => typeOfTraj;
    }
}