using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_BehaviorChanger : MonoBehaviour
{
    public AI ai;
    public AI_BT aibt;
    public AI_FSM aifsm;

    public AI2 ai2;
    public AI_BT2 aibt2;
    public AI_FSM2 aifsm2;

    public Material[] materials; // Array of materials
    private int activeBehavior = 0;

    private enum Engine
    {
        SOURCE_CODE,
        FSM,
        BT
    }

    private Engine activeEngine = Engine.SOURCE_CODE;


    // Update is called once per frame
    void Update()
    {
        // Change to BT
        if (Input.GetKeyDown(KeyCode.B))
        {
            DisableScripts();
            if (activeBehavior == 0) aibt.enabled = true;
            if (activeBehavior == 1) aibt2.enabled = true;
            activeEngine = Engine.BT;
        }

        // Change to FSM
        if (Input.GetKeyDown(KeyCode.X))
        {
            DisableScripts();
            if (activeBehavior == 0) aifsm.enabled = true;
            if (activeBehavior == 1) aifsm2.enabled = true;
        }

        // Change to Source Code
        if (Input.GetKeyDown(KeyCode.S))
        {
            DisableScripts();
            if (activeBehavior == 0) ai.enabled = true;
            if (activeBehavior == 1) ai2.enabled = true;
            activeEngine = Engine.SOURCE_CODE;
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeBehavior();
        }
    }

    void DisableScripts()
    {
        ai.enabled = false;
        aibt.enabled = false;
        aifsm.enabled = false;
        ai2.enabled = false;
        aibt2.enabled = false;
        aifsm2.enabled = false;
    }

    void ChangeBehavior()
    {
        Renderer renderer = GetComponent<Renderer>();
        activeBehavior = activeBehavior == 0 ? 1 : 0;
        renderer.material = materials[activeBehavior];
        
        DisableScripts();
        switch (activeEngine)
        {
            case Engine.SOURCE_CODE:
                if (activeBehavior == 0) ai.enabled = true;
                if (activeBehavior == 1) ai2.enabled = true;
                break;
            case Engine.FSM:
                if (activeBehavior == 0) aifsm.enabled = true;
                if (activeBehavior == 1) aifsm2.enabled = true;
                break;
            case Engine.BT:
                if (activeBehavior == 0) aibt.enabled = true;
                if (activeBehavior == 1) aibt2.enabled = true;
                break;
        }
    }
}