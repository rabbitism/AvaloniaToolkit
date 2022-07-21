using EnvDTE80;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaToolkit.Common
{
    internal static class ProjectHelpers
    {
        // private static readonly DTE2 _dte = AvaloniaToolkitPackage.DTE;

        public static async Task<string> GetRootNamespaceAsync(this Project project)
        {
            if (project is null) return null;
            string @namespace = project.Name ?? string.Empty;
            try
            {
                @namespace = await project.GetAttributeAsync("RootNamespace");
            }
            catch { }
            return @namespace;
        }

        public static async Task<string> GetNamespaceAsync(SolutionItem item)
        {
            Project project;
            if (item is Project p)
            {
                project = p;
            }
            else
            {
                project = item.FindParent(SolutionItemType.Project) as Project;
            }
            var rootNamespace = await GetRootNamespaceAsync(project);
            List<string> sections = new();
            SolutionItem i = item;
            while (i != null && i?.FullPath != project?.FullPath)
            {
                sections.Add(i.Text);
                i = i.Parent;
            }
            sections.Add(rootNamespace);
            sections.Reverse();
            return string.Join(".", sections);
        }

        public static DirectoryInfo GetContainingFolder(this SolutionItem project)
        {
            if (project.Type == SolutionItemType.Project || project.Type == SolutionItemType.PhysicalFile)
            {
                FileInfo fileInfo = new(project.FullPath);
                return fileInfo.Directory;
            }
            else if (project.Type == SolutionItemType.PhysicalFolder)
            {
                DirectoryInfo info = new(project.FullPath);
                return info;
            }
            return null;
        }

        public static Project GetContainingProject(this SolutionItem item)
        {
            if (item is null || item.Type == SolutionItemType.Solution || item.Type == SolutionItemType.SolutionFolder)
            {
                return null;
            }
            SolutionItem i = item;
            // TODO: optimize code. 
            if(i.Parent != null && i.Parent.Type == SolutionItemType.Project)
            {
                return i.Parent as Project;
            }
            while (i.Parent != null && i.Parent.Type != SolutionItemType.Project)
            {
                i = i.Parent;
            }
            return i as Project;
        }
    }
}
