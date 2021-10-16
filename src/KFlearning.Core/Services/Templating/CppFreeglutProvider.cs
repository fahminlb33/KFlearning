using KFlearning.Core.Services;
using KFlearning.Core.Services.Templating;

namespace KFlearning.TemplateProvider
{
    public class CppFreeglutProvider : TemplateProviderBase, ITemplateProvider
    {
        private readonly IPathManager _path;

        public CppFreeglutProvider(IPathManager path)
        {
            _path = path;
        }

        public string Title => "C++ (Freeglut)";

        public void Scaffold(string projectRoot)
        {
            ProjectRoot = projectRoot;

            // program.cpp
            WriteFile("program.cpp", TemplateResource.CPP_program);

            // c_cpp_properties.json
            var content = Transform(TemplateResource.CPP_c_cpp_properties, x => x
                .Replace("{GXX}", _path.GetPath(PathKind.MingwGXXExecutable, true))
                .Replace("{ENV1}", _path.GetPath(PathKind.MingwInclude1Directory, true))
                .Replace("{ENV2}", _path.GetPath(PathKind.MingwInclude2Directory, true)));
            WriteFile(".vscode/c_cpp_properties.json", content);

            // launch.json
            content = Transform(TemplateResource.CPP_launch,
                x => x.Replace("{GDB}", _path.GetPath(PathKind.MingwGDBExecutable, true)));
            WriteFile(".vscode/launch.json", content);

            // settings.json
            WriteFile(".vscode/settings.json", TemplateResource.CPP_settings);

            // tasks.json
            WriteFile(".vscode/tasks.json", TemplateResource.CPP_GUI_tasks);
        }
    }
}