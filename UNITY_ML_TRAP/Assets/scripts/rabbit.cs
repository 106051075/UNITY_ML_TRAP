using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class rabbit : Agent
{
    [Header("速度"), Range(1, 50)]
    public float speed = 10;
    private Rigidbody rigrabbit;
    private Rigidbody rigcarrot;
    private Rigidbody rigfarmer;

    private void Start()
    {
        rigrabbit = GetComponent<Rigidbody>();
        rigcarrot = GameObject.Find("蘿蔔").GetComponent<Rigidbody>();
        rigfarmer = GameObject.Find("農夫").GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        //重設兔子、蘿蔔和農夫的加速度與角速度
        rigrabbit.velocity = Vector3.zero;
        rigrabbit.angularVelocity = Vector3.zero;

        rigcarrot.velocity = Vector3.zero;
        rigcarrot.angularVelocity = Vector3.zero;

        rigfarmer.velocity = Vector3.zero;
        rigfarmer.angularVelocity = Vector3.zero;


        //隨機兔子初始位置
        Vector3 posrabbit = new Vector3(Random.Range(-1f, 1f), 0.1f, Random.Range(-1f, 1f));
        transform.position = posrabbit;
        //隨機蘿蔔和農夫初始位置
        Vector3 poscarrot = new Vector3(Random.Range(-2f, 2f), 0.1f, Random.Range(-2f, 2f));
        rigcarrot.position = poscarrot;
        Vector3 posfarmer = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(-4f, 4f));
        rigfarmer.position = posfarmer;

        carrot.complete = false;
        farmer.complete = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigfarmer.position);
        sensor.AddObservation(rigcarrot.position);
        sensor.AddObservation(rigrabbit.velocity.x);
        sensor.AddObservation(rigrabbit.velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rigrabbit.AddForce(control * speed);


        if (carrot.complete)
        {
            SetReward(1);
            EndEpisode();
        }

        if (transform.position.y < 0 || farmer.complete)
        {
            SetReward(-1);
            EndEpisode();
        }

    }

    
    //public override void Heuristic(float[] actionsOut)
    //{
        //提供開發者控制的方式
        //var action = new float[2];
        //actionsOut[0] = Input.GetAxis("Horizontal");
        //actionsOut[1] = Input.GetAxis("Vertical");
    //}

}

