namespace Sitecore.Support.Shell.Applications.ContentManager.Dialogs.ResetFields
{
    using Sitecore;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Shell;
    using Sitecore.Shell.Applications.ContentManager;
    using Sitecore.Shell.Applications.ContentManager.Dialogs.ResetFields;
    using System;
    using System.Web;
    using System.Web.UI;

    public class ResetFieldsFormatter : Sitecore.Shell.Applications.ContentManager.Dialogs.ResetFields.ResetFieldsFormatter
    {
        public override void RenderField(Control parent, Editor.Field field, bool readOnly)
        {
            Assert.ArgumentNotNull(parent, "parent");
            Assert.ArgumentNotNull(field, "field");
            Sitecore.Data.Fields.Field itemField = field.ItemField;
            Item fieldType = base.GetFieldType(itemField);
            if (fieldType != null)
            {
                if (!itemField.CanWrite)
                {
                    readOnly = true;
                }
                base.RenderMarkerBegin(parent, field.ControlID);
                string typeKey = itemField.TypeKey;
                if ((!string.IsNullOrEmpty(typeKey) && typeKey.Equals("checkbox")) && !UserOptions.ContentEditor.ShowRawValues)
                {
                    this.RenderField(parent, field, fieldType, readOnly);
                    this.RenderLabel(parent, field, fieldType, readOnly);
                    this.RenderMenuButtons(parent, field, fieldType, readOnly);
                }
                else
                {
                    this.RenderLabel(parent, field, fieldType, readOnly);
                    this.RenderMenuButtons(parent, field, fieldType, readOnly);
                    this.RenderField(parent, field, fieldType, readOnly);
                }
                base.RenderMarkerEnd(parent);
            }
        }

        public void RenderLabel(Control parent, Editor.Field field, Item fieldType, bool readOnly)
        {
            Assert.ArgumentNotNull(parent, "parent");
            Assert.ArgumentNotNull(field, "field");
            Assert.ArgumentNotNull(fieldType, "fieldType");
            Sitecore.Data.Fields.Field itemField = field.ItemField;
            Language language = base.Arguments.Language;
            Assert.IsNotNull(language, "language");
            if (itemField.Language != language)
            {
                Item item = ItemManager.GetItem(itemField.Item.ID, language, Sitecore.Data.Version.Latest, itemField.Item.Database);
                if (item != null)
                {
                    itemField = item.Fields[itemField.ID];
                }
            }
            string title = field.TemplateField.GetTitle(language);
            if (string.IsNullOrEmpty(title))
            {
                title = field.TemplateField.IgnoreDictionaryTranslations ? itemField.Name : Translate.Text(itemField.Name);
            }
            string toolTip = itemField.ToolTip;
            if (!string.IsNullOrEmpty(toolTip))
            {
                toolTip = Translate.Text(toolTip);
                if (toolTip.EndsWith(".", StringComparison.InvariantCulture))
                {
                    toolTip = StringUtil.Left(toolTip, toolTip.Length - 1);
                }
                title = title + " - " + toolTip;
            }
            title = HttpUtility.HtmlEncode(title);
            string label = field.ItemField.GetLabel(base.Arguments.IsAdministrator || Settings.ContentEditor.ShowFieldSharingLabels);
            if (!string.IsNullOrEmpty(label))
            {
                title = title + "<span class=\"scEditorFieldLabelAdministrator\"> [" + label + "]</span>";
            }
            string typeKey = itemField.TypeKey;
            if (!string.IsNullOrEmpty(typeKey) && (typeKey != "checkbox"))
            {
                title = title + ":";
            }
            if (readOnly)
            {
                title = "<span class=\"scEditorFieldLabelDisabled\">" + title + "</span>";
            }
            string str5 = HttpUtility.HtmlAttributeEncode(itemField.HelpLink);
            if (str5.Length > 0)
            {
                title = "<a class=\"scEditorFieldLabelLink\" href=\"" + str5 + "\" target=\"__help\">" + title + "</a>";
            }
            string str6 = string.Empty;
            if (itemField.Description.Length > 0)
            {
                str6 = " title=\"" + HttpUtility.HtmlAttributeEncode(itemField.Description) + "\"";
            }
            string str7 = "scEditorFieldLabel";
            title = "<div class=\"" + str7 + "\"" + str6 + ">" + title + "</div>";
            base.AddLiteralControl(parent, title);
        }
    }
}
