//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v8.12.3
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder.Embedded;

namespace Umbraco.Web.PublishedModels
{
	/// <summary>HomePage</summary>
	[PublishedModel("homePage")]
	public partial class HomePage : PublishedContentModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public new const string ModelTypeAlias = "homePage";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<HomePage, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public HomePage(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// About Text
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("aboutText")]
		public global::System.Web.IHtmlString AboutText => this.Value<global::System.Web.IHtmlString>("aboutText");

		///<summary>
		/// About Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("aboutTitle")]
		public string AboutTitle => this.Value<string>("aboutTitle");

		///<summary>
		/// Body Text
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText")]
		public string BodyText => this.Value<string>("bodyText");

		///<summary>
		/// Body Text 1
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText1")]
		public global::System.Web.IHtmlString BodyText1 => this.Value<global::System.Web.IHtmlString>("bodyText1");

		///<summary>
		/// Body Text 1 Header
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText1Header")]
		public string BodyText1Header => this.Value<string>("bodyText1Header");

		///<summary>
		/// Body Text 2
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText2")]
		public global::System.Web.IHtmlString BodyText2 => this.Value<global::System.Web.IHtmlString>("bodyText2");

		///<summary>
		/// Body Text 2 Header
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText2Header")]
		public string BodyText2Header => this.Value<string>("bodyText2Header");

		///<summary>
		/// Body Text 3
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText3")]
		public global::System.Web.IHtmlString BodyText3 => this.Value<global::System.Web.IHtmlString>("bodyText3");

		///<summary>
		/// Body Text 3 Header
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText3Header")]
		public string BodyText3Header => this.Value<string>("bodyText3Header");

		///<summary>
		/// Body Text 4
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText4")]
		public global::System.Web.IHtmlString BodyText4 => this.Value<global::System.Web.IHtmlString>("bodyText4");

		///<summary>
		/// Body Text 4 Header
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText4Header")]
		public string BodyText4Header => this.Value<string>("bodyText4Header");

		///<summary>
		/// Body Text 5
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText5")]
		public string BodyText5 => this.Value<string>("bodyText5");

		///<summary>
		/// Body Text 5 Header
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("bodyText5Header")]
		public string BodyText5Header => this.Value<string>("bodyText5Header");

		///<summary>
		/// Conclusion Text
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("conclusionText")]
		public global::System.Web.IHtmlString ConclusionText => this.Value<global::System.Web.IHtmlString>("conclusionText");

		///<summary>
		/// Conclusion Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("conclusionTitle")]
		public string ConclusionTitle => this.Value<string>("conclusionTitle");

		///<summary>
		/// Contact Mail
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("contactMail")]
		public string ContactMail => this.Value<string>("contactMail");

		///<summary>
		/// Footer Text
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("footerText")]
		public global::System.Web.IHtmlString FooterText => this.Value<global::System.Web.IHtmlString>("footerText");

		///<summary>
		/// Footer Text 2
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("footerText2")]
		public string FooterText2 => this.Value<string>("footerText2");

		///<summary>
		/// Footer Text Contact
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("footerTextContact")]
		public string FooterTextContact => this.Value<string>("footerTextContact");

		///<summary>
		/// Footer Text Contact 2
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("footerTextContact2")]
		public string FooterTextContact2 => this.Value<string>("footerTextContact2");

		///<summary>
		/// Page Sub Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("pageSubTitle")]
		public string PageSubTitle => this.Value<string>("pageSubTitle");

		///<summary>
		/// Page Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("pageTitle")]
		public string PageTitle => this.Value<string>("pageTitle");

		///<summary>
		/// Quote 1
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("quote1")]
		public string Quote1 => this.Value<string>("quote1");

		///<summary>
		/// Quote2
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("quote2")]
		public string Quote2 => this.Value<string>("quote2");

		///<summary>
		/// Quote3
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("quote3")]
		public string Quote3 => this.Value<string>("quote3");
	}
}
