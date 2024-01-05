using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using Util;
using Debug = UnityEngine.Debug;

namespace Editor
{
    public static class Tools 
    {
        private static readonly Dictionary<string, string> CSharpTypeMap = new Dictionary<string, string>() 
        {
            {"int", "int"},
            {"uint", "uint"},
            {"ulong", "ulong"},
            {"long", "long"},
            {"string", "string"},
        };

        private static readonly Dictionary<string, string> ProtoTypeMap = new Dictionary<string, string>()
        {
            {"int", "int32"},
            {"uint", "uint32"},
            {"ulong", "uint64"},
            {"long", "int64"},
            {"string", "string"},
        };

        private static readonly List<string> ObjectTypeList = new List<string>();
        
        
        /// <summary>
        /// excel导出proto文件
        /// </summary>
        // [MenuItem("Tools/ExcelToProto")]
        [MenuItem("Tools/ExcelToProto")]
        public static void ExcelToProto()
        {
            EnumToProto();
            ObjectToProto();
            TableToProto();
        }
        
        /// <summary>
        /// 游戏对象表导出proto文件
        /// 路径：Game/Resource/Enum
        /// </summary>
        // [MenuItem("Tools/EnumToProto")]
        public static void EnumToProto()
        {
            try
            {
                var builder = new StringBuilder();
                builder.Append("syntax = \"proto3\";\r\n");
                var path = "../../Resource/Enum";
                var files = Directory.GetFiles(path);
                foreach (var t in files)
                {
                    AppendEnumProto(t, builder);
                }
                var protoPath = "../../Proto/Enum.proto";
                using var stream = new FileStream(protoPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                var data = Encoding.UTF8.GetBytes(builder.ToString());
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }  
        
        /// <summary>
        /// 策划配置表导出proto文件
        /// 路径：Game/Resource/Table
        /// </summary>
        // [MenuItem("Tools/TableToProto")]
        public static void TableToProto()
        {
            try
            {
                LoadObjectType();
                var builder = new StringBuilder();
                builder.Append("syntax = \"proto3\";\r\n");
                builder.Append("import \"Enum.proto\";\r\n");
                var path = "../../Resource/Table";
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    AppendTableProto(file, builder);
                }

                var protoPath = "../../Proto/ObjectResMsg.proto";
                using var stream = new FileStream(protoPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                var data = Encoding.UTF8.GetBytes(builder.ToString());
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            } 
        }
 
        /// <summary>
        /// 游戏对象表导出proto文件
        /// 路径：Game/Resource/Object
        /// </summary>
        // [MenuItem("Tools/ObjectToProto")]
        public static void ObjectToProto()
        {
            try
            {
                LoadObjectType();
                var builder = new StringBuilder();
                builder.Append("syntax = \"proto3\";\r\n");
                builder.Append("import \"Enum.proto\";\r\n");
                var path = "../../Resource/Object";
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    AppendObjectProto(file, builder);
                }

                var codePath = Path.Combine(Directory.GetCurrentDirectory(), "../../Proto/ObjectMsg.proto");
                using var stream = new FileStream(codePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                var data = Encoding.UTF8.GetBytes(builder.ToString());
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        } 
        
        
        /// <summary>
        /// 游戏对象表导出BaseXXX.cs文件
        /// 路径：Game/Resource/Object
        /// </summary>
        [MenuItem("Tools/ObjectToBaseCs")]
        public static void ObjectToBaseCs()
        {
            LoadObjectType();
            try
            {
                var builder = new StringBuilder();
                builder.Append("using System.Collections.Generic;\r\n");
                builder.Append("using Google.Protobuf;\r\n");
                builder.Append("using Logic.Object;\r\n");
                builder.Append("using Logic;\r\n");
                builder.Append("using Core;\r\n");
                var path = "../../Resource/Object";
                var files = Directory.GetFiles(path);
                foreach (var t in files)
                {
                    AppendBaseCs(t, builder);
                }

                var codePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Scripts/Logic/Auto/BaseObjects.cs");
                using var stream = new FileStream(codePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                var data = Encoding.UTF8.GetBytes(builder.ToString());
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }  
        
        
        /// <summary>
        /// proto 生成 cs文件
        /// </summary>
        [MenuItem("Tools/ProtoToCS")]
        public static void ProtoToCs()
        {
            try
            {
                var protoPath = Path.Combine(Directory.GetCurrentDirectory(), "../../Proto");
                foreach (var file in Directory.GetFiles(protoPath, "*", SearchOption.AllDirectories))
                {
                    var fileName = Path.GetFileName(file);
                    var directory = Path.GetDirectoryName(file);
                    var targetPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Scripts/Logic/Auto/Proto");
                    var arguments = $" -I={directory} --csharp_out={targetPath} {fileName}";
                    var protocPath = Path.Combine(Directory.GetCurrentDirectory(), "../../Tools/Protocbuf/bin/protoc.exe");
                    var process = new Process
                    {
                        StartInfo =
                        {
                            FileName = protocPath,
                            Arguments = arguments,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    process.WaitForExit();

                    string stdOutput = process.StandardOutput.ReadToEnd();
                    if (stdOutput.Length > 0)
                        Debug.LogError(stdOutput);
                    string errOutput = process.StandardError.ReadToEnd();
                    if (errOutput.Length > 0)
                        Debug.LogError(errOutput);

                    process.Close();
                    Debug.Log("proto 导出文件:" + file);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        
        /// <summary>
        /// 导出枚举
        /// </summary>
        /// <param name="fullFile"></param>
        /// <param name="builder"></param>
        private static void AppendEnumProto(string fullFile, StringBuilder builder)
        {
            var workBook = Utility.CreateWorkbook(fullFile);
            var sheet = workBook.GetSheetAt(0);
        
            var fileName = Path.GetFileNameWithoutExtension(fullFile);
            builder.Append("\r\n");
            builder.Append($"enum {fileName}\r\n");
            builder.Append("{\r\n");


            var rows = sheet.LastRowNum;
            for (var i = 0; i <= rows; i++)
            {
                var row = sheet.GetRow(i);
                var attrCell = row.GetCell(0);
                var valueCell = row.GetCell(1);
                builder.Append($"    {Utility.GetCellValue(attrCell)} = {Utility.GetCellValue(valueCell)};\r\n");
            }
            builder.Append("}\r\n");
            Debug.Log("Enum.proto add enum:" + fileName);
        } 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullFile"></param>
        /// <param name="builder"></param>
        private static void AppendTableProto(string fullFile, StringBuilder builder)
        {
            var workBook = Utility.CreateWorkbook(fullFile);
            var sheet = workBook.GetSheetAt(0);
            var attrRow = sheet.GetRow(0);
            var attrTypeRow = sheet.GetRow(1);
            var filterRow = sheet.GetRow(2); //该字段是否写入proto文件
            var fileName = Path.GetFileNameWithoutExtension(fullFile);
            var protoName = fileName + "ResMsg";
            
            builder.Append("\r\n");
            builder.Append($"message {protoName}\r\n");
            builder.Append("{\r\n");

            //默认Id类型
            string idType = "string";
            var index = 0;
            for (var i = 0; i <= attrRow.Cells.Count; i++)
            {

                if (Utility.GetCellValue(filterRow.GetCell(i)) != "1") { continue; }
                index++;
                var attrName = attrRow.GetCell(i);
                var attrType = attrTypeRow.GetCell(i);
                var type = GetProtoAttrType(Utility.GetCellValue(attrType));
                if (i == 0) { idType = type; }
            
                builder.Append($"    {type} {attrName} = {index};\r\n");
            }
            builder.Append("}\r\n");

            builder.Append
            (
                $"message {fileName}ResMapMsg\r\n" +
                "{\r\n" +
                $"   map<{idType}, {protoName}> Map = 1;\r\n" + 
                "}\r\n"
            );
            Debug.Log("ObjectResMsg.proto add Msg:" + fileName);
        }
        
        private static void AppendObjectProto(string fullFile, StringBuilder builder)
        {
            var workBook = Utility.CreateWorkbook(fullFile);
            var sheet = workBook.GetSheetAt(0);
            var firstRow = sheet.GetRow(0);
            var attrIndex = Utility.GetColumnIndex(firstRow, "attr");
            var attrTypeIndex = Utility.GetColumnIndex(firstRow, "attr_type");
            var protoIndex = Utility.GetColumnIndex(firstRow, "proto_export");

            var fileName = Path.GetFileNameWithoutExtension(fullFile);
            var protoName = fileName + "Msg";
            builder.Append("\r\n");
            builder.Append($"message {protoName}\r\n");
            builder.Append("{\r\n");

            var rows = sheet.LastRowNum;
            var index = 0;
            for (var i = 4; i <= rows; i++)
            {
                var row = sheet.GetRow(i);
                if (Utility.GetCellValue(row.GetCell(protoIndex)) != "1") { continue; }
                index++;
                var attrCell = row.GetCell(attrIndex);
                var attrName = Utility.GetCellValue(attrCell);
                var attrTypeCell = row.GetCell(attrTypeIndex);
                var type = GetProtoAttrType(Utility.GetCellValue(attrTypeCell));

                builder.Append($"    {type} {attrName} = {index};\r\n");
            }
            builder.Append("}\r\n");
            Debug.Log("ObjectMsg.proto add Msg:" + fileName);
        }

        /// <summary>
        /// 生成单个BaseXxx 对象
        /// </summary>
        /// <param name="fullFile"></param>
        /// <param name="builder"></param>
        private static void AppendBaseCs(string fullFile, StringBuilder builder)
        {
            var workBook = Utility.CreateWorkbook(fullFile);
            var sheet = workBook.GetSheetAt(0);
            var firstRow = sheet.GetRow(0);
            var attrIndex = Utility.GetColumnIndex(firstRow, "attr");
            var attrTypeIndex = Utility.GetColumnIndex(firstRow, "attr_type");
            var onlyGetterIndex = Utility.GetColumnIndex(firstRow, "only_getter");
            var protoIndex = Utility.GetColumnIndex(firstRow, "proto_export");

            var fileName = Path.GetFileNameWithoutExtension(fullFile);
            builder.Append("\r\n");
            builder.Append($"public abstract class Base{fileName} : BaseObject\r\n");
            builder.Append("{\r\n");
            builder.Append($"    public Base{fileName}(Game game) : base(game, ObjectType.{fileName}){{}}\r\n");


            var rows = sheet.LastRowNum;
            for (var i = 4; i <= rows; i++)
            {
                var row = sheet.GetRow(i);
                var attrCell = row.GetCell(attrIndex);
                var attrName = Utility.GetCellValue(attrCell);
                if (attrName == "Id") { continue; }
                var attrTypeCell = row.GetCell(attrTypeIndex);
                var onlyGetterCell = row.GetCell(onlyGetterIndex);

                var onlyGetterFlag = Utility.GetCellValue(onlyGetterCell) == "1";
                var type = GetCSharpAttrType(Utility.GetCellValue(attrTypeCell));

                if (onlyGetterFlag)
                {
                    builder.Append($"    public abstract {type} {attrName} {{ get; }}\r\n");
                }
                else if (type.StartsWith("List") || type.StartsWith("Dictionary") || ObjectTypeList.Contains(type))
                {
                    builder.Append($"    public {type} {attrName};\r\n");
                }
                else
                {
                    var privateAttr = Utility.FirstCharLower(attrName);
                    builder.Append
                    (
                        $"    protected {type} {privateAttr};\r\n" +
                        $"    public virtual {type} {attrName}\r\n" +
                        "    {\r\n" +
                        $"        get => {privateAttr};\r\n" +
                        "        set\r\n" +
                        "        {\r\n" +
                        $"            var old = {privateAttr};\r\n" +
                        $"            {privateAttr} = value;\r\n" +
                        $"            PostAttrEvent(\"{attrName}\", old, {privateAttr});\r\n" +
                        "        }\r\n" +
                        "    }\r\n"
                    );
                }
            }

            builder.Append("    public override void LoadMsg(IMessage iMessage)\r\n");
            builder.Append("    {\r\n");
            builder.Append($"        var message = iMessage as {fileName}Msg;\r\n");
            builder.Append($"        if (message is null) {{return;}}\r\n");
            for (int i = 4; i <= rows; i++)
            {
                var row = sheet.GetRow(i);
                var attrCell = row.GetCell(attrIndex);
                var attrName = Utility.GetCellValue(attrCell);
                var attrTypeCell = row.GetCell(attrTypeIndex);
                var protoCell = row.GetCell(protoIndex);
                var protoFlag = Utility.GetCellValue(protoCell) == "1";
                var type = GetCSharpAttrType(Utility.GetCellValue(attrTypeCell));
                if (!protoFlag)
                {
                    continue;
                }
                if (type.StartsWith("Dictionary"))
                {
                    var dicPair = GetDicTypePair(type);

                    if (ObjectTypeList.Contains(dicPair.Value))
                    {
                        builder.Append
                        (
                            $"        {attrName} = new {type}();\r\n" +
                            $"        foreach (var pair in message.{attrName})\r\n" +
                            "        {\r\n" +
                            $"            var item = new {dicPair.Value}(this);\r\n" +
                            "            item.LoadMsg(pair.Value);\r\n" +
                            $"            {attrName}.Add(pair.Key, item);\r\n" +
                            "        }\r\n"
                        );
                    }
                    else
                    {
                        builder.Append($"        {attrName} = new {type}(message.{attrName});\r\n");
                    }

                }
                else if (type.StartsWith("List"))
                {
                    var listType = StripBracket(type);
                    if (ObjectTypeList.Contains(listType))
                    {
                        builder.Append
                        (
                            $"        {attrName} = new {type}();\r\n" +
                            $"        for (var i = 0; i < message.{attrName}.Count; i++)\r\n" +
                            "        {\r\n" +
                            $"            var item = new {listType}(this);\r\n" +
                            $"            item.LoadMsg(message.{attrName}[i]);\r\n" +
                            $"            {attrName}.Add(item);\r\n" +
                            "        }\r\n"
                        );
                    }
                    else
                    {
                        builder.Append($"        {attrName} = new {type}(message.{attrName});\r\n");
                    }
                }
                else
                {
                    if (ObjectTypeList.Contains(type))
                    {
                        builder.Append
                        (
                            $"        {attrName} = new {type}(this);\r\n" +
                            $"        {attrName}.LoadMsg(message.{attrName});\r\n"
                        );  
                    }
                    else
                    {
                        builder.Append($"        {attrName} = message.{attrName};\r\n");
                    }
                }
            }
            builder.Append("        AfterLoadMsg();\r\n");
            builder.Append("    }\r\n");
            builder.Append("}\r\n");
            
            Debug.Log("BaseObjects.cs add Class:" + fileName);
        }
        
        /// <summary>
        /// 根据excel解析Proto类型
        /// </summary>
        /// <param name="typeStr"></param>
        /// <returns></returns>
        private static string GetProtoAttrType(string typeStr)
        {
            typeStr = typeStr.Trim();
            //非泛型类型直接返回
            var bracketIndex = typeStr.IndexOf('<');
            if (bracketIndex == -1)
            {
                if (ObjectTypeList.Contains(typeStr))
                {
                    return typeStr + "Msg";
                }

                if (ProtoTypeMap.ContainsKey(typeStr))
                {
                    return ProtoTypeMap[typeStr];
                }
                return typeStr;
            }

            //泛型类型需要递归遍历
            var index1 = typeStr.IndexOf("<", StringComparison.Ordinal);
            var index2 = typeStr.LastIndexOf(">", StringComparison.Ordinal);

            var s1 = typeStr.Substring(0, index1 + 1);
            var s2 = typeStr.Substring(0, index2);

            var bracketInternalStr = s2.Substring(s1.Length, s2.Length - s1.Length);
            typeStr = typeStr.Replace(bracketInternalStr, "{0}");

            if (typeStr.Contains("list"))
            {
                typeStr = typeStr.Replace("list", "repeated");
                typeStr = typeStr.Replace("<", " ");
                typeStr = typeStr.Replace(">", "");
            }

            if (typeStr.Contains("dic"))
            {
                typeStr = typeStr.Replace("dic", "map");
                typeStr = typeStr.Replace("{0}", "{0}, {1}");
            }

            if (typeStr.Contains("map"))
            {
                var commaIndex = bracketInternalStr.IndexOf(',');
                var keyTypeStr = string.Format(typeStr, GetProtoAttrType(bracketInternalStr.Substring(0, commaIndex)), "{0}");
                return string.Format(keyTypeStr, GetProtoAttrType(bracketInternalStr.Substring(commaIndex + 1, bracketInternalStr.Length - commaIndex - 1)));
            }

            return string.Format(typeStr, GetProtoAttrType(bracketInternalStr));
        }
        
        /// <summary>
        /// 加载Object类型
        /// </summary>
        private static void LoadObjectType()
        {
            if (ObjectTypeList.Count > 0){return;}
            var path = "../../Resource/Object";
            var files = Directory.GetFiles(path);
            ObjectTypeList.Clear();
            foreach (var t in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(t);
                ObjectTypeList.Add(fileName);
            }
        }

        private static KeyValuePair<string, string> GetDicTypePair(string dicStr)
        {
            var bracketInternalStr = StripBracket(dicStr);

            var commaIndex = bracketInternalStr.IndexOf(',');
            var keyType = GetCSharpAttrType(bracketInternalStr.Substring(0, commaIndex));
            var valueType = GetCSharpAttrType(bracketInternalStr.Substring(commaIndex + 1, bracketInternalStr.Length - commaIndex - 1));
            return new KeyValuePair<string, string>(keyType.Trim(), valueType.Trim());
        }

        /// <summary>
        /// 剥离类型中的尖括号
        /// </summary>
        /// <param name="typeStr"></param>
        /// <returns></returns>
        private static string StripBracket(string typeStr)
        {
            var index1 = typeStr.IndexOf("<", StringComparison.Ordinal);
            var index2 = typeStr.LastIndexOf(">", StringComparison.Ordinal);
            var s1 = typeStr.Substring(0, index1 + 1);
            var s2 = typeStr.Substring(0, index2);
            var bracketInternalStr = s2.Substring(s1.Length, s2.Length - s1.Length);
            return bracketInternalStr; 
        }

        /// <summary>
        /// 解析C#类型
        /// </summary>
        /// <param name="typeStr"></param>
        /// <returns></returns>
        private static string GetCSharpAttrType(string typeStr)
        {
            typeStr = typeStr.Trim();
            //非泛型类型直接返回
            var bracketIndex = typeStr.IndexOf('<');
            if (bracketIndex == -1)
            {
                if (CSharpTypeMap.ContainsKey(typeStr))
                {
                    return CSharpTypeMap[typeStr];
                }
                return typeStr;
            }

            //泛型类型需要递归遍历
            var bracketInternalStr = StripBracket(typeStr);
            typeStr = typeStr.Replace(bracketInternalStr, "{0}");

            if (typeStr.Contains("list"))
            {
                typeStr = typeStr.Replace("list", "List");
            }

            if (typeStr.Contains("dic"))
            {
                typeStr = typeStr.Replace("dic", "Dictionary");
                typeStr = typeStr.Replace("{0}", "{0}, {1}");
            }

            if (typeStr.Contains("Dictionary"))
            {
                var commaIndex = bracketInternalStr.IndexOf(',');
                var keyTypeStr = string.Format(typeStr, GetCSharpAttrType(bracketInternalStr.Substring(0, commaIndex)), "{0}");
                return string.Format(keyTypeStr, GetCSharpAttrType(bracketInternalStr.Substring(commaIndex + 1, bracketInternalStr.Length - commaIndex - 1)));
            }

            return string.Format(typeStr, GetCSharpAttrType(bracketInternalStr));
        }
    }
}
