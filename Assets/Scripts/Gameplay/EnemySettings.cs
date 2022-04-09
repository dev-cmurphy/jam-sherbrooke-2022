using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="EnemySettings", menuName ="Settings/Enemy Settings", order =-1)]
public class EnemySettings : ScriptableObject
{
    public float Sight;
    public float ChaseTime;
}
