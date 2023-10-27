using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NPOI.SS.UserModel;
using UnityEditor;
using Google.Protobuf;
using UnityEngine;
using Util;

namespace Editor
{
    public static class ExcelTool
    {
        private static Assembly _assembly;
        /// <summary>
        /// excel表导出二进制数据 并本地存储
        /// </summary>
        [MenuItem("Tools/ExcelToByte")]
        public static void ExcelToByte()
        {
            _assembly =  Assembly.Load("Assembly-CSharp");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "../../Resource/Table");
            var files = Directory.GetFiles(path);
            
            Debug.Log("ExcelTool.ExcelToByte 导表开始");
            foreach (var t in files)
            {
                try
                {
                    ExcelToByte(t);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            Debug.Log("ExcelTool.ExcelToByte 导表结束");
        }

        private static void ExcelToByte(string fullFile)
        {
            var workBook = Utility.CreateWorkbook(fullFile);
            var sheet = workBook.GetSheetAt(0);
            var fileName = Path.GetFileNameWithoutExtension(fullFile);
            var dicType = _assembly.GetType(fileName + "ResMapMsg");
            if (dicType != null)
            {
                var dicObj =  Activator.CreateInstance(dicType);

                var propertyInfo =  dicType.GetProperty("Map");
                if (propertyInfo == null){return;}
                var target = propertyInfo.GetValue(dicObj);
                var arguments = propertyInfo.PropertyType.GetGenericArguments();
                var kType = GetTypeByStr(arguments[0].Name);
                var vType = GetTypeByStr(arguments[1].Name);
                var methodInfo = target.GetType().GetMethod("Add", new[]{kType, vType});
                if (methodInfo != null)
                {
                    var rows = sheet.LastRowNum;
                    var attrRow = sheet.GetRow(0);
                    var attrToIndex = new Dictionary<string, int>();
                    for (var i = 0; i < attrRow.Cells.Count; i++)
                    {
                        attrToIndex.Add(Utility.GetCellValue(attrRow.GetCell(i)), i);
                    }
                    
                    for (var i = 4; i <= rows; i++)
                    {
                        var type = _assembly.GetType(fileName + "ResMsg");
                        var obj = Activator.CreateInstance(type); 
                        var id = ProcessExcelRow(sheet.GetRow(i), type, obj, attrToIndex);
                        var key = Convert.ChangeType(id, kType);
                        methodInfo.Invoke(target, new[]{key, obj});
                    }
                }

                //生成每张表的序列化数据
                var data = ToByteArray(dicObj as IMessage);
                var codePath = Path.Combine(Directory.GetCurrentDirectory(), $"../../Resource/Data/{fileName}.byte");
                using var stream = new FileStream(codePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close();
                Debug.Log($"ExcelTool.ExcelToByte 导出 {fileName}.byte");
            }
            else
            {
                Debug.LogError("....type is null");
            }
        }

        /// <summary>
        /// 拷贝过来的
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static byte[] ToByteArray(IMessage message)
        {
            ProtoPreconditions.CheckNotNull(message, nameof (message));
            byte[] flatArray = new byte[message.CalculateSize()];
            CodedOutputStream output = new CodedOutputStream(flatArray);
            message.WriteTo(output);
            output.CheckNoSpaceLeft();
            return flatArray;
        }

        private static object ProcessExcelRow(IRow row, Type type, object obj, Dictionary<string, int> attrToIndex)
        {
            object id = null;
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                var attrIndex = attrToIndex[property.Name];
                var cell = row.GetCell(attrIndex);
                var value = Utility.GetCellValue(cell);
                id ??= value;
                if (string.IsNullOrEmpty(value)){continue;}
                if (property.PropertyType.IsEnum)
                {
                    property.SetValue(obj, Enum.Parse(property.PropertyType, value));
                }
                else if (typeof(System.Collections.IList).IsAssignableFrom(property.PropertyType))
                {
                    var target = property.GetValue(obj);
                    
                    var typeName = property.PropertyType.GetGenericArguments()[0].Name;
                    var itemType = GetTypeByStr(typeName);
                    var methodInfo = target.GetType().GetMethod("Add", new []{itemType});

                    if (methodInfo != null)
                    {
                        var array = value.Split('|');
                        foreach (var t in array)
                        {
                            var args = new object[1];
                            var temp =  t;
                            if (typeName == "String")
                            {
                                args[0] = temp;
                            }
                            else if (typeName == "Int32")
                            {
                                int.TryParse(temp, out int v);
                                args[0] = v;
                            }
                            else if (typeName == "UInt32")
                            {
                                uint.TryParse(temp, out uint v);
                                args[0] = v;
                            } 
                            // else if (typeName == "CostMsg")
                            // {
                            //     args[0] = ParseCost(temp);
                            // } 
                            
                            methodInfo.Invoke(target, args);
                        }
                    }
                }
                else if (typeof(System.Collections.IDictionary).IsAssignableFrom(property.PropertyType))
                {
                    var arguments = property.PropertyType.GetGenericArguments();
                    var target = property.GetValue(obj);
                    var kType = GetTypeByStr(arguments[0].Name);
                    var vType = GetTypeByStr(arguments[1].Name);
                    var methodInfo = target.GetType().GetMethod("Add", new []{kType, vType});
                    if (methodInfo != null)
                    {
                        var array = value.Split('|');
                        foreach (var t in array)
                        {
                            var args = new object[2];
                            var pair = t.Split('=');
                            for (var j = 0; j < args.Length; j++)
                            {
                                var temp =  pair[j];
                                var typeName = arguments[j].Name;
                                if (typeName == "String")
                                {
                                    args[j] = temp;
                                }
                                else if (typeName == "Int32")
                                {
                                    int.TryParse(temp, out int v);
                                    args[j] = v;
                                }
                                else if (typeName == "UInt32")
                                {
                                    uint.TryParse(temp, out uint v);
                                    args[j] = v;
                                }  
                                // else if (typeName == "CostMsg")
                                // {
                                //     args[j] = ParseCost(temp);
                                // }  
                            }
                            
                            methodInfo.Invoke(target, args);
                        }
                    }
                }
                // else if (property.PropertyType.ToString() == "CostMsg")
                // {
                //     // property.SetValue(obj, Convert.ChangeType(ParseCost(value), property.PropertyType));
                // }
                else
                {
                    property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
                }
            }

            return id;
        }

        // private static CostMsg ParseCost(string value)
        // {
        //     var json = (JArray)JsonConvert.DeserializeObject(value);
        //     var cost = new CostMsg();
        //     for (var i = 0; i < json.Count; i++)
        //     {
        //         var strType = json[i][0]?.ToString();
        //         var costItem = new CostItemMsg();
        //         if (strType == null){continue;}
        //         var costItemType = (CostItemType)Enum.Parse(typeof(CostItemType), strType);
        //         costItem.CostItemType = costItemType;
        //         var strObjectType = json[i][1]?.ToString();
        //         var objectType = strObjectType == null? ObjectType.Player : (ObjectType)Enum.Parse(typeof(ObjectType), strObjectType);
        //         costItem.ObjectType = objectType;
        //         
        //         switch (costItemType)
        //         {
        //             case CostItemType.Consume:
        //                 costItem.Consume = new CostItemConsumeMsg()
        //                 {
        //                     AttrName = json[i][3]?.ToString()
        //                 };
        //                 break;
        //             case CostItemType.Between:
        //                 costItem.Between = new CostItemBetweenMsg()
        //                 {
        //                     AttrValue = (uint)json[i][3]
        //                 };
        //                 break; 
        //         }
        //         cost.Items.Add(costItem);
        //     }
        //
        //     return cost;
        // }

        private static Type GetTypeByStr(string typeName)
        {
            switch (typeName)
            {
                case "String":
                    return typeof(String);
                case "Int32":
                    return typeof(Int32);
                case "UInt32":
                    return typeof(UInt32);
                default:
                    return _assembly.GetType(typeName);

            }
        }
    }
}
