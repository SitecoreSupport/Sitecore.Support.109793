namespace Sitecore.Support.Shell.Applications.ContentEditor.Pipelines.RenderContentEditor
{
    using Sitecore.Diagnostics;
    using Sitecore.Shell.Applications.ContentEditor;
    using Sitecore.Shell.Applications.ContentEditor.Pipelines.RenderContentEditor;
    using Sitecore.Support.Shell.Applications.ContentEditor;
    using Sitecore.Support.Shell.Applications.ContentManager.Dialogs.OneColumnPreview;
    using Sitecore.Support.Shell.Applications.ContentManager.Dialogs.ResetFields;
    using System;

    internal class EditorFormatterReplacer
    {
        public void Process(RenderContentEditorArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Sitecore.Shell.Applications.ContentEditor.EditorFormatter formatter = null;
            string name = args.EditorFormatter.GetType().Name;
            if (name != null)
            {
                if (!(name == "EditorFormatter"))
                {
                    if (name == "OneColumnFormatter")
                    {
                        OneColumnFormatter formatter3 = new OneColumnFormatter
                        {
                            Arguments = args.EditorFormatter.Arguments,
                            IsFieldEditor = args.EditorFormatter.IsFieldEditor
                        };
                        formatter = formatter3;
                    }
                    else if (name == "ResetFieldsFormatter")
                    {
                        ResetFieldsFormatter formatter4 = new ResetFieldsFormatter
                        {
                            Arguments = args.EditorFormatter.Arguments,
                            IsFieldEditor = args.EditorFormatter.IsFieldEditor
                        };
                        formatter = formatter4;
                    }
                    else if (name == "TranslatorFormatter")
                    {
                        Sitecore.Support.Shell.Applications.ContentEditor.TranslatorFormatter formatter5 = new Sitecore.Support.Shell.Applications.ContentEditor.TranslatorFormatter
                        {
                            Arguments = args.EditorFormatter.Arguments,
                            IsFieldEditor = args.EditorFormatter.IsFieldEditor
                        };
                        formatter = formatter5;
                    }
                }
                else
                {
                    Sitecore.Support.Shell.Applications.ContentEditor.EditorFormatter formatter2 = new Sitecore.Support.Shell.Applications.ContentEditor.EditorFormatter
                    {
                        Arguments = args.EditorFormatter.Arguments,
                        IsFieldEditor = args.EditorFormatter.IsFieldEditor
                    };
                    formatter = formatter2;
                }
            }
            args.EditorFormatter = formatter;
        }
    }
}
