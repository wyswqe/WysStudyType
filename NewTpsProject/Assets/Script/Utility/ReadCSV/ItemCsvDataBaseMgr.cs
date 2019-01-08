using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Utility.ReadCSV
{

    public class ItemCsvDataBaseMgr<TK> : TSingleton<TK>
    {
        protected ItemCsvBaseData[] CurrentItemData;

        protected virtual CsvNameType CurrentCsvName
        {
            get { return CsvNameType.WeaponsData; }
        }

        public virtual T GetDataByItemId<T>(int itemId) where T : ItemCsvBaseData
        {
            for (int i = 0; i < CurrentItemData.Length; i++)
            {
                if (CurrentItemData[i].ItemID == itemId)
                {
                    return CurrentItemData[i] as T;
                }
            }
            DebugHelper.Log(typeof(T) + " don't have the item is the ItemId = " + itemId);
            return null;
        }

        public override void Init()
        {
            base.Init();
            // DebugHelper.LogError("CurrentXmlName  " + CurrentXmlName);
            List<CSVAnalysis> tempData = ReadTextAssetMgr.instance.dicCsvMode[CurrentCsvName];
            CurrentItemData = new ItemCsvBaseData[tempData.Count];
            for (int i = 0; i < tempData.Count; i++)
            {
                if (tempData[i] is ItemCsvBaseData == false)
                {
                    DebugHelper.LogError("tempData[i] = " + (tempData[i].ItemCsvName));
                }
                CurrentItemData[i] = (ItemCsvBaseData)tempData[i];
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}

