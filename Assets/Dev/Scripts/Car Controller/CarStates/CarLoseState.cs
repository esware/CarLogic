﻿using Managers;
using UnityEngine;

namespace Dev.Scripts.Car_Controller.CarStates
{
    public class CarLoseState:BaseState<CarController>
    {
        public CarLoseState(CarController controller) : base(controller)
        {
            GameEvents.FailEvent?.Invoke();
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}