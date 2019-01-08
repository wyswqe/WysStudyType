using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Utility.ReadCSV
{
    public class ItemCsvBaseData : CSVAnalysis
    {
        public int ItemID;
        public string ItemName = "Item";
        public string Description;

        public override bool AnalySis(string[] data)
        {
            if (data.Length == 0)
            {
                return false;
            }

            ItemID = IntParse(data, 0);
            return true;
        }

        public override string ToString()
        {
            return ItemName;
        }

        public static float[] FloatArray(List<float> parse)
        {
            if (parse == null)
            {
                return null;
            }
            float[] f = new float[parse.Count];
            for (int i = 0; i < parse.Count; i++)
            {
                f[i] = parse[i];
                if (f[i] == 0)
                {
                    return null;
                }
            }
            return f;
        }
    }
}
