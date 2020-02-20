# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.6] - 2020-01-20

### Added
- Added support for serialization of float type
- Added support for deserialization of float type

## [1.0.5] - 2020-01-17

### Fixed
- Fixed a bug which causes null references when serializing empty strings

## [1.0.4] - 2020-01-16

### Added
- Added Assembly Definition

### Fixed
- Fixed a bug preventing the correct deserialization of an object

## [1.0.3] - 2020-01-16

### Added
- Added support for all loaded Porject Assemblies

## [1.0.2] - 2020-01-15

### Added
- Added support for UnityEngine.Vector2 Serialization
- Added support for UnityEngine.Vector3 Serialization
- Added support for UnityEngine.Vector4 Serialization
- Added support for UnityEngine.Color Serialization
- Added support for UnityEngine.Sprite Serialization

## [1.0.1] - 2020-01-15

### Added
- Added support for System.Generic.List Serialization
- Added support for Array Serialization

## [1.0.0] - 2020-01-15

### Added
- "Serializer" class which allows serialization to string and to file
- "Deserializer" class which allows to deserialize from string and file