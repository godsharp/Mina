# Mina

[![license][li]][l] [![GitHub code size in bytes][si]][0]

A simple window service library for .net .

# Build Status

|Branch|Status|
|---|---|
|master|[![Build status](https://ci.appveyor.com/api/projects/status/6lj3u50qyea8m6ts/branch/master?svg=true&style=flat-square)](https://ci.appveyor.com/project/seayxu/mina/branch/master)|
|dev|[![Build status](https://ci.appveyor.com/api/projects/status/6lj3u50qyea8m6ts/branch/dev?svg=true)](https://ci.appveyor.com/project/seayxu/mina/branch/dev)|
|release|[![Build status](https://ci.appveyor.com/api/projects/status/6lj3u50qyea8m6ts/branch/release?svg=true)](https://ci.appveyor.com/project/seayxu/mina/branch/release)|

|Name|Stable|Preview|
|---|:---:|:---:|
| Mina | [![MyGet][mi1]][m1] [![NuGet][ni1]][n1] | [![MyGet][mi2]][m1] [![NuGet][ni2]][n1] |

# Usage

## Install

You can install Mina via NuGet:
```
PM> Install-Package GodSharp.Mina
```

## Use

1. Add a class and inherited from `MinaService`, then override methods `OnInitialize`, `OnStart`, `OnStop`, `OnPause`, `OnContinue`, `OnCustomCommand`, `OnShutdown`, which you need.

```
public class MinaSampleService:MinaService
{
    public override void OnStart(string[] args)
    {
    }

    public override void OnStop()
    {
    }

    public override void OnPause()
    {
    }

    public override void OnContinue()
    {
    }

    public override void OnShutdown()
    {
    }

    public override void OnCustomCommand(int command)
    {
    }
}
```

2. Run service, you can use the following method.

```
MinaHost.Run(new MinaOption(),new MinaSampleService(),args);
MinaHost.Run((o)=>{},new MinaSampleService(),args);
MinaHost.Run<MinaSampleService>(new MinaOption(),args);
MinaHost.Run<MinaSampleService>((o)=>{},args);
```

## Service

The service `install/uninstall`, `start/restart/pause/continue/stop`, `command`.

The example service name is `sample.exe`.

### Help

Syntax:
```
service.exe -h[elp]
service.exe /h[elp]
```

Sample:
```
sample.exe -h
sample.exe -help
sample.exe /h
sample.exe /help
```

### Install

Syntax:
```
service.exe -i[nstall]
service.exe /i[nstall]
```

Sample:
```
sample.exe -i
sample.exe -install
sample.exe /i
sample.exe /install
```

### Uninstall

Syntax:
```
service.exe -u[ninstall]
service.exe /u[ninstall]
```

Sample:
```
sample.exe -u
sample.exe -uninstall
sample.exe /u
sample.exe /uninstall
```

### Start

Syntax:
```
service.exe -start
service.exe /start
```

Sample:
```
sample.exe -start
sample.exe /start
```

### Restart

Syntax:
```
service.exe -r[estart]
service.exe /r[estart]
```

Sample:
```
sample.exe -r
sample.exe -restart
sample.exe /r
sample.exe /restart
```

### Pause

Syntax:
```
service.exe -p[ause]
service.exe /p[ause]
```

Sample:
```
sample.exe -p
sample.exe -pause
sample.exe /p
sample.exe /pause
```

### Continue

Syntax:
```
service.exe -c[ontinue]
service.exe /c[ontinue]
```

Sample:
```
sample.exe -c
sample.exe -continue
sample.exe /c
sample.exe /continue
```

### Stop

Syntax:
```
service.exe -stop
service.exe /stop
```

Sample:
```
sample.exe -stop
sample.exe /stop
```

### Command

Syntax:
```
service.exe -cmd|command command-parameter
service.exe /cmd|command command-parameter
```

`command-parameter` is `int` type.

Sample:
```
sample.exe -cmd 1
sample.exe -command 1
sample.exe /cmd 1
sample.exe /command 1
```

# Sample

See [sample project][sample]

[0]: https://github.com/godsharp/Mina
[si]: https://img.shields.io/github/languages/code-size/godsharp/Mina.svg?style=flat-square

[li]: https://img.shields.io/badge/license-MIT-blue.svg?label=license&style=flat-square
[l]: https://github.com/godsharp/Mina/blob/master/LICENSE

[m1]: https://www.myget.org/Package/Details/godsharp?packageType=nuget&packageId=GodSharp.Mina

[mi1]: https://img.shields.io/myget/godsharp/v/GodSharp.Mina.svg?label=myget&style=flat-square
[mi2]: https://img.shields.io/myget/godsharp/vpre/GodSharp.Mina.svg?label=myget&style=flat-square

[n1]: https://www.nuget.org/packages/GodSharp.Mina

[ni1]: https://img.shields.io/nuget/v/GodSharp.Mina.svg?label=nuget&style=flat-square
[ni2]: https://img.shields.io/nuget/vpre/GodSharp.Mina.svg?label=nuget&style=flat-square

[sample]: ./sample/