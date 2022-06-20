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
	/// <summary>Articles Item</summary>
	[PublishedModel("articlesItem")]
	public partial class ArticlesItem : PublishedContentModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public new const string ModelTypeAlias = "articlesItem";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ArticlesItem, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public ArticlesItem(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Article About Header
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleAboutHeader")]
		public string ArticleAboutHeader => this.Value<string>("articleAboutHeader");

		///<summary>
		/// Article About Text
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleAboutText")]
		public global::System.Web.IHtmlString ArticleAboutText => this.Value<global::System.Web.IHtmlString>("articleAboutText");

		///<summary>
		/// Article Content
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleContent")]
		public global::System.Web.IHtmlString ArticleContent => this.Value<global::System.Web.IHtmlString>("articleContent");

		///<summary>
		/// ArticleImageBW
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleImageBW")]
		public global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent> ArticleImageBW => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>>("articleImageBW");

		///<summary>
		/// Article Image Main
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleImageMain")]
		public global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent> ArticleImageMain => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>>("articleImageMain");

		///<summary>
		/// Article Image Mob1
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleImageMob1")]
		public global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent> ArticleImageMob1 => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>>("articleImageMob1");

		///<summary>
		/// Article Image Mob2
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleImageMob2")]
		public global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent> ArticleImageMob2 => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>>("articleImageMob2");

		///<summary>
		/// Article Image Mob3
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleImageMob3")]
		public global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent> ArticleImageMob3 => this.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Core.Models.PublishedContent.IPublishedContent>>("articleImageMob3");

		///<summary>
		/// Article Link
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleLink")]
		public string ArticleLink => this.Value<string>("articleLink");

		///<summary>
		/// Article Task Header
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleTaskHeader")]
		public string ArticleTaskHeader => this.Value<string>("articleTaskHeader");

		///<summary>
		/// Article Task Text
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleTaskText")]
		public string ArticleTaskText => this.Value<string>("articleTaskText");

		///<summary>
		/// Article Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.12.3")]
		[ImplementPropertyType("articleTitle")]
		public string ArticleTitle => this.Value<string>("articleTitle");
	}
}