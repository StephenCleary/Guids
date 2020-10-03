![Logo](src/icon.png)

# Guids [![Build status](https://github.com/StephenCleary/Guids/workflows/Build/badge.svg)](https://github.com/StephenCleary/Guids/actions?query=workflow%3ABuild) [![codecov](https://codecov.io/gh/StephenCleary/Guids/branch/master/graph/badge.svg)](https://codecov.io/gh/StephenCleary/Guids) [![NuGet version](https://badge.fury.io/nu/Nito.Guids.svg)](https://www.nuget.org/packages/Nito.Guids) [![API docs](https://img.shields.io/badge/API-dotnetapis-blue.svg)](http://dotnetapis.com/pkg/Nito.Guids)

Helper types for creating and interpreting GUIDs.

## Creating GUIDs

The following GUID types are supported:

- Version 5 GUIDs ("Named SHA-1 GUIDs"), generated from a Namespace GUID and a Name, which are hashed together with SHA-1. See `GuidFactory.CreateSha1`.
- Version 4 GUIDs ("Random GUIDs"), generated randomly. See `GuidFactory.CreateRandom`.
- Version 3 GUIDs ("Named MD5 GUIDs"), generated from a Namespace GUID and a Name, which are hashed together with MD5. See `GuidFactory.CreateMd5`.

The version 3 and 5 algorithms have been verified against independent implementations, most notably the Python `uuid` module, and are fully compatible with its GUIDs.

## Decoding GUIDs

Any `Guid` can be decoded into a `DecodedGuid`, which has a variety of properties for extracting information. E.g., you can extract the timestamp and MAC address of the GUID found in the Melissa virus as such:

```C#
var guid = Guid.Parse("572858EA-36DD-11D2-885F-004033E0078E");
var decoded = guid.Decode();
Assert.Equal(GuidVariant.Rfc4122, decoded.Variant);
Assert.Equal(GuidVersion.TimeBased, decoded.Version);
Assert.Equal(new DateTime(1998, 8, 18, 20, 52, 22, 510, DateTimeKind.Utc).AddTicks(10), decoded.CreateTime);
Assert.Equal(new byte[] { 0x00, 0x40, 0x33, 0xE0, 0x07, 0x8E }, decoded.Node);
Assert.True(decoded.NodeIsMac);
```

## Endianness

This library also has a few helper methods for working with big-endian GUID byte arrays, something not supported by the native `Guid` type. Note that the *string* representation of a `Guid` is always big-endian, but the .NET `Guid` uses little-endian byte array representations.

`Guid`s can be created from either big- or little-endian byte arrays. See `GuidFactory.FromLittleEndianByteArray` and `GuidFactory.FromBigEndianByteArray`.

Similarly, `Guid`s can be converted to a big-endian byte array. See the `ToBigEndianByteArray` extension method. The built-in `Guid.ToByteArray` converts to a little-endian byte array.
