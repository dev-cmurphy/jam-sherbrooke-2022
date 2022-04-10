using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] private string paramName;
    [Range(0,1f)]
    [SerializeField] private int intensity;
    [SerializeField] private FMODUnity.StudioEventEmitter emitter;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        emitter.SetParameter(paramName, intensity);
    }
}
