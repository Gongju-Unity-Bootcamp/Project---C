using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    IItem m_Iitem { get; set; }

    ItemType Type { get; set; }
    void UsingItems();

}
