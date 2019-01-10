using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Utility.ReadCSV;
using Assets.Script.Utility;

public class WeaponsData : ItemCsvBaseData
{
    public string describe;
    public string name;

    public int speed;

    public override CsvNameType ItemCsvName
    {
        get { return CsvNameType.WeaponsData; }
    }

    public override bool AnalySis(string[] data)
    {
        if (base.AnalySis(data))
        {
            describe = StrParse(data, 2);
            name = StrParse(data, 3);
            return true;
        }
        return false;
    }

}
