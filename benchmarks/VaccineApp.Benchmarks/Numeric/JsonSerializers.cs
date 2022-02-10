using BenchmarkDotNet.Attributes;
using Core.Models;
using Newtonsoft.Json;
using SystemJsonLib = System.Text.Json;

namespace VaccineApp.Benchmarks.Numeric;

[MemoryDiagnoser]
public class JsonSerializers
{
    private ChildModel _child;

    [GlobalSetup]
    public void Setup()
    {
        _child = new ChildModel(){
            FullName = "Ahmad",
            Id = Guid.NewGuid(),
            DOB = DateTime.UtcNow,
            Gender = "Male",
            RINo = 12,
            OPV0 = true
        };
    }

    [Benchmark]
    public void JsonSerialization_With_Netwtonsoft_Json()
    {
        var s = JsonConvert.SerializeObject(_child);
    }

    [Benchmark]
    public void JsonSerialization_With_System_Text_Json()
    {
        var s = SystemJsonLib.JsonSerializer.Serialize(_child);
    }
}