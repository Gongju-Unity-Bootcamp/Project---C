using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum IsaacID
{
    None = 1000,
    Isaac = 1001
}
public class IsaacData : MonoBehaviour
{
    public IsaacID ID { get; set; }
    public string Name { get; set; }
    public float Attack { get; set; }
    public float AttackSpeed { get; set;}
    public float Speed { get; set; }
    public float Range { get; set; }
    public int Cost { get; set; }   
    public string Sprite { get; set; }
    public string IsaacTone { get; set; }

    
}
