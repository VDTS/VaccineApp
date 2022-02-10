using System.Text;
using BenchmarkDotNet.Attributes;
using DAL.Persistence;

namespace VaccineApp.Benchmarks.Numeric;

[MemoryDiagnoser]
public class StringInterpolations
{
    [Benchmark(Baseline = true)]
    public string Simple_StringInterpolation()
    {
        return DbNodePath.Masjeed("dummy");
    }

    [Benchmark]
    public string StringInterpolation_With_StringBuilder()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Masjeed");
        stringBuilder.Append("dummy");
        stringBuilder.Append(".json");

        return stringBuilder.ToString();
    }

    [Benchmark]
    public string StringInterpolation_With_StringConcat()
    {
        return String.Concat("Masjeed", "dummy", ".json");
    }
}