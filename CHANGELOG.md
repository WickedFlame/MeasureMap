# MeasureMap Changelog
All notable changes to this project will be documented in this file.
 
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).
 
## vNext
### Added
- Added Benchmarks and samples oh how to use the BenchmarkRunner
- SetDuration on BenchmarkRunner
- Factories to easily create trace metrics
- Benchmarks to test MeasureMap features
- Samples to show how to use MeasureMap
 
### Changed
- Traces are now writen to the Logger
- Refactored Trace
- Duration is calculated from ticks
- TraceOptions uses TraceDetail enum to define the granularity of the traces
 
### Fixed
- ProfilerSettings are now passed to all elements of a session
 
## v2.0.1
### Added
- Benchmarks Trace throughput per second
- Customizable Tracer for Results
 
### Changed
- Benchmarks now Trace iterartions instead of memory used
- Complete redo of the trace output
 
### Fixed
 
## v2.0.0
### Added
- Add ThreadId and Iteration to the Tracedetails
- Wait for all threads to end
 
### Changed
- Changed Targetframework to netstandard2.0
- Changed from Task to full Threads
- Display more infos in the traces
- Use Stopwatch instead of DateTime.Now for more accuracy
 
### Fixed
 
## v1.7.0
### Added
- Set the duration that a Profilersession should run for
- Set a Interval to define the pace a task should be executed at
 
### Changed
- Updated .NET Versions to .NetStandard 2.1 and .NET Framework 4.8
 
### Fixed
