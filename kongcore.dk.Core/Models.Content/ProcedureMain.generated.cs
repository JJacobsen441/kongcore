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
	/// <summary>Procedure Main</summary>
	[PublishedModel("procedureMain")]
	public partial class ProcedureMain : PublishedContentModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public new const string ModelTypeAlias = "procedureMain";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ProcedureMain, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public ProcedureMain(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Procedure Body Text
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("procedureBo")]
		public global::System.Web.IHtmlString ProcedureBo => this.Value<global::System.Web.IHtmlString>("procedureBo");

		///<summary>
		/// Procedure Body Text2
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("procedureBodyText2")]
		public global::System.Web.IHtmlString ProcedureBodyText2 => this.Value<global::System.Web.IHtmlString>("procedureBodyText2");

		///<summary>
		/// Procedure Mail
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("procedureMail")]
		public string ProcedureMail => this.Value<string>("procedureMail");

		///<summary>
		/// Procedure Mail2
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("procedureMail2")]
		public string ProcedureMail2 => this.Value<string>("procedureMail2");

		///<summary>
		/// Procedure Sub Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("procedureSubTitle")]
		public string ProcedureSubTitle => this.Value<string>("procedureSubTitle");

		///<summary>
		/// Procedure Sub Title2
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("procedureSubTitle2")]
		public string ProcedureSubTitle2 => this.Value<string>("procedureSubTitle2");

		///<summary>
		/// Procedure Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("procedureTitle")]
		public string ProcedureTitle => this.Value<string>("procedureTitle");

		///<summary>
		/// Procedure Title2
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("procedureTitle2")]
		public string ProcedureTitle2 => this.Value<string>("procedureTitle2");
	}
}
