﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BotLight
{
    /// <summary>
    /// Could be used to handle enabling / disabling bots
    /// </summary>
    [Serializable]
    public class BotManager
    {
        
        [HideInInspector] public GameObject instance;         // A reference to the instance of the bot when it is created.
        private StateController stateController;              // Reference to the StateController for AI
        private BotAttack botAttack;
        private BotHealth botHealth;
        private BotMovement botMovement;

        public void SetupAI()
        {
            
            botAttack = instance.GetComponent<BotAttack>();

            botMovement = instance.GetComponent<BotMovement>();

            stateController = instance.GetComponent<StateController>();
            botMovement.SetupAI(stateController.SetupAI(true));

        }
    }
}