using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Komatsu.Core.Seguridad.Common;
using Komatsu.Core.Seguridad.DataContracts;
using Komatsu.Core.Seguridad.Mvc;
using Komatsu.Core.Seguridad.Validation.Globals;
using System.Diagnostics.CodeAnalysis;
using System.Web.Routing;

namespace Komatsu.Core.Seguridad.Mvc.Helper
{
    public static class UtilExtensions
    {
        public static MvcHtmlString DropDownListForParam3<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItemExtended> selectList,
            object htmlAttributes)
        {
            MvcHtmlString html = default(MvcHtmlString);
            string name = ExpressionHelper.GetExpressionText(expression);

            IDictionary<string, object> validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(name);

            if (selectList == null)
                throw new ArgumentException("No SelectList");

            StringBuilder sBuilder = new StringBuilder();
            TagBuilder comboBuilder = new TagBuilder("select");
            comboBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            comboBuilder.MergeAttribute("name", name);
            comboBuilder.MergeAttributes(validationAttributes);
            foreach (var item in selectList)
                sBuilder.Append(ListItemToOptionParam3(item));

            comboBuilder.InnerHtml = sBuilder.ToString();

            html = MvcHtmlString.Create(comboBuilder.ToString(TagRenderMode.Normal));
            return html;
        }

        internal static string ListItemToOptionParam3(SelectListItemExtended item)
        {
            TagBuilder builder = new TagBuilder("option")
            {
                InnerHtml = HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
                builder.Attributes["value"] = item.Value;
            if (item.Selected)
                builder.Attributes["selected"] = "selected";
            builder.Attributes["nivel"] = item.Opcional1;

            return builder.ToString(TagRenderMode.Normal);
        }

        public static MvcHtmlString CustomDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItemExtended> selectList,
            object htmlAttributes)
        {
            MvcHtmlString html = default(MvcHtmlString);
            string name = ExpressionHelper.GetExpressionText(expression);

            IDictionary<string, object> validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(name);

            if (selectList == null)
                throw new ArgumentException("No SelectList");

            StringBuilder sBuilder = new StringBuilder();
            TagBuilder comboBuilder = new TagBuilder("select");
            comboBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            comboBuilder.MergeAttribute("name", name);
            comboBuilder.MergeAttributes(validationAttributes);
            foreach (var item in selectList)
                sBuilder.Append(ListItemToOption(item));

            comboBuilder.InnerHtml = sBuilder.ToString();

            html = MvcHtmlString.Create(comboBuilder.ToString(TagRenderMode.Normal));
            return html;
        }

        internal static string ListItemToOption(SelectListItemExtended item)
        {
            TagBuilder builder = new TagBuilder("option")
            {
                InnerHtml = HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
                builder.Attributes["value"] = item.Value;
            if (item.Selected)
                builder.Attributes["selected"] = "selected";

            builder.Attributes["nivel"] = item.Opcional1;
            builder.Attributes["color"] = item.Opcional2;

            return builder.ToString(TagRenderMode.Normal);
        }

        public static IEnumerable<SelectListItemExtended> ToSelectListItemExtendedParam3<T>(this IEnumerable<T> enumerable,
            Func<T, string> textFunc, Func<T, Int32> valueFunc, Func<T, bool> estado = null, Func<T, bool> selectedFuc = null) where T : class
        {
            var Items = enumerable.Select(
                f => new SelectListItemExtended
                {
                    Text = textFunc(f),
                    Value = valueFunc(f).ToString(),
                    Opcional1 = estado != null ? estado(f).ToString() : null,
                    Selected = selectedFuc != null && selectedFuc(f)
                }).ToList();
            Items.Insert(0, new SelectListItemExtended { Text = Globals.ComboBox_Seleccione, Value = "" });
            return Items;
        }

        public static IEnumerable<SelectListItemExtended> ToSelectListItemExtended<T>(this IEnumerable<T> enumerable,
            Func<T, string> textFunc, Func<T, Int32> valueFunc, Func<T, Int32> nivelFunc = null, Func<T, string> colorFunc = null, Func<T, bool> selectedFuc = null) where T : class
        {
            var Items = enumerable.Select(
                f => new SelectListItemExtended
                {
                    Text = textFunc(f),
                    Value = valueFunc(f).ToString(),
                    Opcional1 = nivelFunc != null ? nivelFunc(f).ToString() : null,
                    Opcional2 = colorFunc != null ? colorFunc(f) : null,
                    Selected = selectedFuc != null && selectedFuc(f)
                }).ToList();
            Items.Insert(0, new SelectListItemExtended { Text = Globals.ComboBox_Seleccione, Value = "" });
            return Items;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString MetaDropDownList<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return MetaDropDownListT(htmlHelper, expression, selectList, null /* optionLabel */, new RouteValueDictionary(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString MetaDropDownListT<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            IDictionary<string, object> validationAttributes = htmlHelper
                .GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata);

            if (htmlAttributes == null)
                htmlAttributes = validationAttributes;
            else
                htmlAttributes = htmlAttributes.Concat(validationAttributes).ToDictionary(k => k.Key, v => v.Value);

            return SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, optionLabel, htmlAttributes);
        }
    }
}

public class SelectListItemExtended : SelectListItem
{
    //usado para el nivel
    public string Opcional1;

    //usado para el color
    public string Opcional2;


}