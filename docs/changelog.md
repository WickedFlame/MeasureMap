---
title: Changelog
layout: "default"
nav_order: 99
---

## MeasureMap Changelog
All notable changes to this project will be documented in this file.
 
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).
 
### v3.0.0
#### Added
- Benchmarks can be written in a class with the Methods to benchmark marked with the help of attributes.

#### Fixed
- Get null value from ExecutionContext caused an exception

### v2.4.0
#### Added
- ThreadNumber is emitted in the result
- Extension to log to the console 
- OnStartPipeline Event that is run when starting the threads to create the IExecutionContext per Thread
- OnEndPipeline Event that is run after the pipeline per thread is finished
- Settings contain a IsWarmup flag to indicate if the run is a warmup run
- CreateContext Extensionmethod on Settings to create a new IExecutionContext based on the Settings
- Write debug log when the OnPipelineStart event gets executed
- Ensure that all threads are setup and created before the first task is run
- Added Rampup to start threads delayed

#### Fixed
- Get from the IExecutionContext needed the Key to be lowercase
  
### v2.2.1
#### Fixed
- Throughput was not calculated correctly when using multiple threads
 
### v2.2.0
#### Added
- ThreadBehaviour to define how a thread is created
- Allow benchmarks to be run on the MainThread
- OnExecuted to run a delegate after each task run
- Trace - Edit TraceOptions as delegate
 
#### Changed
- Added IDisposable to IThreadSessionHandler
 
#### Fixed
- Markdowntracer traced all data when using DetailPerThread
 
### v2.1.0
#### Added
- ThreadBehaviour to define how a thread is created
- Allow benchmarks to be run on the MainThread
 
#### Changed
- Added IDisposable to IThreadSessionHandler
 
### v2.0.2
#### Added
- Added Benchmarks and samples oh how to use the BenchmarkRunner
- SetDuration on BenchmarkRunner
- Factories to easily create trace metrics
- Benchmarks to test MeasureMap features
- Samples to show how to use MeasureMap
 
#### Changed
- Traces are now writen to the Logger
- Refactored Trace
- Duration is calculated from ticks
- TraceOptions uses TraceDetail enum to define the granularity of the traces
 
#### Fixed
- ProfilerSettings are now passed to all elements of a session
 
### v2.0.1
#### Added
- Benchmarks Trace throughput per second
- Customizable Tracer for Results
 
#### Changed
- Benchmarks now Trace iterartions instead of memory used
- Complete redo of the trace output

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
