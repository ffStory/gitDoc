// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Enum.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from Enum.proto</summary>
public static partial class EnumReflection {

  #region Descriptor
  /// <summary>File descriptor for Enum.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static EnumReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CgpFbnVtLnByb3RvKiMKCEl0ZW1UeXBlEgwKCFJlc291cmNlEAASCQoFU3Bl",
          "ZWQQAioiCgpPYmplY3RUeXBlEgoKBlBsYXllchAAEggKBEhlcm8QAmIGcHJv",
          "dG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(new[] {typeof(global::ItemType), typeof(global::ObjectType), }, null, null));
  }
  #endregion

}
#region Enums
public enum ItemType {
  [pbr::OriginalName("Resource")] Resource = 0,
  [pbr::OriginalName("Speed")] Speed = 2,
}

public enum ObjectType {
  [pbr::OriginalName("Player")] Player = 0,
  [pbr::OriginalName("Hero")] Hero = 2,
}

#endregion


#endregion Designer generated code