// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ObjectResMsg.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from ObjectResMsg.proto</summary>
public static partial class ObjectResMsgReflection {

  #region Descriptor
  /// <summary>File descriptor for ObjectResMsg.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static ObjectResMsgReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChJPYmplY3RSZXNNc2cucHJvdG8aCkVudW0ucHJvdG8i0wEKCkhlcm9SZXNN",
          "c2cSCgoCSWQYASABKAUSDAoETmFtZRgCIAEoCRIbCghJdGVtVHlwZRgDIAEo",
          "DjIJLkl0ZW1UeXBlEg8KB1Jld2FyZHMYBCADKAkSKQoHRGljVGVzdBgFIAMo",
          "CzIYLkhlcm9SZXNNc2cuRGljVGVzdEVudHJ5EhAKCFJld2FyZHMyGAYgAygF",
          "EhAKCFJld2FyZHMzGAcgAygNGi4KDERpY1Rlc3RFbnRyeRILCgNrZXkYASAB",
          "KAUSDQoFdmFsdWUYAiABKAk6AjgBIm4KDUhlcm9SZXNNc2dEaWMSJAoDRGlj",
          "GAEgAygLMhcuSGVyb1Jlc01zZ0RpYy5EaWNFbnRyeRo3CghEaWNFbnRyeRIL",
          "CgNrZXkYASABKAUSGgoFdmFsdWUYAiABKAsyCy5IZXJvUmVzTXNnOgI4ASKv",
          "AQoKSXRlbVJlc01zZxIKCgJJZBgBIAEoBRIMCgROYW1lGAIgASgJEhsKCEl0",
          "ZW1UeXBlGAMgASgOMgkuSXRlbVR5cGUSDwoHUmV3YXJkcxgEIAMoCRIpCgdE",
          "aWNUZXN0GAUgAygLMhguSXRlbVJlc01zZy5EaWNUZXN0RW50cnkaLgoMRGlj",
          "VGVzdEVudHJ5EgsKA2tleRgBIAEoBRINCgV2YWx1ZRgCIAEoCToCOAEibgoN",
          "SXRlbVJlc01zZ0RpYxIkCgNEaWMYASADKAsyFy5JdGVtUmVzTXNnRGljLkRp",
          "Y0VudHJ5GjcKCERpY0VudHJ5EgsKA2tleRgBIAEoBRIaCgV2YWx1ZRgCIAEo",
          "CzILLkl0ZW1SZXNNc2c6AjgBYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::EnumReflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::HeroResMsg), global::HeroResMsg.Parser, new[]{ "Id", "Name", "ItemType", "Rewards", "DicTest", "Rewards2", "Rewards3" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, }),
          new pbr::GeneratedClrTypeInfo(typeof(global::HeroResMsgDic), global::HeroResMsgDic.Parser, new[]{ "Dic" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, }),
          new pbr::GeneratedClrTypeInfo(typeof(global::ItemResMsg), global::ItemResMsg.Parser, new[]{ "Id", "Name", "ItemType", "Rewards", "DicTest" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, }),
          new pbr::GeneratedClrTypeInfo(typeof(global::ItemResMsgDic), global::ItemResMsgDic.Parser, new[]{ "Dic" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
        }));
  }
  #endregion

}
#region Messages
public sealed partial class HeroResMsg : pb::IMessage<HeroResMsg>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<HeroResMsg> _parser = new pb::MessageParser<HeroResMsg>(() => new HeroResMsg());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<HeroResMsg> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ObjectResMsgReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public HeroResMsg() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public HeroResMsg(HeroResMsg other) : this() {
    id_ = other.id_;
    name_ = other.name_;
    itemType_ = other.itemType_;
    rewards_ = other.rewards_.Clone();
    dicTest_ = other.dicTest_.Clone();
    rewards2_ = other.rewards2_.Clone();
    rewards3_ = other.rewards3_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public HeroResMsg Clone() {
    return new HeroResMsg(this);
  }

  /// <summary>Field number for the "Id" field.</summary>
  public const int IdFieldNumber = 1;
  private int id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  /// <summary>Field number for the "Name" field.</summary>
  public const int NameFieldNumber = 2;
  private string name_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Name {
    get { return name_; }
    set {
      name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "ItemType" field.</summary>
  public const int ItemTypeFieldNumber = 3;
  private global::ItemType itemType_ = global::ItemType.Resource;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::ItemType ItemType {
    get { return itemType_; }
    set {
      itemType_ = value;
    }
  }

  /// <summary>Field number for the "Rewards" field.</summary>
  public const int RewardsFieldNumber = 4;
  private static readonly pb::FieldCodec<string> _repeated_rewards_codec
      = pb::FieldCodec.ForString(34);
  private readonly pbc::RepeatedField<string> rewards_ = new pbc::RepeatedField<string>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<string> Rewards {
    get { return rewards_; }
  }

  /// <summary>Field number for the "DicTest" field.</summary>
  public const int DicTestFieldNumber = 5;
  private static readonly pbc::MapField<int, string>.Codec _map_dicTest_codec
      = new pbc::MapField<int, string>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForString(18, ""), 42);
  private readonly pbc::MapField<int, string> dicTest_ = new pbc::MapField<int, string>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::MapField<int, string> DicTest {
    get { return dicTest_; }
  }

  /// <summary>Field number for the "Rewards2" field.</summary>
  public const int Rewards2FieldNumber = 6;
  private static readonly pb::FieldCodec<int> _repeated_rewards2_codec
      = pb::FieldCodec.ForInt32(50);
  private readonly pbc::RepeatedField<int> rewards2_ = new pbc::RepeatedField<int>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<int> Rewards2 {
    get { return rewards2_; }
  }

  /// <summary>Field number for the "Rewards3" field.</summary>
  public const int Rewards3FieldNumber = 7;
  private static readonly pb::FieldCodec<uint> _repeated_rewards3_codec
      = pb::FieldCodec.ForUInt32(58);
  private readonly pbc::RepeatedField<uint> rewards3_ = new pbc::RepeatedField<uint>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<uint> Rewards3 {
    get { return rewards3_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as HeroResMsg);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(HeroResMsg other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (Name != other.Name) return false;
    if (ItemType != other.ItemType) return false;
    if(!rewards_.Equals(other.rewards_)) return false;
    if (!DicTest.Equals(other.DicTest)) return false;
    if(!rewards2_.Equals(other.rewards2_)) return false;
    if(!rewards3_.Equals(other.rewards3_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0) hash ^= Id.GetHashCode();
    if (Name.Length != 0) hash ^= Name.GetHashCode();
    if (ItemType != global::ItemType.Resource) hash ^= ItemType.GetHashCode();
    hash ^= rewards_.GetHashCode();
    hash ^= DicTest.GetHashCode();
    hash ^= rewards2_.GetHashCode();
    hash ^= rewards3_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Name);
    }
    if (ItemType != global::ItemType.Resource) {
      output.WriteRawTag(24);
      output.WriteEnum((int) ItemType);
    }
    rewards_.WriteTo(output, _repeated_rewards_codec);
    dicTest_.WriteTo(output, _map_dicTest_codec);
    rewards2_.WriteTo(output, _repeated_rewards2_codec);
    rewards3_.WriteTo(output, _repeated_rewards3_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Name);
    }
    if (ItemType != global::ItemType.Resource) {
      output.WriteRawTag(24);
      output.WriteEnum((int) ItemType);
    }
    rewards_.WriteTo(ref output, _repeated_rewards_codec);
    dicTest_.WriteTo(ref output, _map_dicTest_codec);
    rewards2_.WriteTo(ref output, _repeated_rewards2_codec);
    rewards3_.WriteTo(ref output, _repeated_rewards3_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (Id != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
    }
    if (Name.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
    }
    if (ItemType != global::ItemType.Resource) {
      size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ItemType);
    }
    size += rewards_.CalculateSize(_repeated_rewards_codec);
    size += dicTest_.CalculateSize(_map_dicTest_codec);
    size += rewards2_.CalculateSize(_repeated_rewards2_codec);
    size += rewards3_.CalculateSize(_repeated_rewards3_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(HeroResMsg other) {
    if (other == null) {
      return;
    }
    if (other.Id != 0) {
      Id = other.Id;
    }
    if (other.Name.Length != 0) {
      Name = other.Name;
    }
    if (other.ItemType != global::ItemType.Resource) {
      ItemType = other.ItemType;
    }
    rewards_.Add(other.rewards_);
    dicTest_.MergeFrom(other.dicTest_);
    rewards2_.Add(other.rewards2_);
    rewards3_.Add(other.rewards3_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Name = input.ReadString();
          break;
        }
        case 24: {
          ItemType = (global::ItemType) input.ReadEnum();
          break;
        }
        case 34: {
          rewards_.AddEntriesFrom(input, _repeated_rewards_codec);
          break;
        }
        case 42: {
          dicTest_.AddEntriesFrom(input, _map_dicTest_codec);
          break;
        }
        case 50:
        case 48: {
          rewards2_.AddEntriesFrom(input, _repeated_rewards2_codec);
          break;
        }
        case 58:
        case 56: {
          rewards3_.AddEntriesFrom(input, _repeated_rewards3_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Name = input.ReadString();
          break;
        }
        case 24: {
          ItemType = (global::ItemType) input.ReadEnum();
          break;
        }
        case 34: {
          rewards_.AddEntriesFrom(ref input, _repeated_rewards_codec);
          break;
        }
        case 42: {
          dicTest_.AddEntriesFrom(ref input, _map_dicTest_codec);
          break;
        }
        case 50:
        case 48: {
          rewards2_.AddEntriesFrom(ref input, _repeated_rewards2_codec);
          break;
        }
        case 58:
        case 56: {
          rewards3_.AddEntriesFrom(ref input, _repeated_rewards3_codec);
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class HeroResMsgDic : pb::IMessage<HeroResMsgDic>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<HeroResMsgDic> _parser = new pb::MessageParser<HeroResMsgDic>(() => new HeroResMsgDic());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<HeroResMsgDic> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ObjectResMsgReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public HeroResMsgDic() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public HeroResMsgDic(HeroResMsgDic other) : this() {
    dic_ = other.dic_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public HeroResMsgDic Clone() {
    return new HeroResMsgDic(this);
  }

  /// <summary>Field number for the "Dic" field.</summary>
  public const int DicFieldNumber = 1;
  private static readonly pbc::MapField<int, global::HeroResMsg>.Codec _map_dic_codec
      = new pbc::MapField<int, global::HeroResMsg>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForMessage(18, global::HeroResMsg.Parser), 10);
  private readonly pbc::MapField<int, global::HeroResMsg> dic_ = new pbc::MapField<int, global::HeroResMsg>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::MapField<int, global::HeroResMsg> Dic {
    get { return dic_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as HeroResMsgDic);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(HeroResMsgDic other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (!Dic.Equals(other.Dic)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    hash ^= Dic.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    dic_.WriteTo(output, _map_dic_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    dic_.WriteTo(ref output, _map_dic_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    size += dic_.CalculateSize(_map_dic_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(HeroResMsgDic other) {
    if (other == null) {
      return;
    }
    dic_.MergeFrom(other.dic_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          dic_.AddEntriesFrom(input, _map_dic_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          dic_.AddEntriesFrom(ref input, _map_dic_codec);
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class ItemResMsg : pb::IMessage<ItemResMsg>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<ItemResMsg> _parser = new pb::MessageParser<ItemResMsg>(() => new ItemResMsg());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<ItemResMsg> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ObjectResMsgReflection.Descriptor.MessageTypes[2]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public ItemResMsg() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public ItemResMsg(ItemResMsg other) : this() {
    id_ = other.id_;
    name_ = other.name_;
    itemType_ = other.itemType_;
    rewards_ = other.rewards_.Clone();
    dicTest_ = other.dicTest_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public ItemResMsg Clone() {
    return new ItemResMsg(this);
  }

  /// <summary>Field number for the "Id" field.</summary>
  public const int IdFieldNumber = 1;
  private int id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  /// <summary>Field number for the "Name" field.</summary>
  public const int NameFieldNumber = 2;
  private string name_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Name {
    get { return name_; }
    set {
      name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "ItemType" field.</summary>
  public const int ItemTypeFieldNumber = 3;
  private global::ItemType itemType_ = global::ItemType.Resource;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::ItemType ItemType {
    get { return itemType_; }
    set {
      itemType_ = value;
    }
  }

  /// <summary>Field number for the "Rewards" field.</summary>
  public const int RewardsFieldNumber = 4;
  private static readonly pb::FieldCodec<string> _repeated_rewards_codec
      = pb::FieldCodec.ForString(34);
  private readonly pbc::RepeatedField<string> rewards_ = new pbc::RepeatedField<string>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<string> Rewards {
    get { return rewards_; }
  }

  /// <summary>Field number for the "DicTest" field.</summary>
  public const int DicTestFieldNumber = 5;
  private static readonly pbc::MapField<int, string>.Codec _map_dicTest_codec
      = new pbc::MapField<int, string>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForString(18, ""), 42);
  private readonly pbc::MapField<int, string> dicTest_ = new pbc::MapField<int, string>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::MapField<int, string> DicTest {
    get { return dicTest_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as ItemResMsg);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(ItemResMsg other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (Name != other.Name) return false;
    if (ItemType != other.ItemType) return false;
    if(!rewards_.Equals(other.rewards_)) return false;
    if (!DicTest.Equals(other.DicTest)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0) hash ^= Id.GetHashCode();
    if (Name.Length != 0) hash ^= Name.GetHashCode();
    if (ItemType != global::ItemType.Resource) hash ^= ItemType.GetHashCode();
    hash ^= rewards_.GetHashCode();
    hash ^= DicTest.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Name);
    }
    if (ItemType != global::ItemType.Resource) {
      output.WriteRawTag(24);
      output.WriteEnum((int) ItemType);
    }
    rewards_.WriteTo(output, _repeated_rewards_codec);
    dicTest_.WriteTo(output, _map_dicTest_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Name);
    }
    if (ItemType != global::ItemType.Resource) {
      output.WriteRawTag(24);
      output.WriteEnum((int) ItemType);
    }
    rewards_.WriteTo(ref output, _repeated_rewards_codec);
    dicTest_.WriteTo(ref output, _map_dicTest_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (Id != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
    }
    if (Name.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
    }
    if (ItemType != global::ItemType.Resource) {
      size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ItemType);
    }
    size += rewards_.CalculateSize(_repeated_rewards_codec);
    size += dicTest_.CalculateSize(_map_dicTest_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(ItemResMsg other) {
    if (other == null) {
      return;
    }
    if (other.Id != 0) {
      Id = other.Id;
    }
    if (other.Name.Length != 0) {
      Name = other.Name;
    }
    if (other.ItemType != global::ItemType.Resource) {
      ItemType = other.ItemType;
    }
    rewards_.Add(other.rewards_);
    dicTest_.MergeFrom(other.dicTest_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Name = input.ReadString();
          break;
        }
        case 24: {
          ItemType = (global::ItemType) input.ReadEnum();
          break;
        }
        case 34: {
          rewards_.AddEntriesFrom(input, _repeated_rewards_codec);
          break;
        }
        case 42: {
          dicTest_.AddEntriesFrom(input, _map_dicTest_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Name = input.ReadString();
          break;
        }
        case 24: {
          ItemType = (global::ItemType) input.ReadEnum();
          break;
        }
        case 34: {
          rewards_.AddEntriesFrom(ref input, _repeated_rewards_codec);
          break;
        }
        case 42: {
          dicTest_.AddEntriesFrom(ref input, _map_dicTest_codec);
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class ItemResMsgDic : pb::IMessage<ItemResMsgDic>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<ItemResMsgDic> _parser = new pb::MessageParser<ItemResMsgDic>(() => new ItemResMsgDic());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<ItemResMsgDic> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ObjectResMsgReflection.Descriptor.MessageTypes[3]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public ItemResMsgDic() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public ItemResMsgDic(ItemResMsgDic other) : this() {
    dic_ = other.dic_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public ItemResMsgDic Clone() {
    return new ItemResMsgDic(this);
  }

  /// <summary>Field number for the "Dic" field.</summary>
  public const int DicFieldNumber = 1;
  private static readonly pbc::MapField<int, global::ItemResMsg>.Codec _map_dic_codec
      = new pbc::MapField<int, global::ItemResMsg>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForMessage(18, global::ItemResMsg.Parser), 10);
  private readonly pbc::MapField<int, global::ItemResMsg> dic_ = new pbc::MapField<int, global::ItemResMsg>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::MapField<int, global::ItemResMsg> Dic {
    get { return dic_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as ItemResMsgDic);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(ItemResMsgDic other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (!Dic.Equals(other.Dic)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    hash ^= Dic.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    dic_.WriteTo(output, _map_dic_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    dic_.WriteTo(ref output, _map_dic_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    size += dic_.CalculateSize(_map_dic_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(ItemResMsgDic other) {
    if (other == null) {
      return;
    }
    dic_.MergeFrom(other.dic_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          dic_.AddEntriesFrom(input, _map_dic_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          dic_.AddEntriesFrom(ref input, _map_dic_codec);
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code