/******************************************************************************************
	Modification History

	<Modified Date			: 12/04/2005    
	<Modified By			: Anurag Verma  
	<Purpose				: Include Order by clause for todolist type query
    

	<Modified Date			: 13/04/2005    
	<Modified By			: Anurag Verma  
	<Purpose				: Adding function for webgrid control
	
	<Modified Date			: 15/04/2005    
	<Modified By			: Gaurav Tyagi  
	<Purpose				: Adding function for fill drop down
	
	<Modified Date			: 21/04/2005     
	<Modified By			: Pradeep Iyer 
	<Purpose				: Moved TransactionLabel XML function from cmsBase to here
	
	<Modified Date			: 25/04/2005    
	<Modified By			: Pradeep Iyer 
	<Purpose				: Added function for retrieving Look up values
	
	<Modified Date			: 26/04/2005    
	<Modified By			: Nidhi
	<Purpose				: Modified function (GetCommonData) for fetching PolicyTerms and LOBs
	
	<Modified Date			: 25/04/2005    
	<Modified By			: Pradeep Iyer 
	<Purpose				: Added function for binding a Drop down with Lookup values
	
	<Modified Date			: 30/05/2005    
	<Modified By			: Anshuman 
	<Purpose				: Added attribute menu_id in menu xml
	
	<Modified Date			: 31/05/2005    
	<Modified By			: Pradeep
	<Purpose				: Added functions to set values in drop downs
	
	<Modified Date			: 02/06/2005    
	<Modified By			: Anshuman
	<Purpose				: Replaced function GetSecurityXML from clscommon to ClsSecurity
	
	<Modified Date			: 02/06/2005    
	<Modified By			: Shrikant Bhatt
	<Purpose				: This function is to add the transaction log entery from those forms/classes where
							< we are not using the default transaction log

    <Modified Date			: 22/06/2005    
	<Modified By			: Anurag Verma
	<Purpose				: Function has been added which will query in the database for grid dropdown values
	
	<Modified Date			: 28/06/2005    
	<Modified By			: Anshuman
	<Purpose				: GetLookup overloads created
	
	<Modified Date          : 17/10/2005
	<Modified By            : Mohit Gupta
	<Purpose                : Adding member clientId in struct stuTransactionInfo.  
	
	<Modified Date          : 03/11/2005
	<Modified By            : Shrikant Bhatt
	<Purpose                : Adding Functions (GetKeyValueWithIP and GetKeyValue) For Reading keys from web.config and adding IP Address with the key
	<Modified Date          : 27/12/2005
	<Modified By            : Sumit Chhabra
	<Purpose                : Added toUpper() functions to getxmlEncoded function with Pradeep
							  
	<TO MOVE TO LOCAL VSS>
*******************************************************************************************/


using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Cms.DataLayer;
using System.Xml;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Collections;
using Cms.Model;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using Cms.EbixDataTypes;
using System.Globalization;


namespace Cms.BusinessLayer.BlCommon
{
    //Added By Ravindra Gupta (09-14-2006)
    //Implementation Of Expression Parser moved from ClsCoverage

    #region Evalute Expression By Reverse Polish Notation
    #region RPN
    /// <summary>
    /// Summary description for RPNParser.
    /// </summary>
    public class RPNParser
    {
        public RPNParser()
        {
        }
        public object EvaluateExpression(string szExpr, Type varType, bool bFormula, Hashtable htValues)
        {
            ArrayList arrExpr = GetPostFixNotation(szExpr, varType, bFormula);
            return EvaluateRPN(arrExpr, varType, htValues);
        }

        #region RPN_Parser
        /// <summary>
        /// 
        /// </summary>
        /// <param name=szExpr></param>
        /// 		
        public ArrayList GetPostFixNotation(string szExpr, Type varType, bool bFormula)
        {
            Stack stkOp = new Stack();
            ArrayList arrFinalExpr = new ArrayList();
            string szResult = "";

            Tokenizer tknzr = new Tokenizer(szExpr);
            foreach (Token token in tknzr)
            {
                string szToken = token.Value.Trim();
                if (szToken.Length == 0)
                    continue;
                if (!OperatorHelper.IsOperator(szToken))
                {
                    Operand oprnd = OperandHelper.CreateOperand(szToken, varType);
                    oprnd.ExtractAndSetValue(szToken, bFormula);
                    arrFinalExpr.Add(oprnd);

                    szResult += szToken;
                    continue;
                }
                string szOp = szToken;
                if (szOp == "(")
                {
                    stkOp.Push(szOp);
                }
                else if (szOp == ")")
                {
                    string szTop;
                    while ((szTop = (string)stkOp.Pop()) != "(")
                    {
                        IOperator oprtr = OperatorHelper.CreateOperator(szTop);
                        arrFinalExpr.Add(oprtr);

                        szResult += szTop;

                        if (stkOp.Count == 0)
                            throw new RPN_Exception("Unmatched braces!");
                    }
                }
                else
                {
                    if (stkOp.Count == 0 || (string)stkOp.Peek() == "("
                        || OperatorHelper.IsHigherPrecOperator(szOp, (string)stkOp.Peek()))
                    {
                        stkOp.Push(szOp);
                    }
                    else
                    {
                        while (stkOp.Count != 0)
                        {
                            if (OperatorHelper.IsLowerPrecOperator(szOp, (string)stkOp.Peek())
                                || OperatorHelper.IsEqualPrecOperator(szOp, (string)stkOp.Peek()))
                            {
                                string szTop = (string)stkOp.Peek();
                                if (szTop == "(")
                                    break;
                                szTop = (string)stkOp.Pop();

                                IOperator oprtr = OperatorHelper.CreateOperator(szTop);
                                arrFinalExpr.Add(oprtr);

                                szResult += szTop;
                            }
                            else
                                break;
                        }
                        stkOp.Push(szOp);
                    }
                }
            }
            while (stkOp.Count != 0)
            {
                string szTop = (string)stkOp.Pop();
                if (szTop == "(")
                    throw new RPN_Exception("Unmatched braces");

                IOperator oprtr = OperatorHelper.CreateOperator(szTop);
                arrFinalExpr.Add(oprtr);

                szResult += szTop;
            }
            return arrFinalExpr;
        }

        #endregion

        public string Convert2String(ArrayList arrExpr)
        {
            string szResult = "";
            foreach (object obj in arrExpr)
            {
                szResult += obj.ToString();
            }
            return szResult;
        }


        #region RPN_Evaluator

        /// <summary>
        /// 		/// 
        /// </summary>
        /// <param name=szExpr>Expression to be evaluated in RPNotation with
        /// single character variables</param>
        /// <param name=htValues>Values for each of the variables in the expression</param>
        /// 		
        public object EvaluateRPN(ArrayList arrExpr, Type varType, Hashtable htValues)
        {
            // initialize stack (integer stack) for results
            Stack stPad = new Stack();
            // begin loop : scan from left to right till end of RPN expression
            foreach (object var in arrExpr)
            {
                Operand op1 = null;
                Operand op2 = null;
                IOperator oprtr = null;
                // Get token
                // if token is 
                if (var is IOperand)
                {
                    // Operand : push onto top of numerical stack
                    stPad.Push(var);
                }
                else if (var is IOperator)
                {
                    // Operator :	
                    //		Pop top of stack into var 1 - op2 first as top of stack is rhs
                    op2 = (Operand)stPad.Pop();
                    if (htValues != null)
                    {
                        op2.Value = htValues[op2.Name];
                    }
                    //		Pop top of stack into var 2
                    op1 = (Operand)stPad.Pop();
                    if (htValues != null)
                    {
                        op1.Value = htValues[op1.Name];
                    }
                    //		Do operation exp for 'this' operator on var1 and var2
                    oprtr = (IOperator)var;
                    IOperand opRes = oprtr.Eval(op1, op2);
                    //		Push results onto stack
                    stPad.Push(opRes);
                }
            }
            //	end loop
            // stack ends up with single value with final result
            return ((Operand)stPad.Pop()).Value;
        }
        #endregion
    }
    #endregion

    #region UtilClasses

    /// <summary>
    /// The given expression can be parsed as either Arithmetic or Logical or 
    /// Comparison ExpressionTypes.  This is controlled by the enums 
    /// ExpressionType::ET_ARITHMETIC, ExpressionType::ET_COMPARISON and
    /// ExpressionType::ET_LOGICAL.  A combination of these enum types can also be given.
    /// E.g. To parse the expression as all of these, pass 
    /// ExpressionType.ET_ARITHMETIC|ExpressionType.ET_COMPARISON|ExpressionType.ET_LOGICAL 
    /// to the Tokenizer c'tor.
    /// </summary>
    [Flags]
    public enum ExpressionType
    {
        ET_ARITHMETIC = 0x0001,
        ET_COMPARISON = 0x0002,
        ET_LOGICAL = 0x0004
    }
    /// <summary>
    /// Currently not used.
    /// </summary>
    public enum TokenType
    {
        TT_OPERATOR,
        TT_OPERAND
    }
    /// <summary>
    /// Represents each token in the expression
    /// </summary>
    public class Token
    {
        public Token(string szValue)
        {
            m_szValue = szValue;
        }
        public string Value
        {
            get
            {
                return m_szValue;
            }
        }
        string m_szValue;
    }
    /// <summary>
    /// Is the tokenizer which does the actual parsing of the expression.
    /// </summary>
    public class Tokenizer : IEnumerable
    {
        public Tokenizer(string szExpression)
            : this(szExpression, ExpressionType.ET_ARITHMETIC |
                ExpressionType.ET_COMPARISON |
                ExpressionType.ET_LOGICAL)
        {
        }
        public Tokenizer(string szExpression, ExpressionType exType)
        {
            m_szExpression = szExpression;
            m_exType = exType;
            m_RegEx = new Regex(OperatorHelper.GetOperatorsRegEx(m_exType));
            m_strarrTokens = SplitExpression(szExpression);
        }
        public IEnumerator GetEnumerator()
        {
            return new TokenEnumerator(m_strarrTokens);
        }
        public string[] SplitExpression(string szExpression)
        {
            return m_RegEx.Split(szExpression);
        }
        ExpressionType m_exType;
        string m_szExpression;
        string[] m_strarrTokens;
        Regex m_RegEx;
    }

    /// <summary>
    /// Enumerator to enumerate over the tokens.
    /// </summary>
    public class TokenEnumerator : IEnumerator
    {
        Token m_Token;
        int m_nIdx;
        string[] m_strarrTokens;

        public TokenEnumerator(string[] strarrTokens)
        {
            m_strarrTokens = strarrTokens;
            Reset();
        }
        public object Current
        {
            get
            {
                return m_Token;
            }
        }
        public bool MoveNext()
        {
            if (m_nIdx >= m_strarrTokens.Length)
                return false;

            m_Token = new Token(m_strarrTokens[m_nIdx]);
            m_nIdx++;
            return true;
        }
        public void Reset()
        {
            m_nIdx = 0;
        }
    }
    #region Exceptions
    /// <summary>
    /// For the exceptions thrown by this module.
    /// </summary>
    public class RPN_Exception : ApplicationException
    {
        public RPN_Exception()
        {
        }
        public RPN_Exception(string szMessage)
            : base(szMessage)
        {
        }
        public RPN_Exception(string szMessage, Exception innerException)
            : base(szMessage, innerException)
        {
        }
    }
    #endregion
    #endregion

    #region Interfaces
    public interface IOperand { }
    public interface IOperator
    {
        IOperand Eval(IOperand lhs, IOperand rhs);
    }

    public interface IArithmeticOperations
    {
        // to support {"+", "-", "*", "/", "%"} operators
        IOperand Plus(IOperand rhs);
        IOperand Minus(IOperand rhs);
        IOperand Multiply(IOperand rhs);
        IOperand Divide(IOperand rhs);
        IOperand Modulo(IOperand rhs);
    }
    public interface IComparisonOperations
    {
        // to support {"==", "!=","<", "<=", ">", ">="} operators
        IOperand EqualTo(IOperand rhs);
        IOperand NotEqualTo(IOperand rhs);
        IOperand LessThan(IOperand rhs);
        IOperand LessThanOrEqualTo(IOperand rhs);
        IOperand GreaterThan(IOperand rhs);
        IOperand GreaterThanOrEqualTo(IOperand rhs);
    }
    public interface ILogicalOperations
    {
        // to support {"||", "&&" } operators
        IOperand OR(IOperand rhs);
        IOperand AND(IOperand rhs);
    }
    #endregion

    #region Operands
    /// <summary>
    /// Base class for all Operands.  Provides datastorage
    /// </summary>
    public abstract class Operand : IOperand
    {
        public Operand(string szVarName, object varValue)
        {
            m_szVarName = szVarName;
            m_VarValue = varValue;
        }
        public Operand(string szVarName)
        {
            m_szVarName = szVarName;
        }
        public override string ToString()
        {
            return m_szVarName;
        }
        public abstract void ExtractAndSetValue(string szValue, bool bFormula);
        public string Name
        {
            get
            {
                return m_szVarName;
            }
            set
            {
                m_szVarName = value;
            }
        }
        public object Value
        {
            get
            {
                return m_VarValue;
            }
            set
            {
                m_VarValue = value;
            }
        }
        protected string m_szVarName = "";
        protected object m_VarValue = null;
    }


    ///////Added For Double
    #region Double Operand  Class Definition

    public class DoubleOperand : Operand, IArithmeticOperations, IComparisonOperations
    {
        public DoubleOperand(string szVarName, object varValue)
            : base(szVarName, varValue)
        {
        }
        public DoubleOperand(string szVarName)
            : base(szVarName)
        {
        }
        public override string ToString()
        {
            return m_szVarName;
        }
        public override void ExtractAndSetValue(string szValue, bool bFormula)
        {
            m_VarValue = !bFormula ? Convert.ToDouble(szValue) : 0;
        }
        /// IArithmeticOperations methods.  Return of these methods is again a LongOperand
        public IOperand Plus(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .Plus : rhs");
            DoubleOperand oprResult = new DoubleOperand("Result", Type.GetType("System.Double"));
            oprResult.Value = (double)this.Value + (double)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Minus(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .Minus : rhs");
            DoubleOperand oprResult = new DoubleOperand("Result", Type.GetType("System.Double"));
            oprResult.Value = (double)this.Value - (double)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Multiply(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new ArgumentException("Argument invalid in DoubleOperand .Multiply : rhs");
            DoubleOperand oprResult = new DoubleOperand("Result", Type.GetType("System.Double"));
            oprResult.Value = (double)this.Value * (double)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Divide(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .Divide : rhs");
            DoubleOperand oprResult = new DoubleOperand("Result", Type.GetType("System.Double"));
            oprResult.Value = (double)this.Value / (double)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Modulo(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .Modulo : rhs");
            DoubleOperand oprResult = new DoubleOperand("Result", Type.GetType("System.Double"));
            oprResult.Value = (double)this.Value % (double)((Operand)rhs).Value;
            return oprResult;
        }

        /// IComparisonOperators methods.  Return values are always BooleanOperands type
        public IOperand EqualTo(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .== : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = (double)this.Value == (double)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand NotEqualTo(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .!= : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((double)this.Value != (double)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand LessThan(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .< : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((double)this.Value < (double)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand LessThanOrEqualTo(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .<= : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((double)this.Value <= (double)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand GreaterThan(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .> : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((double)this.Value > (double)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand GreaterThanOrEqualTo(IOperand rhs)
        {
            if (!(rhs is DoubleOperand))
                throw new RPN_Exception("Argument invalid in DoubleOperand .>= : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((double)this.Value >= (double)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
    }

    #endregion

    /// <summary>
    /// Operand corresponding to the Long (Int32/Int64) datatypes.
    /// </summary>
    public class LongOperand : Operand, IArithmeticOperations, IComparisonOperations
    {
        public LongOperand(string szVarName, object varValue)
            : base(szVarName, varValue)
        {
        }
        public LongOperand(string szVarName)
            : base(szVarName)
        {
        }
        public override string ToString()
        {
            return m_szVarName;
        }
        public override void ExtractAndSetValue(string szValue, bool bFormula)
        {
            m_VarValue = !bFormula ? Convert.ToInt64(szValue) : 0;
        }
        /// IArithmeticOperations methods.  Return of these methods is again a LongOperand
        public IOperand Plus(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.Plus : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value + (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Minus(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.Minus : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value - (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Multiply(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new ArgumentException("Argument invalid in LongOperand.Multiply : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value * (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Divide(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.Divide : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value / (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Modulo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.Modulo : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value % (long)((Operand)rhs).Value;
            return oprResult;
        }

        /// IComparisonOperators methods.  Return values are always BooleanOperands type
        public IOperand EqualTo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.== : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = (long)this.Value == (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand NotEqualTo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.!= : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value != (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand LessThan(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.< : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value < (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand LessThanOrEqualTo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.<= : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value <= (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand GreaterThan(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.> : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value > (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand GreaterThanOrEqualTo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.>= : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value >= (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
    }
    /// <summary>
    /// Operand corresponding to Boolean Type
    /// </summary>
    public class BoolOperand : Operand, ILogicalOperations
    {
        public BoolOperand(string szVarName, object varValue)
            : base(szVarName, varValue)
        {
        }
        public BoolOperand(string szVarName)
            : base(szVarName)
        {
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
        public override void ExtractAndSetValue(string szValue, bool bFormula)
        {
            m_VarValue = !bFormula ? Convert.ToBoolean(szValue) : false;
        }
        public IOperand AND(IOperand rhs)
        {
            if (!(rhs is BoolOperand))
                throw new RPN_Exception("Argument invalid in BoolOperand.&& : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((bool)this.Value && (bool)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand OR(IOperand rhs)
        {
            if (!(rhs is BoolOperand))
                throw new RPN_Exception("Argument invalid in BoolOperand.|| : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((bool)this.Value || (bool)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
    }

    public class OperandHelper
    {
        /// <summary>
        /// Factory method to create corresponding Operands.
        /// Extended this method to create newer datatypes.
        /// </summary>
        /// <param name="szVarName"></param>
        /// <param name="varType"></param>
        /// <param name="varValue"></param>
        /// <returns></returns>
        static public Operand CreateOperand(string szVarName, Type varType, object varValue)
        {
            Operand oprResult = null;
            switch (varType.ToString())
            {
                case "System.Int32":
                case "System.Int64":
                    oprResult = new LongOperand(szVarName, varValue);
                    return oprResult;
                case "System.Double":
                    oprResult = new DoubleOperand(szVarName, varValue);
                    return oprResult;
                //break;
            }
            throw new RPN_Exception("Unhandled type : " + varType.ToString());
        }
        static public Operand CreateOperand(string szVarName, Type varType)
        {
            return OperandHelper.CreateOperand(szVarName, varType, null);
        }
    }
    #endregion

    #region Operators
    /// <summary>
    /// Base class of all operators.  Provides datastorage
    /// </summary>
    public abstract class Operator : IOperator
    {
        public Operator(char cOperator)
        {
            m_szOperator = new String(cOperator, 1);
        }
        public Operator(string szOperator)
        {
            m_szOperator = szOperator;
        }
        public override string ToString()
        {
            return m_szOperator;
        }
        public abstract IOperand Eval(IOperand lhs, IOperand rhs);
        public string Value
        {
            get
            {
                return m_szOperator;
            }
            set
            {
                m_szOperator = value;
            }
        }
        protected string m_szOperator = "";
    }
    /// <summary>
    /// Arithmetic Operator Class providing evaluation services for "+-/*%" operators.
    /// </summary>
    public class ArithmeticOperator : Operator
    {
        public ArithmeticOperator(char cOperator)
            : base(cOperator)
        {
        }
        public ArithmeticOperator(string szOperator)
            : base(szOperator)
        {
        }
        //bool bBinaryOperator = true;

        public override IOperand Eval(IOperand lhs, IOperand rhs)
        {
            if (!(lhs is IArithmeticOperations))
                throw new RPN_Exception("Argument invalid in ArithmeticOperator.Eval - Invalid Expression : lhs");
            switch (m_szOperator)
            {
                case "+":
                    return ((IArithmeticOperations)lhs).Plus(rhs);
                case "-":
                    return ((IArithmeticOperations)lhs).Minus(rhs);
                case "*":
                    return ((IArithmeticOperations)lhs).Multiply(rhs);
                case "/":
                    return ((IArithmeticOperations)lhs).Divide(rhs);
                case "%":
                    return ((IArithmeticOperations)lhs).Modulo(rhs);
            }
            throw new RPN_Exception("Unsupported Arithmetic operation " + m_szOperator);
        }
    }
    /// <summary>
    /// Comparison Operator Class providing evaluation services for "==", "!=","<", "<=", ">", ">=" operators.
    /// </summary>
    public class ComparisonOperator : Operator
    {
        public ComparisonOperator(char cOperator)
            : base(cOperator)
        {
        }
        public ComparisonOperator(string szOperator)
            : base(szOperator)
        {
        }
        //bool bBinaryOperator = true;

        //{"==", "!=","<", "<=", ">", ">="}
        public override IOperand Eval(IOperand lhs, IOperand rhs)
        {
            if (!(lhs is IComparisonOperations))
                throw new RPN_Exception("Argument invalid in ComparisonOperator.Eval - Invalid Expression : lhs");
            switch (m_szOperator)
            {
                case "==":
                    return ((IComparisonOperations)lhs).EqualTo(rhs);
                case "!=":
                    return ((IComparisonOperations)lhs).NotEqualTo(rhs);
                case "<":
                    return ((IComparisonOperations)lhs).LessThan(rhs);
                case "<=":
                    return ((IComparisonOperations)lhs).LessThanOrEqualTo(rhs);
                case ">":
                    return ((IComparisonOperations)lhs).GreaterThan(rhs);
                case ">=":
                    return ((IComparisonOperations)lhs).GreaterThanOrEqualTo(rhs);
            }
            throw new RPN_Exception("Unsupported Comparison operation " + m_szOperator);
        }
    }

    /// <summary>
    /// Logical Operator Class providing evaluation services for && and || operators.
    /// </summary>
    public class LogicalOperator : Operator
    {
        public LogicalOperator(char cOperator)
            : base(cOperator)
        {
        }
        public LogicalOperator(string szOperator)
            : base(szOperator)
        {
        }
        //bool bBinaryOperator = true;

        //{"&&", "||"}
        public override IOperand Eval(IOperand lhs, IOperand rhs)
        {
            if (!(lhs is ILogicalOperations))
                throw new RPN_Exception("Argument invalid in LogicalOperator.Eval - Invalid Expression : lhs");
            switch (m_szOperator)
            {
                case "&&":
                    return ((ILogicalOperations)lhs).AND(rhs);
                case "||":
                    return ((ILogicalOperations)lhs).OR(rhs);
            }
            throw new RPN_Exception("Unsupported Logical operation " + m_szOperator);
        }
    }

    public class OperatorHelper
    {
        /// <summary>
        /// Factory method to create Operator objects.
        /// </summary>
        /// <param name="szOperator"></param>
        /// <returns></returns>
        static public IOperator CreateOperator(string szOperator)
        {
            IOperator oprtr = null;
            if (OperatorHelper.IsArithmeticOperator(szOperator))
            {
                oprtr = new ArithmeticOperator(szOperator);
                return oprtr;
            }
            if (OperatorHelper.IsComparisonOperator(szOperator))
            {
                oprtr = new ComparisonOperator(szOperator);
                return oprtr;
            }
            if (OperatorHelper.IsLogicalOperator(szOperator))
            {
                oprtr = new LogicalOperator(szOperator);
                return oprtr;
            }
            throw new RPN_Exception("Unhandled Operator : " + szOperator);
        }
        static public IOperator CreateOperator(char cOperator)
        {
            return CreateOperator(new string(cOperator, 1));
        }
        /// Some helper functions.
        public static bool IsOperator(string currentOp)
        {
            int nPos = Array.IndexOf(m_AllOps, currentOp.Trim());
            if (nPos != -1)
                return true;
            else
                return false;
        }
        public static bool IsArithmeticOperator(string currentOp)
        {
            int nPos = Array.IndexOf(m_AllArithmeticOps, currentOp);
            if (nPos != -1)
                return true;
            else
                return false;
        }
        public static bool IsComparisonOperator(string currentOp)
        {
            int nPos = Array.IndexOf(m_AllComparisonOps, currentOp);
            if (nPos != -1)
                return true;
            else
                return false;
        }
        public static bool IsLogicalOperator(string currentOp)
        {
            int nPos = Array.IndexOf(m_AllLogicalOps, currentOp);
            if (nPos != -1)
                return true;
            else
                return false;
        }

        #region Precedence
        /// Precedence is determined by relative indices of the operators defined in 
        /// in m_AllOps variable

        /// <summary>
        /// Summary of IsLowerPrecOperator.
        /// </summary>
        /// <param name=allOps></param>
        /// <param name=currentOp></param>
        /// <param name=prevOp></param>
        /// 		
        public static bool IsLowerPrecOperator(string currentOp, string prevOp)
        {
            int nCurrIdx;
            int nPrevIdx;
            GetCurrentAndPreviousIndex(m_AllOps, currentOp, prevOp, out nCurrIdx, out nPrevIdx);
            if (nCurrIdx < nPrevIdx)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Summary of IsHigherPrecOperator.
        /// </summary>
        /// <param name=allOps></param>
        /// <param name=currentOp></param>
        /// <param name=prevOp></param>
        /// 		
        public static bool IsHigherPrecOperator(string currentOp, string prevOp)
        {
            int nCurrIdx;
            int nPrevIdx;
            GetCurrentAndPreviousIndex(m_AllOps, currentOp, prevOp, out nCurrIdx, out nPrevIdx);
            if (nCurrIdx > nPrevIdx)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Summary of IsEqualPrecOperator.
        /// </summary>
        /// <param name=allOps></param>
        /// <param name=currentOp></param>
        /// <param name=prevOp></param>
        /// 		
        public static bool IsEqualPrecOperator(string currentOp, string prevOp)
        {
            int nCurrIdx;
            int nPrevIdx;
            GetCurrentAndPreviousIndex(m_AllOps, currentOp, prevOp, out nCurrIdx, out nPrevIdx);
            if (nCurrIdx == nPrevIdx)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Summary of GetCurrentAndPreviousIndex.
        /// </summary>
        /// <param name=allOps></param>
        /// <param name=currentOp></param>
        /// <param name=prevOp></param>
        /// <param name=nCurrIdx></param>
        /// <param name=nPrevIdx></param>
        /// 		
        private static void GetCurrentAndPreviousIndex(string[] allOps, string currentOp, string prevOp,
            out int nCurrIdx, out int nPrevIdx)
        {
            nCurrIdx = -1;
            nPrevIdx = -1;
            for (int nIdx = 0; nIdx < allOps.Length; nIdx++)
            {
                if (allOps[nIdx] == currentOp)
                {
                    nCurrIdx = nIdx;
                }
                if (allOps[nIdx] == prevOp)
                {
                    nPrevIdx = nIdx;
                }
                if (nPrevIdx != -1 && nCurrIdx != -1)
                {
                    break;
                }
            }
            if (nCurrIdx == -1)
            {
                throw new RPN_Exception("Unknown operator - " + currentOp);
            }
            if (nPrevIdx == -1)
            {
                throw new RPN_Exception("Unknown operator - " + prevOp);
            }

        }
        #endregion

        #region RegEx
        /// <summary>
        /// This gets the regular expression used to find operators in the input
        /// expression.
        /// </summary>
        /// <param name="exType"></param>
        /// <returns></returns>
        static public string GetOperatorsRegEx(ExpressionType exType)
        {
            StringBuilder strRegex = new StringBuilder();
            if ((exType & ExpressionType.ET_ARITHMETIC).Equals(ExpressionType.ET_ARITHMETIC))
            {
                if (strRegex.Length == 0)
                {
                    strRegex.Append(m_szArthmtcRegEx);
                }
                else
                {
                    strRegex.Append("|" + m_szArthmtcRegEx);
                }
            }
            if ((exType & ExpressionType.ET_COMPARISON).Equals(ExpressionType.ET_COMPARISON))
            {
                if (strRegex.Length == 0)
                {
                    strRegex.Append(m_szCmprsnRegEx);
                }
                else
                {
                    strRegex.Append("|" + m_szCmprsnRegEx);
                }
            }
            if ((exType & ExpressionType.ET_LOGICAL).Equals(ExpressionType.ET_LOGICAL))
            {
                if (strRegex.Length == 0)
                {
                    strRegex.Append(m_szLgclRegEx);
                }
                else
                {
                    strRegex.Append("|" + m_szLgclRegEx);
                }
            }
            if (strRegex.Length == 0)
                throw new RPN_Exception("Invalid combination of ExpressionType value");
            return "(" + strRegex.ToString() + ")";
        }
        /// <summary>
        /// Expression to pattern match various operators
        /// </summary>
        static string m_szArthmtcRegEx = @"[+\-*/%()]{1}";
        static string m_szCmprsnRegEx = @"[=<>!]{1,2}";
        static string m_szLgclRegEx = @"[&|]{2}";
        #endregion

        public static string[] AllOperators
        {
            get
            {
                return m_AllOps;
            }
        }

        /// <summary>
        /// All Operators supported by this module currently.
        /// Modify here to add more operators IN ACCORDANCE WITH their precedence.
        /// Additionally add into individual variables to support some helper methods above.
        /// </summary>
        static string[] m_AllOps = { "||", "&&", "|", "^", "&", "==", "!=",
									   "<", "<=", ">", ">=", "+", "-", "*", "/", "%", "(", ")" };
        static string[] m_AllArithmeticOps = { "+", "-", "*", "/", "%" };
        static string[] m_AllComparisonOps = { "==", "!=", "<", "<=", ">", ">=" };
        static string[] m_AllLogicalOps = { "&&", "||" };
    }

    #endregion
    #endregion


    //Added By Ravindra (08-03-2006)
    public sealed class HomeProductType
    {
        private HomeProductType() { }

        public const string HO2_REPLACEMENT = "HO-2^REPLACE";
        public const string HO3_REPLACEMENT = "HO-3^REPLACE";
        public const string HO5_REPLACEMENT = "HO-5^REPLACE";
        public const string HO2_REPAIR = "HO-2^REPAIR";
        public const string HO3_REPAIR = "HO-3^REPAIR";
        public const string HO4_TENANT = "HO-4^TENANT";
        public const string HO6_UNIT = "HO-6^UNIT";
        public const string HO4_DELUXE = "HO-4^DELUXE";
        public const string HO5_PREMIER = "HO-5^PREMIER";
        public const string HO6_DELUXE = "HO-6^DELUXE";
        public const string HO3_PREMIER = "HO-3^PREMIER";

    }
    //Added By Ravindra End Here

    //Added By Swastika : 2nd May'07
    public sealed class DepositType
    {
        private DepositType() { }

        public const string DEPOSIT_CUSTOMER = "CUST";
        public const string DEPOSIT_AGENCY = "AGN";
        public const string DEPOSIT_VENDOR = "VEN";

    }
    //Added By Manoj Rathore : 16 Nov 2007
    public sealed class BillType
    {
        private BillType() { }

        public const int InsuredBill = 11150;
        public const int AgencyBill = 8459;
        public const int AgencyBill1stTerm = 11191;
    }

    #region Class Premium
    public class Premium
    {
        private int intRiskID;
        private string strRiskType;
        private double dblGrossPremium;
        private double dblNetPremium;
        private double dblOtherFees;
        private double dblMCCAFees;
        private bool boolIsDeleted;
        private bool boolIsAdded;
        //Added By Ravindra(08-02-2007)
        //New member variable and respective properties are added to store Inforce Premium
        private double dblInforcePremium;

        private double dblInforceFees;

        public Premium()
        {
            intRiskID = 0;
            dblGrossPremium = 0;
            dblNetPremium = 0;
            dblOtherFees = 0;
            dblMCCAFees = 0;
            strRiskType = "";
            boolIsDeleted = false;
            dblInforcePremium = 0;
            boolIsAdded = true;
        }

        public double InforceFees
        {
            get
            {
                return dblInforceFees;
            }
            set
            {
                dblInforceFees = value;
            }
        }
        public double InforcePremium
        {
            get
            {
                return dblInforcePremium;
            }
            set
            {
                dblInforcePremium = value;
            }
        }
        public bool IsRiskDeleted
        {
            get
            {
                return boolIsDeleted;
            }
            set
            {
                boolIsDeleted = value;
            }
        }

        public bool IsRiskAdded
        {
            get
            {
                return boolIsAdded;
            }
            set
            {
                boolIsAdded = value;
            }
        }

        public int RiskID
        {
            get
            {
                return intRiskID;
            }
            set
            {
                intRiskID = value;
            }
        }
        public double GrossPremium
        {
            get
            {
                return dblGrossPremium;
            }
            set
            {
                dblGrossPremium = value;
            }
        }
        public double NetPremium
        {
            get
            {
                return dblNetPremium;
            }
            set
            {
                dblNetPremium = value;
            }
        }
        public double MCCAFees
        {
            get
            {
                return dblMCCAFees;
            }
            set
            {
                dblMCCAFees = value;
            }
        }
        public double OtherFees
        {
            get
            {
                return dblOtherFees;
            }
            set
            {
                dblOtherFees = value;
            }
        }
        public string RiskType
        {
            get
            {
                return strRiskType;
            }
            set
            {
                strRiskType = value;
            }
        }
    }
    #endregion

    /// <summary>
    /// Common business layer for CMS project
    /// </summary>
    public class ClsCommon : IDisposable
    {
        private DataTable ldRecord;		// Holds the Menu data records returned from database
        private XmlDocument doc;		// Creates XML elements to return XML string.
        private Cms.DataLayer.DataWrapper objDataAccess;
        private int intLoggedInUserId;

        public static string CarrierSystemID = "";//Added by Charles on 19-May-10 for Itrack 51

        public static string CommonCarrierCode = ""; //Added by Agniswar on 09 Dec 2011

        /// <summary>
        /// Culture specific currency symbol
        /// </summary>
        /// Added by Charles on 24-May-2010 for Multilingual Implementation
        public struct enumCurrencySymbol
        {
            public const String US = "$";
            public const String BRAZIL = "R$";
        }
        public struct enumCulture
        {
            public const String US = "en-US";
            public const String BR = "pt-BR";
        }
        #region PopulateDivDeptPC
        /// <summary>
        /// Populates Div/Dept/PC Drop Down 
        /// </summary>
        /// <returns></returns>
        /// Added by Charles on 24-May-2010 for Itrack 92
        public static DataTable PopulateDivDeptPC()
        {
            string strStoredProc = "Proc_PopulateDivDeptPC";
            DataSet dsDivDeptPC = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                dsDivDeptPC = objDataWrapper.ExecuteDataSet(strStoredProc);
                if (dsDivDeptPC.Tables[0].Rows.Count > 0)
                {
                    return dsDivDeptPC.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
        #endregion

        //Added By Pravesh K Chandel 6 April 2010
        /// <summary>
        /// Use to Populate ebix Page Model object
        /// Return the Populated model object
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="objModel"></param>
        /// <returns></returns>
        public static Model.Support.ClsModelBaseClass PopulateEbixPageModel(DataSet ds, Model.Support.ClsModelBaseClass objModel)
        {
            return PopulateEbixPageModel(ds.Tables[0], objModel);
        }
        public static Model.Support.ClsModelBaseClass PopulateEbixPageModel(DataTable dt, Model.Support.ClsModelBaseClass objModel)
        {
            return PopulateEbixPageModel(dt.Rows[0], objModel);
        }
        public static Model.Support.ClsModelBaseClass PopulateEbixPageModel(DataRow dr, Model.Support.ClsModelBaseClass objModel)
        {
            ICollection ColumnsNameKey = objModel.htPropertyCollection.Keys;
            //Looping through all the Property Collections
            foreach (object key in ColumnsNameKey)
            {
                String ColumnType = String.Empty;
                String ColumnName = key.ToString();
                ColumnType = objModel.htPropertyCollection[key].GetType().Name.ToString();
                if (dr.Table.Columns.IndexOf(ColumnName) != -1)
                {
                    switch (ColumnType)
                    {
                        case "EbixInt32":
                            if (dr[ColumnName].ToString() != "")
                                ((Cms.EbixDataTypes.EbixInt32)objModel.htPropertyCollection[key]).CurrentValue = int.Parse(dr[ColumnName].ToString());
                            else
                                ((Cms.EbixDataTypes.EbixInt32)objModel.htPropertyCollection[key]).CurrentValue = -1;
                            break;
                        case "EbixDouble":
                            if (dr[ColumnName].ToString() != "")
                                ((Cms.EbixDataTypes.EbixDouble)objModel.htPropertyCollection[key]).CurrentValue = double.Parse(dr[ColumnName].ToString());
                            //((Cms.EbixDataTypes.EbixDouble)objModel.htPropertyCollection[key]).CurrentValue = Convert.ToDouble(String.Format("{0:0.0000}",ds.Tables[0].Rows[0][ColumnName].ToString()));
                            else
                                ((Cms.EbixDataTypes.EbixDouble)objModel.htPropertyCollection[key]).CurrentValue = -1;
                            break;
                        case "EbixDecimal":
                            if (dr[ColumnName].ToString() != "")
                                ((Cms.EbixDataTypes.EbixDecimal)objModel.htPropertyCollection[key]).CurrentValue = Decimal.Parse(dr[ColumnName].ToString());
                            //((Cms.EbixDataTypes.EbixDouble)objModel.htPropertyCollection[key]).CurrentValue = Convert.ToDouble(String.Format("{0:0.0000}",ds.Tables[0].Rows[0][ColumnName].ToString()));
                            else
                                ((Cms.EbixDataTypes.EbixDecimal)objModel.htPropertyCollection[key]).CurrentValue = -1;
                            break;
                            
                        case "EbixString":
                            ((Cms.EbixDataTypes.EbixString)objModel.htPropertyCollection[key]).CurrentValue = dr[ColumnName].ToString();
                            break;
                        case "EbixDateTime":
                            if (dr[ColumnName].ToString() != "")
                                ((Cms.EbixDataTypes.EbixDateTime)objModel.htPropertyCollection[key]).CurrentValue = Convert.ToDateTime(dr[ColumnName].ToString());
                            break;
                        case "EbixBoolean":
                            if (dr[ColumnName].ToString() != "")
                                ((Cms.EbixDataTypes.EbixBoolean)objModel.htPropertyCollection[key]).CurrentValue = Convert.ToBoolean(dr[ColumnName].ToString());
                            break;
                        default:
                            ((Cms.EbixDataTypes.EbixString)objModel.htPropertyCollection[key]).CurrentValue = dr[ColumnName].ToString();
                            break;

                    }
                }
            }
            return objModel;
        }
        //Added by Charles on 15-Mar-10 for Multilingual Implementation
        private static string strBlLangCulture;
        private static int intBlLangID;
        /// <summary>
        /// Gets/Sets Language-Culture
        /// </summary>
        public static string BL_LANG_CULTURE
        {
            set
            {
                strBlLangCulture = value;
                Cms.Model.Support.ClsModelBaseClass.MODEL_LANG_CULTURE = value; // Added by Charles on 20-Apr-2010 for Multilingual Implementation of Transaction Log
            }
            get
            {
                if (strBlLangCulture == null || strBlLangCulture == "")
                {
                    strBlLangCulture = "en-US";
                }
                if (IsEODProcess)
                    return strBlLangCulture;
                if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["languageCode"] != null)
                {
                    strBlLangCulture = System.Web.HttpContext.Current.Session["languageCode"].ToString();
                    return System.Web.HttpContext.Current.Session["languageCode"].ToString();
                }
                else
                {
                    //System.Web.HttpContext.Current.Response.Redirect("/cms/cmsweb/aspx/login.aspx", true);
                    return "en-US";
                }
            }
        }

        /// <summary>
        /// Gets/Sets Language ID
        /// </summary>
        public static int BL_LANG_ID
        {
            set
            {
                intBlLangID = value;

                Cms.Model.Support.ClsModelBaseClass.MODEL_LANG_ID = value;
                Cms.Model.ClsBaseModel.OLD_MODEL_LANG_ID = value;
            }
            get
            {
                if (intBlLangID <= 0)
                {
                    intBlLangID = 1;
                }
                if (IsEODProcess)
                    return intBlLangID;
                if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["languageId"] != null)
                {
                    intBlLangID=int.Parse(System.Web.HttpContext.Current.Session["languageId"].ToString());
                    return int.Parse(System.Web.HttpContext.Current.Session["languageId"].ToString());
                }
                else
                {
                    //System.Web.HttpContext.Current.Response.Redirect("/cms/cmsweb/aspx/login.aspx", true);
                    return 1;
                }
            }
        }
        //Added till here

        public string strActivateDeactivateProcedure;
        // By default every class will require Transcation Log Entry
        public bool TransactionLogRequired = true;

        public static string ServiceAuthenticationMsg = "You are not authorised for this request.";

        public static string ServiceAuthenticationToken
        {
            get
            {
                return mAuthenticationKey;
            }
            set
            {
                mAuthenticationKey = value;
            }

        }

        public static string ServiceCapitalAuthenticationToken
        {
            get
            {
                return mCapitalAuthenticationKey;
            }
            set
            {
                mCapitalAuthenticationKey = value;
            }

        }

        //Added by Charles on 18-Dec-09 for Itrack 6681
        /// <summary>
        /// Formats Number with commas and decimal places
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static string FormatNumber(string strNumber)
        {
            string ReturnValue = "";
            string CurrencyFormat = "";

            //Added by Charles on 24-May-2010 for Multilingual Implementation
            /*switch (BL_LANG_ID)
            {
                case 2: CurrencyFormat = enumCurrencySymbol.BRAZIL;
                    break;
                case 1:
                default:
                    CurrencyFormat = enumCurrencySymbol.US;
                    break;
            }//Added till here
            */
            CurrencyFormat = GetPolicyCurrencySymbol("");
            if (strNumber.Trim() == "")
                ReturnValue = "";
            else if (strNumber.Trim().ToUpper() == "N/A")
                ReturnValue = strNumber;
            else
            {
                ReturnValue = Convert2Double(strNumber.Trim(), "0,0");
                if (ReturnValue.IndexOf("-") >= 0)
                    ReturnValue = "-" + CurrencyFormat + ReturnValue.Replace("-", "");
                else
                    ReturnValue = CurrencyFormat + ReturnValue;
            }

            if (ReturnValue == (CurrencyFormat + "00"))
                ReturnValue = CurrencyFormat + "0";
            return (ReturnValue);
        }
        //Added by Charles on 18-Dec-09 for Itrack 6681
        public static string Convert2Double(string strNumber, string strFormat)
        {
            try
            {
                Double dblNunber = Convert.ToDouble(strNumber);
                return (dblNunber.ToString(strFormat));
            }
            catch
            {
                return ("");
            }
        }
        public static string GetPolicyCurrencySymbol(string PolicyCurrency)
        {
            string Symbol = enumCurrencySymbol.US;
            if (PolicyCurrency == "" || PolicyCurrency == "1")
                Symbol = enumCurrencySymbol.US; //For US
            // PolicyCurrency = System.Web.HttpContext.Current.Session["policyCurrency"].ToString();
            if (PolicyCurrency == "2") // BRL Real
                Symbol = enumCurrencySymbol.BRAZIL;
            return Symbol;
        }

        public static void setPolicyCurrencySymbol(string Symbol)
        {
            System.Web.HttpContext.Current.Session.Add("PolicyCurrencySymbol", Symbol);
        }
        public static string GetPolicyCurrencySymbol()
        {
            if (System.Web.HttpContext.Current.Session["PolicyCurrencySymbol"] != null)
                return System.Web.HttpContext.Current.Session["PolicyCurrencySymbol"].ToString();
            else
                return enumCurrencySymbol.US;
        }
        //Added till here

        // XML Document to load CustomizedMessages.xml

        #region Const decclaration for policy status
        public const string POLICY_STATUS_NORMAL = "NORMAL";
        public const string POLICY_STATUS_RENEWED = "RENEWED";
        public const string POLICY_STATUS_INACTIVE = "INACTIVE";

        public const string POLICY_STATUS_UNDER_ENDORSEMENT = "UENDRS";
        public const string POLICY_STATUS_UNDER_RENEW = "URENEW";
        public const string POLICY_STATUS_UNDER_CORRECTIVE_USER = "UCORUSER";
        public const string POLICY_STATUS_UNDER_ISSUE = "UISSUE";
        public const string POLICY_STATUS_SUSPENDED = "SUSPENDED";

        public const string POLICY_STATUS_UNDER_REWRITE = "UREWRITE";
        public const string POLICY_STATUS_SUSPENSE_REWRITE = "REWRTSUSP";
        public const string POLICY_STATUS_APPLICATION = "APPLICATION";//Added by Charles on 18-Mar-2010 for Policy Page Implementation
        public const string POLICY_STATUS_REJECT = "REJECT";
        public const string POLICY_STATUS_QAPP = "QAPP";
        public const string POLICY_STATUS_CORRECTIVE_USER = "UCORUSER";
        public const string POLICY_STATUS_ENDORSEMENT_SUSPENSE = "ESUSPENSE";

        #endregion

        #region Printing Option Document Types
        public const string DOCUMENT_TYPE_AUTO_ID_CARD = "AUTO_ID_CARD";
        public const int PRINT_OPTIONS_AUTO_CYCL_NO_OF_COPIES = 1;
        #endregion
        #region Constant for MVR Information
        //public const string MVR_VIOLATION_YEAR_NUM = "2";
        #endregion

        #region Constant for Prior Loss Information
        public const string Insured = "IN";
        public const string SecondInsured = "SI";
        public const string InsuredDriverSameAsInsured = "SA";
        public const string InsuredDriver = "ID";
        public const string Claimant = "CL";
        public const string ClaimantDriver = "CD";
        #endregion

        #region static constructor to set static property ConnStr, This is used to set and get connection string

        private static string commonConnectionString;
        private static string commonConnectionStringQuote;
        private static string CommonDBConnectionString;
        private static string CommonGridConnectionString;
        public static string ConnStrQuote
        {
            set
            {
                commonConnectionStringQuote = value;
            }
            get
            {
                return commonConnectionStringQuote;
            }
        }
        public static string CommonDBConnStr
        {
            set
            {
                CommonDBConnectionString = value;
            }
            get
            {
                return CommonDBConnectionString;
            }
        }
        public static string ConnGridStr
        {
            set
            {
                CommonGridConnectionString = value;
            }
            get
            {
                //return CommonGridConnectionString;
                if (System.Web.HttpContext.Current.Session["ConnGridStr"] != null)
                    return DecryptString(System.Web.HttpContext.Current.Session["ConnGridStr"].ToString());
                else
                {
                    System.Web.HttpContext.Current.Response.Redirect("/cms/cmsweb/aspx/login.aspx", true);
                    return "";
                }
            }
        }
        public static string ConnStr
        {
            set
            {
                commonConnectionString = value;
            }
            get
            {
                if (IsEODProcess)
                    return commonConnectionString;
                else if (System.Web.HttpContext.Current.Session["ConnStr"] != null)
                    return DecryptString(System.Web.HttpContext.Current.Session["ConnStr"].ToString());
                else
                {
                    System.Web.HttpContext.Current.Response.Redirect("/cms/cmsweb/aspx/login.aspx", true);
                    return "";
                }
            }
        }
        //For EOD 
        /***********************************************/
        private static bool mIsEODProcess;
        private static string mWebAppUNCPath;
        private static string mConfigFileName;
        private static string mUploadPath;
        private static string mImpersonationUserId;
        private static string mImpersonationPassword;
        private static string mImpersonationDomain;
        private static string mCmsWebUrl;
        private static string mExtAgnUrl;
        private static string mUploadUrl;
        private static int mEODUserID;
        private static string mEODSystemID;
        private static string mIIXUserName;
        private static string mIIXPassword;
        private static string mIIXAccountNumber;
        private static string mIIXUrl;
        private static string mVehicleClassXml;
        private static bool mIsScoreToFetched;
        private static string mAuthenticationKey;
        private static string mCapitalAuthenticationKey;


        //ADDED BY PRAVEEN KUMAR(9-02-2009)
        private static string mWebServiceURL;

        //Raghav(03-25-2009): To store URL for attachment renderer
        private static string mContentViewerURL;

        public static string ContentViewerURL
        {
            get
            {
                return mContentViewerURL;
            }
        }

        //End of code for redirection to viewer

        public static string CreateContentViewerURL(string FilePath, string FileType)
        {
            string EncryptedURL = mContentViewerURL + "?PN=" + EncryptString(FilePath) + "&EXT=" + EncryptString(FileType);

            return EncryptedURL;
        }
        //set Numberformat info according to supplied policy Currency
        public static NumberFormatInfo GetNumberFormat(int PolicyCurrency)
        {
            NumberFormatInfo Nfi;
            if (PolicyCurrency == 2)
                Nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                Nfi = new CultureInfo(enumCulture.US, true).NumberFormat;

            return Nfi;

        }

        public static string WebServiceURL
        {
            get
            {
                return mWebServiceURL;
            }
        }
        //END PRAVEEN KUMAR

        public static string VehicleClassXml
        {
            get
            {
                return mVehicleClassXml;
            }
            set
            {
                mVehicleClassXml = value;
            }
        }

        public static string IIXUserName
        {
            get
            {
                return mIIXUserName;
            }
            set
            {
                mIIXUserName = value;
            }
        }

        public static string IIXPassword
        {
            get
            {
                return mIIXPassword;
            }
            set
            {
                mIIXPassword = value;
            }
        }

        public static string IIXAccountNumber
        {
            get
            {
                return mIIXAccountNumber;
            }
            set
            {
                mIIXAccountNumber = value;
            }
        }

        public static string IIXUrl
        {
            get
            {
                return mIIXUrl;
            }
            set
            {
                mIIXUrl = value;
            }
        }

        public static string UploadURL
        {
            get
            {
                return mUploadUrl;
            }
            set
            {
                mUploadUrl = value;
            }
        }

        public static string EODSystemID
        {
            get
            {
                return mEODSystemID;
            }
            set
            {
                mEODSystemID = value;
            }
        }

        public static int EODUserID
        {
            get
            {
                return mEODUserID;
            }
            set
            {
                mEODUserID = value;
            }
        }


        public static string CmsWebUrl
        {
            get
            {
                return mCmsWebUrl;
            }
            set
            {
                mCmsWebUrl = value;
            }
        }

        public static string ExtAgnUrl
        {
            get
            {
                return mExtAgnUrl;
            }
            set
            {
                mExtAgnUrl = value;
            }
        }


        public static string ImpersonationUserId
        {
            set
            {
                mImpersonationUserId = value;
            }
            get
            {
                return mImpersonationUserId;
            }
        }

        public static string ImpersonationPassword
        {
            set
            {
                mImpersonationPassword = value;
            }
            get
            {
                return mImpersonationPassword;
            }
        }
        public static string ImpersonationDomain
        {
            set
            {
                mImpersonationDomain = value;
            }
            get
            {
                return mImpersonationDomain;
            }
        }

        public static string UploadPath
        {
            set
            {
                mUploadPath = value;
            }
            get
            {
                return mUploadPath;
            }
        }
        public static bool IsEODProcess
        {
            set
            {
                mIsEODProcess = value;
            }
            get
            {
                return mIsEODProcess;
            }
        }

        public static string ConfigFileName
        {
            set
            {
                mConfigFileName = value;
            }
            get
            {
                return mConfigFileName;
            }
        }

        public static string WebAppUNCPath
        {
            set
            {
                mWebAppUNCPath = value;
            }
            get
            {
                return mWebAppUNCPath;
            }
        }
        public static bool IsScoreToFetched
        {
            set
            {
                mIsScoreToFetched = value;
            }
            get
            {
                return mIsScoreToFetched;
            }
        }



        /***********************************************/
        static ClsCommon()
        {
            //ConnStr = System.Configuration.ConfigurationSettings.AppSettings.Get("DB_CON_STRING");
            CommonDBConnStr =  System.Configuration.ConfigurationManager.AppSettings.Get("DB_CON_STRING");
            ConnStrQuote = System.Configuration.ConfigurationManager.AppSettings.Get("DB_CON_STRING_QUOTE");
            mIsEODProcess = false;
            mIsScoreToFetched = true;

            //Set Credential for Impersonation for Disk IO
            if (System.Configuration.ConfigurationManager.AppSettings.Get("CmsWebUrl") != null)
            {
                mImpersonationUserId = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString().Trim();
                mImpersonationPassword = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString().Trim();
                mImpersonationDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString().Trim();
                mCmsWebUrl = System.Configuration.ConfigurationManager.AppSettings.Get("CmsWebUrl");
                mAuthenticationKey = System.Configuration.ConfigurationManager.AppSettings.Get("AuthenticationToken");
                mCapitalAuthenticationKey = System.Configuration.ConfigurationManager.AppSettings.Get("CapitalAuthenticationToken");
                //ADDED BY PRAVEEN KUMAR(5-02-2009)
                mWebServiceURL = mCmsWebUrl + System.Configuration.ConfigurationManager.AppSettings.Get("WebServiceURL");
                //END PRAVEEN KUMAR

                //Read from web.config
                mContentViewerURL = System.Configuration.ConfigurationManager.AppSettings.Get("ContentViewerURL");

                mWebAppUNCPath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath);
            }
        }
        #endregion

        #region public properties
        public int LoggedInUserId
        {
            set
            {
                intLoggedInUserId = value;
            }
            get
            {
                return intLoggedInUserId;
            }
        }

        #endregion

        public ClsCommon()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public ClsCommon(string strActivateDeactivateProcedure)
        {
            this.strActivateDeactivateProcedure = strActivateDeactivateProcedure;
        }



        #region functions to use values in CustomizedMessages.xml
        public XmlDocument lObjCustomizedMessage = new XmlDocument();
        private static XmlDocument docMessages;
        private const string GENERALMESSAGESSCREENID = "G";

        public static void SetCustomizedXml(string lang_culture)
        {
            try
            {
                if (docMessages == null) 
                {
                    docMessages = new XmlDocument();
                    docMessages.Load(GetKeyValueWithIP("XMLMessagesPath"));
                }
                /*
                if (lang_culture == "en-US" || lang_culture == "" || lang_culture == null)
                {
                    docMessages.Load(GetKeyValueWithIP("XMLMessagesPath"));
                }
                else
                {
                    docMessages.Load(GetKeyValueWithIP("XMLMessagesPath").Replace(".xml", "." + lang_culture + ".xml"));
                }
                */
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }
        private static XmlDocument getdocMessages()
        {
            try
            {
                if (docMessages != null)
                {
                    return docMessages;
                }
                else
                {
                    //docMessages = null; 
                    docMessages = new XmlDocument();
                    docMessages.Load(GetKeyValueWithIP("XMLMessagesPath"));
                    return docMessages;
                }
            }
            catch (Exception exc)
            {
                throw (new Exception("Error while loading the Common CustomizedMessages XML.", exc));
            }
        }
        public string GetCustomizedMessage(string nodePath)
        {
            try
            {
                return lObjCustomizedMessage.SelectSingleNode(nodePath).InnerText.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string FetchGeneralMessage(string messageID, string punctuationMark)
        {
            try
            {
                docMessages = getdocMessages();
                XmlNode root = docMessages.FirstChild;
                bool foundMessageID = false;
                XmlNode nodScreen = root.SelectSingleNode("Culture[@Code='" + BL_LANG_CULTURE + "']/screen[@screenid='" + GENERALMESSAGESSCREENID + "']");
                if (nodScreen != null)
                {
                    XmlNode nodMessage = nodScreen.SelectSingleNode("message[@messageid='" + messageID + "']");
                    if (nodMessage != null)
                    {
                        foundMessageID = true;
                        return nodMessage.InnerText + punctuationMark;
                    }
                }
                if (!foundMessageID)
                {
                    throw (new Exception("Message node against the message id : " + messageID + " not found in the CustomizedMessages XML."));
                }

                return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally { }

        }
        #endregion

        #region Check WaterCraft Exists
        public static bool CheckWatercraft(int customerID, int appID, int appVersionID)
        {
            // if watercraft is attached to HO then call GenerateQuote() 
            string strStoredProcName = "Proc_Chk_Watercraft_Exists", strIS_EXISTS = "";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProcName);
                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
                objDataWrapper.ClearParameteres();
                strIS_EXISTS = ds.Tables[0].Rows[0]["IS_EXISTS"].ToString();
                if (strIS_EXISTS.ToUpper().Trim() == "Y")
                    return true;
                else
                    return false;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        public static bool CheckWatercraft_Pol(int customerID, int polID, int polVersionID)
        {
            string strStoredProcName = "Proc_Chk_Watercraft_Exists_Pol", strIS_EXISTS = "";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", polID);
                objDataWrapper.AddParameter("@POL_VERSION_ID", polVersionID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProcName);
                objDataWrapper.ClearParameteres();
                strIS_EXISTS = ds.Tables[0].Rows[0]["IS_EXISTS"].ToString();
                if (strIS_EXISTS.ToUpper().Trim() == "Y")
                    return true;
                else
                    return false;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        #endregion Endregion

        #region FUNCTION USED IN MAINTAINING TRANSACTION LOG
        /// <summary>
        /// This function is used to retrieve changes stored in database for transaction log based on transaction key
        /// </summary>
        /// <param name="connStr">Connection string</param>
        /// <param name="tranID">Transaction Id</param>
        /// <returns>DataSet containing the change XML</returns>
        public DataSet GetTransactionChanges(string connStr, string tranID)
        {
            SqlParameter[] sparam = new SqlParameter[1];
            try
            {
                sparam[0] = new SqlParameter("@Tran_ID", SqlDbType.Int);
                sparam[0].Value = int.Parse(tranID);

                return DataWrapper.ExecuteDataset(connStr, "Proc_GetTransactionDetails", sparam);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }

        public int WriteTransactionLog(string connStr, int tranID, int userID, string desc, string XMLString)
        {
            string strStoredProc = "Proc_InsertTransactionLog";
            DateTime RecordDate = DateTime.Now;
            int returnResult;

            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(connStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@Tran_Id", tranID);
                objDataWrapper.AddParameter("@log_time", RecordDate);
                objDataWrapper.AddParameter("@user_id", userID);
                objDataWrapper.AddParameter("@description", desc);
                objDataWrapper.AddParameter("@changes_xml", XMLString);

                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                if (returnResult == 1)
                {
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    return -1;
                }
                else
                {
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                    return returnResult;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        /// <summary>
        /// This function is to add the transaction log entery from those forms/classes where
        /// we are not using the default transaction log
        /// </summary>
        /// <param name="objTransaction">Transaction log Modal Object</param>
        /// <returns></returns>
        public int TransactionLogEntry(Cms.Model.Maintenance.ClsTransactionInfo objTransaction)
        {
            string strStoredProc = "Proc_InsertTransactionLog";
            DateTime RecordDate = DateTime.Now;
            int returnResult;

            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(commonConnectionString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@TRANS_TYPE_ID", objTransaction.TRANS_TYPE_ID);
                objDataWrapper.AddParameter("@RECORD_DATE_TIME", RecordDate);
                objDataWrapper.AddParameter("@CLIENT_ID", objTransaction.CLIENT_ID);
                objDataWrapper.AddParameter("@TRANS_DESC", objTransaction.TRANS_DESC);
                objDataWrapper.AddParameter("@CHANGE_XML", objTransaction.CHANGE_XML);
                objDataWrapper.AddParameter("@POLICY_ID", objTransaction.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID", objTransaction.POLICY_VER_TRACKING_ID);
                objDataWrapper.AddParameter("@RECORDED_BY", objTransaction.RECORDED_BY);
                objDataWrapper.AddParameter("@RECORDED_BY_NAME", objTransaction.RECORDED_BY_NAME);
                objDataWrapper.AddParameter("@ENTITY_ID", objTransaction.ENTITY_ID);
                objDataWrapper.AddParameter("@ENTITY_TYPE", objTransaction.ENTITY_TYPE);
                objDataWrapper.AddParameter("@IS_COMPLETED", objTransaction.IS_COMPLETED);
                objDataWrapper.AddParameter("@APP_ID", objTransaction.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", objTransaction.APP_VERSION_ID);
                objDataWrapper.AddParameter("@QUOTE_ID", objTransaction.QUOTE_ID);
                objDataWrapper.AddParameter("@QUOTE_VERSION_ID", objTransaction.QUOTE_VERSION_ID);

                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                if (returnResult == 2)
                {
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    return -1;
                }
                else
                {
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                    return returnResult;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }
        #endregion

        #region functions accept XML string to build sql, checks concureency, updates and overwrite

        /// <summary>
        /// Used to Generate Queries from the Input XML for checking Concurrency. 
        /// If found then selecting the current data from the Database table 
        /// else updating the Database table with the data sent through XML
        /// </summary>
        /// <param name="CompareXML"></param>
        /// <returns>Flag "yes" if no concurrency is founf otherwise returns XML string having new and old values</returns>
        public static string CompareData(string CompareXML)
        {
            string CurrentXML;
            StringBuilder loColumns = new StringBuilder("");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(CompareXML);
            int iresult = -1;

            //Select Query to check for the Concurrency
            loColumns.Append("select count(*) from ");

            //Getting the Table Name
            foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
            {
                if (xmlAt.LocalName.IndexOf("TB_") != -1)
                {
                    loColumns.Append(xmlAt.Value + " where 1=1");
                }
            }

            //Getting the Field Names
            foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
            {
                if (xmlAt.LocalName.IndexOf("OLD_") != -1 || xmlAt.LocalName.IndexOf("PK_") != -1)
                {
                    loColumns.Append(" and " + xmlAt.LocalName + "='" + xmlAt.Value + "' ");
                }
            }

            loColumns.Replace("OLD_", "");
            loColumns.Replace("PK_", "");
            object obj = SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, loColumns.ToString());
            iresult = (int)obj;

            // There is no change in the XML data and DB data.
            if (iresult == 1)
            {
                //Update Command, since no change is found
                loColumns.Append(" update ");
                xmlDoc.LoadXml(CompareXML);

                //Getting the Table Name
                foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
                {
                    if (xmlAt.LocalName.IndexOf("TB_") != -1)
                    {
                        loColumns.Append(xmlAt.Value + " set ");
                    }
                }

                //Getting the Field Names
                foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
                {
                    if (xmlAt.LocalName.IndexOf("OLD_") < 0 && xmlAt.LocalName.IndexOf("PK_") < 0 && xmlAt.LocalName.IndexOf("TB_") < 0)
                    {
                        loColumns.Append(xmlAt.LocalName + "='" + xmlAt.Value + "',");
                    }
                }
                loColumns.Remove(loColumns.Length - 1, 1);

                //Getting the Primary Key for Where Clause
                foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
                {
                    if (xmlAt.LocalName.IndexOf("PK_") > -1)
                    {
                        loColumns.Append(" where " + xmlAt.LocalName + " = '" + xmlAt.Value + "'");
                    }
                }
                loColumns.Replace("OLD_", "");
                loColumns.Replace("PK_", "");
                SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, loColumns.ToString());
                return "yes";
            }
            // some change is found in XML data and	DB data, so return changed value and old values
            else
            {
                //Select Query to fetch Current Data when Concurrency is found
                loColumns.Append(" select ");

                //Getting the Field Names
                foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
                {
                    if (xmlAt.LocalName.IndexOf("OLD_") != -1 || xmlAt.LocalName.IndexOf("PK_") > 0)
                    {
                        loColumns.Append("" + xmlAt.LocalName + ", ");
                    }
                }

                loColumns.Remove(loColumns.Length - 2, 1);
                loColumns.Append(" from ");

                //Getting the Table Name
                foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
                {
                    if (xmlAt.LocalName.IndexOf("TB_") != -1)
                    {
                        loColumns.Append(xmlAt.Value);
                    }
                }

                //Getting the Primary Key for Where Clause
                foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
                {
                    if (xmlAt.LocalName.IndexOf("PK_") > -1)
                    {
                        loColumns.Append(" where " + xmlAt.LocalName + " = '" + xmlAt.Value + "'");
                    }
                }

                loColumns.Replace("OLD_", "");
                loColumns.Replace("PK_", "");
                DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, loColumns.ToString());
                CurrentXML = ds.GetXml();
            }
            string finalxml = ConCurrency(CompareXML, CurrentXML);
            return finalxml;
        }

        /// <summary>
        /// Used to Generate Query from the Input XML,
        /// This function is used when Concurrency is found and User wants to overwrite changes
        /// </summary>
        /// <param name="ConXML"></param>
        /// <returns>If overwritten, returns "yes" otherwise return "no"</returns>
        public static string OverWriteData(string ConXML)
        {
            int iresult = -1;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ConXML);
            StringBuilder loColumns = new StringBuilder("");

            //Update Command to Overwrite Data after Concurrency is found
            loColumns.Append(" update ");

            //Getting the Table Name 
            foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
            {
                if (xmlAt.LocalName.IndexOf("TB_") != -1)
                {
                    loColumns.Append(xmlAt.Value + " set ");
                }
            }

            //Getting the Field Names
            foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
            {
                if (xmlAt.LocalName.IndexOf("OLD_") < 0 && xmlAt.LocalName.IndexOf("PK_") < 0 && xmlAt.LocalName.IndexOf("TB_") < 0)
                {
                    loColumns.Append(xmlAt.LocalName + "='" + xmlAt.Value + "',");
                }
            }
            loColumns.Remove(loColumns.Length - 1, 1);

            //Getting the Primary Key for Where Clause
            foreach (XmlAttribute xmlAt in xmlDoc.DocumentElement.ChildNodes[0].Attributes)
            {
                if (xmlAt.LocalName.IndexOf("PK_") > -1)
                {
                    loColumns.Append(" where " + xmlAt.LocalName + " = '" + xmlAt.Value + "'");
                }
            }

            loColumns.Replace("OLD_", "");
            loColumns.Replace("PK_", "");
            iresult = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, loColumns.ToString());

            if (iresult >= 1)
                return "yes";
            else
                return "no";
        }

        /// <summary>
        /// Used to Return Current Data and old data in one XML after Concurrency is found
        /// </summary>
        /// <param name="CompareXML"></param>
        /// <param name="CurrentXML"></param>
        /// <returns>XML string having attribute values of current and old data</returns>
        public static string ConCurrency(string CompareXML, string CurrentXML)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(CurrentXML); //The Current Data (in XML format)in the Database table

            XmlDocument xmlDoc1 = new XmlDocument();
            xmlDoc1.LoadXml(CompareXML); //The Original Data (in XML format) got from ASPx page

            for (int i = 0; i < xmlDoc.GetElementsByTagName("Table1").Item(0).ChildNodes.Count; i++)
            {
                string attName = "DB_" + xmlDoc.GetElementsByTagName("Table1").Item(0).ChildNodes[i].LocalName;
                string attValue = xmlDoc.GetElementsByTagName("Table1").Item(0).ChildNodes[i].InnerText;
                XmlAttribute xt = xmlDoc1.CreateAttribute(attName);
                xt.Value = attValue;
                XmlAttributeCollection xtcol = xmlDoc1.GetElementsByTagName("RowData").Item(0).Attributes;
                xtcol.Append(xt);
            }
            string finalXML = xmlDoc1.OuterXml;
            return finalXML;
        }
        #endregion

        #region Login and authentication functiom
        public string[] validateLogin(string proc_name, string sysid, string username, string password)
        {
            DataSet ds = new DataSet();


            SqlParameter[] arrsparam = new SqlParameter[4];

            arrsparam[0] = new SqlParameter("@sysid", SqlDbType.VarChar, 50);
            arrsparam[0].Value = sysid;

            arrsparam[1] = new SqlParameter("@username", SqlDbType.VarChar, 50);
            arrsparam[1].Value = username;

            arrsparam[2] = new SqlParameter("@password", SqlDbType.VarChar, 50);
            arrsparam[2].Value = password;

            arrsparam[3] = new SqlParameter("@result", SqlDbType.VarChar, 50);
            arrsparam[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "sp_validateLogin", arrsparam);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string[] resultArray = new string[ds.Tables[0].Rows.Count];

                    resultArray[0] = ds.Tables[0].Rows[0][0].ToString();


                    resultArray[1] = ds.Tables[0].Rows[0][1].ToString();


                    resultArray[2] = ds.Tables[0].Rows[0][2].ToString();


                    resultArray[3] = ds.Tables[0].Rows[0][3].ToString();


                    resultArray[4] = ds.Tables[0].Rows[0][4].ToString();
                    return resultArray;
                }
            }
            else
            {
                string[] resultArray = new string[ds.Tables[0].Rows.Count];

                resultArray[0] = arrsparam[3].Value.ToString();
                return resultArray;
            }
            return new string[0];

        }
        #endregion

        #region public utility functions for all calss inherited from ClsCommon
        /// <summary>
        /// return the inner text of any first specified node in any xml doc
        /// </summary>
        /// <returns>Value(Inner text) of Node, if node not exists then empty string</returns>
        public static string GetNodeValue(XmlDocument xmlDoc, string strNodeName)
        {
            try
            {
                return xmlDoc.SelectNodes(strNodeName).Item(0).InnerText;
            }
            catch
            {
                return "";
            }
        }

        public static DataTable GetUserList()
        {
            try
            {
                DataSet objUserData = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_SelectUser");
                return objUserData.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Done for Itrack Issue 6658 on 3 Nov 09
        public static DataSet GetToDoUserList(int userID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@USERID", userID);
                DataSet objUserData = objDataWrapper.ExecuteDataSet("Proc_GetToDoUserList");
                return objUserData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }
        #region Function to get Assigned Drtivers according to Age :
        /// <summary>
        /// 
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static IList GetLookupForDriverAssignedVehicles(int age)
        {
            return GetDriverAssignedAs(age);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static IList GetDriverAssignedAs(int age)
        {
            IList alLookup = new ArrayList();

            IDataReader reader = DataWrapper.ExecuteReader(ConnStr, CommandType.Text, "Exec Proc_GetDriverAssignedAs " + age.ToString() + "," + BL_LANG_ID.ToString());// BL_LANG_ID added by Charles on 15-Mar-10 for Multilingual Implementation

            while (reader.Read())
            {
                ClsLookupInfo objLookup = new ClsLookupInfo();

                objLookup.LookupID = Convert.ToInt32(reader["LOOKUP_UNIQUE_ID"]);
                objLookup.LookupDesc = Convert.ToString(reader["LOOKUP_VALUE_DESC"]);
                objLookup.LookupCode = Convert.ToString(reader["LOOKUP_VALUE_CODE"]);

                alLookup.Add(objLookup);
            }

            reader.Close();

            return alLookup;
        }

        /// <summary>
        /// AJAX CALL
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static DataSet GetLookupForDriverAssignedAs(int age)
        {
            DataSet ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.Text, "Proc_GetDriverAssignedAs " + age.ToString() + "," + BL_LANG_ID.ToString());// BL_LANG_ID added by Charles on 15-Mar-10 for Multilingual Implementation
            return ds;
        }
        #endregion




        /// <summary>
        /// This function is made for diary setup in maintenance which is fetching only wolverine and active users
        /// </summary>
        /// <returns>Datatable consisting of user id,user first name and user last name</returns>
        public static DataTable GetUserListForDiarySetup()
        {
            try
            {
                DataSet objUserData = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_SelectUserForDiarySetup");
                return objUserData.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ReplaceInvalidCharecter(string aString)
        {
            if (aString != null)
                return aString.Replace("'", "''");
            else
                return aString;
        }
        public static bool IsInteger(string theValue)
        {
            try
            {
                Convert.ToInt32(theValue);
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region static common utility functions

        /// <summary>
        /// Date Format for Database
        /// </summary>
        /// <param name="date">Date as String in Current Culture Format</param>
        /// <returns>Date as DateTime object in Current Culture Format</returns>
        public static DateTime ConvertToDate(string date)
        {
            try
            {
                // Changed by Charles for Multilingual Implementation
                //return Convert.ToDateTime(date, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                // Added by Charles on 17-May-2010 for Multilingual Implementation
                if (date == null)
                {
                    return Convert.ToDateTime(DateTime.MinValue, System.Globalization.DateTimeFormatInfo.CurrentInfo);
                }

                if ((date.IndexOf(" ") != -1) && (date.IndexOf("-", date.IndexOf(" ")) != -1))
                {
                    date = date.Substring(0, date.IndexOf(" "));
                }
                return Convert.ToDateTime(date, System.Globalization.DateTimeFormatInfo.CurrentInfo);
                //Added till here
            }
            catch (Exception ex)
            {
                //return DateTime.MinValue;
                throw (ex);
            }
        }

        /// <summary>
        /// Converts string date object in mm/dd/yyyy format to dd/mm/yyyy format, if current culture date format is dd/mm/yyyy
        /// </summary>
        /// <param name="date">string date object in mm/dd/yyyy format</param>
        /// <returns>date object in dd/mm/yyyy format</returns>
        /// Added by Charles for Multilingual Implementation
        public static DateTime ConvertDBDateToCulture(string date)
        {
            return Convert.ToDateTime(Convert.ToDateTime(date, System.Globalization.DateTimeFormatInfo.InvariantInfo).ToShortDateString()
                , System.Globalization.DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Reads the RESX file passed as an argument and returns a Transaction XML string
        /// </summary>
        /// <param name="resourceFileName">The name of the RESX resource file to read.</param>
        /// <returns></returns>
        public static string MapTransactionLabel(string resourceFile)
        {
            const string ROOTNODENAME = "LabelFieldMapping";
            const string MAPPINGNODENAME = "Map";

            //string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + resourceFile);
            string filePath;
            if (mIsEODProcess == true)
            {
                string strTemp = resourceFile.Replace("/", @"\");
                //Added by Charles on 15-Mar-2010 for Multilingual Implementation
                if (BL_LANG_CULTURE.ToUpper() == "EN-US")
                {
                    filePath = mWebAppUNCPath + @"\" + strTemp;
                }
                else
                {
                    filePath = mWebAppUNCPath + @"\" + strTemp.Replace(".resx", "." + BL_LANG_CULTURE + ".resx");
                }//Added till here
                filePath = System.IO.Path.GetFullPath(filePath);

                //Added by Charles on 15-Mar-2010 for Multilingual Implementation
                if (!System.IO.File.Exists(filePath))
                {
                    filePath = System.IO.Path.GetFullPath(mWebAppUNCPath + @"\" + strTemp);
                }//Added till here
            }
            else
            {
                //Added by Charles on 15-Mar-2010 for Multilingual Implementation
                if (BL_LANG_CULTURE.ToUpper() == "EN-US")
                {
                    filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + resourceFile);
                }
                else
                {
                    filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + resourceFile.Replace(".resx", "." + BL_LANG_CULTURE + ".resx"));
                }

                if (!System.IO.File.Exists(filePath))
                {
                    filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + resourceFile);
                }
                //Added till here
            }
            //if (!System.IO.File.Exists(filePath))
            //{
            //    throw (new Exception("\n Resource File: " + filePath + " \n does not exist"));
            //}

            //System.Web.HttpContext.Current.Request.ApplicationPath + "/" + 

            // hold the XML string having map information for field and label
            XmlDocument xmlTransaction = new XmlDocument();
            XmlDocument xmlResourceFile = new XmlDocument();

            xmlResourceFile.Load(filePath);
            XmlNodeList nodeList = xmlResourceFile.SelectNodes("root/data");

            // create root node of transactLabel
            XmlElement rootNode = xmlTransaction.CreateElement(ROOTNODENAME);


            /*
            // Create a ResXResourceReader for the file items.resx.
            ResXResourceReader rsxr = new ResXResourceReader(filePath);

            // Create an IDictionaryEnumerator to iterate through the resources.
            IDictionaryEnumerator id = rsxr.GetEnumerator();       

            // Iterate through the key value pairs and create XML 
            foreach (DictionaryEntry d in rsxr) 
            {
                //Eliminate the key values starting with $
                if ( d.Key.ToString().StartsWith("$") == false )
                {
                    string strFieldName = d.Key.ToString().Substring(3);
                    string strLabel = d.Value.ToString();

                    XmlElement labelNode = xmlTransaction.CreateElement(MAPPINGNODENAME);
                    labelNode.SetAttribute("label",strLabel);
                    labelNode.SetAttribute("field",strFieldName);
                    rootNode.AppendChild(labelNode);
                }

            }
            */

            foreach (XmlNode dataNode in nodeList)
            {
                string dataValue = "";

                if (dataNode.Attributes["name"] != null)
                {
                    dataValue = dataNode.Attributes["name"].Value.Trim();
                }

                //Eliminate the key values starting with $
                if (dataValue.StartsWith("$") == false)
                {
                    string strFieldName = dataValue.Substring(3);
                    string strLabel = "";

                    XmlNode valueNode = dataNode.SelectSingleNode("value");

                    if (valueNode != null)
                    {
                        strLabel = valueNode.InnerText.Trim();

                        XmlElement labelNode = xmlTransaction.CreateElement(MAPPINGNODENAME);
                        labelNode.SetAttribute("label", strLabel);
                        labelNode.SetAttribute("field", strFieldName);
                        rootNode.AppendChild(labelNode);
                    }
                }
            }

            /*
            //Close the reader.
            rsxr.Close();
            */

            if (rootNode.HasChildNodes)
            {
                xmlTransaction.AppendChild(rootNode);

                return xmlTransaction.OuterXml;
            }

            //If no entries found in the resource file
            return "";


        }

        //Added by Agniswar

        public static string GetTransactionLabelFromXml(string xmlFilePath)
        {
            const string ROOTNODENAME = "LabelFieldMapping";
            const string MAPPINGNODENAME = "Map";

            // hold the XML string having map information for field and label
            XmlDocument xmlTransaction = new XmlDocument();
            XmlDocument xmlResourceFile = new XmlDocument();

            xmlResourceFile.Load(xmlFilePath);
            XmlNodeList nodeList = xmlResourceFile.SelectNodes("Root/PageElement");

            // create root node of transactLabel
            XmlElement rootNode = xmlTransaction.CreateElement(ROOTNODENAME);


          

            foreach (XmlNode dataNode in nodeList)
            {
                string dataValue = "";

                if (dataNode.Attributes["name"] != null)
                {
                    dataValue = dataNode.Attributes["name"].Value.Trim();
                }

                //Eliminate the key values starting with $
                if (dataValue.StartsWith("$") == false)
                {
                    string strFieldName = dataValue.Substring(3);
                    string strLabel = "";

                    XmlNode valueNode = dataNode.SelectSingleNode("Culture[@Code='" + BL_LANG_CULTURE + "']/Caption");

                    if (valueNode != null)
                    {
                        strLabel = valueNode.InnerText.Trim();

                        XmlElement labelNode = xmlTransaction.CreateElement(MAPPINGNODENAME);
                        labelNode.SetAttribute("label", strLabel);
                        labelNode.SetAttribute("field", strFieldName);
                        rootNode.AppendChild(labelNode);
                    }
                }
            }

            /*
            //Close the reader.
            rsxr.Close();
            */

            if (rootNode.HasChildNodes)
            {
                xmlTransaction.AppendChild(rootNode);

                return xmlTransaction.OuterXml;
            }

            //If no entries found in the resource file
            return "";


        }

        //Till here

        public static int GetLookupUniqueId(string LookupName, string LookupValueCode)
        {
            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                objWrapper.AddParameter("@LOOKUP_NAME", LookupName);
                objWrapper.AddParameter("@LOOKUP_VALUE_CODE", LookupValueCode);
                SqlParameter param = (SqlParameter)objWrapper.AddParameter("@RET_VAL", 0, SqlDbType.Int, ParameterDirection.ReturnValue);

                objWrapper.ExecuteDataSet("Proc_GetLookupID");

                return Convert.ToInt32(param.Value);
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }

        //Fetch Trailer Deductible Id - Asfa Praveen - 02/July/2007
        public static int GetTrailerDedId(DateTime EffectiveDate, decimal TralierDed, string StateName, string BoatTypeCode)
        {
            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                objWrapper.AddParameter("@EFFECTIVE_DATE", EffectiveDate);
                objWrapper.AddParameter("@TRALIER_DED", TralierDed);
                objWrapper.AddParameter("@STATE_NAME", StateName);
                objWrapper.AddParameter("@BOATTYPE_CODE", BoatTypeCode);

                DataSet ds = objWrapper.ExecuteDataSet("Proc_FetchTrailerDedID");

                return Convert.ToInt32(ds.Tables[0].Rows[0]["LIMIT_DEDUC_ID"]);
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }

        /// <summary>
        /// Lookup Method to return DataTable with extra parameters for sorting and order by 
        /// </summary>
        /// <param name="lookupCode"></param>
        /// <param name="lookupParam"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>

        public static DataTable GetLookupTable(string lookupCode, string lookupParam, string orderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];//3
            sqlParams[0] = new SqlParameter("@LookupCode", lookupCode);
            if (lookupParam == "-1")
                sqlParams[1] = new SqlParameter("@LookupParam", null);
            else
                sqlParams[1] = new SqlParameter("@LookupParam", lookupParam);
            sqlParams[2] = new SqlParameter("@OrderBy", orderBy);
            sqlParams[3] = new SqlParameter("@LANG_ID", BL_LANG_ID);//Added by Charles on 15-Mar-10 for Multilingual Implementation

            DataSet ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_GetLookupValues", sqlParams);
            ds.Tables[0].Columns["LOOKUP_UNIQUE_ID"].ColumnName = "LookupID";
            ds.Tables[0].Columns["LOOKUP_VALUE_DESC"].ColumnName = "LookupDesc";
            ds.Tables[0].Columns["LOOKUP_VALUE_CODE"].ColumnName = "LookupCode";
            return ds.Tables[0];
        }


        /// <summary>
        /// Used to populate Lookup Dropdown lists
        /// </summary>
        /// <param name="lookupCode"></param>
        /// <returns></returns>
        public static DataTable GetLookupTable(string lookupCode)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];//1
            sqlParams[0] = new SqlParameter("@LookupCode", lookupCode);
            sqlParams[1] = new SqlParameter("@LANG_ID", BL_LANG_ID);//Added by Charles on 15-Mar-10 for Multilingual Implementation

            DataSet ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_GetLookupValues", sqlParams);

            ds.Tables[0].Columns["LOOKUP_UNIQUE_ID"].ColumnName = "LookupID";
            ds.Tables[0].Columns["LOOKUP_VALUE_DESC"].ColumnName = "LookupDesc";
            ds.Tables[0].Columns["LOOKUP_VALUE_CODE"].ColumnName = "LookupCode";
            return ds.Tables[0];

        }

        /// <summary>
        /// Used to populate Lookup Dropdown lists
        /// </summary>
        /// <param name="lookupCode"></param>
        /// <returns></returns>
        public static IList GetLookup(string lookupCode)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];//1
            sqlParams[0] = new SqlParameter("@LookupCode", lookupCode);
            sqlParams[1] = new SqlParameter("@LANG_ID", BL_LANG_ID);//Added by Charles on 15-Mar-10 for Multilingual Implementation
            //Itrack # 5087
            return GetMotorLookup(sqlParams);
            //return GetLookup(sqlParams);	

        }

        /// <summary>
        /// Used to populate Lookup Dropdown lists
        /// </summary>
        /// <param name="lookupCode"></param>
        /// <returns></returns>
        public static IList GetLookup(string lookupCode, string lookupParam)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];//2
            sqlParams[0] = new SqlParameter("@LookupCode", lookupCode);
            sqlParams[1] = new SqlParameter("@LookupParam", lookupParam);
            sqlParams[2] = new SqlParameter("@LANG_ID", BL_LANG_ID);//Added by Charles on 15-Mar-10 for Multilingual Implementation
            return GetLookup(sqlParams);
        }

        /// <summary>
        /// Used to populate Lookup Dropdown lists 
        /// retrieve records in order specified in Lookup_Sys_Def field of mnt_lookup_values.   
        /// </summary>
        /// <param name="lookupCode"></param>
        /// <returns></returns>
        public static IList GetLookup(string lookupCode, string lookupParam, string orderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];//3
            sqlParams[0] = new SqlParameter("@LookupCode", lookupCode);
            if (lookupParam == "-1")
                sqlParams[1] = new SqlParameter("@LookupParam", null);
            else
                sqlParams[1] = new SqlParameter("@LookupParam", lookupParam);
            sqlParams[2] = new SqlParameter("@OrderBy", orderBy);
            sqlParams[3] = new SqlParameter("@LANG_ID", BL_LANG_ID);//Added by Charles on 15-Mar-10 for Multilingual Implementation
            return GetLookup(sqlParams);
        }
        //ADDED BY PRAVEEN 
        //TO GET THE VALUE OF BOAT TYPE  in order
        public static IList GetLookupForwaterCraftType(string lookupCode, string lookupParam, string orderBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];//3
            sqlParams[0] = new SqlParameter("@LookupCode", lookupCode);
            if (lookupParam == "-1")
                sqlParams[1] = new SqlParameter("@LookupParam", null);
            else
                sqlParams[1] = new SqlParameter("@LookupParam", lookupParam);
            sqlParams[2] = new SqlParameter("@OrderBy", orderBy);
            sqlParams[3] = new SqlParameter("@LANG_ID", BL_LANG_ID);//Added by Charles on 15-Mar-10 for Multilingual Implementation
            return GetLookupBoatType(sqlParams);
        }

        //ADDED BY PRAVEEN TO GET THE VALUE OF BOT TYPE IN ORDER WISE
        private static IList GetLookupBoatType(SqlParameter[] sqlParams)
        {
            IList alLookup = new ArrayList();

            IDataReader reader = DataWrapper.ExecuteReader(ConnStr, CommandType.StoredProcedure, "Proc_GetLookupValuesForBoatType", sqlParams);

            while (reader.Read())
            {
                ClsLookupInfo objLookup = new ClsLookupInfo();

                objLookup.LookupID = Convert.ToInt32(reader["LOOKUP_UNIQUE_ID"]);
                objLookup.LookupDesc = Convert.ToString(reader["LOOKUP_VALUE_DESC"]);
                objLookup.LookupCode = Convert.ToString(reader["LOOKUP_VALUE_CODE"]);

                alLookup.Add(objLookup);
            }

            reader.Close();

            return alLookup;
        }


        private static IList GetLookup(SqlParameter[] sqlParams)
        {
            IList alLookup = new ArrayList();

            IDataReader reader = DataWrapper.ExecuteReader(ConnStr, CommandType.StoredProcedure, "Proc_GetLookupValues", sqlParams);

            while (reader.Read())
            {
                ClsLookupInfo objLookup = new ClsLookupInfo();

                objLookup.LookupID = Convert.ToInt32(reader["LOOKUP_UNIQUE_ID"]);
                objLookup.LookupDesc = Convert.ToString(reader["LOOKUP_VALUE_DESC"]);
                objLookup.LookupCode = Convert.ToString(reader["LOOKUP_VALUE_CODE"]);

                alLookup.Add(objLookup);
            }

            reader.Close();

            return alLookup;
        }

        //Itrack # 5087
        private static IList GetMotorLookup(SqlParameter[] sqlParams)
        {
            IList alLookup = new ArrayList();

            IDataReader reader = DataWrapper.ExecuteReader(ConnStr, CommandType.StoredProcedure, "Proc_GetLookupValues", sqlParams);

            while (reader.Read())
            {
                ClsLookupInfo objLookup = new ClsLookupInfo();

                objLookup.LookupID = Convert.ToInt32(reader["LOOKUP_UNIQUE_ID"]);
                objLookup.LookupDesc = Convert.ToString(reader["LOOKUP_VALUE_DESC"]);
                objLookup.LookupCode = Convert.ToString(reader["LOOKUP_VALUE_CODE"]);
                objLookup.LookupDesc1 = Convert.ToString(reader["LOOKUP_VALUE_DESC1"]);
                if (objLookup.LookupID == 11927 || objLookup.LookupID == 11928 || objLookup.LookupID == 11929 || objLookup.LookupID == 11930)
                {
                    alLookup.Remove(objLookup);
                }
                else
                {
                    alLookup.Add(objLookup);
                }
            }

            reader.Close();

            return alLookup;
        }

        /// <summary>
        /// Binds a dropdown list to look up values from the database
        /// </summary>
        /// <param name="ddlLookup"></param>
        /// <param name="lookupCode"></param>
        public static void BindLookupDDL(ref System.Web.UI.WebControls.DropDownList ddlLookup, string lookupCode)
        {
            ddlLookup.DataTextField = "LookupDesc";
            ddlLookup.DataValueField = "LookupID";

            ddlLookup.DataSource = GetLookup(lookupCode);
            ddlLookup.DataBind();

        }

        /// <summary>
        /// Binds a list box to look up values from the database
        /// </summary>
        /// <param name="lstLookup"></param>
        /// <param name="lookupCode"></param>
        public static void BindLookupLST(ref System.Web.UI.WebControls.ListBox lstLookup, string lookupCode)
        {
            lstLookup.DataTextField = "LookupDesc";
            lstLookup.DataValueField = "LookupID";
            lstLookup.DataSource = GetLookup(lookupCode);
            lstLookup.DataBind();
        }

        public static void BindLookupDDL(System.Web.UI.WebControls.DropDownList ddlLookup, string lookupCode, string firstItem)
        {
            ddlLookup.DataTextField = "LookupDesc";
            ddlLookup.DataValueField = "LookupID";

            ddlLookup.DataSource = GetLookup(lookupCode);
            ddlLookup.DataBind();

            if (firstItem != null)
            {
                ddlLookup.Items.Insert(0, new ListItem(firstItem, firstItem));
            }

        }

        public static DataTable GetCoverages(System.Web.UI.WebControls.DropDownList ddlDropDown, string lookupCode, string firstItem, string covCode, int stateID, int lobID)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[1] = new SqlParameter("@COV_REF_CODE", covCode);
            sqlParams[2] = new SqlParameter("@STATE_ID", stateID);
            sqlParams[3] = new SqlParameter("@LOB_ID", lobID);

            DataSet ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_GetCoverages", sqlParams);

            return ds.Tables[0];
        }

        public static void BindCoveragesDDL(System.Web.UI.WebControls.DropDownList ddlDropDown, string lookupCode, string firstItem, string covCode, int stateID, int lobID)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[1] = new SqlParameter("@COV_REF_CODE", covCode);
            sqlParams[2] = new SqlParameter("@STATE_ID", stateID);
            sqlParams[3] = new SqlParameter("@LOB_ID", lobID);

            DataSet ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_GetCoverages", sqlParams);

            ddlDropDown.DataTextField = "COV_DES";
            ddlDropDown.DataValueField = "COV_ID";

            ddlDropDown.DataSource = ds.Tables[0];
            ddlDropDown.DataBind();

            if (firstItem != null)
            {
                ddlDropDown.Items.Insert(0, new ListItem(firstItem, firstItem));
            }
        }


        /// <summary>
        /// Finds and selects the passed in value in a DDL 
        /// </summary>
        /// <param name="ddlLookup"></param>
        /// <param name="lookupCode"></param>
        /// <param name="firstItem"></param>
        public static void SelectValueinDDL(DropDownList cmbDropdown, object objValue)
        {
            if (objValue == System.DBNull.Value) return;

            ListItem listItem;

            listItem = cmbDropdown.Items.FindByValue(Convert.ToString(objValue));
            cmbDropdown.SelectedIndex = cmbDropdown.Items.IndexOf(listItem);
        }

        //public static void SelectValueinDDL(global::Coolite.Ext.Web.ComboBox CCcmbDropdown, object objValue)
        //{
        //    if (objValue == System.DBNull.Value) return;

        //    Coolite.Ext.Web.ListItem ClistItem = new Coolite.Ext.Web.ListItem();
        //    ClistItem.Value =Convert.ToString(objValue);
        //    CCcmbDropdown.SelectedIndex = CCcmbDropdown.Items.IndexOf(ClistItem);

        //}
        /// <summary>
        /// Finds and selects the passed in text in a DDL 
        /// </summary>
        /// <param name="ddlLookup"></param>
        /// <param name="lookupCode"></param>
        /// <param name="firstItem"></param>
        public static void SelectValueinDDL(DropDownList cmbDropdown, string strValue)
        {
            ListItem listItem;

            listItem = cmbDropdown.Items.FindByValue(strValue);
            cmbDropdown.SelectedIndex = cmbDropdown.Items.IndexOf(listItem);
        }

        public static void SelectValueInDDL(DropDownList cmbDropdown, object objText)
        {
            if (objText == System.DBNull.Value) return;

            ListItem listItem;

            listItem = cmbDropdown.Items.FindByValue(Convert.ToString(objText));
            cmbDropdown.SelectedIndex = cmbDropdown.Items.IndexOf(listItem);
        }

        public static void SelectTextinDDL(DropDownList cmbDropdown, object objText)
        {
            if (objText == System.DBNull.Value) return;

            ListItem listItem;

            listItem = cmbDropdown.Items.FindByText(Convert.ToString(objText));
            cmbDropdown.SelectedIndex = cmbDropdown.Items.IndexOf(listItem);
        }

        public static void SelectTextinDDL(DropDownList cmbDropdown, string strText)
        {
            ListItem listItem;

            listItem = cmbDropdown.Items.FindByText(strText);
            cmbDropdown.SelectedIndex = cmbDropdown.Items.IndexOf(listItem);
        }

        /// <summary>
        /// Selects the values in the passed in comma separated list in the check box list
        /// </summary>
        /// <param name="cbl"></param>
        /// <param name="commaSeparatedValues"></param>
        public static void SelectCheckBoxList(CheckBoxList cbl, string commaSeparatedValues)
        {
            if (commaSeparatedValues == null) return;

            if (commaSeparatedValues.Trim() == "") return;

            string[] strArray = commaSeparatedValues.Split(',');

            if (strArray == null || strArray.Length == 0) return;

            for (int i = 0; i < cbl.Items.Count; i++)
            {
                ListItem li = cbl.Items[i];

                for (int j = 0; j < strArray.Length; j++)
                {
                    if (li.Value == strArray[j])
                    {
                        li.Selected = true;
                    }
                }

            }
        }


        /// <summary>
        /// Selects the values in the passed in comma separated list in the check box list
        /// </summary>
        /// <param name="cbl"></param>
        /// <param name="commaSeparatedValues"></param>
        public static void SelectCheckBoxListText(CheckBoxList cbl, string commaSeparatedValues)
        {
            if (commaSeparatedValues == null) return;

            if (commaSeparatedValues.Trim() == "") return;

            string[] strArray = commaSeparatedValues.Split(',');

            if (strArray == null || strArray.Length == 0) return;

            for (int i = 0; i < cbl.Items.Count; i++)
            {
                ListItem li = cbl.Items[i];

                for (int j = 0; j < strArray.Length; j++)
                {
                    if (li.Text == strArray[j])
                    {
                        li.Selected = true;
                    }
                }

            }
        }
        public static void SetComboValueForConcatenatedString(System.Web.UI.HtmlControls.HtmlSelect comboID, string SelectedValue, char EncodedCharacter, int ValuePosition)
        {

            if (comboID == null) return;
            comboID.SelectedIndex = -1;
            string encoded_string = "", comboIDValue = "";
            for (int i = 0; i < comboID.Items.Count; i++)
            {
                encoded_string = comboID.Items[i].Value;
                string[] array = encoded_string.Split(EncodedCharacter);
                //Get comboIDValue
                comboIDValue = array[ValuePosition];
                //If the code value matches with selected value, set the selected Index to it and return
                if (comboIDValue == SelectedValue)
                {
                    comboID.Items[i].Selected = true;
                    return;
                }
            }

        }

        public static void SetComboValueForConcatenatedString(DropDownList comboID, string SelectedValue, char EncodedCharacter, int ValuePosition)
        {

            if (comboID == null) return;
            comboID.SelectedIndex = -1;
            string encoded_string = "", comboIDValue = "";
            for (int i = 0; i < comboID.Items.Count; i++)
            {
                encoded_string = comboID.Items[i].Value;
                string[] array = encoded_string.Split(EncodedCharacter);
                //Get comboIDValue
                comboIDValue = array[ValuePosition];
                //If the code value matches with selected value, set the selected Index to it and return
                if (comboIDValue == SelectedValue)
                {
                    comboID.Items[i].Selected = true;
                    return;
                }
            }

        }


        /// <summary>
        /// Removes a particular drop-down value from the existing dropdown option values based on value provided
        /// </summary>
        /// <param name="cmbDropdown">Dropdown to be worked upon</param>
        /// <param name="objValue">Option value to be removed</param>
        public static void RemoveOptionFromDropdownByValue(DropDownList cmbDropdown, string objValue)
        {
            if (objValue == null || objValue.Trim() == "") return;

            ListItem listItem;

            listItem = cmbDropdown.Items.FindByValue(Convert.ToString(objValue));
            if (listItem != null)
                cmbDropdown.Items.Remove(listItem);
        }

        /// <summary>
        /// Removes a particular drop-down value from the existing dropdown option values based on text-value provided
        /// </summary>
        /// <param name="cmbDropdown">Dropdown to be worked upon</param>
        /// <param name="objValue">Option text value to be removed</param>
        public static void RemoveOptionFromDropdownByText(DropDownList cmbDropdown, string objValue)
        {
            if (objValue == null || objValue.Trim() == "") return;

            ListItem listItem;

            listItem = cmbDropdown.Items.FindByText(Convert.ToString(objValue));
            if (listItem != null)
                cmbDropdown.Items.Remove(listItem);
        }


        /// <summary>
        /// Returns the application path
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationPath()
        {
            string WEB_PROTOCOL = "";
            if (System.Web.HttpContext.Current.Request.IsSecureConnection)
                WEB_PROTOCOL = "https://";
            else
                WEB_PROTOCOL = "http://";

            //Modified by Abhishek Goel on dated 20/02/2013 
            //return WEB_PROTOCOL + System.Web.HttpContext.Current.Request.Url.Host + System.Web.HttpContext.Current.Request.ApplicationPath;
            return WEB_PROTOCOL + System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + System.Web.HttpContext.Current.Request.ApplicationPath;

        }

        /// <summary>
        /// Returnd the URL of the lookup window
        /// </summary>
        /// <returns></returns>
        public static string GetLookupWindowURL()
        {
            return GetApplicationPath() + @"/cmsweb/aspx/LookupForm1.aspx";
        }

        /// <summary>
        /// Returnd the URL of the lookup window
        /// </summary>
        /// <returns></returns>
        public static string GetLookupURL()
        {
            return GetApplicationPath() + @"/cmsweb/aspx/LookupForm2.aspx";
        }

        public static string GetExternalAgencyURL()
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get("ExtAgnUrl");
        }
        /// <summary>
        /// to get Agency Numeric Code :
        /// </summary>
        /// <param name="agencyNumCode">passed with URL</param>
        /// <returns></returns>
        public static string GetAgencyNumCode(string agencyNumCode)
        {
            string agencyCode = "";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@AGENCY_CODE", agencyNumCode);
            try
            {
                DataSet ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_GetAgencyNumCode", sqlParams);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        agencyCode = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return agencyCode;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyNumCode"></param>
        /// <returns></returns>
        public static DataSet GetAgencyFromCombinedCode(string agencyCode, string userId)
        {

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@AGENCY_CODE", agencyCode);
            sqlParams[1] = new SqlParameter("@USER_ID", userId);
            DataSet ds = null;
            try
            {
                ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_GetAgencyFromCombinedCode", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
            }
            return ds;



        }
        /// <summary>
        /// Returns the county for the passed zip code
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public static string GetCountyForZip(string zip)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ZIP_ID", zip);

            object objCounty = SqlHelper.ExecuteScalar(ConnStr, CommandType.StoredProcedure, "Proc_GetCountyForZip", sqlParams);

            if (objCounty == System.DBNull.Value || objCounty == null)
            {
                return "";
            }

            return objCounty.ToString();

        }

        public static DataTable GetStateNameOnStateID(int custId, int appId, int appVerId)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@custId", custId);
            sqlParams[1] = new SqlParameter("@appId", appId);
            sqlParams[2] = new SqlParameter("@appVerId", appVerId);
            DataSet ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_GetStateNameforID", sqlParams);
            return ds.Tables[0];
        }
        public static string EnocodeSqlCharacters(string strValue)
        {
            return strValue.Replace("'", "''");
        }

        public static string DecodeXMLCharacters(string strValue)
        {
            if (strValue.IndexOf("&amp;") > -1)
            {
                strValue = strValue.Replace("&amp;", "&");
            }

            if (strValue.IndexOf("&lt;") > -1)
            {
                strValue = strValue.Replace("&lt;", "<");
            }

            if (strValue.IndexOf("&gt;") > -1)
            {
                strValue = strValue.Replace("&gt;", ">");
            }

            if (strValue.IndexOf("&apos;") > -1)
            {
                strValue = strValue.Replace("&apos;", "'");
            }

            if (strValue.IndexOf("&quot;") > -1)
            {
                strValue = strValue.Replace("&quot", "\"");
            }

            if (strValue.IndexOf("&nbsp;") > -1)
            {
                strValue = strValue.Replace("&nbsp;", "");
            }
            return strValue;
        }

        public static string EncodeXMLCharacters(string strValue)
        {
            if (strValue.IndexOf("&") > -1)
            {
                strValue = strValue.Replace("&", "&amp;");
            }

            if (strValue.IndexOf("<") > -1)
            {
                strValue = strValue.Replace("<", "&lt;");
            }

            if (strValue.IndexOf(">") > -1)
            {
                strValue = strValue.Replace(">", "&gt;");
            }

            if (strValue.IndexOf("'") > -1)
            {
                strValue = strValue.Replace("'", "&apos;");
            }

            //if ( strValue.IndexOf("&") > -1 )
            //{
            //	strValue = strValue.Replace("&","&amp;");
            //}

            return strValue;
        }

        public static string GetXMLEncoded(DataTable dataTable)
        {
            if (dataTable == null) return "";

            if (dataTable.Rows.Count == 0) return "";

            StringBuilder sbXML = new StringBuilder();

            sbXML.Append("<NewDataSet>");
            sbXML.Append(Environment.NewLine);
            foreach (DataRow dr in dataTable.Rows)
            {
                sbXML.Append("<Table>");
                sbXML.Append(Environment.NewLine);
                foreach (DataColumn dc in dataTable.Columns)
                {
                    if (dr[dc] == System.DBNull.Value)
                    {
                        sbXML.Append("<" + dc.ColumnName.ToUpper() + "/>");
                    }
                    else
                    {
                        sbXML.Append("<" + dc.ColumnName.ToUpper() + ">");
                        sbXML.Append(EncodeXMLCharacters(dr[dc].ToString()));
                        sbXML.Append("</" + dc.ColumnName.ToUpper() + ">");
                    }
                    sbXML.Append(Environment.NewLine);
                }
                sbXML.Append(Environment.NewLine);
                sbXML.Append("</Table>");
                sbXML.Append(Environment.NewLine);

            }

            sbXML.Append("</NewDataSet>");
            return sbXML.ToString();
        }

        public static void AddTransactionLog(Cms.Model.Maintenance.ClsTransactionInfo objTransaction, SqlTransaction tran)
        {


            DateTime RecordDate = DateTime.Now;

            SqlParameter[] param = new SqlParameter[16];


            //code commented and added by Ashwani
            param[0] = new SqlParameter("@TRANS_TYPE_ID", objTransaction.TRANS_TYPE_ID);
            param[1] = new SqlParameter("@RECORD_DATE_TIME", RecordDate);
            param[2] = new SqlParameter("@CLIENT_ID", objTransaction.CLIENT_ID);
            param[3] = new SqlParameter("@TRANS_DESC", objTransaction.TRANS_DESC);
            param[4] = new SqlParameter("@CHANGE_XML", objTransaction.CHANGE_XML);
            param[5] = new SqlParameter("@POLICY_ID", objTransaction.POLICY_ID);
            param[6] = new SqlParameter("@POLICY_VER_TRACKING_ID", objTransaction.POLICY_VER_TRACKING_ID);
            param[7] = new SqlParameter("@RECORDED_BY", objTransaction.RECORDED_BY);
            param[8] = new SqlParameter("@RECORDED_BY_NAME", objTransaction.RECORDED_BY_NAME);
            param[9] = new SqlParameter("@ENTITY_ID", objTransaction.ENTITY_ID);
            param[10] = new SqlParameter("@ENTITY_TYPE", objTransaction.ENTITY_TYPE);
            param[11] = new SqlParameter("@IS_COMPLETED", objTransaction.IS_COMPLETED);
            param[12] = new SqlParameter("@APP_ID", objTransaction.APP_ID);
            param[13] = new SqlParameter("@APP_VERSION_ID", objTransaction.APP_VERSION_ID);
            param[14] = new SqlParameter("@QUOTE_ID", objTransaction.QUOTE_ID);
            param[15] = new SqlParameter("@QUOTE_VERSION_ID", objTransaction.QUOTE_VERSION_ID);

            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "Proc_InsertTransactionLog", param);

        }

        /// <summary>
        /// Returns the XMl representation of a datatable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static string GetXML(DataTable dataTable)
        {
            if (dataTable == null) return "";

            if (dataTable.Rows.Count == 0) return "";

            StringBuilder sbXML = new StringBuilder();

            sbXML.Append("<NewDataSet>");
            sbXML.Append(Environment.NewLine);
            foreach (DataRow dr in dataTable.Rows)
            {
                sbXML.Append("<Table>");
                sbXML.Append(Environment.NewLine);
                foreach (DataColumn dc in dataTable.Columns)
                {
                    if (dr[dc] == System.DBNull.Value)
                    {
                        sbXML.Append("<" + dc.ColumnName + "/>");
                    }
                    else
                    {
                        sbXML.Append("<" + dc.ColumnName + ">");
                        sbXML.Append(RemoveJunkXmlCharactersRates(dr[dc].ToString()));
                        sbXML.Append("</" + dc.ColumnName + ">");
                    }

                    sbXML.Append(Environment.NewLine);
                }
                sbXML.Append(Environment.NewLine);
                sbXML.Append("</Table>");
                sbXML.Append(Environment.NewLine);

            }

            sbXML.Append("</NewDataSet>");

            return sbXML.ToString();


        }
        public static string GetXML(DataTable dataTable, int PolicyCurrency)
        {
            //Default policy Currency = US Dollar
            if (PolicyCurrency < 0)
                PolicyCurrency = 1;

            NumberFormatInfo numberFormatInfo;
            if (PolicyCurrency == 2)
                numberFormatInfo = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                numberFormatInfo = new CultureInfo(enumCulture.US, true).NumberFormat;
            numberFormatInfo.NumberDecimalDigits = 2;

            return GetXML(dataTable, numberFormatInfo);

        }
        /// <summary>
        /// Get Data Table xml according to Policy Currency 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="NumberFormat"></param>
        /// <returns></returns>
        public static string GetXML(DataTable dataTable, NumberFormatInfo NumberFormat)
        {
            if (dataTable == null) return "";

            if (dataTable.Rows.Count == 0) return "";

            StringBuilder sbXML = new StringBuilder();

            sbXML.Append("<NewDataSet>");
            sbXML.Append(Environment.NewLine);
            foreach (DataRow dr in dataTable.Rows)
            {
                sbXML.Append("<Table>");
                sbXML.Append(Environment.NewLine);
                foreach (DataColumn dc in dataTable.Columns)
                {
                    if (dr[dc] == System.DBNull.Value)
                    {
                        sbXML.Append("<" + dc.ColumnName + "/>");
                    }
                    else
                    {
                        if (dc.DataType.Name.ToUpper() == "DECIMAL")
                        {
                            sbXML.Append("<" + dc.ColumnName + ">");
                            //double DecimalVal = Convert.ToDouble(dr[dc]);//,NumberFormat),2);//.ToString()
                            sbXML.Append(RemoveJunkXmlCharactersRates(Convert.ToDouble(dr[dc]).ToString(NumberFormat)));//Convert.ToDouble(dr[dc],NumberFormat).ToString()));
                            sbXML.Append("</" + dc.ColumnName + ">");
                        }
                        else
                        {
                            sbXML.Append("<" + dc.ColumnName + ">");
                            sbXML.Append(RemoveJunkXmlCharactersRates(dr[dc].ToString()));
                            sbXML.Append("</" + dc.ColumnName + ">");
                        }
                    }

                    sbXML.Append(Environment.NewLine);
                }
                sbXML.Append(Environment.NewLine);
                sbXML.Append("</Table>");
                sbXML.Append(Environment.NewLine);

            }

            sbXML.Append("</NewDataSet>");

            return sbXML.ToString();


        }
        public static string GetXMLWithOutRemovejunk(DataTable dataTable)
        {
            if (dataTable == null) return "";

            if (dataTable.Rows.Count == 0) return "";

            StringBuilder sbXML = new StringBuilder();

            sbXML.Append("<NewDataSet>");
            sbXML.Append(Environment.NewLine);
            foreach (DataRow dr in dataTable.Rows)
            {
                sbXML.Append("<Table>");
                sbXML.Append(Environment.NewLine);
                foreach (DataColumn dc in dataTable.Columns)
                {
                    if (dr[dc] == System.DBNull.Value)
                    {
                        sbXML.Append("<" + dc.ColumnName + "/>");
                    }
                    else
                    {
                        if (dc.DataType.Name.ToUpper() == "DECIMAL")
                        {
                            sbXML.Append("<" + dc.ColumnName + ">");
                            sbXML.Append((Convert.ToDouble(dr[dc]).ToString()));
                            sbXML.Append("</" + dc.ColumnName + ">");
                        }
                        else
                        {
                            sbXML.Append("<" + dc.ColumnName + ">");
                            sbXML.Append((dr[dc].ToString()));
                            sbXML.Append("</" + dc.ColumnName + ">");
                        }
                    }

                    sbXML.Append(Environment.NewLine);
                }
                sbXML.Append(Environment.NewLine);
                sbXML.Append("</Table>");
                sbXML.Append(Environment.NewLine);

            }

            sbXML.Append("</NewDataSet>");

            return sbXML.ToString();
        }
        public static string RemoveJunkXmlCharactersRates(string strNodeContent)
        {
            strNodeContent = strNodeContent.Replace("&", "&amp;");
            strNodeContent = strNodeContent.Replace("<", "&lt;");
            strNodeContent = strNodeContent.Replace(">", "&gt;");
            return strNodeContent;
        }
        //Added by Manoj Rathore on 8th May 2009 
        public static string RemoveJunkXmlCharacters(string strNodeContent)
        {
            strNodeContent = strNodeContent.Replace("&", "&amp;");
            strNodeContent = strNodeContent.Replace("<", "&lt;");
            strNodeContent = strNodeContent.Replace(">", "&gt;");
            strNodeContent = strNodeContent.Replace("\"", "&quot;");
            strNodeContent = strNodeContent.Replace("'", "&#39;");
            return strNodeContent;
        }

        public static string FetchValueFromXML(string nodeName, string XMLString)
        {
            try
            {
                if (XMLString == "")
                    return "0";
                string strRetval = "";
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLString);
                XmlNodeList nodList = doc.GetElementsByTagName(nodeName);
                if (nodList.Count > 0)
                {
                    strRetval = nodList.Item(0).InnerText;
                }
                return strRetval;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {

            }
        }

        #endregion

        #region These functions will be removed from BLCommon
        public DataSet ExecuteInlineQuery(string sql)
        {
            return SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, sql);
        }
        public DataSet ExecuteInlineQuery(string sql, int iStartRec, int iEndRec)
        {
            try
            {
                DataSet ds = new DataSet();
                // Testing and Debug - Rajan
                //sql = "SELECT  TOP 20 c.customerid,s.supplierid,c.companyname,c.contactname,s.companyname,s.contactname,orderdate,productname,s.address,unitsonorder,p.unitprice,s.country,s.city FROM customers c,orders o,[order details] od,products p,suppliers s WHERE c.customerid=o.customerid and o.orderid=od.orderid and od.productid=p.productid and  s.supplierid = p.supplierid;SELECT COUNT(*) FROM customers c,orders o,[order details] od,products p,suppliers s WHERE c.customerid=o.customerid and o.orderid=od.orderid and od.productid=p.productid and  s.supplierid = p.supplierid";
                //ConnStr = "user id=sa;password=;initial catalog=Northwind;data source=ANSHUMAN;Connect Timeout=30"; 
                // End
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sql, ConnStr);
                da.Fill(ds, iStartRec, iEndRec, "Mapper");
                return ds;
            }
            catch 
            {
                throw new Exception(sql);
            }


        }
        public int ExecuteNonQuery(string sql)
        {
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql);
        }
        public object ExecuteScalar(string sql)
        {
            return SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql);
        }

        /// <summary>
        /// A temprorary method which is deleting record
        /// </summary>
        /// <param name="deleteClause"></param>
        /// <returns></returns>
        public bool TempMethodDelete(string deleteClause)
        {
            string strSql;
            strSql = " delete from [order details] where orderid in( "
                + " select orderid from orders where " + deleteClause + ") ";
            try
            {
                int intRecordsAffected = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, strSql);
                strSql = " delete from orders where " + deleteClause;
                intRecordsAffected = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, strSql);
                strSql = " delete from customers where " + deleteClause;
                intRecordsAffected = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, strSql);
                if (intRecordsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region These functions are used to fetch menu data and create XML string

        /// <summary>
        /// This function is used to fetch the menu data from data base
        /// </summary>
        /// <returns>Data Table with menu data which will be used by GetMenuXml function to generate xml</returns>
        public DataTable GetMenuData()
        {
            //This function is used to fetch Menu data from database. --- Gaurav

            //Added by Charles on 12-Apr-10 for Multilingual Support
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
            return objWrapper.ExecuteDataSet("proc_getMenuData").Tables[0]; //Added till here
            //return SqlHelper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"proc_getMenuData").Tables[0]; //Commented by Charles on 12-Apr-10 for Multilingual Support
        }

        public DataTable GetLobMenuData(string LobString, string PolicyLevel, string AgencyCode)
        {
            //This function is used to fetch default Menu data from database with out risk sub menus
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objWrapper.AddParameter("@LOB_STRING", LobString);
            objWrapper.AddParameter("@POLICY_LEVEL", PolicyLevel);
            objWrapper.AddParameter("@AGENCY_CODE", AgencyCode);
            objWrapper.AddParameter("@LANG_ID", BL_LANG_ID); //Added by Charles on 12-Apr-10 for Multilingual Support
            return objWrapper.ExecuteDataSet("proc_getLobMenuDataEx").Tables[0];
        }

        /// <summary>
        /// Use GetDefaultMenuData to generate xml for that data retruned. This will generate different levels of menus like( topmenu, menu,submenu,subsubmenu).
        /// </summary>
        /// <returns></returns>
        public string GetLobMenuXml(string LobString, string PolicyLevel, string AgencyCode)
        {
            // This function use Menu Data return by GetMenuData and generate XML for the same.
            ldRecord = GetLobMenuData(LobString, PolicyLevel, AgencyCode);
            try
            {
                doc = new XmlDocument();
                XmlElement root = doc.CreateElement("root");
                int i = 0;
                while (i < ldRecord.Rows.Count)
                {
                    XmlElement button = doc.CreateElement("button");
                    button.SetAttribute("name", ldRecord.Rows[i]["button_name"].ToString());
                    button.SetAttribute("menu_id", ldRecord.Rows[i]["button_id"].ToString());
                    button.SetAttribute("linkUrl", ldRecord.Rows[i]["button_link"].ToString());
                    button.SetAttribute("image", ldRecord.Rows[i]["button_image"].ToString());
                    button.SetAttribute("enabled", Convert.ToInt32(ldRecord.Rows[i]["button_hidestatus"]) == 0 ? "true" : "false");
                    button.SetAttribute("default_page", ldRecord.Rows[i]["default_page"].ToString());
                    if (ldRecord.Rows[i]["topmenu_id"] != DBNull.Value)
                        i = topmenu(i, button, Convert.ToInt32(ldRecord.Rows[i]["button_id"]));
                    else
                        i++;
                    root.AppendChild(button);
                }
                return root.OuterXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetDefaultMenuData(string AgencyCode)
        {
            //This function is used to fetch default Menu data from database with out risk sub menus
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objWrapper.AddParameter("@AGENCY_CODE", AgencyCode);
            objWrapper.AddParameter("@LANG_ID", BL_LANG_ID); //Added by Charles on 12-Apr-10 for Multilingual Support

            //This function is used to fetch default Menu data from database with out risk sub menus
            return objWrapper.ExecuteDataSet("proc_getDefaultMenuDataEx").Tables[0];
        }

        public DataSet GetNavigationXmlChild(int menuID, int CustID, int polID, int polVerID)
        {
            //This function is used to fetch default Menu data from database with out risk sub menus
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objWrapper.AddParameter("@MENU_ID", menuID);
            objWrapper.AddParameter("@CUSTOMER_ID", CustID);
            objWrapper.AddParameter("@POLICY_ID", polID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", polVerID);

            //This function is used to fetch default Menu data from database with out risk sub menus
            return objWrapper.ExecuteDataSet("GET_XML_CHILD_DATA");
        }


        //Added by Agniswar for Tree View Navigation XML
        public DataSet GetDefaultLayoutXml(string strLOBCode)
        {
            
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@LOB_STRING", strLOBCode);
            //objWrapper.AddParameter("@LANG_ID", BL_LANG_ID); 
            
            return objWrapper.ExecuteDataSet("proc_GetDefaultLayoutXml_New");
        }


        /// <summary>
        /// Use GetDefaultMenuData to generate xml for that data retruned. This will generate different levels of menus like( topmenu, menu,submenu,subsubmenu).
        /// </summary>
        /// <returns></returns>
        public string GetDefaultMenuXml(string AgencyCode)
        {
            // This function use Menu Data return by GetMenuData and generate XML for the same.
            ldRecord = GetDefaultMenuData(AgencyCode);
            try
            {
                doc = new XmlDocument();
                XmlElement root = doc.CreateElement("root");
                int i = 0;
                while (i < ldRecord.Rows.Count)
                {
                    XmlElement button = doc.CreateElement("button");
                    button.SetAttribute("name", ldRecord.Rows[i]["button_name"].ToString());
                    button.SetAttribute("menu_id", ldRecord.Rows[i]["button_id"].ToString());
                    button.SetAttribute("linkUrl", ldRecord.Rows[i]["button_link"].ToString());
                    button.SetAttribute("image", ldRecord.Rows[i]["button_image"].ToString());
                    button.SetAttribute("enabled", Convert.ToInt32(ldRecord.Rows[i]["button_hidestatus"]) == 0 ? "true" : "false");
                    button.SetAttribute("default_page", ldRecord.Rows[i]["default_page"].ToString());
                    if (ldRecord.Rows[i]["topmenu_id"] != DBNull.Value)
                        i = topmenu(i, button, Convert.ToInt32(ldRecord.Rows[i]["button_id"]));
                    else
                        i++;
                    root.AppendChild(button);
                }
                return root.OuterXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Use GetMenuData to generate xml for that data retrun by GetMenuData. This will generate different levels of menus like( topmenu, menu,submenu,subsubmenu).
        /// </summary>
        /// <returns> xml menu which will be used by menu parser ( Java Script)</returns>
        public string GetMenuXml()
        {
            // This function use Menu Data return by GetMenuData and generate XML for the same.
            ldRecord = GetMenuData();
            try
            {
                doc = new XmlDocument();
                XmlElement root = doc.CreateElement("root");
                int i = 0;
                while (i < ldRecord.Rows.Count)
                {
                    XmlElement button = doc.CreateElement("button");
                    button.SetAttribute("name", ldRecord.Rows[i]["button_name"].ToString());
                    button.SetAttribute("menu_id", ldRecord.Rows[i]["button_id"].ToString());
                    button.SetAttribute("linkUrl", ldRecord.Rows[i]["button_link"].ToString());
                    button.SetAttribute("image", ldRecord.Rows[i]["button_image"].ToString());
                    button.SetAttribute("enabled", Convert.ToInt32(ldRecord.Rows[i]["button_hidestatus"]) == 0 ? "true" : "false");
                    button.SetAttribute("default_page", ldRecord.Rows[i]["default_page"].ToString());
                    if (ldRecord.Rows[i]["topmenu_id"] != DBNull.Value)
                        i = topmenu(i, button, Convert.ToInt32(ldRecord.Rows[i]["button_id"]));
                    else
                        i++;
                    root.AppendChild(button);
                }
                return root.OuterXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get all the locations based on the customer id.
        /// </summary>
        /// <returns></returns>
        public DataSet GetLocationDetails(int CustomerID,int Pol_ID,int Pol_version_ID)
        {
            string strStoredProc = "Proc_GetLocationDetails";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@POL_ID", Pol_ID);
            objWrapper.AddParameter("@POL_VERSION_ID", Pol_version_ID);


            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds;
        }


        private int topmenu(int i, XmlElement button, int buttonId)
        {
            // This function is used to generate Top Level of menu.
            while (i < ldRecord.Rows.Count)
            {
                if (Convert.ToInt32(ldRecord.Rows[i]["button_id"]) == buttonId)
                {
                    if (ldRecord.Rows[i]["topmenu_id"] != DBNull.Value)
                    {
                        XmlElement topmenu = doc.CreateElement("topMenu");
                        topmenu.SetAttribute("name", ldRecord.Rows[i]["topmenu_name"].ToString());
                        topmenu.SetAttribute("menu_id", ldRecord.Rows[i]["topmenu_id"].ToString());
                        topmenu.SetAttribute("linkUrl", ldRecord.Rows[i]["topmenu_link"].ToString());
                        topmenu.SetAttribute("enabled", Convert.ToInt32(ldRecord.Rows[i]["topmenu_hidestatus"]) == 0 ? "true" : "false");
                        if (ldRecord.Rows[i]["menu_id"] != DBNull.Value)
                            i = menu(i, topmenu, Convert.ToInt32(ldRecord.Rows[i]["topmenu_id"]));
                        else
                            i++;
                        button.AppendChild(topmenu);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return i;
        }
        private int menu(int i, XmlElement topmenu, int topmenuId)
        {// This function is used to generate Second Level of menu.
            while (i < ldRecord.Rows.Count)
            {
                if ((ldRecord.Rows[i]["topmenu_id"] != DBNull.Value) && (Convert.ToInt32(ldRecord.Rows[i]["topmenu_id"]) == topmenuId))
                {
                    if (ldRecord.Rows[i]["menu_id"] != DBNull.Value)
                    {
                        XmlElement menu = doc.CreateElement("menu");
                        menu.SetAttribute("name", ldRecord.Rows[i]["menu_name"].ToString());
                        menu.SetAttribute("menu_id", ldRecord.Rows[i]["menu_id"].ToString());
                        menu.SetAttribute("linkUrl", ldRecord.Rows[i]["menu_link"].ToString());
                        menu.SetAttribute("enabled", Convert.ToInt32(ldRecord.Rows[i]["menu_hidestatus"]) == 0 ? "true" : "false");
                        if (ldRecord.Rows[i]["submenu_id"] != DBNull.Value)
                            i = subMenu(i, menu, Convert.ToInt32(ldRecord.Rows[i]["menu_id"]));
                        else
                            i++;
                        topmenu.AppendChild(menu);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return i;
        }
        private int subMenu(int i, XmlElement menu, int menuId)
        {// This function is used to generate Sub menu of menu.
            while (i < ldRecord.Rows.Count)
            {
                if ((ldRecord.Rows[i]["menu_id"] != DBNull.Value) && (Convert.ToInt32(ldRecord.Rows[i]["menu_id"]) == menuId))
                {
                    if (ldRecord.Rows[i]["submenu_id"] != DBNull.Value)
                    {
                        XmlElement subMenu = doc.CreateElement("subMenu");
                        subMenu.SetAttribute("name", ldRecord.Rows[i]["submenu_name"].ToString());
                        subMenu.SetAttribute("menu_id", ldRecord.Rows[i]["submenu_id"].ToString());
                        subMenu.SetAttribute("linkUrl", ldRecord.Rows[i]["submenu_link"].ToString());
                        subMenu.SetAttribute("enabled", Convert.ToInt32(ldRecord.Rows[i]["submenu_hidestatus"]) == 0 ? "true" : "false");
                        if (ldRecord.Rows[i]["subsubmenu_id"] != DBNull.Value)
                            i = subSubMenu(i, subMenu, Convert.ToInt32(ldRecord.Rows[i]["submenu_id"]));
                        else
                            i++;
                        menu.AppendChild(subMenu);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return i;
        }
        private int subSubMenu(int i, XmlElement subMenu, int subMenuId)
        {// This function is used to generate sub sub Menu of menu.
            while (i < ldRecord.Rows.Count)
            {
                if ((ldRecord.Rows[i]["submenu_id"] != DBNull.Value) && (Convert.ToInt32(ldRecord.Rows[i]["submenu_id"]) == subMenuId))
                {
                    if (ldRecord.Rows[i]["subsubmenu_id"] != DBNull.Value)
                    {
                        XmlElement subSubMenu = doc.CreateElement("subSubMenu");
                        subSubMenu.SetAttribute("name", ldRecord.Rows[i]["subsubmenu_name"].ToString());
                        subSubMenu.SetAttribute("menu_id", ldRecord.Rows[i]["subsubmenu_id"].ToString());
                        subSubMenu.SetAttribute("linkUrl", ldRecord.Rows[i]["subsubmenu_link"].ToString());
                        subSubMenu.SetAttribute("enabled", Convert.ToInt32(ldRecord.Rows[i]["subsubmenu_hidestatus"]) == 0 ? "true" : "false");
                        i++;
                        subMenu.AppendChild(subSubMenu);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return i;
        }
        #endregion

        #region These functions are being used temporarily for sample transaction page.

        public DataSet GetSampleData(string userId, string connString, int divId)
        {
            StringBuilder SqlQuery = new StringBuilder(1000);
            SqlQuery.Append("select DIV_ID,DIV_CODE,DIV_NAME,DIV_ADD1,DIV_COUNTRY,DIV_EXT,DIV_PHONE,CREATED_BY,CONVERT(VARCHAR(50),CREATED_DATETIME,101) CREATED_DATETIME,CONVERT(VARCHAR(50),LAST_UPDATED_DATETIME,101) LAST_UPDATED_DATETIME");
            SqlQuery.Append(" from MNT_DIV_LIST where DIV_ID =" + divId + " and CREATED_BY=" + userId);
            DataSet tempData = DataWrapper.ExecuteDataset(connString, CommandType.Text, SqlQuery.ToString());

            return tempData;
        }


        public int InsertSampleTransaction(string userId, string connString, string txtEmployeeId, string txtEmployeeLastName, string txtAddress, string txtCountry, string txtExtention, bool rdoYes, string txtLastUpdateDateTime)
        {
            string lStrAppConnection = "";
            string rYes = "";
            int lResult = -1;
            if (rdoYes)
                rYes = "Yes";
            else
                rYes = "No";

            try
            {

                lStrAppConnection = connString;
                objDataAccess = new DataWrapper(lStrAppConnection, CommandType.Text);
                string lStrUserId = userId == "" ? "0" : userId;
                StringBuilder lQuery = new StringBuilder(1000);
                StringBuilder sqlString = new StringBuilder(1000);
                lQuery.Append("INSERT INTO MNT_DIV_LIST(DIV_CODE,DIV_NAME,DIV_ADD1,DIV_COUNTRY,DIV_EXT,CREATED_BY,CREATED_DATETIME,LAST_UPDATED_DATETIME) values ('");
                lQuery.Append(txtEmployeeId + "','" + txtEmployeeLastName + "','" + txtAddress + "','" + txtCountry + "','" + txtExtention + "'," + int.Parse(userId) + ",getdate(),'" + txtLastUpdateDateTime + "')");

                objDataAccess.ExecuteNonQuery(lQuery.ToString());
                lResult = 1;
            }
            catch (Exception oEx) { throw oEx; }
            finally
            {

            }
            return lResult;
        }


        public int UpdateSampleTransaction(string userId, string connString, string txtEmployeeId, string txtEmployeeLastName, string txtAddress, string txtCountry, string txtExtention, bool rdoYes, string txtLastUpdateDateTime, int divId)
        {
            string lStrAppConnection = "";
            string rYes = "";
            int lResult = -1;
            if (rdoYes)
                rYes = "Yes";
            else
                rYes = "No";

            try
            {
                lStrAppConnection = connString;
                objDataAccess = new DataWrapper(lStrAppConnection, CommandType.Text);
                StringBuilder lQuery = new StringBuilder(1000);
                StringBuilder sqlString = new StringBuilder(1000);
                lQuery.Append("UPDATE MNT_DIV_LIST SET DIV_CODE='" + txtEmployeeId + "',DIV_NAME='" + txtEmployeeLastName + "',DIV_ADD1='" + txtAddress + "',DIV_COUNTRY='" + txtCountry + "',DIV_EXT='" + txtExtention + "',LAST_UPDATED_DATETIME='" + txtLastUpdateDateTime + "',DIV_PHONE='" + rYes + "'  WHERE CREATED_BY=" + int.Parse(userId) + " and div_id=" + divId);
                objDataAccess.ExecuteNonQuery(lQuery.ToString());
                lResult = 1;
            }
            catch (Exception oEx) { throw oEx; }
            finally
            {

            }
            return lResult;
        }


        #endregion
        // Added by Asfa
        public static string GetCountryList(string strCOUNTRY_ID)
        {
            string strSql = "Proc_GetCountryList";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@COUNTRY_ID", strCOUNTRY_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
                return objDataSet.Tables[0].Rows[0]["COUNTRY_NAME"].ToString();
            else
                return "";
        }
        public DataTable GetDDLDataSource(string strObjectName, string DbObjectType,string strTextField, string strValueField, string strWhereClause,Hashtable ParamList)
        {
            string strSql = "Proc_GetBindingData";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            if ((DbObjectType=="Proc" || DbObjectType=="Procedure") && ParamList.Keys.Count>0)
            {
                strSql = strObjectName;
                foreach (object Key in ParamList.Keys)
                {
                    objDataWrapper.AddParameter(Key.ToString(), ParamList[Key].ToString());
                }
            }
            else
            {
            objDataWrapper.AddParameter("@DbObjectName", strObjectName);
            objDataWrapper.AddParameter("@DbObjectType", DbObjectType);
            objDataWrapper.AddParameter("@TextField", strTextField);
            objDataWrapper.AddParameter("@ValueField", strValueField);
            objDataWrapper.AddParameter("@WhereClause", strWhereClause);
            }
            try
            {
                DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
                return objDataSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
        #region get Formated/encripted SSN No/Federal Ids
        public static string GetEncriptedFormatedString(string DataXML, ref string hidFieldValue, string FieldName)
        {
            string strCapEncriptFormated = "";
            try
            {
                if (DataXML.IndexOf("NewDataSet") >= 0)
                {
                    XmlDocument objxml = new XmlDocument();
                    objxml.LoadXml(DataXML);
                    XmlNode node = objxml.SelectSingleNode("NewDataSet");
                    XmlNode noder1 = node.SelectSingleNode("Table/" + FieldName);
                    if (noder1 != null)
                    {
                        hidFieldValue = noder1.InnerText;
                        string strDecriptValue = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
                        if (strDecriptValue.Trim() != "")
                        {
                            string strvaln = "xxx-xx-";
                            strvaln += strDecriptValue.Substring(strvaln.Length, strDecriptValue.Length - strvaln.Length);
                            strCapEncriptFormated = strvaln;
                        }
                        else
                            strCapEncriptFormated = "";
                    }

                    objxml = null;
                }
                return strCapEncriptFormated;
            }
            catch
            {
                return "";
            }
        }
        #endregion
        #region Methods used by Singelton Class
        public SqlDataAdapter GetCommonData()
        {
            try
            {
                //return DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,"select COUNTRY_ID,COUNTRY_CODE,COUNTRY_NAME form MNT_COUNTRY_LIST;select COUNTRY_ID,STATE_ID,STATE_CODE,STATE_NAME from MNT_COUNTRY_STATE_LIST");
                //SqlDataAdapter lObjDataAdapter = new SqlDataAdapter("select COUNTRY_ID,COUNTRY_CODE,COUNTRY_NAME from MNT_COUNTRY_LIST select COUNTRY_ID,STATE_ID,STATE_CODE,STATE_NAME,IS_ACTIVE from MNT_COUNTRY_STATE_LIST select TYPEID,TYPEDESC from TODOLISTTYPES order by TYPEDESC select TRANS_TYPE_ID,TRANS_TYPE_NAME from TRANSACTIONTYPELIST order by TRANS_TYPE_NAME select  LOOKUP_VALUE_ID, LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID = 315 order by LOOKUP_VALUE_DESC select LOB_ID, LOB_DESC FROM MNT_LOB_MASTER order by LOB_DESC SELECT CONTRACTTYPEID,CONTRACT_TYPE_DESC FROM MNT_REINSURANCE_CONTRACT_TYPE WHERE ISNULL(IS_ACTIVE,'Y')='Y' ORDER BY CONTRACT_TYPE_DESC  SELECT SUBLINE_CODE_ID, SUBLINE_CODE +': '+SUBLINE_CODE_DESC AS SUBLINE FROM MNT_SUBLINE_CODE ORDER BY  SUBLINE; ",ConnStr);

                /*Only USA country info is fetched and State_ID was fetched for Country=1 causing error for customer of other countries state.  (Below line commented by Asfa) */
                //SqlDataAdapter lObjDataAdapter = new SqlDataAdapter("select COUNTRY_ID,COUNTRY_CODE,COUNTRY_NAME from MNT_COUNTRY_LIST WHERE DISPLAY_ALL=0 select COUNTRY_ID,STATE_ID,STATE_CODE,STATE_NAME,IS_ACTIVE from MNT_COUNTRY_STATE_LIST WHERE COUNTRY_ID=1 select TYPEID,TYPEDESC from TODOLISTTYPES order by TYPEDESC select TRANS_TYPE_ID,TRANS_TYPE_NAME from TRANSACTIONTYPELIST order by TRANS_TYPE_NAME select  LOOKUP_VALUE_ID, LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID = 315 order by LOOKUP_VALUE_DESC select LOB_ID, LOB_DESC FROM MNT_LOB_MASTER order by LOB_DESC SELECT CONTRACTTYPEID,CONTRACT_TYPE_DESC FROM MNT_REINSURANCE_CONTRACT_TYPE WHERE ISNULL(IS_ACTIVE,'Y')='Y' ORDER BY CONTRACT_TYPE_DESC  SELECT SUBLINE_CODE_ID, SUBLINE_CODE +': '+SUBLINE_CODE_DESC AS SUBLINE FROM MNT_SUBLINE_CODE ORDER BY  SUBLINE select COUNTRY_ID,COUNTRY_CODE,COUNTRY_NAME from MNT_COUNTRY_LIST ; ",ConnStr);

                //Changed by Charles on 12-Apr-10 for Multilingual Support
                //SqlDataAdapter lObjDataAdapter = new SqlDataAdapter("select COUNTRY_ID,COUNTRY_CODE,COUNTRY_NAME from MNT_COUNTRY_LIST select COUNTRY_ID,STATE_ID,STATE_CODE,STATE_NAME,IS_ACTIVE from MNT_COUNTRY_STATE_LIST select TYPEID,TYPEDESC from TODOLISTTYPES order by TYPEDESC select TRANS_TYPE_ID,TRANS_TYPE_NAME from TRANSACTIONTYPELIST order by TRANS_TYPE_NAME select  LOOKUP_VALUE_ID, LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID = 315 order by LOOKUP_VALUE_DESC select LOB_ID, LOB_DESC FROM MNT_LOB_MASTER order by LOB_DESC SELECT CONTRACTTYPEID,CONTRACT_TYPE_DESC FROM MNT_REINSURANCE_CONTRACT_TYPE WHERE ISNULL(IS_ACTIVE,'Y')='Y' ORDER BY CONTRACT_TYPE_DESC  SELECT SUBLINE_CODE_ID, SUBLINE_CODE +': '+SUBLINE_CODE_DESC AS SUBLINE FROM MNT_SUBLINE_CODE ORDER BY  SUBLINE select COUNTRY_ID,COUNTRY_CODE,COUNTRY_NAME from MNT_COUNTRY_LIST ; ",ConnStr);
                SqlDataAdapter lObjDataAdapter = new SqlDataAdapter(@"SELECT COUNTRY_ID,COUNTRY_CODE,COUNTRY_NAME FROM MNT_COUNTRY_LIST WITH(NOLOCK) WHERE IS_ACTIVE='Y'; 
SELECT COUNTRY_ID,STATE_ID,STATE_CODE,STATE_NAME,IS_ACTIVE FROM MNT_COUNTRY_STATE_LIST WITH(NOLOCK) WHERE IS_ACTIVE='Y' and COUNTRY_ID = 7;
(SELECT TYPEID,TYPEDESC,1 AS LANG_ID FROM TODOLISTTYPES WITH(NOLOCK) UNION SELECT TYPEID,TYPEDESC,3 AS LANG_ID FROM TODOLISTTYPES WITH(NOLOCK) UNION SELECT TYPEID,TYPEDESC,LANG_ID FROM TODOLISTTYPES_MULTILINGUAL WITH(NOLOCK)) ORDER BY TYPEDESC ;
(SELECT TRANS_TYPE_ID,TRANS_TYPE_NAME,1 AS LANG_ID FROM TRANSACTIONTYPELIST WITH(NOLOCK) UNION SELECT TRANS_TYPE_ID,TRANS_TYPE_NAME,3 AS LANG_ID FROM TRANSACTIONTYPELIST WITH(NOLOCK)
UNION SELECT TRANS_TYPE_ID,TRANS_TYPE_NAME,LANG_ID FROM TRANSACTIONTYPELIST_MULTILINGUAL WITH(NOLOCK)) ORDER BY TRANS_TYPE_NAME ;
(SELECT  MLV.LOOKUP_VALUE_ID, MLV.LOOKUP_VALUE_DESC,1 AS LANG_ID FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK) WHERE MLV.LOOKUP_ID = 315 UNION
SELECT  MLV.LOOKUP_VALUE_ID, MLV.LOOKUP_VALUE_DESC,3 AS LANG_ID FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK) WHERE MLV.LOOKUP_ID = 315
UNION SELECT  MLV.LOOKUP_VALUE_ID, ML.LOOKUP_VALUE_DESC,ML.LANG_ID FROM MNT_LOOKUP_VALUES_MULTILINGUAL ML WITH(NOLOCK)
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID = ML.LOOKUP_UNIQUE_ID WHERE MLV.LOOKUP_ID = 315) ORDER BY LOOKUP_VALUE_DESC ;
(SELECT LOB_ID, LOB_DESC,3 AS LANG_ID,PRODUCT_TYPE FROM MNT_LOB_MASTER  WITH(NOLOCK) WHERE ISNULL(IS_ACTIVE,'Y')='Y') ORDER BY LOB_DESC  ;
(SELECT CONTRACTTYPEID,CONTRACT_TYPE_DESC,1 AS LANG_ID FROM MNT_REINSURANCE_CONTRACT_TYPE WITH(NOLOCK) WHERE ISNULL(IS_ACTIVE,'Y')='Y' UNION
SELECT CONTRACTTYPEID,CONTRACT_TYPE_DESC,3 AS LANG_ID FROM MNT_REINSURANCE_CONTRACT_TYPE WITH(NOLOCK) WHERE ISNULL(IS_ACTIVE,'Y')='Y' 
UNION SELECT CONTRACTTYPEID,CONTRACT_TYPE_DESC,LANG_ID FROM MNT_REINSURANCE_CONTRACT_TYPE_MULTILINGUAL WITH(NOLOCK) WHERE ISNULL(IS_ACTIVE,'Y')='Y' ) ORDER BY CONTRACT_TYPE_DESC ;
(SELECT SUBLINE_CODE_ID, SUBLINE_CODE +': '+SUBLINE_CODE_DESC AS SUBLINE, 1 AS LANG_ID FROM MNT_SUBLINE_CODE WITH(NOLOCK) UNION
SELECT SUBLINE_CODE_ID, SUBLINE_CODE +': '+SUBLINE_CODE_DESC AS SUBLINE,3 AS LANG_ID FROM MNT_SUBLINE_CODE WITH(NOLOCK) 
UNION SELECT SUBLINE_CODE_ID, SUBLINE_CODE +': '+SUBLINE_CODE_DESC AS SUBLINE,LANG_ID FROM MNT_SUBLINE_CODE_MULTILINGUAL WITH(NOLOCK))ORDER BY  SUBLINE ;
SELECT COUNTRY_ID,COUNTRY_CODE,COUNTRY_NAME FROM MNT_COUNTRY_LIST WITH(NOLOCK) WHERE IS_ACTIVE='Y' ; SELECT SUSEP_LOB_ID,SUSEP_LOB_CODE,SUSEP_LOB_DESC from MNT_SUSEP_LOB_MASTER WITH(NOLOCK);SELECT CURRENCY_ID,CURR_CODE,CURR_DESC,CURR_SYMBOL,CURR_DECIMALSEPR,CURR_THOUSANDSEPR FROM MNT_CURRENCY_MASTER WITH(NOLOCK) WHERE ISNULL(IS_ACTIVE,'Y')='Y' ;", ConnStr);

                return lObjDataAdapter;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "Transaction Information Structure property"
        //property for holding transaction info
        private stuTransactionInfo transactionInfoParams;
        public stuTransactionInfo TransactionInfoParams
        {
            set
            {
                transactionInfoParams = value;
            }
        }
        #endregion

        #region "Common Activate/Deactivate Method"
        //		/// <summary>
        //		/// Author: Ajit
        //		/// Dated: 4 Apr, 2005
        //		/// Activate or deactivates based on code parameter, without transaction log.
        //		/// </summary>
        //		/// <param name="CODE"></param>
        //		/// <param name="IS_ACTIVE"></param>
        //		/// if value of CODE "Y" then is activated else deactivated if "N"
        public virtual void ActivateDeactivate(string strCode, string isActive, Cms.Model.Maintenance.ClsTransactionInfo objTransaction)
        { }
        //			DataWrapper objDataWrapper			=	new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
        //			try
        //			{
        //			
        //				objDataWrapper.AddParameter("@CODE",strCode);
        //				objDataWrapper.AddParameter("@IS_ACTIVE",isActive);
        //				objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure);
        //				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //			}
        //			catch(Exception ex)
        //			{
        //				throw ex;
        //			}
        //			finally
        //			{
        //				if(objDataWrapper != null) objDataWrapper.Dispose();
        //			}
        //		}

        /// <summary>
        /// Author: Ajit
        /// Dated: 4 Apr, 2005
        /// Activate or deactivates based on code parameter, with transaction log.
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="isActive"></param>
        /// <param name="objstrTransactionInfo"></param>
        public virtual string ActivateDeactivate(string strCode, string isActive)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CODE", strCode);
                objDataWrapper.AddParameter("@IS_ACTIVE", isActive);
                SqlParameter objPaam = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL", System.Data.DbType.Int16, System.Data.ParameterDirection.ReturnValue);

                //objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure,objTranasction);

                if (this.TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransaction = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransaction.RECORDED_BY = transactionInfoParams.loggedInUserId;
                    objTransaction.RECORDED_BY_NAME = transactionInfoParams.loggedInUserName;
                    objTransaction.CLIENT_ID = transactionInfoParams.clientId;
                    objTransaction.TRANS_TYPE_ID = 2;
                    objTransaction.TRANS_DESC = transactionInfoParams.transactionDescription;
                    objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure, objTransaction);
                }
                else
                {
                    objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (objPaam != null)
                {
                    return (objPaam.Value.ToString());
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        // Overload added by Swarup 27/06/07
        public virtual string ActivateDeactivate(string strCode, string isActive, string strCustomInfo)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CODE", strCode);
                objDataWrapper.AddParameter("@IS_ACTIVE", isActive);
                SqlParameter objPaam = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL", System.Data.DbType.Int16, System.Data.ParameterDirection.ReturnValue);

                //objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure,objTranasction);

                if (this.TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransaction = new Cms.Model.Maintenance.ClsTransactionInfo();
                    // objTransaction.TRANS_TYPE_ID = transactionID;
                    objTransaction.TRANS_TYPE_ID = 0;
                    objTransaction.RECORDED_BY = transactionInfoParams.loggedInUserId;
                    objTransaction.RECORDED_BY_NAME = transactionInfoParams.loggedInUserName;
                    objTransaction.CLIENT_ID = transactionInfoParams.clientId;

                    objTransaction.TRANS_DESC = transactionInfoParams.transactionDescription;
                    objTransaction.CUSTOM_INFO = strCustomInfo;
                    objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure, objTransaction);
                }
                else
                {
                    objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (objPaam != null)
                {
                    return (objPaam.Value.ToString());
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }


        #endregion

        #region FUNCTION FOR WEBGRID CONTROL
        public DataSet ExecuteGridQuery(string type)
        {
            return ExecuteGridQuery(type, "");

        }
        public DataSet ExecuteGridQuery(string type, string AgencyID)
        {
            DataSet dsGrid = new DataSet();
            string strStoredProc = "Proc_ExecuteGridQuery";
            DateTime RecordDate = DateTime.Now;
            //int			returnResult;

            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(ConnGridStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.AddParameter("@TYPE", type);

                if (AgencyID.Trim() != "")
                    objDataWrapper.AddParameter("@AGENCYID", AgencyID);

                objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);//Added by Charles on 25-May-2010 for Multilingual Support 

                dsGrid = objDataWrapper.ExecuteDataSet(strStoredProc);

                return dsGrid;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsGrid.Dispose();
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add ClsCommon.Dispose implementation
            if (lObjCustomizedMessage != null) lObjCustomizedMessage = null;
        }

        #endregion

        #region "SqlHelper wrapper functions to execute ad-hoc inline queries from presentation layer"
        public static DataSet ExecuteDataSet(string selectStatement)
        {
            return DataWrapper.ExecuteDataset(ConnStr, CommandType.Text, selectStatement, null);
        }
        #endregion

        #region "Fill Drop down Functions"
        /// <summary>
        /// This function is used to fill Time Zone Drop down on page
        /// </summary>
        /// <param name="objDropDownList"></param>
        public static void GetTimeZoneDropDown(DropDownList objDropDownList)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
            DataTable objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillTimeZoneDropDown").Tables[0];
            objDropDownList.DataSource = objDataTable;
            objDropDownList.DataTextField = "TIME_DESC";
            objDropDownList.DataValueField = "TIME_CODE";
            objDropDownList.DataBind();
        }

        #endregion

        #region Workfow Control function
        public static string GetWorkflowScreens(string screenID)
        {
            string strStoredProc = "Proc_GetWorkflowScreens";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@SCREEN_ID", screenID);
            try
            {
                return objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0].Rows[0]["WORKFLOW_SCREENS"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
        public static DataSet GetScreenDetails(string screenID)
        {
            string strStoredProc = "Proc_GetMNT_WORKFLOW_LIST";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@SCREEN_ID", screenID);
            objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID); //Added by Charles on 13-Apr-2010 for Multilingual Support

            try
            {
                return objDataWrapper.ExecuteDataSet(strStoredProc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
        public static DataSet GetLOBWiseScreenList(string strLOBCode, int CountryId)
        {
            string strStoredProc = "Proc_GetLOBWiseScreenList";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LOBCode", strLOBCode);
            if (CountryId != 0)
                objDataWrapper.AddParameter("@CountryID", CountryId);

            try
            {
                return objDataWrapper.ExecuteDataSet(strStoredProc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static int GetCountSql(string sqlString)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text);
            try
            {
                return objDataWrapper.ExecuteDataSet(sqlString).Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
        #endregion

        #region Reading Key From Web.Config
        /// <summary>
        /// Function For Reading keys from web.config and adding IP Address with the key
        /// </summary>
        /// <param name="strKeyName">KeyName</param>
        /// <returns>KeyValue with IP address</returns>
        /*public static string GetKeyValueWithIP(string strKeyName)
        {
            string strKeyValue;
            string strURLKeyValue;

            strKeyValue		=	System.Configuration.ConfigurationSettings.AppSettings.Get(strKeyName).ToString();
            strURLKeyValue	=	System.Configuration.ConfigurationSettings.AppSettings.Get("CmsWebUrl").ToString();
			
            return strURLKeyValue + strKeyValue;
        }*/

        public static string GetKeyValueWithIP(string strKeyName)
        {
            string ConfigFilePath;
            if (mIsEODProcess == true)
            {
                string strTemp = mConfigFileName.Replace("/", @"\");
                ConfigFilePath = mWebAppUNCPath + @"\" + strTemp;
                ConfigFilePath = System.IO.Path.GetFullPath(ConfigFilePath);
            }
            else
            {
                ConfigFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + System.Configuration.ConfigurationManager.AppSettings.Get("ConfigFile").ToString());
            }

            XmlDocument ConfigDoc = new XmlDocument();
            ConfigDoc.Load(ConfigFilePath);

            XmlNode rootNode = ConfigDoc.SelectSingleNode("Configuration");
            XmlNode pathNode = rootNode.SelectSingleNode("Parameters[@Type='Path']");
            XmlNode configNode = pathNode.SelectSingleNode("Parameter[@Name='" + strKeyName + "']");
            string KeyValue = configNode.Attributes["Value"].Value.Trim();
            return System.IO.Path.GetFullPath(mWebAppUNCPath + @"\" + KeyValue);

        }
        public static string GetPdfTemplatePath(string strTempPath,string strKeyName)
        {
            string ConfigFilePath;
            if (mIsEODProcess == true)
            {
                string strTemp = mConfigFileName.Replace("/", @"\");
                ConfigFilePath = mWebAppUNCPath + @"\" + strTemp;
                ConfigFilePath = System.IO.Path.GetFullPath(ConfigFilePath);
            }
            else
            {
                ConfigFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + System.Configuration.ConfigurationManager.AppSettings.Get("ConfigFile").ToString());
            }

            XmlDocument ConfigDoc = new XmlDocument();
            ConfigDoc.Load(ConfigFilePath);

            XmlNode rootNode = ConfigDoc.SelectSingleNode("Configuration");
            XmlNode pathNode = rootNode.SelectSingleNode("Parameters[@Type='Path']");
            XmlNode configNode = pathNode.SelectSingleNode("Parameter[@Name='" + strKeyName + "']");
            string KeyValue = configNode.Attributes["Value"].Value.Trim();
            return System.IO.Path.GetFullPath(strTempPath + @"\" + KeyValue);

        }
        

        /// <summary>
        /// Function For Reading keys from web.config
        /// </summary>
        /// <param name="strKeyName">KeyName</param>
        /// <returns>KeyValue</returns>
        public static string GetKeyValue(string strKeyName)
        {
            string ConfigFilePath;
            if (mIsEODProcess == true)
            {
                string strTemp = mConfigFileName.Replace("/", @"\");
                ConfigFilePath = mWebAppUNCPath + @"\" + strTemp;
                ConfigFilePath = System.IO.Path.GetFullPath(ConfigFilePath);
            }
            else
            {
                ConfigFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + System.Configuration.ConfigurationManager.AppSettings.Get("ConfigFile").ToString());
            }

            XmlDocument ConfigDoc = new XmlDocument();
            ConfigDoc.Load(ConfigFilePath);

            XmlNode rootNode = ConfigDoc.SelectSingleNode("Configuration");
            XmlNode pathNode = rootNode.SelectSingleNode("Parameters[@Type='Display']");
            XmlNode configNode = pathNode.SelectSingleNode("Parameter[@Name='" + strKeyName + "']");
            string KeyValue = configNode.Attributes["Value"].Value.Trim();
            return KeyValue;
        }

        public static string GetKeyValueForSetup(string strKeyName)
        {
            string ConfigFilePath;
            if (mIsEODProcess == true)
            {
                string strTemp = mConfigFileName.Replace("/", @"\");
                ConfigFilePath = mWebAppUNCPath + @"\" + strTemp;
                ConfigFilePath = System.IO.Path.GetFullPath(ConfigFilePath);
            }
            else
            {
                ConfigFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + System.Configuration.ConfigurationManager.AppSettings.Get("ConfigFile").ToString());
            }

            XmlDocument ConfigDoc = new XmlDocument();
            ConfigDoc.Load(ConfigFilePath);

            XmlNode rootNode = ConfigDoc.SelectSingleNode("Configuration");
            XmlNode pathNode = rootNode.SelectSingleNode("Parameters[@Type='Setup']");
            XmlNode configNode = pathNode.SelectSingleNode("Parameter[@Name='" + strKeyName + "']");
            string KeyValue = configNode.Attributes["Value"].Value.Trim();
            return KeyValue;
        }
        #endregion

        # region "Function call eligible process"

        /// <summary>
        /// Retreivies the list of eligible process of the specified policy
        /// </summary>
        /// <param name="intCustomerID">CustomerID</param>
        /// <param name="intPolicyID">Policy ID</param>
        /// <param name="intPolicyVersionID">Policy Version ID</param>
        /// <returns>Returns the dataset of eligible process</returns>
        public DataSet GetEligibleProcess(int intCustomerID, int intPolicyID, int intPolicyVersionID)
        {

            string strStoredProc = "Proc_GetEligibleProcess";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", intPolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", intPolicyVersionID);
                objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }


        #endregion

        # region R E I N S U R A N C E   C O M P A N Y   N A M E S

        public static DataTable GetReinsuranceCompanyNames()
        {
            try
            {
                DataSet lobjDs = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "Proc_Get_REIN_COMAPANY_NAME");
                return lobjDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        # endregion

        #region Function to replace a node value in a given XML string
        public static string ReplaceNodeValue(string strXml, string strNode, string strNewValue)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xNode;
            try
            {
                xDoc.LoadXml(strXml);
                xNode = xDoc.SelectSingleNode(strNode);
                if (xNode != null && xNode.InnerText != "")
                {
                    xNode.InnerText = strNewValue;
                }
                return strXml;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                xDoc = null;
                xNode = null;
            }
        }

        #endregion

        #region Function to remove a node value in a given XML string
        public static string RemoveNode(string strXml, string strNode)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xNode;
            try
            {
                if (strXml.Trim() != "")
                {
                    xDoc.LoadXml(strXml);
                    xNode = xDoc.SelectSingleNode(strNode);
                    if (xNode != null)
                    {
                        xDoc.DocumentElement.RemoveChild(xNode);
                    }
                    return xDoc.InnerXml.ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                xDoc = null;
                xNode = null;
            }
        }


        #endregion

        #region Function to remove a node value in a given XML string
        public static string ReplaceNodeAttributeValue(string strXml, string strNode, string strAttributeName, string NewValue)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xNode;
            try
            {
                if (strXml.Trim() != "")
                {
                    xDoc.LoadXml(strXml);
                    xNode = xDoc.SelectSingleNode(strNode);
                    if (xNode != null && xNode.Attributes[strAttributeName] != null)
                    {
                        xNode.Attributes[strAttributeName].Value = NewValue;
                    }
                    return xDoc.InnerXml.ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                xDoc = null;
                xNode = null;
            }
        }


        #endregion

        #region Function to remove a node value in a given XML string of nodelist
        public static string ReplaceNodeListAttributeValue(string strXml, string strNode, string strRootPath, string strAttributeName, string NewValue)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xNode;
            XmlNodeList xNodeList;
            try
            {

                if (strXml.Trim() != "")
                {
                    xDoc.LoadXml(strXml);
                    xNodeList = xDoc.SelectNodes(strRootPath);
                    for (int i = 0; i < xNodeList.Count; i++)
                    {
                        xNode = xNodeList.Item(i).SelectSingleNode(strNode);
                        if (xNode != null && xNode.Attributes[strAttributeName] != null)
                        {
                            xNode.Attributes[strAttributeName].Value = NewValue;
                        }
                    }
                    return xDoc.InnerXml.ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                xDoc = null;
                xNode = null;
            }
        }


        #endregion


        #region LOB Enumeration to be used at Business Layer
        public enum enumLOB
        {
            HOME = 1,
            AUTOP = 2,
            CYCL = 3,
            BOAT = 4,
            UMB = 5,
            REDW = 6,
            GENL = 7,
            Motor = 38
        }
        public enum enumYESNO_LOOKUP_UNIQUE_ID
        {
            YES = 10963,
            NO = 10964

        }
        public enum enumRULES_VERIFICATION_STATUS
        {
            REFERRED_OR_ALL_VERIFIED = 0,
            REJECTED_OR_MANDATORY_DATA_MISSING = 1
        }
        #endregion

        #region Functions to work with Multiple-Selection Listbox
        /// <summary>
        /// General function to obtain selected values from multi-select listbox adding a user-specified 
        /// delimiter
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="Delimiter"></param>
        /// <returns></returns>
        public static string GetDelimitedValuesFromListbox(System.Web.UI.WebControls.ListBox combo, char Delimiter)
        {
            string strPAYEE = "";
            if (combo != null && combo.Items.Count > 0)
            {
                foreach (ListItem li in combo.Items)
                {
                    if (li.Selected)
                    {
                        strPAYEE = strPAYEE + li.Value + Delimiter.ToString();
                    }
                }
                if (strPAYEE.Length > 0)
                    strPAYEE = strPAYEE.Substring(0, (strPAYEE.Length - 1));
            }
            return strPAYEE;
        }
        public static string GetDelimitedTextValuesFromListbox(System.Web.UI.WebControls.ListBox combo, char Delimiter)
        {
            string strPAYEE = "";
            if (combo != null && combo.Items.Count > 0)
            {
                foreach (ListItem li in combo.Items)
                {
                    if (li.Selected)
                    {
                        strPAYEE = strPAYEE + li.Text + Delimiter.ToString();
                    }
                }
                if (strPAYEE.Length > 0)
                    strPAYEE = strPAYEE.Substring(0, (strPAYEE.Length - 1));
            }
            return strPAYEE;
        }
        //Added For Itrack 5124
        public static string GetDelimitedFromListbox(System.Web.UI.HtmlControls.HtmlSelect combo, char Delimiter)
        {
            string strPAYEE = "";
            if (combo != null && combo.Items.Count > 0)
            {
                foreach (ListItem li in combo.Items)
                {
                    if (li.Selected)
                    {
                        string[] strVal = li.Value.Split('^');
                        if (strVal.Length > 0)
                        {
                            strPAYEE = strPAYEE + strVal[0].ToString() + Delimiter.ToString();
                        }
                    }
                }
                if (strPAYEE.Length > 0)
                    strPAYEE = strPAYEE.Substring(0, (strPAYEE.Length - 1));
            }
            return strPAYEE;
        }
        //Overload Function
        public static string GetDelimitedTextValuesFromListbox(System.Web.UI.HtmlControls.HtmlSelect combo, char Delimiter)
        {
            string strPAYEE = "";
            if (combo != null && combo.Items.Count > 0)
            {
                foreach (ListItem li in combo.Items)
                {
                    if (li.Selected)
                    {
                        strPAYEE = strPAYEE + li.Text + Delimiter.ToString();
                    }
                }
                if (strPAYEE.Length > 0)
                    strPAYEE = strPAYEE.Substring(0, (strPAYEE.Length - 1));
            }
            return strPAYEE;
        }

        public static void SelectTextValuesAtCombobox(System.Web.UI.WebControls.DropDownList cmbDDL, string strSelectedValues, char Delimiter)
        {
            bool BreakLoop = false;
            for (int a = 0; a < cmbDDL.Items.Count; a++)
            {
                string[] tmpValues = cmbDDL.Items[a].Value.Split(Delimiter);

                for (int i = 0; i < tmpValues.Length; i++)
                {
                    if (tmpValues[i] == strSelectedValues)
                    {
                        cmbDDL.Items[a].Selected = true;
                        BreakLoop = true;
                        continue;
                    }
                }
                if (BreakLoop)
                    break;
            }
        }


        /// <summary>
        /// Function selects value at multi-select listbox given selected values in string with delimiter
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="strSelectedValues"></param>
        /// <param name="Delimiter"></param>
        public static void SelectValuesAtListbox(System.Web.UI.WebControls.ListBox combo, string strSelectedValues, char Delimiter)
        {
            if (strSelectedValues == "" || strSelectedValues.Length < 1 || combo == null || combo.Items.Count < 1 || Delimiter.ToString() == "")
                return;
            string[] strPayees = strSelectedValues.Split(Delimiter);
            for (int i = 0; i < strPayees.Length; i++)
            {
                for (int j = 0; j < combo.Items.Count; j++)
                {
                    if (combo.Items[j].Value == strPayees[i].ToString())
                    {
                        combo.Items[j].Selected = true;
                        continue;
                    }

                }
            }
        }
        public static void SelectTextValuesAtListbox(System.Web.UI.WebControls.ListBox combo, string strSelectedTextValues, char Delimiter)
        {
            if (strSelectedTextValues == "" || strSelectedTextValues.Length < 1 || combo == null || combo.Items.Count < 1 || Delimiter.ToString() == "")
                return;
            string[] strPayees = strSelectedTextValues.Split(Delimiter);
            for (int i = 0; i < strPayees.Length; i++)
            {
                for (int j = 0; j < combo.Items.Count; j++)
                {
                    if (combo.Items[j].Text == strPayees[i].ToString())
                    {
                        combo.Items[j].Selected = true;
                        continue;
                    }

                }
            }

        }
        //Added For Itrack 5124
        //public static void SelectTextAtListbox(System.Web.UI.HtmlControls.HtmlSelect  combo, string strSelectedTextValues, char Delimiter,string REQ_SPECIAL_HANDLING)
        public static void SelectTextAtListbox(System.Web.UI.HtmlControls.HtmlSelect combo, string strSelectedTextValues, char Delimiter)
        {
            if (strSelectedTextValues == "" || strSelectedTextValues.Length < 1 || combo == null || combo.Items.Count < 1 || Delimiter.ToString() == "")
                return;
            string[] strPayees = strSelectedTextValues.Split(Delimiter);
            for (int i = 0; i < strPayees.Length; i++)
            {
                for (int j = 0; j < combo.Items.Count; j++)
                {
                    //if(combo.Items[j].Value  == strPayees[i].ToString() + "^" + REQ_SPECIAL_HANDLING)
                    if (combo.Items[j].Value.Trim() != "")
                    {
                        string[] temp = combo.Items[j].Value.Split(Delimiter);
                        if (temp[0] == strPayees[i].ToString())
                        {
                            combo.Items[j].Selected = true;
                            continue;
                        }
                    }

                }
            }

        }
        //Overloaded Function 
        public static void SelectTextValuesAtListbox(System.Web.UI.HtmlControls.HtmlSelect cmbPAYE, string strSelectedTextValues, char Delimiter)
        {
            if (strSelectedTextValues == "" || strSelectedTextValues.Length < 1 || cmbPAYE == null || cmbPAYE.Items.Count < 1 || Delimiter.ToString() == "")
                return;
            string[] strPayees = strSelectedTextValues.Split(Delimiter);
            for (int i = 0; i < strPayees.Length; i++)
            {
                for (int j = 0; j < cmbPAYE.Items.Count; j++)
                {
                    if (cmbPAYE.Items[j].Text == strPayees[i].ToString())
                    {
                        cmbPAYE.Items[j].Selected = true;
                        continue;
                    }

                }
            }

        }

        #endregion


        #region Function EvalNode,GetEffectiveMasterRules,GetEffectiveRules

        /// <summary>
        /// Returns Effective Master Rules 
        /// </summary>
        /// <param name="parentNode">Parent Group Node</param>
        /// <param name="AppEffectiveDate">Application Effective Date</param>
        /// <param name="StateId">State ID</param>
        /// <returns>Array List Of Master Rules</returns>

        public ArrayList GetEffectiveMasterRules(XmlNode parentNode, DateTime AppEffectiveDate, string StateId)
        {
            //XmlNodeList ruleNodes = new XmlNodeList();
            ArrayList ruleNodes = new ArrayList();

            DateTime startDate, endDate;
            XmlNodeList masterNodes = parentNode.SelectNodes("Rule[@Action='Master']");
            foreach (XmlNode effectiveRule in masterNodes)
            {
                startDate = Convert.ToDateTime(effectiveRule.Attributes["StartDate"].Value);
                endDate = Convert.ToDateTime(effectiveRule.Attributes["EndDate"].Value);
                if (AppEffectiveDate >= startDate && AppEffectiveDate <= endDate)
                {
                    if (effectiveRule.Attributes["STATE_ID"] == null ||
                        effectiveRule.Attributes["STATE_ID"].Value == StateId)
                    {
                        ruleNodes.Add(effectiveRule);
                    }
                }
            }
            return ruleNodes;

        }

        /// <summary>
        /// Returns an array list containing XmlNodes(Database Rules)  from GroupNode 
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="AppEffectiveDate"></param>
        /// <returns></returns>
        public ArrayList GetEffectiveRules(XmlNode parentNode, DateTime AppEffectiveDate, string StateId)
        {
            //XmlNodeList ruleNodes = new XmlNodeList();
            ArrayList ruleNodes = new ArrayList();

            DateTime startDate, endDate;
            XmlNodeList dataNodes = parentNode.SelectNodes("Rule[@Action='Database']");
            foreach (XmlNode effectiveRule in dataNodes)
            {
                startDate = Convert.ToDateTime(effectiveRule.Attributes["StartDate"].Value);
                endDate = Convert.ToDateTime(effectiveRule.Attributes["EndDate"].Value);
                if (AppEffectiveDate >= startDate && AppEffectiveDate <= endDate)
                {
                    if (effectiveRule.Attributes["STATE_ID"] == null ||
                        effectiveRule.Attributes["STATE_ID"].Value == StateId)
                    {
                        ruleNodes.Add(effectiveRule);
                    }
                }
            }
            return ruleNodes;

        }

        /// <summary>
        /// Evaluate An XML Condition Node
        /// </summary>
        /// <param name="node">XML Condition Node </param>
        /// <returns>Result Of Expression In Condition Node</returns>
        public string EvalNode(XmlNode node, Hashtable masterKeys)
        {
            string Operand1 = node.Attributes["Operand1"].Value;
            string Operand2 = node.Attributes["Operand2"].Value;
            string Operator = node.Attributes["Operator"].Value;
            string strTemp = "";

            if (Operand1.StartsWith("$"))
            {
                strTemp = Operand1.Substring(Operand1.IndexOf('$') + 1);
                Operand1 = masterKeys[strTemp].ToString();
                //Split the Operand if it contains Split Amount 
                string[] strSplits = Operand1.Split('/');
                if (strSplits.Length > 0)
                {
                    string[] strAmt1 = strSplits[0].Split(' ');
                    if (strAmt1.Length > 0)
                    {
                        if (strAmt1[0].Trim() != "")
                        {
                            Operand1 = strAmt1[0];
                        }
                    }
                }
            }
            strTemp = "";
            if (Operand2.StartsWith("$"))
            {
                strTemp = Operand2.Substring(Operand2.IndexOf('$') + 1);
                Operand2 = masterKeys[strTemp].ToString();

                //Split the Operand if it contains Split Amount 
                string[] strSplits = Operand2.Split('/');
                if (strSplits.Length > 0)
                {
                    string[] strAmt1 = strSplits[0].Split(' ');
                    if (strAmt1.Length > 0)
                    {
                        if (strAmt1[0].Trim() != "")
                        {
                            Operand2 = strAmt1[0];
                        }
                    }
                }
            }
            //For comparing Strings 
            if (node.Attributes["OperandType"] != null)
            {
                string OperandType = node.Attributes["OperandType"].Value;
                if (OperandType == "String")
                {
                    bool result = false;
                    if (Operator == "==")
                    {
                        if (Operand1 == Operand2)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    if (Operator == "!=")
                    {
                        if (Operand1 == Operand2)
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                    return result.ToString();
                }
            }//end of String comparision

            //Check if the operand starts with "$" sign if yes fetch value from 
            //respective key from key value pair


            if (Operand1.Trim() != "" && Operand2.Trim() != "" && Operator.Trim() != "")
            {
                RPNParser parser = new RPNParser();
                ArrayList arrExpr = parser.GetPostFixNotation(Operand1 + Operator + Operand2,
                    Type.GetType("System.Double"), false);
                string szResult = parser.Convert2String(arrExpr);
                return parser.EvaluateRPN(arrExpr, Type.GetType("System.Double"), null).ToString();
            }
            return "";

        }


        protected void SetKeys(XmlNodeList toSetNodeArray, Hashtable masterKeys)
        {
            foreach (XmlNode toSetNode in toSetNodeArray)
            {
                string strKey = toSetNode.Attributes["Key"].Value;
                string strValue = toSetNode.Attributes["Value"].Value;
                if (strKey.StartsWith("$"))
                {
                    strKey = strKey.Substring(strKey.IndexOf('$') + 1);
                }
                if (strValue.Trim() == "")
                {
                    strValue = EvalNode(toSetNode, masterKeys);
                }
                else if (strValue.StartsWith("$"))
                {
                    strValue = strValue.Substring(strValue.IndexOf('$') + 1);
                    strValue = masterKeys[strValue].ToString();
                }


                if (masterKeys.ContainsKey(strKey))
                {
                    masterKeys.Remove(strKey);
                }
                masterKeys.Add(strKey, strValue);
            }
        }


        #endregion
        #region  process information of a policy version
        public DataTable GetPolicyProcessInfo(int CustomerID, int PolId, int PolVersionId)
        {

            string strStoredProc = "Proc_GetPolicyProcessInfo";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);

                DataSet dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper.Dispose();
            }
        }
        public DataTable GetPolicyStatusInfo(int CustomerID, int PolId, int PolVersionId)
        {

            string strStoredProc = "Proc_GetPolicyProcessInfo";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);

                DataSet dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
                return dsTemp.Tables[1];
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper.Dispose();
            }
        }
        public DataTable GetPolicyMaxVerInfo(int CustomerID, int PolId, int PolVersionId)
        {

            string strStoredProc = "Proc_GetPolicyProcessInfo";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);

                DataSet dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
                return dsTemp.Tables[2];
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper.Dispose();
            }
        }
        public DataTable GetEndoPrePolicyMaxVerInfo(int CustomerID, int PolId, int PolVersionId)
        {

            string strStoredProc = "Proc_GetPolicyProcessInfo";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);

                DataSet dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
                return dsTemp.Tables[3];
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper.Dispose();
            }
        }
        public DataTable GetPolicyFromConversionInfo(int CustomerID, int PolId, int PolVersionId)
        {
            string strStoredProc = "PROC_GetPolicyFromConversionInfo";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);

                DataSet dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
                return dsTemp.Tables[0];
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper.Dispose();
            }
        }
        #endregion

        #region GetLOB_ID procedures for application and policy
        public static int GetPolicyLOBID(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            return GetPolicyLOBID(CustomerID, PolicyID, PolicyVersionID, null);
        }
        public static int GetPolicyLOBID(int CustomerID, int PolicyID, int PolicyVersionID, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_GetPolicyLOBID";
            DataSet dsTemp;
            if (objDataWrapper == null)
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0 && dsTemp.Tables[0].Rows[0][0] != null && dsTemp.Tables[0].Rows[0][0].ToString() != "")
                    return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //				if(objDataWrapper != null) 
                //				{
                //					objDataWrapper.Dispose();
                //				}
            }
        }

        public static int GetApplicationLOBID(int CustomerID, int AppID, int AppVersionID)
        {
            return GetApplicationLOBID(CustomerID, AppID, AppVersionID, null);
        }

        public static int GetApplicationLOBID(int CustomerID, int AppID, int AppVersionID, DataWrapper objDataWrapper)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetApplicationLOB");
                if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0 && dsTemp.Tables[0].Rows[0][0] != null && dsTemp.Tables[0].Rows[0][0].ToString() != "")
                    return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                else
                    return 0;
            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }
        #endregion
        #region GetAppefectivedate and State Info
        public static string GetApplicationDateadState(int CustomerID, int AppID, int AppVersionID)
        {
            return GetApplicationDateadState(CustomerID, AppID, AppVersionID, null);
        }

        public static string GetApplicationDateadState(int CustomerID, int AppID, int AppVersionID, DataWrapper objDataWrapper)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetApplicationLOB");
                if (dsTemp != null && dsTemp.Tables.Count > 0)
                    return dsTemp.Tables[0].Rows[0][3].ToString() + "^" + dsTemp.Tables[0].Rows[0][4].ToString();
                else
                    return "0";
            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }
        public static string GetPolicyDateadState(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            return GetPolicyDateadState(CustomerID, PolicyID, PolicyVersionID, null);
        }
        public static string GetPolicyDateadState(int CustomerID, int PolicyID, int PolicyVersionID, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_GetPolicyLOBID";
            DataSet dsTemp;
            if (objDataWrapper == null)
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp != null && dsTemp.Tables.Count > 0)
                    return dsTemp.Tables[0].Rows[0][1].ToString() + "^" + dsTemp.Tables[0].Rows[0][2].ToString();
                else
                    return "0";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //				if(objDataWrapper != null) 
                //				{
                //					objDataWrapper.Dispose();
                //				}
            }
        }


        #endregion

        #region GetLOBXML

        public static string GetXmlForLobByState()
        {
            string strSql = "Proc_GetLobAndSubLOBByState";
            string strReturnValue;

            DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, strSql);

            if (objDataSet.Tables[0].Rows.Count == 0)
            {
                strReturnValue = "";
            }
            else
            {
                strReturnValue = objDataSet.GetXml();
            }

            return strReturnValue;
        }

        /// <summary>
        /// Gets LOB and SUB LOB
        /// </summary>
        /// <returns></returns>
        /// Added by Charles on 16-Mar-2010 for Policy Page Implementation
        public static string GetXmlForLobWithoutState()
        {
            string strSql = "Proc_GetLobAndSubLobWithoutState";
            string strReturnValue;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);

            DataSet objDataSet = null;
            objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);

            objDataSet = objDataWrapper.ExecuteDataSet(strSql);

            if (objDataSet.Tables[0].Rows.Count == 0)
            {
                strReturnValue = "";
            }
            else
            {
                strReturnValue = objDataSet.GetXml();
            }

            return strReturnValue;
            
        }

        //Added by Ruchika Chauhan on 30-Jan-2012 for TFS # 836
        public static DataSet GetSubLOBs(int LOB_ID)
        {
            string strSql = "Proc_GetSubLOBs";            
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);

            DataSet objDataSet = null;
            objDataWrapper.AddParameter("@LOBID", LOB_ID);
            objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);            

            objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }

        #region GetCLASSXML

        public static string GetXmlForClass()
        {
            string strSql = "Proc_GetClassOnStateId";
            string strReturnValue;

            DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, strSql);

            if (objDataSet.Tables[0].Rows.Count == 0)
            {
                strReturnValue = "";
            }
            else
            {
                strReturnValue = objDataSet.GetXml();
            }

            return strReturnValue;
        }
        #endregion
        /// <summary>
        /// Following method returns Lob Code corresponding to a LobId
        /// </summary>
        /// <param name="gstrLobID"></param>
        /// <returns>LobCode</returns>
        public static string GetLobCodeForLobId(string gstrLobID)
        {
            string strLOBCODE = "";

            if (gstrLobID == ((int)enumLOB.AUTOP).ToString())
                strLOBCODE = "PPA";
            else if (gstrLobID == ((int)enumLOB.HOME).ToString())
                strLOBCODE = "HOME";
            else if (gstrLobID == ((int)enumLOB.BOAT).ToString())
                strLOBCODE = "WAT";
            else if (gstrLobID == ((int)enumLOB.CYCL).ToString())
                strLOBCODE = "MOT";
            else if (gstrLobID == ((int)enumLOB.REDW).ToString())
                strLOBCODE = "RENT";
            else if (gstrLobID == ((int)enumLOB.UMB).ToString())
                strLOBCODE = "UMB";
            else if (gstrLobID == ((int)enumLOB.Motor).ToString())
                strLOBCODE = "Motor";
            return strLOBCODE;

        }
        #endregion
        #region RANDOM CODE
        /// <summary>
        /// Generating DRIVER_CODE based on driver first name , last name 
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(string FirstName, string LastName)
        {
            Random randam = new Random();
            string randomCode = randam.Next(100).ToString();
            if (FirstName.Length < 1 && LastName.Length < 1)
                return "";
            string firstpart = FirstName;
            string secpart = LastName;

            if (firstpart.Length > 3)
                firstpart = firstpart.Substring(0, 3);
            if (secpart.Length > 3)
                secpart = secpart.Substring(0, 3);

            return firstpart + secpart + randomCode;
        }
        #endregion

        #region encription/decription of text
        // by Pravesh

        // Encrypt a string into a string using a password 
        public static string EncryptString(string strText)
        {
            string Password = "EBIXINDIA";
            // First we need to turn the input string into a byte array. 
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(strText);
            // Then, need to turn the password into Key and IV 
            //here i  using salt to make it harder to guess our key using a dictionary attack - 
            // trying to guess a password by enumerating all possible words. 
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            // Now get the key/IV and do the encryption using the function that accepts byte arrays. 
            // Using PasswordDeriveBytes object here are first getting 32 bytes for the Key 
            // (the default Rijndael key length is 256bit = 32bytes) and then 16 bytes for the IV. 
            // IV should always be the block size, which is by default 16 bytes (128 bit) for Rijndael. 

            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            // Now we need to turn the resulting byte array into a string. 
            // Here I m going to be using Base64 encoding that is designed exactly for what we are trying to do. 

            return Convert.ToBase64String(encryptedData);

        }
        // Encrypt a byte array into a byte array using a key and an IV 

        private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream that is going to accept the encrypted bytes 
            MemoryStream ms = new MemoryStream();
            // Create a symmetric algorithm. 
            // We are going to use Rijndael because it is strong and available on all platforms. 
            Rijndael alg = Rijndael.Create();
            // Now set the key and the IV. 
            // following block of plaintext. This is done to make encryption more secure. 
            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be pumping our data. 
            // CryptoStreamMode.Write means that we are going to be writing data to the stream 
            // and the output will be written in the MemoryStream we have provided. 

            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            // Write the data and make it do the encryption 
            cs.Write(clearData, 0, clearData.Length);
            // Close the crypto stream (or do FlushFinalBlock). 
            cs.Close();
            // Now get the encrypted data from the MemoryStream. 
            byte[] encryptedData = ms.ToArray();
            return encryptedData;

        }

        // Decrypt a string into a string using a password 

        public static string DecryptString(string strText)
        {
            string Password = "EBIXINDIA";
            // First  need to turn the input string into a byte array. 
            // We presume that Base64 encoding was used 
            if (strText.Trim() == "") return "";
            byte[] cipherBytes = Convert.FromBase64String(strText);

            // Then, need to turn the password into Key and IV 
            // here using salt to make it harder to guess our key using a dictionary attack - 
            // trying to guess a password by enumerating all possible words. 

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            // Now get the key/IV and do the decryption using the function that accepts byte arrays. 
            // Using PasswordDeriveBytes object we are first getting 32 bytes for the Key 
            // (the default Rijndael key length is 256bit = 32bytes) and then 16 bytes for the IV. 
            // IV should always be the block size, which is by default 16 bytes (128 bit) for Rijndael. 

            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            // Now we need to turn the resulting byte array into a string. 
            // We are going to be using Base64 encoding that is designed exactly for what we are using 

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }
        // Decrypt a byte array into a byte array using a key and an IV 

        private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream that is going to accept the decrypted bytes 

            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. 
            // We are going to use Rijndael because it is strong and available on all platforms. 
            Rijndael alg = Rijndael.Create();
            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because the algorithm is operating in its default 
            // mode called CBC (Cipher Block Chaining). The IV is XORed with the first block (8 byte) 
            // of the data after it is decrypted, and then each decrypted block is XORed with the previous 
            // cipher block. This is done to make encryption more secure. 

            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be pumping our data. 
            // CryptoStreamMode.Write means that we are going to be writing data to the stream 
            // and the output will be written in the MemoryStream we have provided. 

            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            // Write the data and make it do the decryption 
            cs.Write(cipherData, 0, cipherData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our decryption and there is no more data coming in, 
            // and it is now remove the padding and finalize the decryption process. 

            cs.Close();

            // Now get the decrypted data from the MemoryStream. 
            byte[] decryptedData = ms.ToArray();
            return decryptedData;

        }

        #endregion

        # region compute federal id for transaction log

        public static string ComputeFederalID(string FederalID)
        {
            string strvaln = "";
            try
            {
                string strFEDERAL_ID = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(FederalID);
                if (strFEDERAL_ID != "")
                {

                    for (int len = 0; len < strFEDERAL_ID.Length - 4; len++)
                        strvaln += "x";

                    strvaln += strFEDERAL_ID.Substring(strvaln.Length, strFEDERAL_ID.Length - strvaln.Length);
                    return strvaln;
                }
                else
                    return strvaln;
            }
            catch
            {
                return strvaln;

            }
        }

        #endregion

        #region Security on Grid Buttons
        /// <summary>
        /// Security on Grid Buttons --COMMIT BUTTON WILL HAVE EXECUTE RIGHTS
        /// Check Index (Commit Button)
        /// Void Index (Commit Button)
        /// </summary>
        /// <param name="strSecurity"></param>
        /// <returns></returns>
        public static bool CheckSecurity(string strSecurity)
        {
            string strWrite = "";
            string strExecute = "";

            if (strSecurity != null && strSecurity != "")
            {
                strWrite = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("Write", strSecurity);
                strExecute = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("Execute", strSecurity);
            }
            if (strExecute.ToUpper() == "Y")
            {
                return true;
            }
            else
                return false;

        }
        #endregion

        #region DATE CALCULATION
        /// <summary>
        /// Calculate Age (With Leap year)
        /// </summary>
        /// <param name="inDate1"></param>
        /// <param name="inDate2"></param>
        /// <returns></returns>
        public static string DateDiffAsString(DateTime inDate1, DateTime inDate2)
        {
            int y = 0; int m = 0; int d = 0;   //make sure date1 is before (or equal to) date2.. 
            DateTime date1 = inDate1 <= inDate2 ? inDate1 : inDate2;
            DateTime date2 = inDate1 <= inDate2 ? inDate2 : inDate1;
            DateTime temp1;
            if (DateTime.IsLeapYear(date1.Year) && !DateTime.IsLeapYear(date2.Year) && date1.Month == 2 && date1.Day == 29)
            {
                temp1 = new DateTime(date2.Year, date1.Month, date1.Day - 1);
            }
            else
            { temp1 = new DateTime(date2.Year, date1.Month, date1.Day); }
            y = date2.Year - date1.Year - (temp1 > date2 ? 1 : 0);
            m = date2.Month - date1.Month + (12 * (temp1 > date2 ? 1 : 0));
            d = date2.Day - date1.Day;
            if (d < 0)
            {
                if (date2.Day == DateTime.DaysInMonth(date2.Year, date2.Month) && (date1.Day >= DateTime.DaysInMonth(date2.Year, date2.Month) || date1.Day >= DateTime.DaysInMonth(date2.Year, date1.Month)))
                {
                    d = 0;
                }
                else
                {
                    m--;
                    if (DateTime.DaysInMonth(date2.Year, date2.Month) == DateTime.DaysInMonth(date1.Year, date1.Month) && date2.Month != date1.Month)
                    {
                        int dayBase = date2.Month - 1 > 0 ? DateTime.DaysInMonth(date2.Year, date2.Month - 1) : 31;
                        d = dayBase + d;
                    }
                    else
                    {
                        // d = DateTime.DaysInMonth(date2.Year, date1.Month) + d;    
                        d = DateTime.DaysInMonth(date2.Year, date2.Month == 1 ? 12 : date2.Month - 1) + d;
                    }
                }
            }
            string ts = "";



            if (y >= 0)
                ts += y + ":";
            if (m >= 0)
                ts += m + ":";
            if (d >= 0)
                ts += d;


            return ts;
        }
        #endregion

        #region Function to check the existence of XML file for Localizing 
        public static bool IsXMLResourceExists(string Path, string FileName)
        {
           // if (File.Exists(@Root + "/support/PageXml/" + LocalisedFolder + "/"+ FileName))
            string lstrUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName");
            string lstrPassword = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd");
            string lstrDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain");

            ClsAttachment objImpersonatioon = new ClsAttachment();
            if (objImpersonatioon.ImpersonateUser(lstrUserName, lstrPassword, lstrDomain))
            {
                if (File.Exists(Path + "/" + FileName))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        #endregion

        public DataSet GetPolicyDetails(int CustomerID, int AppID, int AppVersionID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetPolicyDetails";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);

                //objDataWrapper.AddParameter("@APP_ID",AppID);
                //objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
                if (AppID != 0 && AppVersionID != 0)
                {
                    objDataWrapper.AddParameter("@APP_ID", AppID);
                    objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID);
                }
                if (PolicyID != 0 && PolicyVersionID != 0)
                {
                    objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                }
                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }

        public static bool CheckReadSecurity(string strSecurity)
        {
            string strWrite = "";
            string strExecute = "";

            if (strSecurity != null && strSecurity != "")
            {
                strWrite = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("Write", strSecurity);
                strExecute = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("Execute", strSecurity);
            }
            if (strWrite.ToUpper() == "Y")
            {
                return true;
            }
            else
                return false;

        }

        #region For Auto Endorsement
        //Added By Lalit For Auto Endorsment
        /// <summary>
        /// Get Policy Customer id,Policy Id, policy Version id
        /// </summary>
        /// <param name="policyNumber">String</param>
        /// <returns>Dataset</returns>        
        public DataSet GetPolicyDetails(string policyNumber, string calledFrom)
        {
            Cms.DataLayer.DataWrapper objWrapper = null;
            try
            {
                objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
                objWrapper.AddParameter("@POLICY_NUM", policyNumber);
                objWrapper.AddParameter("@CALLED_FROM", calledFrom);
                DataSet dsTemp = new DataSet();
                dsTemp = objWrapper.ExecuteDataSet("proc_getpolicy_details");
                return dsTemp;

            }
            catch (Exception objExp)
            {
                throw (new Exception("Error occured in GetPolicyDetails\n " + objExp.Message));
            }
            finally
            {
                objWrapper.Dispose();
            }


        }

        public DataSet GetPolicy_ProcessDetails(int iCustomerId, int iPolicyId, int iPolicyVersionId)
        {
            Cms.DataLayer.DataWrapper objWrapper = null;
            try
            {
                objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
                objWrapper.AddParameter("@CUSTOMER_ID", iCustomerId);
                objWrapper.AddParameter("@POLICY_ID", iPolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", iPolicyVersionId);
                DataSet dsTemp = new DataSet();
                dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyProcessStatus");
                return dsTemp;

            }
            catch (Exception objExp)
            {
                throw (new Exception("Error occured in GetPolicyProcessDetails\n " + objExp.Message));
            }
            finally
            {
                objWrapper.Dispose();
            }


        }

        #endregion

        #region CONVERT NUMBER TO PORTUGUESE WORDS
        //Added by Pradeep Kushwaha on 09-Dec-2010
        public String changeNumericToWords(double numb)
        {
            String num = numb.ToString();
            return changeToWords(num, false);
        }
        private String changeToWords(String numb, bool isCurrency)
        {
            
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = (isCurrency) ? ("apenas") : ("");
            try
            {
                numb = numb.Replace(",", ".");
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    points = points.ToString().Length == 1 ? points + "0" : points;
                    if (points.ToString().Length > 0)
                    {
                        isCurrency = true;
                        andStr = (isCurrency) ? (" e ") : (" ponto ");// just to separate whole numbers from points/cents
                        endStr = (isCurrency) ? (" centavos " + endStr) : ("");//endStr = (isCurrency) ? ("Cents " + endStr) : ("");
                        pointStr = translateCentsExceed(points);//pointStr = translateCents(points);
                        
                    }
                }
                val = String.Format("{0} {1}{2} {3} {4}", translateWholeNumber(wholeNo).Trim(), translateWholeNumber(wholeNo).Trim().ToString() != "" ? numb == "1" ? "Real " : "Reais " : "", andStr, pointStr, endStr);
                
            }
            catch { ;}
            return val;
        }
        private String translateCentsExceed(String number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;
                bool isDone = false;
                double dblAmt = (Convert.ToDouble(number));
                
                if (dblAmt > 0)
                {
                    beginsZero = number.StartsWith("0");
                    int numDigits = Convert.ToInt64(number).ToString().Length;
                    int pos = 0;//store digit grouping
                    String place = "";//digit grouping name:hundres,thousand,etc...
                    switch (numDigits)
                    {
                        case 1://ones' range
                            word = ones(number);
                            isDone = true;
                            break;
                        case 2://tens' range
                            word = tens(number);
                            isDone = true;
                            break;
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)
                        word = translateCentsExceed(number.Substring(0, pos)) + place + translateCentsExceed(number.Substring(pos));
                        //check for trailing zeros
                        if (beginsZero) word = " e " + word.Trim();
                    }
                    //ignore digit grouping names
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { ;}
            return word.Trim();
        }
        private String translateWholeNumber(String number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX
                bool isDone = false;//test if already translated
                double dblAmt = (Convert.ToDouble(number));
                //if ((dblAmt > 0) && number.StartsWith("0"))
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric
                    beginsZero = number.StartsWith("0");
                    int numDigits = Convert.ToInt64(number).ToString().Length;
                    int pos = 0;//store digit grouping
                    String place = "";//digit grouping name:hundres,thousand,etc...
                    switch (numDigits)
                    {
                        case 1://ones' range
                            word = ones(number);
                            isDone = true;
                            break;
                        case 2://tens' range
                            word = tens(number);
                            isDone = true;
                            break;
                        case 3://hundreds' range
                            word = ThreeDigit(number);
                            isDone = true;
                            break;
                        case 4://thousands' range
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Mil ";
                            break;
                        case 7://millions' range
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            if (pos >= 3)
                            {
                                if (int.Parse(number.Substring(0, pos)) % 100 == 0)
                                    place = " Milhão ";
                                else
                                    place = " Milhões ";
                            }
                            else
                                place = " Milhão ";
                            break;
                        case 10://Billions's range
                            pos = (numDigits % 10) + 1;
                            place = " Bilhão ";
                            break;
                        //add extra case options for anything above Billion...
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)
                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                        //check for trailing zeros
                        if (beginsZero) word = " e " + word.Trim();
                    }
                    //ignore digit grouping names
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { ;}
            return word.Trim();
        }
        private String ThreeDigit(String digit)
        {
            int num = Convert.ToInt32(digit);
            int digt = num / 100;
            int rem = num % 100;
            String name = null;
            if (num >= 100 && num < 101)
            {
                name = " Cem ";
            }
            else
            {
                switch (digt)
                {
                    case 1:
                        name = " Cento ";
                        break;
                    case 2:
                        name = " Duzentos ";
                        break;
                    case 3:
                        name = " Trezentos ";
                        break;
                    case 4:
                        name = " Quatrocentos ";
                        break;
                    case 5:
                        name = " Quinhentos ";
                        break;
                    case 6:
                        name = " Seiscentos ";
                        break;
                    case 7:
                        name = " Setecentos ";
                        break;
                    case 8:
                        name = " Oitocentos ";
                        break;
                    case 9:
                        name = " Novecentos ";
                        break;
                    default:
                        name = "";
                        break;
                }
            }
            if (rem != 0)
            {
                if (rem < 10)
                {
                    if (rem == 1)
                    {
                        name = name + " e " + "um";
                    }
                    else
                    {
                        name = name + " e " + ones(rem.ToString());
                    }
                }
                else
                    name = name + " e " + tens(rem.ToString());
            }
            return name;
        }
        private String tens(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = null;
            switch (digt)
            {
                case 10:
                    name = " Dez ";
                    break;
                case 11:
                    name = " Onze ";
                    break;
                case 12:
                    name = " Doze ";
                    break;
                case 13:
                    name = " Treze ";
                    break;
                case 14:
                    name = " Quartoze ";
                    break;
                case 15:
                    name = " Quinze ";
                    break;
                case 16:
                    name = " Dezesseis ";
                    break;
                case 17:
                    name = " Dezessete ";
                    break;
                case 18:
                    name = " Dezoito ";
                    break;
                case 19:
                    name = " Dezenove ";
                    break;
                case 20:
                    name = " Vinte ";
                    break;
                case 30:
                    name = " Trinta ";
                    break;
                case 40:
                    name = " Quarenta ";
                    break;
                case 50:
                    name = " Cinquenta ";
                    break;
                case 60:
                    name = " Sessenta ";
                    break;
                case 70:
                    name = " Setenta ";
                    break;
                case 80:
                    name = " Oitenta ";
                    break;
                case 90:
                    name = " Noventa ";
                    break;
                default:
                    if (digt > 0)
                    {
                        //name = tens(digit.Substring(0, 1) + "0") + " e " + ones(digit.Substring(1));
                        name = tens(digit.Substring(0, 1) + "0") + " e " + (int.Parse(digit) % 10 == 1 ? "um" : ones(digit.Substring(1)));
                    }
                    break;
            }
            return name;
        }
        Boolean flage = false;
        private String ones(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = "";
            switch (digt)
            {
                case 1:
                    if (flage == false)
                        name = "Hum";
                    else
                        name = "um";
                    flage = true;
                    break;
                case 2:
                    name = "Dois";
                    break;
                case 3:
                    name = "Três";
                    break;
                case 4:
                    name = "Quatro";
                    break;
                case 5:
                    name = "Cinco";
                    break;
                case 6:
                    name = "Seis";
                    break;
                case 7:
                    name = "Sete";
                    break;
                case 8:
                    name = "Oito";
                    break;
                case 9:
                    name = "Nove";
                    break;
            }
            return name;
        }
        private String translateCents(String cents)
        {
            String cts = "", digit = "", engOne = "";
            for (int i = 0; i < cents.Length; i++)
            {
                digit = cents[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cts += " " + engOne;
            }
            return cts;
        }
        //Added Till here 
        #endregion

        //Added By lalit to get html from datatable for bind dropdown,
        public static string ConvertDataTableToDDLString(DataTable dt)
        {
            StringBuilder strbldr = new StringBuilder();
            //string defaultval = "";
            // strbldr.Append(@"<OPTION value=" + defaultval + "></OPTION>");

            //AGENCY_ID
            foreach (DataRow dr in dt.Rows)
            {
                strbldr.Append(@"<OPTION value=" + dr["AGENCY_ID"] + ">" + dr["AGENCY_NAME_ACTIVE_STATUS"] + "</OPTION>");
            }


            //<OPTION value=""></OPTION> <OPTION selected value="43">Commission</OPTION> <OPTION value=44>Enrollment fee</OPTION> <OPTION value=45>Pro-Labore</OPTION>
            return strbldr.ToString();

        }

        public static DataSet GetPolicyDisplayVersion(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, string CALLED_FROM)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                DataSet DsPolicy = null;
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@CALLED_FROM", CALLED_FROM);
                DsPolicy = objDataWrapper.ExecuteDataSet("Proc_GetPolicyDisplayVersion");
                objDataWrapper.ClearParameteres();
                return DsPolicy;
            }
            catch (Exception ex) { throw ex; }
        }
    }


    #region "Cached Components"
    /// <summary>
    /// All cached controls implement this interface.
    /// </summary>
    public interface ICachedControl
    {
        void setCachedDataSource(string key, object tDataSource);
        bool setDataSourceFromCache(string key);
    }
    #region "Cached Dropdownlist"
    [DefaultProperty("DataSource"),
    ToolboxData("<{0}:CachedDropDownList runat=server></{0}:CachedDropDownList>")]
    public class CachedDropDownList : System.Web.UI.WebControls.DropDownList, ICachedControl
    {

        public bool setDataSourceFromCache(string key)
        {
            if (this.Page.Cache[key] == null)
                return false;
            else
            {
                base.DataSource = this.Page.Cache.Get(key);
                return true;
            }
        }
        /// <summary> 
        /// This method sets the dataSource
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        public void setCachedDataSource(string key, object tDataSource)
        {
            if (this.Page.Cache[key] != null)
                this.Page.Cache.Remove(key);
            this.Page.Cache.Insert(key, tDataSource);
            base.DataSource = tDataSource;
        }


    }
    #endregion
    #endregion

    #region "Transaction Information Structure"
    public struct stuTransactionInfo
    {
        public int loggedInUserId;
        public string transactionDescription;
        public string loggedInUserName;
        public int clientId;
    }

    #endregion

    # region "Default"
    public class Default
    {

        public static int GetIntFromString(string strInt)
        {
            if (strInt.Trim() == "")
            {
                return 0;
            }

            return int.Parse(strInt);
        }

        public static Decimal GetDecimalFromString(string strDecimal)
        {
            if (strDecimal.Trim() == "")
            {
                return 0;
            }

            return Decimal.Parse(strDecimal);
        }

        public static Double GetDoubleFromString(string strDouble)
        {
            if (strDouble.Trim() == "")
            {
                return 0;
            }

            return Double.Parse(strDouble);
        }

        public static DateTime GetDateFromString(string strDate)
        {
            if (strDate.Trim() == "")
            {
                return DateTime.MinValue;
            }

            return DateTime.Parse(strDate);

        }

        public static object GetDateNull(DateTime dt)
        {
            if (dt == DateTime.MinValue)
            {
                return System.DBNull.Value;
            }
            else
            {
                return dt;
            }

        }

        public static int GetInt(object o)
        {
            return o == DBNull.Value ? 0 : Convert.ToInt32(o);
        }

        public static string GetString(object o)
        {
            return o == DBNull.Value ? "" : Convert.ToString(o);
        }

        public static DateTime GetDateTime(object o)
        {
            return o == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(o);
        }
        /// <summary>
        /// Checks for the validaty of a string to be a proper date. Returns true when the string contains the date, 
        /// else it returns false. The method can be used when using string variables to store/ pass date values. 
        /// It can be used to check that the string variable contains the date in proper format.
        /// </summary>
        /// <param name="sDate">A string variable containing date</param>
        /// <returns>returns true when the string contains a proper date</returns>
        /// <returns>returns false when the string does not contain a proper date</returns>
        public bool IsDate(string sdate)
        {
            DateTime dt;
            bool isDate = true;
            try
            {
                if (sdate == "") isDate = false;
                dt = DateTime.Parse(sdate);
            }
            catch
            {
                isDate = false;
            }
            return isDate;
        }

        public static object GetDoubleNull(double doubleValue)
        {
            if (doubleValue == 0 || doubleValue == -1)
            {
                return System.DBNull.Value;
            }
            else
            {
                return doubleValue;
            }

        }

        public static object GetIntNull(int intValue)
        {
            if (intValue == 0 || intValue == -1)
            {
                return System.DBNull.Value;
            }
            else
            {
                return intValue;
            }

        }



        public static object GetDoubleNullFromNegative(double doubleValue)
        {
            if (doubleValue < 0)
            {
                return System.DBNull.Value;
            }
            else
            {
                return doubleValue;
            }

        }

        public static object GetIntNullFromNegative(int intValue)
        {
            if (intValue < 0)
            {
                return System.DBNull.Value;
            }
            else
            {
                return intValue;
            }

        }

        public static object GetStringNull(string strValue)
        {
            if (strValue == "")
            {
                return System.DBNull.Value;
            }
            else
            {
                return strValue;
            }

        }

    }
    # endregion

}
