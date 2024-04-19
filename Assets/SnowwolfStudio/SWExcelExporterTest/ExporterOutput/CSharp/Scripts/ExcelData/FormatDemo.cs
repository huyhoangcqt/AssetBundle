using System.IO;
using System.Collections.Generic;

namespace ExcelData
{
    public class FormatDemo : ILocalizationSheet
    {
        public class Item
        {
            public string key;
            public int intVal;
            public uint uintVal;
            public long longVal;
            public ulong ulongVal;
            public byte byteVal;
            public float floatVal;
            public double doubleVal;
            public string strVal;
            public string __lKey_lstrVal;
            public string lstrVal;
            public int[] intArray;
            public float[] floatArray;
            public string[] stringArray;
            public float clientOnlyVal;
        }

        private static FormatDemo s_Instance;
        private static FormatDemo Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new FormatDemo();
                    s_Instance.Init();
                    DataService.RegisterSheet(s_Instance);
                }
                return s_Instance;
            }
        }

        public static Item GetItem(string key)
        {
            Instance.m_Items.TryGetValue(key, out Item foundItem);
            #if UNITY_EDITOR
            if (foundItem == null)
            {
                UnityEngine.Debug.LogWarningFormat("{0} do not contains item of key '{1}'.", Instance.sheetName, key);
            }
            #endif
            return foundItem;
        }

        public static IEnumerable<KeyValuePair<string, Item>> GetDict()
        {
            return Instance.m_Items;
        }

        private Dictionary<string, Item> m_Items = new Dictionary<string, Item>();

        public string sheetName => "FormatDemo";

        private void Init()
        {
            byte[] bytes = DataService.GetSheetBytes(sheetName);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using(BinaryReader reader = new BinaryReader(ms))
                {
                    reader.ReadString(); //sheetName

                    //Read header
                    SheetHeader sheetHeader = new SheetHeader();
                    sheetHeader.ReadFrom(reader);
                    List<SheetHeader.Item> headerItems = sheetHeader.items;

                    int columns = headerItems.Count;
                    int rows = reader.ReadInt32();

                    //Get Item indices
                    int keyIndex = sheetHeader.IndexOf("key", "string");
                    int intValIndex = sheetHeader.IndexOf("intVal", "int");
                    int uintValIndex = sheetHeader.IndexOf("uintVal", "uint");
                    int longValIndex = sheetHeader.IndexOf("longVal", "long");
                    int ulongValIndex = sheetHeader.IndexOf("ulongVal", "ulong");
                    int byteValIndex = sheetHeader.IndexOf("byteVal", "byte");
                    int floatValIndex = sheetHeader.IndexOf("floatVal", "float");
                    int doubleValIndex = sheetHeader.IndexOf("doubleVal", "double");
                    int strValIndex = sheetHeader.IndexOf("strVal", "string");
                    int lstrValIndex = sheetHeader.IndexOf("lstrVal", "localizedstring");
                    int intArrayIndex = sheetHeader.IndexOf("intArray", "int[]");
                    int floatArrayIndex = sheetHeader.IndexOf("floatArray", "float[]");
                    int stringArrayIndex = sheetHeader.IndexOf("stringArray", "string[]");
                    int clientOnlyValIndex = sheetHeader.IndexOf("clientOnlyVal", "float");

                    #if UNITY_EDITOR
                    bool promptMismatchColumns = false;
                    #endif
                    for (int i = 0; i < rows; ++i)
                    {
                        Item newItem = new Item();
                        for (int j = 0; j < columns; ++j)
                        {
                            SheetHeader.Item headerItem = headerItems[j];

                            if (j == keyIndex)
                            {
                                newItem.key = reader.ReadString();
                            }
                            else if (j == intValIndex)
                            {
                                newItem.intVal = reader.ReadInt32();
                            }
                            else if (j == uintValIndex)
                            {
                                newItem.uintVal = reader.ReadUInt32();
                            }
                            else if (j == longValIndex)
                            {
                                newItem.longVal = reader.ReadInt64();
                            }
                            else if (j == ulongValIndex)
                            {
                                newItem.ulongVal = reader.ReadUInt64();
                            }
                            else if (j == byteValIndex)
                            {
                                newItem.byteVal = reader.ReadByte();
                            }
                            else if (j == floatValIndex)
                            {
                                newItem.floatVal = reader.ReadSingle();
                            }
                            else if (j == doubleValIndex)
                            {
                                newItem.doubleVal = reader.ReadDouble();
                            }
                            else if (j == strValIndex)
                            {
                                newItem.strVal = reader.ReadString();
                            }
                            else if (j == lstrValIndex)
                            {
                                newItem.__lKey_lstrVal = reader.ReadString();
                            }
                            else if (j == intArrayIndex)
                            {
                                int l_10 = reader.ReadInt32();
                                newItem.intArray = new int[l_10];
                                for(int i_10 = 0; i_10 < l_10; ++i_10)
                                {
                                    newItem.intArray[i_10] = reader.ReadInt32();
                                }
                            }
                            else if (j == floatArrayIndex)
                            {
                                int l_11 = reader.ReadInt32();
                                newItem.floatArray = new float[l_11];
                                for(int i_11 = 0; i_11 < l_11; ++i_11)
                                {
                                    newItem.floatArray[i_11] = reader.ReadSingle();
                                }
                            }
                            else if (j == stringArrayIndex)
                            {
                                int l_12 = reader.ReadInt32();
                                newItem.stringArray = new string[l_12];
                                for(int i_12 = 0; i_12 < l_12; ++i_12)
                                {
                                    newItem.stringArray[i_12] = reader.ReadString();
                                }
                            }
                            else if (j == clientOnlyValIndex)
                            {
                                newItem.clientOnlyVal = reader.ReadSingle();
                            }
                            else
                            {
                                DataService.ReadAndDrop(reader, headerItem.valType);
                                #if UNITY_EDITOR
                                if (!promptMismatchColumns)
                                {
                                    UnityEngine.Debug.LogWarningFormat("Data sheet '{0}' find mismatch columns for '{1}({2})'.", sheetName, headerItem.name, headerItem.valType);
                                    promptMismatchColumns = true;
                                }
                                #endif
                            }
                        }
                        m_Items.Add(newItem.key, newItem);
                    }
                }
            }
            
            RefreshLocalizationValues();
        }

        public void RefreshLocalizationValues()
        {
            foreach(var kv in m_Items)
            {
                Item item = kv.Value;
                item.lstrVal = DataService.GetLocalizedText(sheetName, item.__lKey_lstrVal);
            }
        }
    }
}
    