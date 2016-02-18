/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/27 10:05:30  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.Component.Tools
{
    public static class ExpressionExtension
    {
        /// <summary>
        /// 构建查询条件And表达式（扩展方法）
        /// </summary>
        /// <typeparam name="T">试题类型</typeparam>
        /// <param name="first">表达式一</param>
        /// <param name="second">表达式二</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {

            DynamicLambda<T> bulider = new DynamicLambda<T>();
            first = bulider.BuildQueryAnd(first, second);
            return first;
        }


        /// <summary>
        /// 构建查询条件Or表达式（扩展方法）
        /// </summary>
        /// <typeparam name="T">试题类型</typeparam>
        /// <param name="first">表达式一</param>
        /// <param name="second">表达式二</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {

            DynamicLambda<T> bulider = new DynamicLambda<T>();
            first = bulider.BuildQueryOr(first, second);
            return first;
        }
    }

    /// <summary>
    /// Dynamic Expression, it is used to combine seperate lambda expressions
    /// </summary>
    /// <typeparam name="T">Type of Entity</typeparam>
    /// <remarks>
    /// For combine the seperate lambda expression, it must change the two lambda have the same parameter, 
    /// otherwise it would throw exception for cannot find the parameter.
    ///</remarks>
    public class DynamicLambda<T>
    {

        private ParameterExpression parameter;

        private ParameterExpressionRewriter paraRewriter;
        public DynamicLambda()
        {
            parameter = Expression.Parameter(typeof(T), "parameter");
            paraRewriter = new ParameterExpressionRewriter(parameter);
        }

        /// <summary>
        /// Combine two lambda epression by AndAlso operation
        /// </summary>
        /// <param name="leftExpression">Left Expression, and it can be null.</param>
        /// <param name="rightExpression">Right Expression, and it can be null.</param>
        /// <returns>Combined Expression. But may be null.</returns>
        /// <remarks>
        /// If leftParameter have value, and rightParameter is null, then return rightParamter.
        /// If rightParameter have value, and leftParameter is null, then return leftParamter.
        /// If both parameters is null, then return null.
        /// </remarks>
        public Expression<Func<T, bool>> BuildQueryAnd(Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            //If leftExpression Is Nothing Then
            //    Throw New ArgumentNullException("leftExpression", "Left Expression can not be null.")
            //End If
            //If rightExpression Is Nothing Then
            //    Throw New ArgumentNullException("rightExpression", "Right Expression can not be null.")
            //End If

            //Check Left Expression whether is null
            if (leftExpression == null)
            {
                if (rightExpression == null)
                {
                    //if the Left Expression and Right Expresstion both is null, then return null.
                    return null;
                }
                else
                {
                    //if only Right Expression not null, then return Right Expression
                    return rightExpression;
                }
            }
            else
            {
                if (rightExpression == null)
                {
                    //if the Right Expression is null and Left Expression is not null, then return Left Expression
                    return leftExpression;
                }
            }


            //Change the Left Expression's Parameter
            leftExpression = paraRewriter.VisitAndConvert(leftExpression, "BuildQueryAnd");
            //Change the Right Expression's Parameter
            rightExpression = paraRewriter.VisitAndConvert(rightExpression, "BuildQueryAnd");

            //Combine two lambda expression by AndAlso operation
            dynamic result = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            //Build the Expression to Lambda, and return it.
            return Expression.Lambda<Func<T, bool>>(result, parameter);

        }

        /// <summary>
        /// Combine two lambda epression by OrElse operation
        /// </summary>
        /// <param name="leftExpression">Left Expression, and it can be null.</param>
        /// <param name="rightExpression">Right Expression, and it can be null.</param>
        /// <returns>Combined Expression. But may be null.</returns>
        /// <remarks>
        /// If leftParameter have value, and rightParameter is null, then return rightParamter.
        /// If rightParameter have value, and leftParameter is null, then return leftParamter.
        /// If both parameters is null, then return null.
        /// </remarks>
        public Expression<Func<T, bool>> BuildQueryOr(Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            //If leftExpression Is Nothing Then
            //    Throw New ArgumentNullException("leftExpression", "Left Expression can not be null.")
            //End If
            //If rightExpression Is Nothing Then
            //    Throw New ArgumentNullException("rightExpression", "Right Expression can not be null.")
            //End If

            if (leftExpression == null)
            {
                if (rightExpression == null)
                {
                    return null;
                }
                else
                {
                    return rightExpression;
                }
            }
            else
            {
                if (rightExpression == null)
                {
                    return leftExpression;
                }
            }

            //Change the Left Expression's Parameter
            leftExpression = paraRewriter.VisitAndConvert(leftExpression, "BuildQueryOr");
            //Change the Right Expression's Parameter
            rightExpression = paraRewriter.VisitAndConvert(rightExpression, "BuildQueryOr");

            //Combine two lambda expression by OrElse operation
            dynamic result = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            //Build the Expression to Lambda, and return it.
            return System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(result, parameter);

        }

    }

    /// <summary>
    /// This Class is inherit from ExpressionVisitor.
    /// It is use to rebuild the Expression Tree and change the Expression's Parameter
    /// </summary>
    /// <remarks></remarks>
    public class ParameterExpressionRewriter : ExpressionVisitor
    {


        private ParameterExpression _param;
        //Public Function Modify(ByVal expr As Expression) As Expression
        //    Return Visit(expr)
        //End Function

        /// <summary>
        /// Create an instance, and set the parameter to replace
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        public ParameterExpressionRewriter(ParameterExpression param)
        {
            _param = param;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node != null && !object.ReferenceEquals(node, _param))
            {
                return _param;
            }
            return node;
        }

    }
}
