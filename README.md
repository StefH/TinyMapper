TinyMapper.Signed - a quick object mapper for .Net
======================================================

## Info
This is a **signed** version from [TinyMapper](https://github.com/TinyMapper/TinyMapper).


## Installation

[![NuGet](https://img.shields.io/nuget/v/TinyMapper.Signed)](https://www.nuget.org/packages/TinyMapper.Signed)

## Performance Comparison


![Performance Comparison](https://raw.githubusercontent.com/TinyMapper/TinyMapper/master/Source/Benchmark/DataSource/PrimitiveTypeMapping.jpg)


## Getting Started

``` csharp
TinyMapper.Bind<Person, PersonDto>();

var person = new Person
{
	Id = Guid.NewGuid(),
	FirstName = "John",
	LastName = "Doe"
};

var personDto = TinyMapper.Map<PersonDto>(person);
```

Ignore mapping source members and bind members with different names/types

``` csharp
TinyMapper.Bind<Person, PersonDto>(config =>
{
	config.Ignore(x => x.Id);
	config.Ignore(x => x.Email);
	config.Bind(source => source.LastName, target => target.Surname);
	config.Bind(target => source.Emails, typeof(List<string>));
});

var person = new Person
{
	Id = Guid.NewGuid(),
	FirstName = "John",
	LastName = "Doe",
	Emails = new List<string>{"support@tinymapper.net", "MyEmail@tinymapper.net"}
};

var personDto = TinyMapper.Map<PersonDto>(person);
```

`TinyMapper` supports the following platforms:
* .Net 3.5+
* .NET Standard 1.3
* .NET Standard 2.1
* .NET 8

## What to read

 * [TinyMapper: yet another object to object mapper for .net](http://www.codeproject.com/Articles/886420/TinyMapper-yet-another-object-to-object-mapper-for)
 * [Complex mapping](https://github.com/TinyMapper/TinyMapper/wiki/Complex-mapping)
 * [How to create custom mapping](https://github.com/TinyMapper/TinyMapper/wiki/Custom-mapping)
 
## Contributors
A big thanks to all of TinyMapper's contributors:
 
 * [nzaugg](https://github.com/nzaugg)
 * [Sdzeng](https://github.com/Sdzeng)
 * [iEmiya](https://github.com/iEmiya)
 * [lijaso](https://github.com/lijaso)
 * [nomailme](https://github.com/nomailme)
 * [Skaiol](https://github.com/Skaiol)
 * [Sufflavus](https://github.com/Sufflavus)
 * [qihangnet](https://github.com/qihangnet)
 * [teknogecko](https://github.com/teknogecko)
 * [Samtrion](https://github.com/Samtrion)
 * [DerHulk](https://github.com/DerHulk)
 * [Stef Heyenrath](https://github.com/StefH)