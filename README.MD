﻿# ClassHydrate.Net

> Turn your C# objects into editable dictionaries — and back again.

[![.NET](https://github.com/AlexandreSanscartier/ClassHydrate.Net/actions/workflows/dotnet.yml/badge.svg)](https://github.com/AlexandreSanscartier/ClassHydrate.Net/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

<!--
Optionally add badges here later, e.g.:

[![Build Status](...)](...)
[![NuGet](...)](...)
[![License](...)](...)
-->

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [When to Use](#when-to-use)
- [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Basic Examples](#basic-examples)
  - [Limitations](#limitations)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [Development](#development)
- [Versioning](#versioning)
- [License](#license)

---

## Overview

**ClassHydrate.Net** is a small C# library that:

- **Dehydrates** a class instance into a `IClassPropertyBag : IReadOnlyDictionary<string, IClassProperty>` (plus optional metadata).
- Lets you **inspect and modify** that dictionary (e.g., in a UI or over the wire).
- **Rehydrates** the dictionary back into a strongly-typed instance.

Typical use cases include:

- Automatic form generation in web applications.
- Generic configuration / settings editors.
- Building simple admin UIs without hand-writing view models.

The focus is on:

- Being **framework-agnostic** (works with ASP.NET, Blazor, console apps, etc.).
- Providing **predictable round-trips**: `object → dictionary → object`.
- Using reflection so you don’t have to create additional mapping layers.

---

## Features

- 🔁 **Bidirectional mapping**  
  Class instance ⇄ `IClassPropertyBag`.

- 🧩 **Metadata-aware design** (planned)  
  Expose property type, nullability, attributes, etc. for UI generation.

- ⚙️ **Reflection-based, schema-less**  
  Works off your existing classes; no separate mapping configuration required to start.

- 🧪 **Test-friendly**  
  Deterministic behavior for dehydrating and rehydrating objects.

- 🧱 **Framework-agnostic**  
  No hard dependency on ASP.NET / MVC / Blazor; just plain C#.

---

## When to Use

Use **ClassHydrate.Net** when you:

- Want to build **generic UI** (forms, inspectors, editors) from your C# types.
- Need to **transport or store object state** as a dictionary and reconstruct later.
- Want to allow **partial updates / patching** via dictionaries (e.g., JSON from a client).
- Prefer **not** to maintain complex mapping profiles (like AutoMapper) just to work with dynamic UIs.

Maybe don’t use it when:

- You need **heavyweight object-to-object mapping** between many different types.
- You already have **strongly typed view models** and don’t need a dynamic layer.

---

## Getting Started

### Installation

> 🔧 **Note:** Fill in the actual NuGet package name and commands once published.

```powershell
# TODO: Update with real package info
dotnet add package ClassHydrate.Net
```

### Basic Examples

Dehydrate into a IClassPropertyBag with no set values

```csharp
public class Person {
	public int Id { get; set; }
	public string Name { get; set; }
}

var hydrator = new Hydrator();
var result hydrator.Dehydrate<Person>();
/*
["Id"]		->  IClassProperty { Name = "Id", Value = null, Type = typeof(int) }
["Name"]	->  IClassProperty { Name = "Name", Value = null, Type = typeof(string) }
*/
```

Dehydrate into a IClassPropertyBag with set values from an object

```csharp
public class Person {
	public int Id { get; set; }
	public string Name { get; set; }
}

var person = new Person { Id = 1, Name = "Alice" };
var hydrator = new Hydrator();
var result hydrator.Dehydrate(person);
/*
["Id"]		->  IClassProperty { Name = "Id", Value = 1, Type = typeof(int) }
["Name"]	->  IClassProperty { Name = "Name", Value = "Alice", Type = typeof(string) }
*/
```

Rehydrate from a IClassPropertyBag back into a strongly-typed object

```csharp
public class Person {
	public int Id { get; set; }
	public string Name { get; set; }
}

// Normally you would get this from Dehydrate();
var classPropertyBag = new ClassPropertyBag(
    typeof(ClassWithManyConstructors),
    new Dictionary<string, IClassProperty>(StringComparer.OrdinalIgnoreCase)
    {
        { "Id", CreateClassProperty("Id", 42) },
        { "Name", CreateClassProperty("Name", "Alice") },
    }
);

var hydrator = new Hydrator();
var result = hydrator.Hydrate<Person>(classPropertyBag);
/*
result -> Person { Id = 42, Name = "Alice" }
*/
```

Dehydrate, modify values and then Hydrate

```csharp
public class Person {
	public int Id { get; set; }
	public string Name { get; set; }
}

var hydrator = new Hydrator();
var classPropertyBag = hydrator.Dehydrate<Person>();

// Modify values
classPropertyBag.TryEditValue("Id", 42);
classPropertyBag.TryEditValue("Name", "Alice");

var result = hydrator.Hydrate<Person>(classPropertyBag);

/*
result -> Person { Id = 42, Name = "Alice" }
*/
```

## Limitations

- Currently supports only primitive types and strings. Complex types and collections are not fully supported yet.
- In order to properly detect constructors the property names must match the constructor parameter names (case-insensitive).
- Does not yet support validation rules or attributes on properties (except basic type validation).
- Performance may be impacted for very large object graphs due to reflection overhead.

## Roadmap

- [x] Initial release with basic Dehydrate and Hydrate functionality.
- [ ] Adding custom validation rules
- [ ] Support for nested objects and collections.
