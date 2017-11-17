// ****************************************************************************
// <author>Jonas Tampier</author>
// <email>jonas@tampier.de</email>
// <date>06.10.2017</date>
// <project>ItemsFilter</project>
// <license> GNU Lesser General Public License version 3 (LGPLv3) </license>
// ****************************************************************************
using System.Windows;
using System.Windows.Data;
using System.Web.UI;
using System.Diagnostics;
using System;
using System.Text.RegularExpressions;

namespace BolapanControl.ItemsFilter
{
    public class BindingEvaluator
    {
        //private static readonly char[] expressionPartSeparator = new char[] { '.'};
        private static readonly Regex expressionPartSeparator = new Regex("\\.(?![^\\[]*\\])");
        private static readonly char[] indexExprStartChars = new char[] { '[', '('}; 
  
        /// <devdoc>
        ///    <para>Evaluates data binding expressions at runtime. While
        ///       this method is automatically called when you create data bindings in a RAD
        ///       designer, you can also use it declaratively if you want to simplify the casting 
        ///       to a text string to be displayed on a browser. To do so, you must place the
        ///       <%# and %> tags, which are also used in standard ASP.NET data binding, around the data binding expression.</para> 
        ///    <para>This method is particularly useful when data binding against controls that 
        ///       are in a templated list.</para>
        ///    <note type="caution"> 
        ///       Since this method is called at runtime, it can cause performance
        ///       to noticeably slow compared to standard ASP.NET databinding syntax.
        ///       Use this method judiciously.
        ///    </note> 
        /// </devdoc>
        public static object Eval(object container, string expression) { 
            if (expression == null) { 
                throw new ArgumentNullException("expression");
            } 
            expression = expression.Trim();
            if (expression.Length == 0) { 
                throw new ArgumentNullException("expression");
            } 
            if (container == null) {
                return null; 
            }
            string[] expressionParts = expressionPartSeparator.Split(expression);
            return Eval(container, expressionParts);
        } 
  
 
        /// <devdoc> 
        /// </devdoc>
        private static object Eval(object container, string[] expressionParts) {
            Debug.Assert((expressionParts != null) && (expressionParts.Length != 0),
                         "invalid expressionParts parameter"); 
            object prop; 
            int i; 
            for (prop = container, i = 0; (i < expressionParts.Length) && (prop != null); i++) { 
                string expr = expressionParts[i];
                if (expr.StartsWith("Item[") && expr.EndsWith("]"))
                    expr = expr.Substring(4);
                bool indexedExpr = expr.IndexOfAny(indexExprStartChars) >= 0;
                 if (indexedExpr == false) { 
                    prop = DataBinder.GetPropertyValue(prop, expr);
                } 
                else { 
                    prop = DataBinder.GetIndexedPropertyValue(prop, expr);
                } 
            }
             return prop;
        } 

    }
}