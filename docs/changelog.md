---
title: Changelog
layout: "default"
nav_order: 99
---

## MeasureMap Changelog
### v2.0.0
- Use Thread instead of Task to run the Sessions in. This improves performance and adds stability to the sessions
- Simple Sessions now also start a own thread to run the session in
- Trace shows more information about the result
- Information per thread is displayed in the trace
- Internally all measures are made with Stopwatch instead of DateTime. This adds to performance and accuracy
- ExecutionContext is added to the Task to share information better
- Fix: Thread and Iteration info was not allways correct
- All threads are waited for before ending the session and returning the result

### v1.7.0
- Set the duration that a Profilersession should run for
- Set a Interval to define the pace a task should be executed at
- Updated .NET Versions to .NetStandard 2.1 and .NET Framework 4.8

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
