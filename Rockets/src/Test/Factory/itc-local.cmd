@echo off
echo.
echo Integration Test Script on LOCAL SERVER
echo -------------------------------------
echo.

dotnet itc.dll RocketFactoryTests.dll /config it-local.laconf -trun
REM dotnet itc.dll Erx.Business.Tests.Integ.dll /config it-local.laconf -trun

echo.
echo.
