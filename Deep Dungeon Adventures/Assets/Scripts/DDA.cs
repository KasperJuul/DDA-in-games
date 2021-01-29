using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Meta.Numerics.Data;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;

public class DDA : MonoBehaviour
{
    FrameTable table = new FrameTable();
    float trackingRate = 1f;
    float nextTrack = 0f;
    SummaryStatistics damageSample = new SummaryStatistics();
    public Transform detector;
    [SerializeField] private LayerMask whatIsEnemy;
    public int accumulatedDamage = 0;
    public double p = 0;
    public Collider2D[] encounters;
    public Health health;
    [SerializeField] private bool inFight = false;
    public RoomTrigger bossRoom;
    private string path = "";


    private void Start()
    {
        table.AddColumn<int>("Damage");
        health = GetComponent<Health>();
        CreateLogFile();
    }

    private void Update()
    {
        encounters = Physics2D.OverlapCircleAll(detector.position, 2f, whatIsEnemy);
        if(encounters.Length > 0)
        {
            inFight = true;
        }
        else
        {
            inFight = false;
            
        }

        if (inFight && Time.time > nextTrack)
        {
            RegisterDamage(accumulatedDamage);
            accumulatedDamage = 0;
            nextTrack = Time.time + trackingRate;

            if(damageSample.Count > 4)
            {
                Debug.Log("mean of sample: " + damageSample.Mean);
                Debug.Log("SD of sample: " + damageSample.StandardDeviation);
                if (damageSample.StandardDeviation != 0)
                {
                    NormalDistribution normalDistribution = new NormalDistribution(damageSample.Mean, damageSample.StandardDeviation);
                    p = normalDistribution.RightProbability(0);
                }

                if (p < 0.75 && health.currenthealth > 70) // Comfort Zone
                {
                    EnemySettings.Instance.attackTimer -= 0.1f;
                    if(EnemySettings.Instance.bossAttackTimer > 1f)
                    {
                        EnemySettings.Instance.bossAttackTimer -= 0.05f;
                    }
                    if (bossRoom.inRoom && EnemySettings.Instance.bossAttackTimer > 0.6f)
                    {
                        EnemySettings.Instance.bossAttackTimer -= 0.1f;
                    }
                }
                else if (p > .5 && health.currenthealth < 50) // Discomfort Zone
                {
                    EnemySettings.Instance.attackTimer = 0.8f;
                    if (bossRoom.inRoom)
                    {
                        EnemySettings.Instance.bossAttackTimer = 1.5f;
                    }
                }
                Debug.Log("probability of getting damage is: " + p);
                AddToLog("probability of getting damage is: " + p);
            }
        }
    }

    public void RegisterDamage(int damage)
    {
        damageSample.Add(damage);
    }

    public void ClearSample()
    {
        damageSample = new SummaryStatistics();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(detector.position, 2f);
    }

    void CreateLogFile()
    {
        path = Application.dataPath + "/Logs" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second + ".txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Log for Dynamic game at " + System.DateTime.Now + "\n\n\n");
        }
    }

    void AddToLog(string log)
    {
        File.AppendAllText(path, log + "\n\n");
    }
}
