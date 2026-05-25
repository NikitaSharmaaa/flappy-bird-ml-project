using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Go_Between_Pipe : Agent
{
    public LogicScript LogicScript;
    public Birdscript Birdscript;
    public RayPerceptionSensorComponent3D raySensor;
    public Vector3 startPosition;

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];

        if (action == 1)
        {
            Birdscript.jump();
        }

        if (Birdscript.isBirdAlive)
        {
            AddReward(0.1f);
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("Agent Initialized");
        startPosition = transform.position;
        Birdscript = GetComponent<Birdscript>();
        raySensor = GetComponentInChildren<RayPerceptionSensorComponent3D>();
    }

    public override void OnEpisodeBegin()
    {
        //Resetting Game
        LogicScript.restartgame();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Birdscript.isBirdAlive && other.CompareTag("Score"))
        {
            AddReward(1f);  // reward for crossing pipe
            LogicScript.addscore(1);
            Debug.Log("Passed pipe!");
        }
    }

    public void death()
    {
        if (!Birdscript.isBirdAlive) return; // Prevent duplicate calls when the bird is already dead

        Birdscript.isBirdAlive = false; // Mark the bird as dead
        Debug.Log("Bird Died At Go_Between_Pipe Death()");
        AddReward(-100.0f);
        EndEpisode();   //Episode End -> Calls OnEpisodeBegin()
    }

    public void reward(float delta)
    {
        AddReward((float)delta);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        // 0 = Do nothing, 1 = Flap (jump)
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

}
