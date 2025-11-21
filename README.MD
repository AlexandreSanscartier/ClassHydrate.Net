# ClassHydrate.Net

> Turn your C# objects into editable dictionaries — and back again.

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
  - [Basic Example](#basic-example)
- [Core Concepts](#core-concepts)
  - [Hydration & Dehydration](#hydration--dehydration)
  - [Property Metadata](#property-metadata)
- [API Overview](#api-overview)
- [Usage Examples](#usage-examples)
  - [Dynamic Form Generation](#dynamic-form-generation)
  - [Patch / Partial Updates](#patch--partial-updates)
  - [Generic Admin / Backoffice UIs](#generic-admin--backoffice-uis)
- [Advanced Topics](#advanced-topics)
  - [Custom Converters](#custom-converters)
  - [Ignoring or Renaming Properties](#ignoring-or-renaming-properties)
  - [Validation](#validation)
- [Performance Notes](#performance-notes)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [Development](#development)
- [Versioning](#versioning)
- [License](#license)

---

## Overview

**ClassHydrate.Net** is a small C# library that:

- **Dehydrates** a class instance into a `Dictionary<string, object>` (plus optional metadata).
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
  Class instance ⇄ `Dictionary<string, object>`.

- 🧩 **Metadata-aware design** (planned)  
  Expose property type, nullability, attributes, etc. for UI generation.

- ⚙️ **Reflection-based, schema-less**  
  Works off your existing classes; no separate mapping configuration required to start.

- 🧪 **Test-friendly**  
  Deterministic behavior for dehydrating and rehydrating objects.

- 🧱 **Framework-agnostic**  
  No hard dependency on ASP.NET / MVC / Blazor; just plain C#.

*(This list is illustrative — adjust as the implementation evolves.)*

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
