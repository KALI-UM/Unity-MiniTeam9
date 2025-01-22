using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveData
{
    public int Verison { get;  protected set; }

    public abstract SaveData VersionUp();
}

public class SaveDataV1 : SaveData
{
    public string PlayerName = "TEST";

    public SaveDataV1()
    {
        Verison = 1;
    }

    public override SaveData VersionUp()
    {
        return this;
    }
}

