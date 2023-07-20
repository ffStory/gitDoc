using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor;

public static class GenerateCode 
{
    private static Dictionary<string, string> CSharpTypeMap = new Dictionary<string, string>() 
    {
        {"int", "int"},
        {"string", "string"},
    };

    private static Dictionary<string, string> ProtoTypeMap = new Dictionary<string, string>()
    {
        {"int", "int32"},
        {"string", "string"},
    };

    private static List<string> EnumTypeList = new List<string>();
    private static List<string> ObjectTypeList = new List<string>();

    [MenuItem("Auto/ExportResource")]
    public static void ExportResource()
    {
        LoadType();
        ExportBaseObject();
        ExportProtoBase();
        ExportProtoEnum();
    }

    private static void LoadType()
    {
        var path = "../../Resource/Object";
        var files = Directory.GetFiles(path);
        ObjectTypeList.Clear();
        for (int i = 0; i < files.Length; i++)
        {
            var fileName = Path.GetFileNameWithoutExtension(files[i]);
            ObjectTypeList.Add(fileName);
        }

        path = "../../Resource/Enum";
        files = Directory.GetFiles(path);
        EnumTypeList.Clear();
        for (int i = 0; i < files.Length; i++)
        {
            var fileName = Path.GetFileNameWithoutExtension(files[i]);
            EnumTypeList.Add(fileName);
        }
    }

    /// <summary>
    /// 根据excel生成Base对象
    /// </summary>
    public static void ExportBaseObject()
    {
        try
        {
            var builder = new StringBuilder();
            builder.Append("using System.Collections.Generic;\n");
            builder.Append("using Google.Protobuf;\n");
            var path = "../../Resource/Object";
            var fiels = Directory.GetFiles(path);
            for (int i = 0; i < fiels.Length; i++)
            {
                var fiel = fiels[i];
                AppendObj(fiel, builder);
            }

            var codePath = "D:\\Documents\\wangfanfan\\Desktop\\Doc\\gitDoc\\Game\\Client\\ff\\Assets\\Scripts\\Logic\\Auto\\BaseObjects.cs";
            using var stream = new FileStream(codePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            var data = Encoding.UTF8.GetBytes(builder.ToString());
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Close();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
        }
    }

    /// <summary>
    /// 根据excel生成proto
    /// </summary>
    public static void ExportProtoBase()
    {
        try
        {
            var path = "../../Resource/Object";
            var files = Directory.GetFiles(path);
            var builder = new StringBuilder();
            builder.Append("syntax = \"proto3\";\r\n");
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                AppendProto(file, builder);
            }

            var codePath = "D:\\Documents\\wangfanfan\\Desktop\\Doc\\gitDoc\\Game\\Proto\\Auto\\ObjectMsg.proto";
            using var stream = new FileStream(codePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            var data = Encoding.UTF8.GetBytes(builder.ToString());
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Close();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
        }
    }

    private static void ExportProtoEnum()
    {
        try
        {
            var builder = new StringBuilder();
            builder.Append("syntax = \"proto3\";\r\n");
            var enumDir = "D:\\Documents\\wangfanfan\\Desktop\\Doc\\gitDoc\\Game\\Resource\\Enum";
            var files = Directory.GetFiles(enumDir);
            for (int i = 0; i < files.Length; i++)
            {
                AppendEnum(files[i], builder);
            }
            var codePath = $"D:\\Documents\\wangfanfan\\Desktop\\Doc\\gitDoc\\Game\\Proto\\Auto\\Enum.proto";
            using var stream = new FileStream(codePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            var data = Encoding.UTF8.GetBytes(builder.ToString());
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Close();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
        }
    }

    [MenuItem("Auto/ProtoToCS")]
    public static void ProtoToCS()
    {
        try
        {
            var protoPath = "D:\\Documents\\wangfanfan\\Desktop\\Doc\\gitDoc\\Game\\Proto";
            foreach (string file in Directory.GetFiles(protoPath, "*", SearchOption.AllDirectories))
            {
                var fileName = Path.GetFileName(file);
                var directory = Path.GetDirectoryName(file);
                var arguments = $" -I={directory} --csharp_out=D:/Documents/wangfanfan/Desktop/Doc/gitDoc/Game/Client/ff/Assets/Scripts/Logic/Auto/Proto {fileName}";
                var process = new Process
                {
                    StartInfo =
                {
                    FileName = "protoc.exe",
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
                    UnityEngine.Debug.LogError(stdOutput);
                string errOutput = process.StandardError.ReadToEnd();
                if (errOutput.Length > 0)
                    UnityEngine.Debug.LogError(errOutput);

                process.Close();
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
        }
    }

    private static void AppendProto(string fullFile, StringBuilder builder)
    {
        var workBook = CreateWorkbook(fullFile);
        var sheet = workBook.GetSheetAt(0);
        var firstRow = sheet.GetRow(0);
        var attrIndex = GetColumnIndex(firstRow, "attr");
        var attrTypeIndex = GetColumnIndex(firstRow, "attr_type");
        var protoIndex = GetColumnIndex(firstRow, "proto_export");

        var fileName = Path.GetFileNameWithoutExtension(fullFile);
        var protoName = fileName + "Msg";
        builder.Append($"message {protoName}\n");
        builder.Append("{\n");

        var rows = sheet.LastRowNum;
        var index = 0;
        for (int i = 4; i <= rows; i++)
        {
            var row = sheet.GetRow(i);
            if (GetCellValue(row.GetCell(protoIndex)) != "1") { continue; }
            index++;
            var attrCell = row.GetCell(attrIndex);
            var attrName = GetCellValue(attrCell);
            var attrTypeCell = row.GetCell(attrTypeIndex);
            var type = GetProtoAttrType(GetCellValue(attrTypeCell));

            builder.Append($"    {type} {attrName} = {index};\n");
        }
        builder.Append("}\n");
    }

    /// <summary>
    /// 导出枚举
    /// </summary>
    /// <param name="fullFile"></param>
    /// <param name="builder"></param>
    private static void AppendEnum(string fullFile, StringBuilder builder)
    {
        var workBook = CreateWorkbook(fullFile);
        var sheet = workBook.GetSheetAt(0);
        
        var fileName = Path.GetFileNameWithoutExtension(fullFile);
        builder.Append($"enum {fileName}\n");
        builder.Append("{\n");


        var rows = sheet.LastRowNum;
        for (int i = 0; i <= rows; i++)
        {
            var row = sheet.GetRow(i);
            var attrCell = row.GetCell(0);
            var valueCell = row.GetCell(1);
            builder.Append($"    {GetCellValue(attrCell)} = {GetCellValue(valueCell)};\n");
        }
        builder.Append("}\n");
    }

    private static void AppendObj(string fullFile, StringBuilder builder)
    {
        var workBook = CreateWorkbook(fullFile);
        var sheet = workBook.GetSheetAt(0);
        var firstRow = sheet.GetRow(0);
        var attrIndex = GetColumnIndex(firstRow, "attr");
        var attrTypeIndex = GetColumnIndex(firstRow, "attr_type");
        var onlyGetterIndex = GetColumnIndex(firstRow, "only_getter");
        var protoIndex = GetColumnIndex(firstRow, "proto_export");

        var fileName = Path.GetFileNameWithoutExtension(fullFile);
        builder.Append($"public abstract class Base{fileName} : BaseObject\n");
        builder.Append("{\n");
        builder.Append($"    public Base{fileName}(Game game) : base(game, ObjectType.{fileName}){{}}\n");


        var rows = sheet.LastRowNum;
        for (int i = 4; i <= rows; i++)
        {
            var row = sheet.GetRow(i);
            var attrCell = row.GetCell(attrIndex);
            var attrName = GetCellValue(attrCell);
            if (attrName == "id") { continue; }
            var attrTypeCell = row.GetCell(attrTypeIndex);
            var onlyGetterpeCell = row.GetCell(onlyGetterIndex);

            var onlyGetterFlag = GetCellValue(onlyGetterpeCell) == "1";
            var type = GetCSharpAttrType(GetCellValue(attrTypeCell));

            if (onlyGetterFlag)
            {
                builder.Append($"    public abstract {type} {attrName} {{ get; }}\n");
            }
            else if (type.StartsWith("List") || type.StartsWith("Dictionary"))
            {
                builder.Append($"    public {type} {attrName};\n");
            }
            else
            {
                builder.Append
                    (
                    $"    protected {type} _{attrName};\r\n" +
                    $"    public {type} {attrName}\r\n" +
                    "    {\r\n" +
                    $"        get{{return _{attrName};}}\r\n" +
                    "        set\r\n        {\r\n" +
                    $"            var old = _{attrName};\r\n" +
                    $"            _{attrName} = value;\r\n" +
                    $"            PostAttrEvent(\"{attrName}\", old, {attrName});\r\n" +
                    "        }\r\n" +
                    "    }\n"
                    );
            }
        }

        builder.Append("    public override void LoadMsg(IMessage iMessage)\n");
        builder.Append("    {\n");
        builder.Append($"        var message = iMessage as {fileName}Msg;\n");
        for (int i = 4; i <= rows; i++)
        {
            var row = sheet.GetRow(i);
            var attrCell = row.GetCell(attrIndex);
            var attrName = GetCellValue(attrCell);
            var attrTypeCell = row.GetCell(attrTypeIndex);
            var protoCell = row.GetCell(protoIndex);
            var protoFlag = GetCellValue(protoCell) == "1";
            var type = GetCSharpAttrType(GetCellValue(attrTypeCell));
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
                        $"        foreach (var pair in message.{FirstCharUp(attrName)})\r\n" +
                        "        {\r\n" +
                        $"            var item = new {dicPair.Value}(this);\r\n" +
                        "            item.LoadMsg(pair.Value);\r\n" +
                        $"            {attrName}.Add(pair.Key, item);\r\n" +
                        "        }\r\n"
                        );
                }
                else
                {
                    builder.Append($"        {attrName} = new {type}(message.{FirstCharUp(attrName)});\n");
                }

            }
            else if (type.StartsWith("List"))
            {
                var listType = GetListType(type);
                if (ObjectTypeList.Contains(listType))
                {
                    builder.Append
                        (
                        $"        {attrName} = new {type}();\r\n" +
                        $"        for (int i = 0; i < message.{FirstCharUp(attrName)}.Count; i++)\r\n" +
                        "        {\r\n" +
                        $"            var item = new {listType}(this);\r\n" +
                        $"            item.LoadMsg(message.{FirstCharUp(attrName)}[i]);\r\n" +
                        $"            {attrName}.Add(item);\r\n" +
                        "        }\r\n"
                        );
                }
                else
                {
                    builder.Append($"        {attrName} = new {type}(message.{FirstCharUp(attrName)});\n");
                }
            }
            else
            {
                builder.Append($"        {attrName} = message.{FirstCharUp(attrName)};\n");
            }
        }
        builder.Append("        AfterLoadMsg();\n");
        builder.Append("    }\n");
        builder.Append("}\n");
    }


    private static KeyValuePair<string, string> GetDicTypePair(string dicStr)
    {
        var index1 = dicStr.IndexOf("<");
        var index2 = dicStr.LastIndexOf(">");
        var s1 = dicStr.Substring(0, index1 + 1);
        var s2 = dicStr.Substring(0, index2);
        var bracketInteralStr = s2.Substring(s1.Length, s2.Length - s1.Length);

        var commaIndex = bracketInteralStr.IndexOf(',');
        var keyType = GetCSharpAttrType(bracketInteralStr.Substring(0, commaIndex));
        var valueType = GetCSharpAttrType(bracketInteralStr.Substring(commaIndex + 1, bracketInteralStr.Length - commaIndex - 1));
        return new KeyValuePair<string, string>(keyType.Trim(), valueType.Trim());
    }

    private static string GetListType(string listStr)
    {
        var index1 = listStr.IndexOf("<");
        var index2 = listStr.LastIndexOf(">");
        var s1 = listStr.Substring(0, index1 + 1);
        var s2 = listStr.Substring(0, index2);
        var bracketInteralStr = s2.Substring(s1.Length, s2.Length - s1.Length);
        return bracketInteralStr;
    }

    public static string FirstCharUp(this string s)
    {
        if (String.IsNullOrEmpty(s))
        {
            throw new ArgumentException("String is mull or empty");
        }

        return s[0].ToString().ToUpper() + s.Substring(1);
    }

    public static IWorkbook CreateWorkbook(string path)
    {
        try
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            return WorkbookFactory.Create(stream);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"����{path}ʧ��\n{e.Message}\n{e.StackTrace}\n InnerException=>{e.InnerException}");
            throw;
        }
    }

    public static void WriteExcel(IWorkbook workbook, string path)
    {
        using var ms = new MemoryStream();
        using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        workbook.Write(ms);
        var b = ms.ToArray();
        fs.Write(b, 0, b.Length);
        fs.Flush();
        fs.Close();
    }

    public static int GetColumnIndex(IRow row, string name)
    {
        var index = -1;
        if (row == null || string.IsNullOrEmpty(name)) return index;
        foreach (var cell in row.Cells)
        {
            if (GetCellValue(cell).ToLower().Trim() != name.ToLower().Trim()) continue;
            index = cell.ColumnIndex;
            break;
        }

        return index;
    }


    /// <summary>
    /// 解析类型
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
        //var bracketInteralStr = GetTypeString(ref typeStr);

        var index1 = typeStr.IndexOf("<");
        var index2 = typeStr.LastIndexOf(">");

        var s1 = typeStr.Substring(0, index1 + 1);
        var s2 = typeStr.Substring(0, index2);

        var bracketInteralStr = s2.Substring(s1.Length, s2.Length - s1.Length);
        typeStr = typeStr.Replace(bracketInteralStr, "{0}");

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
            var commaIndex = bracketInteralStr.IndexOf(',');
            var keyTypeStr = string.Format(typeStr, GetCSharpAttrType(bracketInteralStr.Substring(0, commaIndex)), "{0}");
            return string.Format(keyTypeStr, GetCSharpAttrType(bracketInteralStr.Substring(commaIndex + 1, bracketInteralStr.Length - commaIndex - 1)));
        }

        return string.Format(typeStr, GetCSharpAttrType(bracketInteralStr));
    }

    public static string GetProtoAttrType(string typeStr)
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
        //var bracketInteralStr = GetTypeString(ref typeStr);

        var index1 = typeStr.IndexOf("<");
        var index2 = typeStr.LastIndexOf(">");

        var s1 = typeStr.Substring(0, index1 + 1);
        var s2 = typeStr.Substring(0, index2);

        var bracketInteralStr = s2.Substring(s1.Length, s2.Length - s1.Length);
        typeStr = typeStr.Replace(bracketInteralStr, "{0}");

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
            var commaIndex = bracketInteralStr.IndexOf(',');
            var keyTypeStr = string.Format(typeStr, GetProtoAttrType(bracketInteralStr.Substring(0, commaIndex)), "{0}");
            return string.Format(keyTypeStr, GetProtoAttrType(bracketInteralStr.Substring(commaIndex + 1, bracketInteralStr.Length - commaIndex - 1)));
        }

        return string.Format(typeStr, GetProtoAttrType(bracketInteralStr));
    }

    public static string GetCellValue(ICell cell)
    {
        if (cell == null) return string.Empty;
        var str = cell.CellType switch
        {
            CellType.Unknown => string.Empty,
            CellType.Numeric => cell.NumericCellValue.ToString(CultureInfo.InvariantCulture),
            CellType.String => cell.StringCellValue,
            CellType.Formula => cell.StringCellValue,
            CellType.Blank => string.Empty,
            CellType.Boolean => cell.BooleanCellValue.ToString(),
            CellType.Error => cell.ErrorCellValue.ToString(),
            _ => throw new ArgumentOutOfRangeException()
        };

        //����һ��ת��
        return str.Replace("\\n", "\n");
    }
}
