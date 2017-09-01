# Collector.Common.Azure.Webjobs.Extensions

Common extensions for azure webjobs

## Getting Started

Start start template for azure webjobs in visual studio

### Installing



`Install-Package Collector.Common.Azure.Webjobs.Extensions`
## Usage 
```
private static void Main()
{
    var config = new JobHostConfiguration();

    if (config.IsDevelopment)
    {
        config.UseDevelopmentSettings();
    }
    //Add this line
    config.ConverterManager.ConfigureJsonConverter();
    using (var host = new JobHost(config))
    {
        host.RunAndBlock();
    }
}
```
Also se `Collector.Common.Azure.Webjobs.Extensions.Sample`

## Running the tests

You know... test run explorer or ncrunch

## Built With

* [Visual studio](http://www.visualstudio.com) - The IDE

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Felix Svensson** - *Initial work* - [Klowdo](https://github.com/klowdo)
