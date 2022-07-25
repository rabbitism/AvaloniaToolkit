﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace AvaloniaToolkit.TextTemplates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Code\Galaxism\AvaloniaToolkit\AvaloniaToolkit\TextTemplates\MultiValueConverterTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class MultiValueConverterTemplate : BaseTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Globalization;\r\nus" +
                    "ing Avalonia.Data.Converters;\r\n\r\nnamespace ");
            
            #line 13 "C:\Code\Galaxism\AvaloniaToolkit\AvaloniaToolkit\TextTemplates\MultiValueConverterTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    internal class ");
            
            #line 15 "C:\Code\Galaxism\AvaloniaToolkit\AvaloniaToolkit\TextTemplates\MultiValueConverterTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write(" : IMultiValueConverter\r\n    {\r\n        public object? Convert(IList<object?> val" +
                    "ues, Type targetType, object? parameter, CultureInfo culture)\r\n        {\r\n      " +
                    "      return new Avalonia.Data.BindingNotification(values);\r\n        }\r\n    }\r\n}" +
                    "\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "C:\Code\Galaxism\AvaloniaToolkit\AvaloniaToolkit\TextTemplates\MultiValueConverterTemplate.tt"

private string _NameField;

/// <summary>
/// Access the Name parameter of the template.
/// </summary>
private string Name
{
    get
    {
        return this._NameField;
    }
}

private string _NamespaceField;

/// <summary>
/// Access the Namespace parameter of the template.
/// </summary>
private string Namespace
{
    get
    {
        return this._NamespaceField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public override void Initialize()
{
    base.Initialize();
    if ((this.Errors.HasErrors == false))
    {
bool NameValueAcquired = false;
if (this.Session.ContainsKey("Name"))
{
    this._NameField = ((string)(this.Session["Name"]));
    NameValueAcquired = true;
}
if ((NameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Name");
    if ((data != null))
    {
        this._NameField = ((string)(data));
    }
}
bool NamespaceValueAcquired = false;
if (this.Session.ContainsKey("Namespace"))
{
    this._NamespaceField = ((string)(this.Session["Namespace"]));
    NamespaceValueAcquired = true;
}
if ((NamespaceValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Namespace");
    if ((data != null))
    {
        this._NamespaceField = ((string)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
