using System.Collections.Generic;
using UnityEngine;

public class AttackBank : MonoBehaviour
{
    [SerializeField]
    private List<AttackPattern> patterns;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AttackPattern GetRandomPattern()
    {
        return patterns[Random.Range(0, patterns.Count)];
    }
}
