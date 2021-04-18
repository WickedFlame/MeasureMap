---
title: Changelog
layout: "default"
nav_order: 99
---

## MeasureMap Changelog
### 1.6.2
- Improved Threading behaviour in SessionHandler

### 1.6.1
- Expose Settings as Method
- Altered the Processoraffinity

### 1.6.0
- Added Settings to the ProfilingSession and BenchmarkRunner
- Added setting to disable the Warmup task run
#### Breaking
- Moved Itterations Property to Settings

### 1.5.2
- Simplify adding SessionHandler Middleware to the SessionPipeline
- Setup method for initializing a Session

### 1.5.1
- Extend ProfilerSession configuration for Benchmarking

### 1.5.0
- Refactored TaskHandler to ProcessingPipeline
- Refactored ITaskHandler to ITaskMiddleware
- Refactoring AddCondition to Assert

### 1.5.0
- Benchmarking multiple ProfilerSessions

### 1.4.0
- Converted to .Net Standard library
