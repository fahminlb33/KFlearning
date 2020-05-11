// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : CppConsoleProvider.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using KFlearning.Core.Services;

namespace KFlearning.Services.Templates
{
    public class CppConsoleProvider : TemplateProviderBase, ITemplateProvider
    {
        private readonly IPathManager _path;

        public CppConsoleProvider(IPathManager path)
        {
            _path = path;
        }

        public string Title => "C++ (Konsol)";

        public void Provide(string projectRoot)
        {
            ProjectRoot = projectRoot;

            // program.cpp
            WriteFile("program.cpp", TR.CPP_program);

            // c_cpp_properties.json
            var content = Transform(TR.CPP_c_cpp_properties, x => x
                .Replace("{GXX}", _path.GetPath(PathKind.MingwGXXExecutable, true))
                .Replace("{ENV1}", _path.GetPath(PathKind.MingwInclude1Directory, true))
                .Replace("{ENV2}", _path.GetPath(PathKind.MingwInclude2Directory, true)));
            WriteFile(".vscode/c_cpp_properties.json", content);

            // launch.json
            content = Transform(TR.CPP_launch,
                x => x.Replace("{GDB}", _path.GetPath(PathKind.MingwGDBExecutable, true)));
            WriteFile(".vscode/launch.json", content);

            // settings.json
            WriteFile(".vscode/settings.json", TR.CPP_settings);

            // tasks.json
            WriteFile(".vscode/tasks.json", TR.CPP_Console_tasks);
        }
    }
}