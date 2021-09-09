// See https://aka.ms/new-console-template for more information


using CliWrap;

using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("Startup");

var basePath = AppDomain.CurrentDomain.BaseDirectory.Split("Aspire")[0];

var solution = Path.Combine(basePath, "Aspire");

var stdOutBuffer = new StringBuilder();
var stdErrBuffer = new StringBuilder();
var isOver = false;

_ = Task.Run(() =>
{
    while (!isOver)
    {
        Console.Clear();
        Console.WriteLine(stdOutBuffer.ToString());
        Console.WriteLine(stdErrBuffer.ToString());
        Thread.Sleep(200);
    }
});

var result = await Cli.Wrap("dotnet")
    .WithArguments("build Aspire.sln")
    .WithWorkingDirectory(solution)
    .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
    .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
    .WithValidation(CommandResultValidation.None)
    .ExecuteAsync();

isOver = true;

if (result.ExitCode != 0)
{
    Console.WriteLine("构建失败");
    return;
}

basePath = Path.Combine(solution, "src");

var directories = Directory.GetDirectories(basePath);

foreach (var classify in directories)
{
    foreach (var project in Directory.GetDirectories(classify))
    {
        var projectName = project.Split("\\").Last();
        var csproj = Path.Combine(project, $"{projectName}.csproj");
        var content = File.ReadAllText(csproj);
        var regex = new Regex("<Version>[0-9].[0-9].[0-9][0-9][0-9]</Version>");
        var match = regex.Match(content);
        if (!match.Success || match.Groups.Count < 1)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine(csproj);
            Console.WriteLine("没有匹配到版本");
            Console.WriteLine("-------------------------------");
            continue;
        }

        var version = match.Groups[0].Value.Split(new[] { "<Version>", "</Version>" }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

        if (string.IsNullOrWhiteSpace(version)) continue;

        if (!int.TryParse(version.Split(".")[2], out var third)) continue;

        // TODO 没有考虑进位

        content = content.Replace(
            match.Groups[0].Value,
            $"<Version>{string.Join(".", version.Split(".").Take(2))}.{++third}</Version>");

        File.WriteAllText(csproj, content);
    }
}


stdOutBuffer.Clear();
stdErrBuffer.Clear();

result = await Cli.Wrap("dotnet")
   .WithArguments(new[] { "pack", "Aspire.sln", "--no-build", "--no-restore", "--output", "nupkgs" })
   .WithWorkingDirectory(solution)
   .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
   .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
   .WithValidation(CommandResultValidation.None)
   .ExecuteAsync();

Console.WriteLine(stdOutBuffer.ToString());
Console.WriteLine(stdErrBuffer.ToString());

Console.WriteLine("Done");

Console.ReadLine();
