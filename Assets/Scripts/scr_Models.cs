using System;
using UnityEngine;


public static class scr_Models 
{
        #region - Player -
        
        [Serializable]
        public class PlayerSettingsModel
        {
            [Header("View Settings Mouse")]
            public float ViewXSensitivityM;
            public float ViewYSensitivityM;

            public bool ViewXInverted;
            public bool ViewYInverted;
            
            [Header("View Settings Controller")]
            public float ViewXSensitivityC;
            public float ViewYSensitivityC;

            [Header("Movement - Running")]
            public float RunningForwardSpeed;
            public float RunningStrafeSpeed;
            
            [Header("Movement - Walking")]
            public float WalkingForwardSpeed;
            public float WalkingStrafeSpeed;

            [Header("Jump")]
            public float JumpingHeight;
            public float JumpingFallOff;
        }
        
        #endregion
}
